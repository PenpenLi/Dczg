using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct int2
{
    public  int x;
    public  int y;
    public  int2 NE
    {
        get 
        {
            int2 v = new int2();
            if ((y & 1) == 1)
            {
                v.x = x + 1;
                v.y = y + 1;
            }
            else
            {
                v.x = x;
                v.y = y + 1;
            }
            return v;
        }
    }
    public int2 NW
    {
        get
        {
            int2 v = new int2();
            if ((y & 1) == 1)
            {
                v.x = x;
                v.y = y + 1;
            }
            else
            {
                v.x = x-1;
                v.y = y + 1;
            }
            return v;
        }
    }
    public int2 W
    {
        get
        {
            int2 v = new int2();
            v.x = x - 1;
            v.y = y ;
            return v;
        }
    }
    public int2 SW
    {
        get
        {
            int2 v = new int2();
            if ((y & 1) == 1)
            {
                v.x = x;
                v.y = y - 1;
            }
            else
            {
                v.x = x -   1;
                v.y = y - 1;
            }
            return v;
        }
    }
    public int2 SE
    {
        get
        {
            int2 v = new int2();
            if ((y & 1) == 1)
            {
                v.x = x + 1;
                v.y = y - 1;
            }
            else
            {
                v.x = x;
                v.y = y - 1;
            }
            return v;
        }
    }
    public int2 E
    {
        get
        {
            int2 v = new int2();
            v.x = x + 1;
            v.y = y;
            return v;
        }
    }

    public  int2[] Neighbour()
    {
        int2[] v  =    new int2[6];// { NE, NW, W, SW, SE, E };

        v[2].x = x - 1;
        v[2].y = y;

        v[5].x = x + 1;
        v[5].y = y;

        if ((y & 1) == 1)
        {
            v[0].x = x + 1;
            v[0].y = y + 1;

            v[1].x = x;
            v[1].y = y + 1;

            v[3].x = x;
            v[3].y = y - 1;

            v[4].x = x + 1;
            v[4].y = y - 1;
            
        }
        else
        {
            v[0].x = x ;
            v[0].y = y + 1;

            v[1].x = x  -   1;
            v[1].y = y + 1;

            v[3].x = x-1;
            v[3].y = y - 1;

            v[4].x = x;
            v[4].y = y - 1;
            
        }
        return v;
    }
    public bool Equal(int2 v)
    {
        return x == v.x && y == v.y;
    }
}

public class SearchNode
{
    public SearchNode parent=null;
    public int2 v;
    public float walk_distance =   0.0f;
    public float distance = 0.0f;
    public Vector3 p;
    public void SetParent(SearchNode n,float fStep)
    {
        parent  =   n;
        if(parent!=null)
        {
            walk_distance   =   parent.walk_distance+fStep;
        }
    }
    public float weight()
    {
        return walk_distance + distance;
    }

}

public class NavMeshVoxelGenerate : MonoBehaviour {
    int iLine = 250;
    int iRow = 250;
    int iLayer = 64;
    float fScale    =   1.0f;
    float honeycombLine = 1.0f;
    int Count = 0;
    Vector3[] RenderVertex = null;
    int[] RenderIndex = null;
    Vector3 pos = Vector3.zero;
    Vector3 bound_min;
    Vector3 bound_max;
    float LayerHeight = 0.1f;
    byte[] navData = null;

    public GameObject StartPoint = null;
    public GameObject EndPoint = null;
    public GameObject StartPointDebug = null;
    public GameObject EndPointDebug = null;
    public GameObject PathDebug = null;
    List<Vector3> vPath = null;
    float fTime = 0.0f;

