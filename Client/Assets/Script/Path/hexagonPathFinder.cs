using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Hexagon
{
    public class SearchNode
    {
        public SearchNode parent=null;
        public Coord v;
        public float walk_distance =   0.0f;	//< 当前节点到起始节点的累计权重aa
        public float distance = 0.0f;			//< 当前节点到目标节点的直线距离aa
        public Vector3 p;
//		protected float mStraightWeight = 0.0f;	//< 当前节点到上一节点的弯曲权重(用于保持路线尽可能直)aa
		public void SetStepWeight(float fStep)
        {
            if(parent!=null)
            {
                walk_distance   =   parent.walk_distance+fStep;

//				if (parent.parent != null)
//				{
//					Vector3 v1 = (parent.p - parent.parent.p).normalized;
//					Vector3 v2 = (p - parent.p).normalized;
//					float dot = Vector3.Dot(v1, v2);
//					if (dot < 0.95f)
//						mStraightWeight = Manager.GetSingleton().GetHexagonCost() * PathFinder.StraightWeightScale;
//				}
            }
        }

        public float weight()
        {
			return walk_distance + distance * PathFinder.HeuristicWeightScale/* + mStraightWeight*/;
        }
    }

	/// <summary>
	/// 寻路计算器aa
	/// </summary>
    public class PathFinder
    {
		public static float StaticWeightScale = 10.0f;		//< 网格静态权重系数aa
		public static float DynamicWeightScale = 10.0f;		//< 角色动态权重系数aa
		public static float StraightWeightScale = 0.5f;		//< 尽量搜索直线路线的权重系数aa
		public static float HeuristicWeightScale = 1.8f;	//< 启发值系数aa

        public static bool  MiddlePointWeight = true;
        Manager mgr = null;
        List<SearchNode> lstSearch = new List<SearchNode>(1000);
        public PathFinder()
        {
            mgr = Manager.GetSingleton();
        }
        SearchNode NewSearchNode(Coord v, Vector3 end)
        {

            SearchNode n = new SearchNode();
            n.p = mgr.Coord_Position(v, mgr.GetHeight(v));
            n.v = v;
            n.walk_distance = 0.0f;

            Vector3 vOffset = (n.p - end);
            /*
            vOffset = new Vector3(Mathf.Abs(vOffset.x),Mathf.Abs(vOffset.x),Mathf.Abs(vOffset.x));
            if(vOffset.x >= vOffset.z)
            {
                n.distance = vOffset.z + vOffset.x*0.2f + vOffset.y;
            }
            else
            {
                float val = vOffset.z/Mathf.Sqrt(3.0f);
                n.distance = vOffset.x + val + vOffset.y;
            }
            */
            n.distance = vOffset.magnitude;

            return n;
        }

		// 结算当前Hexagon到父Hexagon的权重aa
		//	1.Hexagon之间的距离aa
		//	2.Hexagon之间的高度差aa
		//	3.Hexagon上的边缘权重aa
		//	4.Hexagon上的动态权重aa
        float CalcWeight(SearchNode node,Vector3 end,ushort parent,ushort child)
        {
            int parentHeight = parent & Tile.HeightMask;
            int childHeight = child & Tile.HeightMask;
            float heightoffset = 0.0f;// (childHeight - parentHeight) * mgr.GetLayerHeight();
            //if (heightoffset < 0.0f)
           // {
           //     heightoffset = -heightoffset;
           // }

			int iStaticWeight = (child & Tile.EdgeWeightMask) >> Tile.EdgeWeightShift;
			float fStaticWeight = iStaticWeight * StaticWeightScale;

			int iDynamicWeight = (child & Tile.ActorWeightMask) >> Tile.ActorWeightShift;
			float fDynamicWeight = iDynamicWeight * DynamicWeightScale;

            float fDistance = 0.0f;
            if (MiddlePointWeight)
            {
                if (node.parent != null)
                {
                    if (node.parent.parent == null)
                    {
                        fDistance = mgr.GetHexagonCost() * 0.5f;
                    }
                    else
                    {
                        Vector3 v = (node.p - node.parent.parent.p) * 0.5f;
                        fDistance = v.magnitude;
                    }
                    Vector3 vLength = (node.p + node.parent.p) * 0.5f - end;
                    node.distance = vLength.magnitude;
                }

            }
            else
            {
                fDistance = mgr.GetHexagonCost();
            }

          
			return fDistance + heightoffset + fStaticWeight + fDynamicWeight;
        }

		// 使用A*算法计算最短路径aa
        public bool AStar(Vector3 begin, Vector3 end, out List<Vector3> path, bool OptimizePath)
        {
            path = null;
            if (mgr == null)
            {
                return false;
            }

            Coord vbegin = new Coord();
            ushort beginheight = 0;
            if (!mgr.Position_Coord_Radius(begin, ref vbegin, ref beginheight)) 
            {
                return false;
            }

			Coord vend = new Coord();
			ushort endheight = 0;
            if (!mgr.Position_Coord_Radius(end, ref vend, ref endheight))
            {
                return false;
            }

            //List<SearchNode> lstUnSearch = new List<SearchNode>();
            BT.BinaryTree lstUnSearch = new BT.BinaryTree();
            SearchNode newNode = NewSearchNode(vbegin, end);
            lstUnSearch.Push(newNode.weight(), newNode);
            mgr.AddSearchMark(newNode.v);
            if (!WalkHoneycomb(vend, end, lstSearch, lstUnSearch))
            {
                ClearSearchMask(lstSearch, lstUnSearch);
                return false;
            }
            SearchNode node = lstSearch[lstSearch.Count - 1];
            

            path = new List<Vector3>();
            path.Insert(0, node.p);
            while (true)
            {
                if (node.parent == null)
                {
                    break;
                }
                else
                {
                    if (OptimizePath)
                    {
                        if (node.parent != null)
                        {
                            path.Insert(0, (node.p + node.parent.p) * 0.5f);
                        }
                        else
                        {
                            //path.Insert(0, node.p);
                        }
                    }
                    else
                    {
                        path.Insert(0, node.p);
                    }
                    node = node.parent;
                }
            }
            ClearSearchMask(lstSearch, lstUnSearch);
            return true;
        }

        bool WalkHoneycomb(Coord vend, Vector3 endPos, List<SearchNode> search, BT.BinaryTree unsearch)
        {
            int count = 0;
            while (true)
            {
                SearchNode node = (SearchNode)unsearch.Pop();
                if (node == null)
                {
                    return false;
                }
                search.Add(node);

                if (node.v.Equal(vend))
                {
                    return true;
                }
                ushort currentData = mgr.GetHeight(node.v);
                int current = currentData & Tile.HeightMask;

                Coord[] varray = node.v.Neighbour();
                int newchild = 0;
                
                    for (int i = 0; i < varray.Length; i++)
                    {
                        Coord c = varray[i];
                        if (!mgr.IsCoordValid(c))
                        {
                            continue;
                        }
                        int layerCount = mgr.GetTileLayerCount(c);
                        for (int layer = 0; layer < layerCount; layer++)
                        {
                            Coord newC = new Coord();
                            newC.x = c.x;
                            newC.y = layer;
                            newC.z = c.z;
                            //c.y = layer;
                            

                            ushort by = mgr.GetHeight(newC);

                            if ((by & Tile.Walkable) == 0)
                            {
                                continue;
                            }
                            if ((by & Tile.HasSearch) != 0)
                            {
                                continue;
                            }
                            //if (!newC.Equal(vend))
                            //{
                            //    if ((by & Tile.HasPlayer) != 0)
                            //    {
                            //        continue;
                            //    }
                            //}

                            int height = by & Tile.HeightMask;
                            float fHeightOffset = (height - current) * mgr.GetLayerHeight();
                            if (fHeightOffset > 0.5f || fHeightOffset < -0.5f)
                            {
                                continue;
                            }

                            SearchNode n = NewSearchNode(newC, endPos);
                            n.parent = node;
                            n.SetStepWeight(CalcWeight(n, endPos, currentData, by));
                            count++;

                            newchild++;
                            unsearch.Push(n.weight(), n);
                            mgr.AddSearchMark(n.v);
                        }
                    }
                
            }
            return false;
        }
        public void ClearSearchMask(List<SearchNode> search, BT.BinaryTree unsearch)
        {
            foreach (SearchNode n in search)
            {
                mgr.RemoveSearchMark(n.v);

            }
            search.Clear();
            SearchNode no = (SearchNode)unsearch.Pop();
            while (true)
            {
                if (no == null)
                {
                    break;
                }
                mgr.RemoveSearchMark(no.v);
                no = (SearchNode)unsearch.Pop();
            }
            unsearch.Clear();
        }
        void ClearSearchMaskOnly(List<SearchNode> search, BT.BinaryTree unsearch)
        {
            foreach (SearchNode n in search)
            {
                mgr.RemoveSearchMark(n.v);

            }
            BT.Node no = unsearch.HeadNode();
            while (true)
            {
                if (no == null)
                {
                    break;
                }
                SearchNode sn = (SearchNode)no.obj;
                mgr.RemoveSearchMark(sn.v);
                no = no.next;
            }
        }
        public bool DebugAStar(Vector3 begin, Vector3 end, ref List<SearchNode> lstTempSearch, ref BT.BinaryTree lstUnSearch)
        {
            if (mgr == null)
            {
                return false;
            }

            Coord vbegin = new Coord();
            ushort beginheight = 0;
            Coord vend = new Coord();
            ushort endheight = 0;
            if (!mgr.Position_Coord(begin, ref vbegin, ref beginheight) || !mgr.Position_Coord(end, ref vend, ref endheight))
            {
                return false;
            }
            
            SearchNode newNode = NewSearchNode(vbegin, end);
            lstUnSearch.Push(newNode.weight(), newNode);
            mgr.AddSearchMark(newNode.v);
            if (!WalkHoneycomb(vend, end, lstTempSearch, lstUnSearch))
            {
                ClearSearchMaskOnly(lstTempSearch, lstUnSearch);
                return false;
            }

            ClearSearchMaskOnly(lstTempSearch, lstUnSearch);
            return true;
        }
        
    }
}
