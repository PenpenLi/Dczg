using UnityEngine;
using System.Collections;

public class HexagonElement : MonoBehaviour {

    static GameObject parent = null;
    static Vector3[] vertex = null;
    static int[] face = null;

	// 当前Hexagon坐标aa
	public Hexagon.Coord hexagonCoord;

	// 当前Hexagon取值aa
	public ushort mValue = 0;

	// 当前着色类型aa
	protected int mType = 0;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mType == 3)
		{
			mValue = Hexagon.Manager.GetSingleton().GetHeight(hexagonCoord);

			Vector4 kColor = Vector2.zero;
			int iActorWeight = (mValue & Hexagon.Tile.ActorWeightMask) >> Hexagon.Tile.ActorWeightShift;
			if (iActorWeight == 0)
				kColor = new Vector4(0.0f, 0.0f, 0.0f); 
			else if (iActorWeight == 1)
				kColor = new Vector4(0.5f, 0.5f, 0.5f);
			else if (iActorWeight == 2)
				kColor = new Vector4(0.0f, 1.0f, 0.0f);
			else if (iActorWeight == 3)
				kColor = new Vector4(0.0f, 0.0f, 1.0f); 
			else
				kColor = new Vector4(1.0f, 0.0f, 0.0f);

			MeshRenderer kMeshRender = this.gameObject.GetComponent<MeshRenderer>();
			kMeshRender.material.SetVector("_Color", kColor);
		}
	}

	// 构建一个调试Hexagonaa
    void Build(Hexagon.Coord v, ushort height, float fWeight,int type)
    {
		hexagonCoord = v;
		mValue = height;
		mType = type;

        MeshFilter mf   =   gameObject.AddComponent<MeshFilter>();
        MeshRenderer r = gameObject.AddComponent<MeshRenderer>();

        Shader shader = Shader.Find("SDShader/SelfIllum_Object");
        r.material = new Material(shader);

		int actor = (height & Hexagon.Tile.ActorWeightMask) >> Hexagon.Tile.ActorWeightShift;
		int edge = (height & Hexagon.Tile.EdgeWeightMask) >> Hexagon.Tile.EdgeWeightShift;
        float actorweight = actor * 0.1f;
        float edgweight = edge * 0.1f;
        Vector4 color = Vector4.one*fWeight*0.01f;
        
        if (type == 0) 
        {
            color = new Vector4(actorweight, 0, edgweight); 
        }
        else if (type == 1)
        {
            color = new Vector4(0, actorweight+0.2f, edgweight); 
        }
        else if (type == 2)
        {
            color = Vector4.one*(actorweight+0.5f);
        }
		else
		{
			color = Vector4.one;
		}
         
        r.material.SetVector("_Color", color);

		mf.mesh.vertices = vertex;
		mf.mesh.triangles = face;
    }

	// 新建一个调试Hexagon对象aa
    public static GameObject NewElement(Hexagon.Coord v, ushort height,float fWeight,int type)
    {
		if (vertex == null)
		{
			vertex = new Vector3[7];

			float fSize = Hexagon.Manager.GetSingleton().GetHexagonSize() * 0.95f;
			float halfSize = fSize * 0.5f;

			float fSqrt12 = halfSize * 2.0f * Mathf.Sqrt(1.0f / 12.0f);
			float fHeight = 0.1f;

			vertex[0] = Vector3.zero;
			vertex[1] = new Vector3(halfSize, fHeight, -fSqrt12);
			vertex[2] = new Vector3(halfSize, fHeight, fSqrt12);
			vertex[3] = new Vector3(0, fHeight, 2.0f * fSqrt12);

			vertex[4] = new Vector3(-halfSize, fHeight, fSqrt12);
			vertex[5] = new Vector3(-halfSize, fHeight, -fSqrt12);
			vertex[6] = new Vector3(0, fHeight, -2.0f * fSqrt12);

			face = new int[6 * 3]
            {
                0,6,5,
                0,5,4,
                0,4,3,
                0,3,2,
                0,2,1,
                0,1,6
            };
		}

        if(parent==null)
        {
            parent  =   new GameObject();
            parent.transform.position   =   Vector3.zero;
            parent.transform.rotation   =   Quaternion.identity;
        }

        Vector3 pos =   Hexagon.Manager.GetSingleton().Coord_Position(v, height);
        GameObject obj = new GameObject();
        obj.name = v.x + "_" + v.z + "_" + v.y;
        obj.transform.parent = parent.transform;
        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.identity; ;

        HexagonElement ele  =   obj.AddComponent<HexagonElement>();
        ele.Build(v, height,fWeight,type);
        return obj;
    }
}
