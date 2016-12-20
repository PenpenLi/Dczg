using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour 
{
	static void	Load(string str, Vector3 pos, BubbleType type, bool bSelf)
	{
        BubbleManager.Instance.AddBubble(str,pos,type,bSelf);
	}

	public	static	void	OtherHurt(int val,Vector3 pos, BubbleType type){
        if (val <= 0)
            return;
		Load(val.ToString(), pos, type, false);
	}

    public static void MissBubble(Vector3 pos, bool bSelf)
    {
		Load("Miss", pos, BubbleType.eBT_Miss, bSelf);
	}

    public static void Dodge(Vector3 pos, bool bSelf)
    {
        Load("dodge", pos, BubbleType.eBT_Dodge, bSelf);
    }

    public static void AddSp(int val, Vector3 pos, bool bSelf)
	{
		Load(val.ToString(), pos, BubbleType.eBT_AddSp, bSelf);
	}

    public static void AddHp(int val, Vector3 pos, bool bSelf)
	{
		Load(val.ToString(), pos, BubbleType.eBT_AddHp, bSelf);
	}

    public static void SelfHurt(int val, Vector3 pos, BubbleType type)
	{
		Load(val.ToString(), pos, type, false);
	}

    public static bool IsHurtOther(BubbleType type)
    {
        return type == BubbleType.eBT_BaseHurt || type == BubbleType.eBT_CriticalBaseHurt || type == BubbleType.eBT_CriticalSkillHurt || type == BubbleType.eBT_SkillHurt;
    }
	
	public	string	num			=	"0";
	public	Color	color		=	new Color(1,1,1,1);
	
	public	BubbleRule	rule;
	GameObject	CameraObj;
	bool		bInit;
	public  bool        bPause = false;     
	float		fCurrent;
	public	bool		bSelf = false;
	
	protected int mOwnerID = 0;
	public int OwnerID
	{
		set {mOwnerID = value;}
		get {return mOwnerID;}
	}
	
	protected float mSpeed = 1.0f;
	public float Speed
	{
		set {mSpeed = value;}
		get {return mSpeed;}
	}
	public enum BubbleType{
		eBT_BaseHurt,       //基础攻击aaa
		eBT_SkillHurt,      //技能攻击aaa
		eBT_CriticalBaseHurt,	//基础攻击暴击aaa
		eBT_CriticalSkillHurt,
		eBT_AddHp,
		eBT_AddSp,
		eBT_SelfHurt,
		eBT_Miss,
		//eBT_Immunity,
		eBT_PickUpMoney,
		eBT_PickUpItem,
		//eBT_Exp,
		eBT_Dodge,
		eBT_PetHurt,  //宠物攻击aaa
        eBT_Max,
	};
	
	public	static	Color GetBubbleColor(BubbleType type)
	{
		switch(type)
		{
			case BubbleType.eBT_BaseHurt:
			case BubbleType.eBT_CriticalBaseHurt:
			{
				return new Color(1,1,1,1);
			}
			case BubbleType.eBT_SkillHurt:
			case BubbleType.eBT_CriticalSkillHurt:
			{
				return new Color(192,183,37,1)/255.0f;
			}
			case BubbleType.eBT_AddHp:
			{
				return new Color(65,233,13,1)/255.0f;
			}
			case BubbleType.eBT_AddSp:
			{
				return new Color(61,188,230,1)/255.0f;
			}
			case BubbleType.eBT_SelfHurt:
			{
				return new Color(233,13,13,1)/255.0f;
			}
			case BubbleType.eBT_PetHurt:
			{
				return new Color(139, 0, 255, 1)/255.0f;
			}
            case BubbleType.eBT_Dodge:
            {
                return new Color(64, 0, 128, 1) / 255.0f;
            }
		}
		return new Color(1,1,1,1);
	}

	public	static	BubbleRule	NewRule(BubbleType type)
	{
		switch(type){
			case BubbleType.eBT_BaseHurt:
			case BubbleType.eBT_SkillHurt:
			{
				return new TargetRule();
			}
			case BubbleType.eBT_CriticalBaseHurt:
			case BubbleType.eBT_CriticalSkillHurt:
			{
				return new CriticalHurtRule();
			}
			case BubbleType.eBT_PickUpItem:
			case BubbleType.eBT_PickUpMoney:
			{
				return new UIRule();
			}
            //case BubbleType.eBT_Exp:
            //{
            //    return new ExpRule();
            //}
		}
		return new TargetRule();
	}
		
	public	void	Reset()
	{
		fCurrent	=	0.0f;
		color		=	new Color(color.r,color.g,color.b,1);
	}
	// Use this for initialization
	void Start () {
		fCurrent		=	0.0f;
		bInit			=	false;
	}
	
	// Update is called once per frame
	void Update () {
		if(bPause)
			return;
		fCurrent+=(Time.deltaTime*mSpeed);
		
		float fLerp	=	rule.CalcLerp(fCurrent);
		color		=	new Color(color.r,color.g,color.b,fLerp);
		MeshRenderer r = GetComponent<MeshRenderer>();
		r.material.SetColor("_colorBias",color);
		
		rule.CalcPosition(transform,fCurrent);
		transform.localScale	=	Vector3.one*rule.CalcScale(fCurrent);;
	
		if(rule.IsDead(fCurrent)){
			BubbleManager.Instance.DeleteBubble(this);
			GameObject.Destroy(gameObject);
		}
	}

	
	public	void	SetNumber(string	number,Color c){
		if(num != number || !bInit)
		{
			if(!bInit){
				bInit	=	true;
			}
			MeshFilter	f	=	GetComponent<MeshFilter>();
			f.mesh.Clear();
			
			num	=	number;
			char[] carray	=	num.ToCharArray();
			
			uint	vertexCount	=	(uint)carray.Length*4;
			uint	indexCount	=	(uint)carray.Length*6;
			
			float fBeginX	=	-carray.Length*0.5f;
						
			Vector3[] 	pos	=	new Vector3[vertexCount];
			Vector2[]	uv		=	new Vector2[vertexCount];
			for(int i=0;i<carray.Length;i++)
			{				
				pos[i*4]	=new Vector3(fBeginX+i-0.6f,1,0);
				pos[i*4+1]	=new Vector3(fBeginX+i+0.6f,1,0);
				pos[i*4+2]	=new Vector3(fBeginX+i-0.6f,-1,0);
				pos[i*4+3]	=new Vector3(fBeginX+i+0.6f,-1,0);
			
				uint idx = (uint)carray[i] - '0';
				float	y	=	1.0f -	(idx&0xFC)/16.0f;
				float	x	=	(idx&0x3)/4.0f;
				
				uv[i*4]	=new Vector2(x+0.4f*0.125f,y);
				uv[i*4+1]=new Vector2(x+0.25f-0.4f*0.125f,y);
				uv[i*4+2]=new Vector2(x+0.4f*0.125f,y-0.25f);
				uv[i*4+3]=new Vector2(x+0.25f-0.4f*0.125f,y-0.25f);
			}
			f.mesh.vertices	=	pos;
			f.mesh.uv		=	uv;
			
			int[] index	=	new int[indexCount];
			for(int i=0;i<carray.Length;i++)
			{
				index[i*6]	=	i*4;
				index[i*6+1]	=	i*4+1;
				index[i*6+2]	=	i*4+2;
				index[i*6+3]	=	i*4+2;
				index[i*6+4]	=	i*4+1;
				index[i*6+5]	=	i*4+3;
				
			}
			f.mesh.triangles	=	index;
			f.mesh.bounds	=	new Bounds(new Vector3(-fBeginX*2,-1,-1),new Vector3(fBeginX*2,1,1));
			
			MeshRenderer r = GetComponent<MeshRenderer>();
			r.material.SetColor("_colorBias",c);
			color	=	c;
		}
	}
	void	FillVertex(Vector3[] pos,Vector2[] uv,float x,float y,float size)
	{
		float halfSize	=	size*0.5f;
		float posx = size*4;
		
		pos[0]	=new Vector3(-posx,1,0);
		pos[1]	=new Vector3(posx,1,0);
		pos[2]	=new Vector3(-posx,-1,0);
		pos[3]	=new Vector3(posx,-1,0);
			
				
		uv[0]	=new Vector2(x,y);
		uv[1]	=new Vector2(x+size,y);
		uv[2]	=new Vector2(x,y-0.25f);
		uv[3]	=new Vector2(x+size,y-0.25f);
	}
	public	void	SetOther(BubbleType t,Color c){
		if(!bInit)
		{
			bInit	=	true;
			
			MeshFilter	f	=	GetComponent<MeshFilter>();
			f.mesh.Clear();
			
			uint	vertexCount	=	4;
			uint	indexCount	=	6;
			
			float fBeginX	=	-1;
			
			Vector3[] 	pos	=	new Vector3[vertexCount];
			Vector2[]	uv		=	new Vector2[vertexCount];
			
			switch(t){
				case BubbleType.eBT_Miss:{
					FillVertex(pos,uv,0.46f,0.5f,0.54f);
				}break;
				case BubbleType.eBT_Dodge:{
                    FillVertex(pos, uv, 0.0f, 0.25f, 0.5f);
				}break;
			}
			
				
			
			f.mesh.vertices	=	pos;
			f.mesh.uv		=	uv;
			
			int[] index	=	new int[indexCount];
			
			index[0]	=	0;
			index[1]	=	1;
			index[2]	=	2;
			index[3]	=	2;
			index[4]	=	1;
			index[5]	=	3;
				
			
			f.mesh.triangles	=	index;
			f.mesh.bounds	=	new Bounds(new Vector3(-fBeginX*2,-1,-1),new Vector3(fBeginX*2,1,1));
			
			MeshRenderer r = GetComponent<MeshRenderer>();
			r.material.SetColor("_colorBias",c);
			color	=	c;
			
		}
	}
	public	void	SetOrginScale(float fScale){
		if(rule!=null){
			if(fScale>0.0f)
				rule.fOrginScale	=	fScale;
		}
	}
}
