using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Hexagon
{
    public class Common
    {
        public static void CalcBound(Vector3[] vArray, ref Vector3 bound_min, ref Vector3 bound_max)
        {
            Vector3 vmin = vArray[0];
            Vector3 vmax = vArray[0];
            foreach (Vector3 v in vArray)
            {
                if (vmin.x > v.x) vmin.x = v.x;
                if (vmin.y > v.y) vmin.y = v.y;
                if (vmin.z > v.z) vmin.z = v.z;

                if (vmax.x < v.x) vmax.x = v.x;
                if (vmax.y < v.y) vmax.y = v.y;
                if (vmax.z < v.z) vmax.z = v.z;
            }
            bound_min = vmin;
            bound_max = vmax;
        }
    }

	/// <summary>
	/// Hexagon坐标aa
	/// </summary>
    public struct Coord
    {
        public int x;
        public int z;
        public int y;

		public Coord(int iX, int iY, int iZ)
		{
			x = iX;
			y = iY;
			z = iZ;
		}

		public Coord(Coord kCoord)
		{
			x = kCoord.x;
			y = kCoord.y;
			z = kCoord.z;
		}

        public Coord[] Neighbour()
        {
            Coord[] v = new Coord[6];// { NE, NW, W, SW, SE, E };

            v[2].x = x - 1;
            v[2].z = z;

            v[5].x = x + 1;
            v[5].z = z;

            if ((z & 1) == 1)
            {
                v[0].x = x + 1;
                v[0].z = z + 1;

                v[1].x = x;
                v[1].z = z + 1;

                v[3].x = x;
                v[3].z = z - 1;

                v[4].x = x + 1;
                v[4].z = z - 1;

            }
            else
            {
                v[0].x = x;
                v[0].z = z + 1;

                v[1].x = x - 1;
                v[1].z = z + 1;

                v[3].x = x - 1;
                v[3].z = z - 1;

                v[4].x = x;
                v[4].z = z - 1;

            }
            return v;
        }

        public bool Equal(Coord v)
        {
            return x == v.x && z == v.z && y == v.y;
        }
    }

	/// <summary>
	/// Hexagon管理器aa
	/// </summary>
    public class Manager : TSingleton<Manager>
    {
        public static int TileSize		= 0x10;	//< 0b 0001 0000
		public static int TileSizeShift = 0x4;
		public static int TileSizeMask	= 0xf;	//< 0b 0000 1111
        
        int xCount = 256;
        int zCount = 256;
        int yCount = 1;
        int TotalCount = 0;
        int xTileCount = 0;
        int zTileCount = 0;

        int iLayer = Tile.HeightMask;
        float   fYLayerHeight = 0.1f;
        float   fXStep = 0.8f;
        float   fZStep = 1.0f;

        float fHeightFix = 1.0f;
        
		// 整个Hexagon空间的边界aa
        Vector3 bound_min;
        Vector3 bound_max;

		// 整个Hexagon空间的Tile数据aa
        Tile[] tileData = null;

		// 寻路计算器aa
        PathFinder finder = null;

		// 初始化aa
        public bool Init()
        {
            Release();

            fZStep = 0.5f * fXStep * Mathf.Sqrt(3.0f);

			// 获取导航面片三角形aa
            Vector3[] vertex = null;
            int[] face = null;
            NavMesh.Triangulate(out vertex, out face);
            if (face == null)
            {
                return false;
            }

            Common.CalcBound(vertex,ref bound_min,ref bound_max);

            fYLayerHeight = (bound_max.y - bound_min.y) / Tile.HeightMask;

            xCount = (int)((bound_max.x - bound_min.x) / fXStep) + 1;
            zCount = (int)((bound_max.z - bound_min.z) / fZStep) + 1;
            TotalCount = xCount * zCount;
            xTileCount = (xCount + TileSize - 1) / TileSize;
            zTileCount = (zCount + TileSize - 1) / TileSize;

            tileData = new Tile[xTileCount * zTileCount];

            // 建立六边形数据aa
            BuildHexagonData(vertex, face);

            // 对比并交换值,保证low都是较小值 high都是较大值aa
            //SortbyHeight();

            // 调整边缘和中心权重值aa
            BuildHexagonWeight();

			// 寻路计算器aa
            finder = new PathFinder();

            return true;
        }

		// 销毁aa
		public void Release()
		{
			tileData = null;
		}

		// 指定Tile的层数aa
        public int GetTileLayerCount(int TileX,int TileZ)
        {
            int idx = TileZ * xTileCount + TileX;
            if (tileData[idx] == null)
            {
                return 0;
            }
            return tileData[idx].GetLayerCount();
        }

		// 指定Hexagon坐标的Tile的层数aa
        public int GetTileLayerCount(Coord v)
        {
			int TileX = v.x >> TileSizeShift;
			int TileZ = v.z >> TileSizeShift;
            return GetTileLayerCount(TileX, TileZ);
        }

		// 从已有的行走面片构建Hexagon数据aa
        void BuildHexagonData(Vector3[] vertex, int[] face)
        {
            int iFaceCount = face.Length / 3;
            //遍历每个三角形 逐个注入到双层的2维纹理中..
            for (int i = 0; i < iFaceCount; i++)
            {
                Vector3[] v = new Vector3[3];
                v[0] = vertex[face[i * 3]];

                //计算每个三角形的 包围盒 只计算XZ..
                Vector2 vmin = new Vector2(v[0].x, v[0].z);
                Vector2 vmax = vmin;
                for (int j = 1; j < 3; j++)
                {
                    v[j] = vertex[face[i * 3 + j]];
                    Vector2 vTemp = new Vector2(v[j].x, v[j].z);
                    if (vmin.x > vTemp.x) vmin.x = vTemp.x;
                    if (vmin.y > vTemp.y) vmin.y = vTemp.y;

                    if (vmax.x < vTemp.x) vmax.x = vTemp.x;
                    if (vmax.y < vTemp.y) vmax.y = vTemp.y;
                }


                //将包围盒转化为 索引值..
                int minx = (int)((vmin.x - bound_min.x) / fXStep);
                int miny = (int)((vmin.y - bound_min.z) / fZStep);

                int maxx = (int)((vmax.x - bound_min.x) / fXStep);
                int maxy = (int)((vmax.y - bound_min.z) / fZStep);

                //最最低索引开始 发射2维射线与三角形求交  并得到 相交部分的高度值..
                for (int x = minx; x <= maxx + 1; x++)
                {
                    if (x >= xCount)
                    {
                        continue;
                    }

                    for (int z = miny; z <= maxy + 1; z++)
                    {
                        if (z >= zCount)
                        {
                            continue;
                        }
                        float xoffset = 0.0f;
                        if ((z & 1) == 1)
                        {
                            xoffset = fXStep * 0.5f;
                        }

                        float fx = x * fXStep + bound_min.x + xoffset;
                        float fz = z * fZStep + bound_min.z;
                        Ray r = new Ray(new Vector3(fx, bound_max.y + 1.0f, fz), new Vector3(0, -1, 0));
                        float dis = 100000.0f;
                        if (FingerGesturesInitializer.Ray_Triangle(r, v[0], v[1], v[2], ref dis))
                        {
                            float height = bound_max.y + 1.0f - dis;
                            uint h = (uint)((height - bound_min.y) / fYLayerHeight);

                            if (h >= Tile.HeightMask)
                            {
                                h = Tile.HeightMask;
                            }
                            SavePoint(x, z, h);

                        }

                    }
                }

            }
        }
        void SavePoint(int x, int z, uint height)
        {
			int tileX = x >> TileSizeShift;
			int tileZ = z >> TileSizeShift;
            int idx = tileZ * xTileCount + tileX;
            if (tileData[idx] == null)
            {
                tileData[idx] = new Tile();
            }

			int x_inTile = x & TileSizeMask;
			int z_inTile = z & TileSizeMask;
            if (!tileData[idx].Insert(x_inTile, z_inTile, height))
            {
                 Debug.Log("insert point failed!");
            }
        }

// 		void SortbyHeight()
// 		{
// 			int OneLayerTileCount = xTileCount * zTileCount;
// 			for (int z = 0; z < zTileCount; z++)
// 			{
// 				for (int x = 0; x < xTileCount; x++)
// 				{
// 					int idxLow = z * xTileCount + x;
// 					int idxHigh = OneLayerTileCount + idxLow;
// 
// 					if (tileData[idxHigh] != null)
// 					{
// 						tileData[idxLow].CompareSwap(tileData[idxHigh]);
// 					}
// 				}
// 			}
// 		}

		// 构建边缘权重aa
		public void BuildHexagonWeight()
        {
            for (int z = 0; z < zCount; z++)
            {
                for (int x = 0; x < xCount; x++)
                {
                    Coord v = new Coord();
                    v.x = x; v.z = z; v.y = 0;

                    int layerCount = GetTileLayerCount(v);
                    for (int layer = 0; layer < layerCount; layer++)
                    {
                        v.y = layer;

                        ushort current = GetHeight(v);
                        if (current == 0)
                        {
                            continue;
                        }

                        int currentHeight = current & Tile.HeightMask;
                        int neighbourCount = 0;

                        Coord[] NEIGHBOUR = v.Neighbour();

                        for (int i = 0; i < NEIGHBOUR.Length; i++)
                        {
                            if (!IsCoordValid(NEIGHBOUR[i]))
                            {
                                continue;
                            }
                            int neighbourLayerCount = GetTileLayerCount(NEIGHBOUR[i]);
                            for (int nlayer = 0; nlayer < neighbourLayerCount; nlayer++)
                            {
                                NEIGHBOUR[i].y = nlayer;
                                    
                                //Debug.Log(NEIGHBOUR[i].x + "__" + NEIGHBOUR[i].y + "__" + NEIGHBOUR[i].z + "__" );
                                ushort data = GetHeight(NEIGHBOUR[i]);
                                if ((data & Tile.Walkable) != 0)
                                {

                                    int offset = currentHeight - (int)(data & Tile.HeightMask);

                                    float f = offset * fYLayerHeight;
                                    if (f < 0.5f && f > -0.5f)
                                    {
                                        neighbourCount++;
                                    }
                                }
                            }
                        }

                        if (neighbourCount > 6)
                        {
                            neighbourCount = 6;
                        }
                        neighbourCount = 6 - neighbourCount;
                        current = (ushort)(current | (neighbourCount << 8));
                        SetHeight(v, current);
                    }
                }
            }
        }
        public bool IsCoordValid(Coord v)
        {
            if (v.x >= xCount || v.z >= zCount || v.x < 0 || v.z < 0)
            {
                return false;
            }

            return true;
        }

		// 计算给定Hexagon坐标系的世界坐标aa
        public Vector3 Coord_Position(Coord v, ushort height)
        {
            float xoffset = 0.0f;
            if ((v.z & 1) == 1)
            {
                xoffset = fXStep * 0.5f;
            }
            int h = height & Tile.HeightMask;
            float x = v.x * fXStep + bound_min.x + xoffset;
            float z = v.z * fZStep + bound_min.z;
            float y = h * fYLayerHeight + bound_min.y;
            return new Vector3(x, y, z);
        }

		//
        public ushort GetHeight(Coord v)
        { 
            int tileX = v.x >>4;
            int tileZ = v.z >>4;
            int idx = tileZ * xTileCount + tileX;
            
            if (tileData[idx] == null)
            {
                return 0;
            }
            int x_inTile    =   v.x&0xf;
            int z_inTile    =   v.z&0xf;
            return tileData[idx].GetHeight(x_inTile, z_inTile, v.y);
        }

		//
        public void SetHeight(Coord v,ushort height)
        {
            int tileX = v.x >> 4;
            int tileZ = v.z >> 4;
            int idx =  tileZ * xTileCount + tileX;
            if (tileData[idx] == null)
            {
                return;
            }
            int x_inTile = v.x & 0xf;
            int z_inTile = v.z & 0xf;
            tileData[idx].SetHeight(x_inTile, z_inTile,v.y, height);
        }

		// 
        bool Coord_Height(ref Coord v, ref ushort height)
        {
            ushort h = height;

            int layerCount = GetTileLayerCount(v);
            int validCount = 0;
            ushort us = 0;
            ushort[] heightArray = new ushort[layerCount * 2];
            for (int layer = 0; layer < layerCount; layer++)
            {
                v.y = layer;
                us = (ushort)(GetHeight(v));
                if (us == 0)
                {
                    continue;
                }
                int hl = (us & 0xff);
                if (hl > h)
                {
                    continue;
                }

                heightArray[validCount * 2] = us;
                heightArray[validCount * 2 + 1] = (ushort)layer;
                validCount++;
            }

            if (validCount == 0)
            {
                v.y = 0;
                height = 0;
                return false;
            }
            else
            {
                int max = 0;
                int layer = 0;
                for (int i = 0; i < validCount; i++)
                {
                    int currentHeight = (heightArray[i * 2] & 0xff);
                    if (currentHeight > (max & 0xff))
                    {
                        max = heightArray[i * 2];
                        layer = heightArray[i * 2 + 1];
                    }

                }
                v.y = layer;
                height = (ushort)max;
                return true;
            }
        
        }

		// 计算给定世界坐标对应的Hexagon坐标aa
        public bool Position_Coord(Vector3 p, ref Coord v, ref ushort height)
        {
            if (tileData == null)
            {
                return false;
            }
            Vector3 offset = p - bound_min;

            v.z = (int)(offset.z / fZStep + 0.5f);
            if ((v.z & 1) == 1)
            {
                v.x = (int)(offset.x / fXStep);
            }
            else
            {
                v.x = (int)(offset.x / fXStep + 0.5f);
            }

            bool valid  =   IsCoordValid(v);
            if (valid)
            {
                height = (ushort)((p.y + fHeightFix - bound_min.y) / fYLayerHeight);
                return Coord_Height(ref v, ref height);
            }
            else
            {
                return false;
            }
 
            return true;
        }

		//
        public bool Position_Coord_Radius(Vector3 p, ref Coord v, ref ushort height)
        {
            if (!Position_Coord(p, ref v, ref height))
            {
                height = (ushort)((p.y + fHeightFix - bound_min.y) / fYLayerHeight);
                Coord[] neighbour = v.Neighbour();
                ushort[] harray = new ushort[6];
                int count = 0;
                for (int i = 0; i < neighbour.Length; i++)
                {
                    v = neighbour[i];
                    v.y = i;
                    if (!IsCoordValid(v))
                    {
                        continue;
                    }
                    ushort temp = 0;
                    if (Coord_Height(ref v, ref temp))
                    {
                        harray[count] = temp;
                        count++;
                    }
                }

                if (count == 0)
                {
                    return false;
                }
                else
                {
                    ushort temp = harray[0];
                    int min = Mathf.Abs(height - temp);
                    for (int i = 1; i < count; i++)
                    {
                        int val = Mathf.Abs(height - harray[i]);
                        if (val < min)
                        {
                            min = val;
                            temp = harray[i];
                        }
                    }
                    height = temp;
                    return true;
                }

                return false;
            }
            return true;
        }
        public void AddSearchMark(Coord v)
        {
			int tileX = v.x >> TileSizeShift;
			int tileZ = v.z >> TileSizeShift;
            int idx = tileZ * xTileCount + tileX;
            if (tileData[idx] == null)
            {
                return;
            }
			int x_inTile = v.x & TileSizeMask;
			int z_inTile = v.z & TileSizeMask;
            tileData[idx].AddSearchMark(x_inTile, z_inTile,v.y);
        }

        public void RemoveSearchMark(Coord v)
        {
			int tileX = v.x >> TileSizeShift;
			int tileZ = v.z >> TileSizeShift;
            int idx = tileZ * xTileCount + tileX;
            if (tileData[idx] == null)
            {
                return;
            }
			int x_inTile = v.x & TileSizeMask;
			int z_inTile = v.z & TileSizeMask;
            tileData[idx].RemoveSearchMark(x_inTile, z_inTile,v.y);
        }

		public float GetHexagonSize()
		{
			return fXStep;
		}

        public float GetLayerHeight()
        {
            return fYLayerHeight;
        }

        public float GetHexagonCost()
        {
            return fXStep;
        }

        public void DebugRender()
        {
            GameObject root = new GameObject();
            root.name = "Hexagon Debug";
            root.transform.position =   bound_min;
            root.transform.rotation =   Quaternion.identity;

                for (int z = 0; z < zTileCount; z++)
                { 
                    for(int x=0;x<xTileCount;x++)
                    {
                        
                        int idx = z * xTileCount + x;
                        if (tileData[idx] == null)
                        {
                            continue;
                        }
                        tileData[idx].DebugRender(x,z,root,fXStep,fZStep,fYLayerHeight);
                    }
                }
            
        }

		// 对指定圆形范围内的对象,注入权重aa
        public bool FindInjectCell(Vector3 kPosition, float fRadius, ref List<Coord> kDynamicHexagonWeight)
        {
            if (tileData == null)
            {
                return false;
            }

            ushort usValue = 0;
            Coord kCoord = new Coord();
            if (!Position_Coord(kPosition, ref kCoord, ref usValue))
            {
                return false;
            }
            if (IsCoordValid(kCoord))
            {
                List<Coord> kNeighbourCoord = new List<Coord>();
                for (int iDistance = 0; ; ++iDistance)
                {
					kNeighbourCoord.Clear();
                    bool bSucceed = GetNeighbourCoord(kCoord, iDistance, ref kNeighbourCoord);
                    if (!bSucceed)
                        break;

                    foreach (Coord kCoordItem in kNeighbourCoord)
                    {
                        if (!IsCoordValid(kCoordItem))
                        {
                            continue;
                        }
                        // 检测Hexagon是否在半径以内aa
                        Vector3 kPositionItem = Coord_Position(kCoordItem, 0);
                        Vector3 kDistance = kPositionItem - kPosition;
                        kDistance.y = 0.0f;
                        if (kDistance.magnitude > fRadius + fXStep * 0.5f)
                            continue;

                        // 计算Hexagon单元所属Tileaa
                        int iX = kCoordItem.x >> TileSizeShift;
                        int iZ = kCoordItem.z >> TileSizeShift;
                        int iIndex = iZ * xTileCount + iX;
                        if (tileData[iIndex] == null)
                            continue;

                        // 注入权重到Hexagon单元(WARNING:高度上可能有问题)aa
                        int iXInTile = kCoordItem.x & TileSizeMask;	//< 计算Tile内部偏移aa
                        int iZInTile = kCoordItem.z & TileSizeMask;	//< 计算Tile内部偏移aa
                        usValue = tileData[iIndex].GetHeight(iXInTile, iZInTile, kCoord.y);
                        if (usValue == 0)
                            continue;

                        int iWeight = (usValue & Tile.ActorWeightMask);
                        if (iWeight == Tile.ActorWeightMask)
                            continue;

                        Coord kSavedCoord = new Coord(kCoordItem);
                        kSavedCoord.y = kCoord.y;
                        kDynamicHexagonWeight.Add(kCoordItem);
                    }
                }
            }
            return true;
        }

		// 动态改变角色所在位置权重aa
		public void InjectActor(List<Coord> kDynamicHexagonWeight)
        {
			foreach (Coord kCoordItem in kDynamicHexagonWeight)
			{
				// 计算Hexagon单元所属Tileaa
				int iX = kCoordItem.x >> TileSizeShift;
				int iZ = kCoordItem.z >> TileSizeShift;
				int iIndex = iZ * xTileCount + iX;
				if (tileData[iIndex] == null)
					continue;

				// 注入权重到Hexagon单元(WARNING:高度上可能有问题)aa
				int iXInTile = kCoordItem.x & TileSizeMask;	//< 计算Tile内部偏移aa
				int iZInTile = kCoordItem.z & TileSizeMask;	//< 计算Tile内部偏移aa
                ushort usValue = tileData[iIndex].GetHeight(iXInTile, iZInTile, kCoordItem.y);
				if (usValue == 0)
					continue;

				int iWeight = (usValue & Tile.ActorWeightMask);
				if (iWeight == Tile.ActorWeightMask)
					continue;

				iWeight += Tile.ActorWeightStep;
				ushort usNewValue = (ushort)((usValue & (~Tile.ActorWeightMask)) | iWeight);
                tileData[iIndex].SetHeight(iXInTile, iZInTile, kCoordItem.y, usNewValue);
			}	
        }

		// 动态改变角色所在位置权重aa
		public void UninjectActor(ref List<Coord> kDynamicHexagonWeight)
        {
			foreach (Coord kCoordItem in kDynamicHexagonWeight)
			{
				// 计算Hexagon单元所属Tileaa
				int iX = kCoordItem.x >> TileSizeShift;
				int iZ = kCoordItem.z >> TileSizeShift;
				int iIndex = iZ * xTileCount + iX;
				if (tileData[iIndex] == null)
					continue;

				// 取消Hexagon单元注入的数据aa
				int iXInTile = kCoordItem.x & TileSizeMask;	//< 计算Tile内部偏移aa
				int iZInTile = kCoordItem.z & TileSizeMask;	//< 计算Tile内部偏移aa
				ushort usValue = tileData[iIndex].GetHeight(iXInTile, iZInTile, kCoordItem.y);
				if (usValue == 0)
					continue;

				int iWeight = (usValue & Tile.ActorWeightMask);
				if (iWeight == 0)
					continue;

				iWeight -= Tile.ActorWeightStep;
				ushort usNewValue = (ushort)((usValue & (~Tile.ActorWeightMask)) | iWeight);
				tileData[iIndex].SetHeight(iXInTile, iZInTile, kCoordItem.y, usNewValue);
			}
        }

		// 获取指定Hexagon指定距离邻域的Hexagon(区分中心位于偶数行还是位于奇数行)aa
		protected List<Coord> kNeighbourCoordOdd1;
		protected List<Coord> kNeighbourCoordOdd2;
		protected List<Coord> kNeighbourCoordOdd3;
		protected List<Coord> kNeighbourCoordEven1;
		protected List<Coord> kNeighbourCoordEven2;
		protected List<Coord> kNeighbourCoordEven3;
		public bool GetNeighbourCoord(Coord kCoord, int iDistance, ref List<Coord> kNeighbourCoord)
		{
			if (kNeighbourCoord == null)
				return false;

			if (iDistance == 0)
			{
				kNeighbourCoord.Add(kCoord);
				return true;
			}
			else if (iDistance == 1)
			{
				if ((kCoord.z & 1) == 1)
				{
					if (kNeighbourCoordOdd1 == null)
					{
						kNeighbourCoordOdd1 = new List<Coord>(6);
						kNeighbourCoordOdd1.Add(new Coord(-1, 0, 0));
						kNeighbourCoordOdd1.Add(new Coord(1, 0, 0));
						kNeighbourCoordOdd1.Add(new Coord(0, 0, -1));
						kNeighbourCoordOdd1.Add(new Coord(1, 0, -1));
						kNeighbourCoordOdd1.Add(new Coord(0, 0, 1));
						kNeighbourCoordOdd1.Add(new Coord(1, 0, 1));
					}

					foreach (Coord kCoordItem in kNeighbourCoordOdd1)
					{
						kNeighbourCoord.Add(new Coord(kCoord.x + kCoordItem.x, 0, kCoord.z + kCoordItem.z));
					}
				}
				else
				{
					if (kNeighbourCoordEven1 == null)
					{
						kNeighbourCoordEven1 = new List<Coord>(6);
						kNeighbourCoordEven1.Add(new Coord(-1, 0, 0));
						kNeighbourCoordEven1.Add(new Coord(1, 0, 0));
						kNeighbourCoordEven1.Add(new Coord(-1, 0, -1));
						kNeighbourCoordEven1.Add(new Coord(0, 0, -1));
						kNeighbourCoordEven1.Add(new Coord(-1, 0, 1));
						kNeighbourCoordEven1.Add(new Coord(0, 0, 1));
					}

					foreach (Coord kCoordItem in kNeighbourCoordEven1)
					{
						kNeighbourCoord.Add(new Coord(kCoord.x + kCoordItem.x, 0, kCoord.z + kCoordItem.z));
					}
				}

				return true;
			}
			else if (iDistance == 2)
			{
				if ((kCoord.z & 1) == 1)
				{
					if (kNeighbourCoordOdd2 == null)
					{
						kNeighbourCoordOdd2 = new List<Coord>(12);
						kNeighbourCoordOdd2.Add(new Coord(-2, 0, 0));
						kNeighbourCoordOdd2.Add(new Coord(2, 0, 0));

						kNeighbourCoordOdd2.Add(new Coord(-1, 0, -1));
						kNeighbourCoordOdd2.Add(new Coord(2, 0, -1));

						kNeighbourCoordOdd2.Add(new Coord(-1, 0, 1));
						kNeighbourCoordOdd2.Add(new Coord(2, 0, 1));

						kNeighbourCoordOdd2.Add(new Coord(-1, 0, -2));
						kNeighbourCoordOdd2.Add(new Coord(0, 0, -2));
						kNeighbourCoordOdd2.Add(new Coord(1, 0, -2));

						kNeighbourCoordOdd2.Add(new Coord(-1, 0, 2));
						kNeighbourCoordOdd2.Add(new Coord(0, 0, 2));
						kNeighbourCoordOdd2.Add(new Coord(1, 0, 2));
					}

                    foreach (Coord kCoordItem in kNeighbourCoordOdd2)
					{
						kNeighbourCoord.Add(new Coord(kCoord.x + kCoordItem.x, 0, kCoord.z + kCoordItem.z));
					}
				}
				else
				{
					if (kNeighbourCoordEven2 == null)
					{
						kNeighbourCoordEven2 = new List<Coord>(12);
						kNeighbourCoordEven2.Add(new Coord(-2, 0, 0));
						kNeighbourCoordEven2.Add(new Coord(2, 0, 0));

						kNeighbourCoordEven2.Add(new Coord(-2, 0, -1));
						kNeighbourCoordEven2.Add(new Coord(1, 0, -1));

						kNeighbourCoordEven2.Add(new Coord(-2, 0, 1));
						kNeighbourCoordEven2.Add(new Coord(1, 0, 1));

						kNeighbourCoordEven2.Add(new Coord(-1, 0, -2));
						kNeighbourCoordEven2.Add(new Coord(0, 0, -2));
						kNeighbourCoordEven2.Add(new Coord(1, 0, -2));

						kNeighbourCoordEven2.Add(new Coord(-1, 0, 2));
						kNeighbourCoordEven2.Add(new Coord(0, 0, 2));
						kNeighbourCoordEven2.Add(new Coord(1, 0, 2));
					}

					foreach (Coord kCoordItem in kNeighbourCoordEven2)
					{
						kNeighbourCoord.Add(new Coord(kCoord.x + kCoordItem.x, 0, kCoord.z + kCoordItem.z));
					}
				}

				return true;
			}
			else if (iDistance == 3)
			{
				if ((kCoord.z & 1) == 1)
				{
					if (kNeighbourCoordOdd3 == null)
					{
						kNeighbourCoordOdd3 = new List<Coord>(18);
						kNeighbourCoordOdd3.Add(new Coord(-3, 0, 0));
						kNeighbourCoordOdd3.Add(new Coord(3, 0, 0));

						kNeighbourCoordOdd3.Add(new Coord(-2, 0, -1));
						kNeighbourCoordOdd3.Add(new Coord(3, 0, -1));

						kNeighbourCoordOdd3.Add(new Coord(-2, 0, 1));
						kNeighbourCoordOdd3.Add(new Coord(3, 0, 1));

						kNeighbourCoordOdd3.Add(new Coord(-2, 0, -2));
						kNeighbourCoordOdd3.Add(new Coord(2, 0, -2));

						kNeighbourCoordOdd3.Add(new Coord(-2, 0, 2));
						kNeighbourCoordOdd3.Add(new Coord(2, 0, 2));

						kNeighbourCoordOdd3.Add(new Coord(-1, 0, -3));
						kNeighbourCoordOdd3.Add(new Coord(0, 0, -3));
						kNeighbourCoordOdd3.Add(new Coord(1, 0, -3));
						kNeighbourCoordOdd3.Add(new Coord(2, 0, -3));

						kNeighbourCoordOdd3.Add(new Coord(-1, 0, 3));
						kNeighbourCoordOdd3.Add(new Coord(0, 0, 3));
						kNeighbourCoordOdd3.Add(new Coord(1, 0, 3));
						kNeighbourCoordOdd3.Add(new Coord(2, 0, 3));
					}

                    foreach (Coord kCoordItem in kNeighbourCoordOdd3)
					{
						kNeighbourCoord.Add(new Coord(kCoord.x + kCoordItem.x, 0, kCoord.z + kCoordItem.z));
					}
				}
				else
				{
					if (kNeighbourCoordEven3 == null)
					{
						kNeighbourCoordEven3 = new List<Coord>(18);
						kNeighbourCoordEven3.Add(new Coord(-3, 0, 0));
						kNeighbourCoordEven3.Add(new Coord(3, 0, 0));

						kNeighbourCoordEven3.Add(new Coord(-3, 0, -1));
						kNeighbourCoordEven3.Add(new Coord(2, 0, -1));

						kNeighbourCoordEven3.Add(new Coord(-3, 0, 1));
						kNeighbourCoordEven3.Add(new Coord(2, 0, 1));

						kNeighbourCoordEven3.Add(new Coord(-2, 0, -2));
						kNeighbourCoordEven3.Add(new Coord(2, 0, -2));

						kNeighbourCoordEven3.Add(new Coord(-2, 0, 2));
						kNeighbourCoordEven3.Add(new Coord(2, 0, 2));

						kNeighbourCoordEven3.Add(new Coord(-2, 0, -3));
						kNeighbourCoordEven3.Add(new Coord(-1, 0, -3));
						kNeighbourCoordEven3.Add(new Coord(0, 0, -3));
						kNeighbourCoordEven3.Add(new Coord(1, 0, -3));

						kNeighbourCoordEven3.Add(new Coord(-2, 0, 3));
						kNeighbourCoordEven3.Add(new Coord(-1, 0, 3));
						kNeighbourCoordEven3.Add(new Coord(0, 0, 3));
						kNeighbourCoordEven3.Add(new Coord(1, 0, 3));
					}

					foreach (Coord kCoordItem in kNeighbourCoordEven3)
					{
						kNeighbourCoord.Add(new Coord(kCoord.x + kCoordItem.x, 0, kCoord.z + kCoordItem.z));
					}
				}

				return true;
			}
			else
			{
				return false;
			}
		}

        public bool FindPath(Vector3 begin, Vector3 end, out List<Vector3> path,bool OptimizePath)
        {
            if (finder != null)
            {
                if (finder.AStar(begin, end, out path,OptimizePath))
                {
                    return true;
                }
            }
            path = null;
            return false;
        }
        public bool DebugFindPath(Vector3 begin, Vector3 end, ref List<SearchNode> lstSearch,ref BT.BinaryTree lstUnSearch)
        {
            if (finder != null)
            {
                if (finder.DebugAStar(begin, end, ref lstSearch, ref lstUnSearch))
                {
                    return true;
                }
            }
           
            return false;
        }
        List<MovePointState> lstTask = new List<MovePointState>();
        public void AddTask(MovePointState task)
        {
            lstTask.Add(task);
        }
        public void Update()
        {
            if (lstTask.Count > 0)
            {
                MovePointState move = lstTask[0];
                lstTask.RemoveAt(0);
                //move.Begin
                move.FindPath();
            }
        }
    }
    
}