    List<SearchNode> lstSearch = new List<SearchNode>();
	// Use this for initialization
	void Start () {
        //honeycombLine   =   0.5f*fScale*Mathf.Sqrt(3.0f);
        //if (BuildHoneycomb())
        //{
        //    pos = bound_min;
        //}
        //MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        //MeshRenderer r = gameObject.AddComponent<MeshRenderer>();
        //mf.mesh = new Mesh();
        //mf.mesh.vertices = RenderVertex;
        //mf.mesh.triangles = RenderIndex;

        //transform.position = pos;
        //transform.rotation = Quaternion.identity;
        //transform.localScale = Vector3.one;

        Hexagon.Manager.GetSingleton().Init();
        Hexagon.Manager.GetSingleton().DebugRender();

	}
    void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,100,100),"TestAStar"))
        {
            //GetIndex(StartPoint.transform.position);

            Hexagon.PathFinder find = new Hexagon.PathFinder();
            BT.BinaryTree.BeginWatch();
            //List<Vector3> vPath =   null;
            bool bRet   =   find.AStar(StartPoint.transform.position, EndPoint.transform.position, out vPath,true);
            
            Debug.Log(BT.BinaryTree.EndWatch());
            if (bRet)
            {
                Debug.Log("AStar OK!");

            }
            else
            {
                Debug.Log("AStar Failed!");
            }
        }
    }
	// Update is called once per frame
	void Update () {
        Hexagon.Coord v = new  Hexagon.Coord();
        ushort height = 0;
        Hexagon.Manager.GetSingleton().Position_Coord(StartPoint.transform.position,ref v,ref height);

        Vector3 vp = Hexagon.Manager.GetSingleton().Coord_Position(v, height);

        StartPointDebug.transform.position = vp;
        fTime += Time.deltaTime*5.0f;
        int index = (int)fTime;
        if (vPath != null)
        {
            if (vPath.Count > 0)
            {
                int i = index % vPath.Count;
                if (PathDebug != null)
                {
                    PathDebug.transform.position = vPath[i];
                }
            }
        }
	}
    void BuildQuadMesh()
    {
        
        RenderVertex = new Vector3[Count];
        RenderIndex = new int[Count * 6];

        for (int i = 0; i < iRow; i++)
        {
            for (int j = 0; j < iLine; j++)
            {
                RenderVertex[i * iLine + j] = new Vector3(j * fScale, -10.0f, i * fScale);//

            }
        }
        int Quad = 0;
        for (int i = 0; i < iRow - 1; i++)
        {
            for (int j = 0; j < iLine - 1; j++)
            {
                RenderIndex[Quad * 6] = (i + 1) * iLine + j;
                RenderIndex[Quad * 6 + 1] = i * iLine + j + 1;
                RenderIndex[Quad * 6 + 2] = i * iLine + j;
                RenderIndex[Quad * 6 + 3] = (i + 1) * iLine + j + 1;
                RenderIndex[Quad * 6 + 4] = i * iLine + j + 1;
                RenderIndex[Quad * 6 + 5] = (i + 1) * iLine + j;
                Quad++;
            }
        }
    }
    void SwapVertex(Vector3[] v, int i, int j,int compare)
    {
        if (v[j][compare] < v[i][compare])
        {
            Vector3 vtemp = v[i];
            v[i] = v[j];
            v[j] = vtemp;
        }
    }
    void SortVertex(Vector3[] v,int compare)
    {
        SwapVertex(v, 0, 1, compare);
        SwapVertex(v, 0, 2, compare);
        SwapVertex(v, 1, 2, compare);

    }
    bool BuildQuad()
    {
        Vector3[] vertex = null;
        int[] f = null;
        NavMesh.Triangulate(out vertex, out f);

        if (f == null)
        {
            return false;
        }

        CalcBound(vertex);

        LayerHeight = (bound_max.y - bound_min.y) / iLayer;

        iLine = (int)((bound_max.x - bound_min.x) / fScale) + 1;
        iRow = (int)((bound_max.z - bound_min.z) /fScale)   + 1;
        Count = iLine * iRow;
        BuildQuadMesh();

        //uint[] heightArray = new uint[256*256];
        int iFaceCount = f.Length / 3;
        //遍历每个三角形 逐个注入到双层的2维纹理中..
        for (int i = 0; i < iFaceCount; i++)
        {
            Vector3[] v = new Vector3[3];
            v[0]    =   vertex[f[i * 3]];
            
            //计算每个三角形的 包围盒 只计算XZ..
            Vector2 vmin = new Vector2(v[0].x, v[0].z);
            Vector2 vmax = vmin;
            for (int j = 1; j < 3; j++)
            {
                v[j] = vertex[f[i * 3+j]];
                Vector2 vTemp = new Vector2(v[j].x, v[j].z);
                if (vmin.x > vTemp.x) vmin.x = vTemp.x;
                if (vmin.y > vTemp.y) vmin.y = vTemp.y;

                if (vmax.x < vTemp.x) vmax.x = vTemp.x;
                if (vmax.y < vTemp.y) vmax.y = vTemp.y;
            }
           

            //将包围盒转化为 索引值..
            int minx = (int)((vmin.x - bound_min.x) / fScale);
            int miny = (int)((vmin.y - bound_min.z) / fScale);

            int maxx = (int)((vmax.x - bound_min.x) / fScale);
            int maxy = (int)((vmax.y - bound_min.z) / fScale);

            //最最低索引开始 发射2维射线与三角形求交  并得到 相交部分的高度值..
            for (int j = minx; j <= maxx+1; j++)
            {
                if (j >= iLine)
                {
                    continue;
                }
                for (int k = miny; k <= maxy+1; k++)
                {
                    if (k >= iRow)
                    {
                        continue;
                    }
                    float x =   j*fScale + bound_min.x ;
                    float z = k * fScale + bound_min.z;
                    Ray r = new Ray(new Vector3(x, bound_max.y + 1.0f, z), new Vector3(0, -1, 0));
                    float dis   =   100000.0f;
                    if(FingerGesturesInitializer.Ray_Triangle(r,v[0],v[1],v[2],ref dis))
                    {
                        float height    =   r.GetPoint(dis).y;
                        uint h  =   (uint)((height -    bound_min.x)/LayerHeight);
                        //SaveVoxel(heightArray, j, k, h);
                        //Debug.Log(j + " " + k);

                        Vector3 ver = RenderVertex[k * iLine + j];
                        ver.y = height;
                        RenderVertex[k * iLine + j] = ver;
                    }
                }
            }

        }

        return true;
    }

    bool BuildHoneycomb()
    {
        Vector3[] vertex = null;
        int[] f = null;
        NavMesh.Triangulate(out vertex, out f);

        if (f == null)
        {
            return false;
        }

        CalcBound(vertex);

        LayerHeight = (bound_max.y - bound_min.y) / iLayer;

        iLine = (int)((bound_max.x - bound_min.x) /fScale) + 1;
        iRow = (int)((bound_max.z - bound_min.z) /honeycombLine) + 1;
        Count = iLine * iRow;
        BuildHoneycombMesh();
        navData =   new byte[Count];

        int iFaceCount = f.Length / 3;
        //遍历每个三角形 逐个注入到双层的2维纹理中..
        for (int i = 0; i < iFaceCount; i++)
        {
            Vector3[] v = new Vector3[3];
            v[0] = vertex[f[i * 3]];

            //计算每个三角形的 包围盒 只计算XZ..
            Vector2 vmin = new Vector2(v[0].x, v[0].z);
            Vector2 vmax = vmin;
            for (int j = 1; j < 3; j++)
            {
                v[j] = vertex[f[i * 3 + j]];
                Vector2 vTemp = new Vector2(v[j].x, v[j].z);
                if (vmin.x > vTemp.x) vmin.x = vTemp.x;
                if (vmin.y > vTemp.y) vmin.y = vTemp.y;

                if (vmax.x < vTemp.x) vmax.x = vTemp.x;
                if (vmax.y < vTemp.y) vmax.y = vTemp.y;
            }


            //将包围盒转化为 索引值..
            int minx = (int)((vmin.x - bound_min.x) / fScale);
            int miny = (int)((vmin.y - bound_min.z) / honeycombLine);

            int maxx = (int)((vmax.x - bound_min.x) / fScale);
            int maxy = (int)((vmax.y - bound_min.z) / honeycombLine);

            //最最低索引开始 发射2维射线与三角形求交  并得到 相交部分的高度值..
            for (int j = minx; j <= maxx + 1; j++)
            {
                if (j >= iLine)
                {
                    continue;
                }

                for (int k = miny; k <= maxy + 1; k++)
                {
                    if (k >= iRow)
                    {
                        continue;
                    }
                    float xoffset = 0.0f;
                    if ((k & 1) == 1)
                    {
                        xoffset = fScale * 0.5f;
                    }

                    float x = j * fScale + bound_min.x + xoffset;
                    float z = k * honeycombLine + bound_min.z;
                    Ray r = new Ray(new Vector3(x, bound_max.y + 1.0f, z), new Vector3(0, -1, 0));
                    float dis = 100000.0f;
                    if (FingerGesturesInitializer.Ray_Triangle(r, v[0], v[1], v[2], ref dis))
                    {
                        float height = bound_max.y  +1.0f - dis;
                        //uint h = (uint)((height - bound_min.y) / LayerHeight);
                        //SaveVoxel(heightArray, j, k, h);
                        //Debug.Log(j + " " + k);
                        //if (h >= 63)
                        //{
                        //    h = 63;
                        //}

                        //navData[k * iLine + j] = (byte)( 0x80+h);

                        Vector3 ver = RenderVertex[k * iLine + j];
                        ver.y = height - bound_min.y;
                        RenderVertex[k * iLine + j] = ver;
                    }
                    
                }
            }

        }
        return true;
    }
    void BuildHoneycombMesh()
    {
        RenderVertex = new Vector3[Count];
        RenderIndex = new int[Count * 6];

        for (int i = 0; i < iRow; i++)
        {
            for (int j = 0; j < iLine; j++)
            {
                float xoffset = 0.0f;
                if ((i & 1)==1)
                {
                    xoffset = fScale*0.5f;
                }
                RenderVertex[i * iLine + j] = new Vector3(j * fScale + xoffset, -10.0f, i * honeycombLine);//

            }
        }
        int Quad = 0;
        for (int i = 0; i < iRow - 1; i++)
        {
            int oddnumber = i & 1;

            for (int j = 0; j < iLine - 1; j++)
            {
                if (oddnumber == 1)
                {
                    RenderIndex[Quad * 6] = (i + 1) * iLine + j;
                    RenderIndex[Quad * 6 + 1] = (i+1) * iLine + j + 1;
                    RenderIndex[Quad * 6 + 2] = i * iLine + j;
                    RenderIndex[Quad * 6 + 3] = (i + 1) * iLine + j + 1;
                    RenderIndex[Quad * 6 + 4] = i * iLine + j + 1;
                    RenderIndex[Quad * 6 + 5] = i  * iLine + j;
                }
                else
                {
                    RenderIndex[Quad * 6] = (i + 1) * iLine + j;
                    RenderIndex[Quad * 6 + 1] = i * iLine + j + 1;
                    RenderIndex[Quad * 6 + 2] = i * iLine + j;
                    RenderIndex[Quad * 6 + 3] = (i + 1) * iLine + j + 1;
                    RenderIndex[Quad * 6 + 4] = i * iLine + j + 1;
                    RenderIndex[Quad * 6 + 5] = (i + 1) * iLine + j;
                }
                Quad++;
            }
        }
    }
    
    void SaveVoxel(uint[] heightArray, int x, int z, uint y)
    {
        uint i = heightArray[256 * z + x];
        uint walk    =   (i>>16)&0xff;
        if(walk==0)
        {
            walk++;
            i = (walk<<16 )|y;
        }
        if (walk == 1)
        {
            walk++;
            i = (walk << 16) | y << 8 | (i & 0xff);
        }
        else
        {
            i = (walk << 16) | y << 8 | (i & 0xff);
        }
        heightArray[256 * z + x] = i;
    }
    void CalcBound(Vector3[] vArray)
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

    int Int2_Index(int2 v)
    { 
        return v.y*iLine+v.x;
    }
    bool IsIndexValid(int2 v)
    {
        if (v.x >= iLine || v.y >= iRow || v.x < 0 || v.y < 0)
        {
            return false;
        }
        
        return true;
    }
    bool IsIndexWalkable(int2 v)
    {

        byte by = navData[Int2_Index(v)];
        if ((by & 0x80) == 0)
        {
            return false;
        }
        if ((by & 0x40) != 0)
        {
            return false;
        }
        return true;
    }
    bool GetIndex(Vector3 p,ref int2 xy)
    {
        Vector3 v = p - bound_min;
        
        xy.y = (int)(v.z / honeycombLine+0.5f);
        if ((xy.y & 1 )== 1)
        {
            xy.x = (int)(v.x / fScale);
        }
        else
        {
            xy.x = (int)(v.x / fScale+0.5f);
        }
        return IsIndexValid(xy);
    }
    Vector3 int2_position(int2 v,byte height)
    {
        float xoffset = 0.0f;
        if ((v.y & 1) == 1)
        {
            xoffset = fScale*0.5f;
        }
        int h = height & 0x3f;
        float x = v.x * fScale + bound_min.x + xoffset;
        float z = v.y * honeycombLine + bound_min.z;
        float y = h*LayerHeight + bound_min.y;
        return new Vector3(x, y, z);
    }


    bool AStar(Vector3 begin, Vector3 end, out List<Vector3> path)
    {
        path = null;
        if (navData == null)
        {
           
            return false;
        }

        int2 vbegin = new int2();
        int2 vend = new int2();
        if (!GetIndex(begin, ref vbegin) || !GetIndex(end, ref vend))
        {
            return false;
        }
        if (!IsIndexWalkable(vbegin) || !IsIndexWalkable(vend))
        {
            return false;
        }

        
        //List<SearchNode> lstUnSearch = new List<SearchNode>();
        BT.BinaryTree lstUnSearch = new BT.BinaryTree();
        SearchNode newNode = NewSearchNode(vbegin, end);
        lstUnSearch.Push(newNode.weight(),newNode);
        MarkSearched(newNode);
        if (!WalkHoneycomb(vend, end,lstSearch, lstUnSearch))
        {
            return false;
        }
        SearchNode node = lstSearch[lstSearch.Count - 1];

        path = new List<Vector3>();
        while (true)
        {
            path.Insert(0, node.p);
            if (node.parent == null)
            {
                break;
            }
            else
            {
                node = node.parent;
            }
        }
        foreach (SearchNode n in lstSearch)
        {
            UnMarkSearched(n);

        }
        lstSearch.Clear();
        SearchNode no = (SearchNode)lstUnSearch.Pop();
        while (true)
        {
            if (no == null)
            {
                break;
            }
            UnMarkSearched(no);
            no = (SearchNode)lstUnSearch.Pop();
        }
        lstUnSearch.Clear();
        return true;
    }

    bool WalkHoneycomb(int2 vend, Vector3 endPos, List<SearchNode> search, BT.BinaryTree unsearch)
    {
        int count   =   0;
        while (true)
        {
            SearchNode node = (SearchNode)unsearch.Pop();
            if (node == null)
            {
                return false;
            }
            //Debug.Log(node.v.x + " " + node.v.y);
            search.Add(node);
            
            if (node.v.Equal(vend))
            {
                return true;
            }

            int current = navData[Int2_Index(node.v)]&0x3f;

            int2[] varray = node.v.Neighbour();
            int newchild = 0;
            for (int i = 0; i < varray.Length; i++)
            {
                if (!IsIndexValid(varray[i]))
                {
                    continue;
                }
                if (!IsIndexWalkable(varray[i]))
                {
                    continue;
                }

                int by = navData[Int2_Index(varray[i])]&0x3f;

                float fHeightOffset = Mathf.Abs(by - current) * LayerHeight;
                if (fHeightOffset > 0.5f)
                {
                    continue;
                }

                SearchNode n = NewSearchNode(varray[i], endPos);
                n.SetParent(node, fScale);
                count++;
                //Debug.Log(count);
                newchild++;
                unsearch.Push(n.weight(),n);
                MarkSearched(n);
            }
            //Debug.Log("Add Node " + newchild);
            
        }

        return false;
    }

    SearchNode NewSearchNode(int2 v,Vector3 end)
    {

        SearchNode n = new SearchNode();
        n.p             =   int2_position(v,navData[Int2_Index(v)]);
        n.v = v;
        n.walk_distance =   0.0f;
        n.distance = (n.p - end).magnitude;
        
        return n;
    }
    public void MarkSearched(SearchNode n)
    {
        int index   =   Int2_Index(n.v);
        byte by = navData[index];
        by |= 0x40;
        navData[index] = by;
        
    }
    public void UnMarkSearched(SearchNode n)
    {
        int index = Int2_Index(n.v);
        byte by = navData[index];
        int val = by & (~0x40);
        navData[index] = (byte)val;
    }
}
