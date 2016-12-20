using UnityEngine;
using System.Collections;

public class sdChainRender : MonoBehaviour {
	public	GameObject Target		=	null;
	MeshRenderer		mr			=	null;
	// Use this for initialization
	void Start () {
		//Target	=	GameObject.Find("Sphere");
		mr	=	GetComponent<MeshRenderer>();
	}
	
	void	ModifyMesh(Vector3 v,float iIndex)
	{
		float fScale	=	3.0f;
		MeshFilter	f	=	GetComponent<MeshFilter>();
		f.mesh.Clear();
		//v.y=0.0f;
		Vector3 vDir	=	v;
		vDir.Normalize();
		Vector3 up	=	new Vector3(0,1,0);
		Vector3 right	=	Vector3.Cross(vDir,up);
		
			float fDis	=	v.magnitude/fScale;
			Vector3[] 	pos		=	new Vector3[4];
			Vector2[]	uv		=	new Vector2[4];
			for(int i=0;i<1;i++)
			{				
				pos[i*4]	=right*0.25f*fScale;
				pos[i*4+1]	=right*0.25f*fScale+v;
				pos[i*4+2]	=-right*0.25f*fScale;
				pos[i*4+3]	=-right*0.25f*fScale+v;				
				
				uv[i*4]		=new Vector2(0.25f*(iIndex+1),1.0f);
				uv[i*4+1]	=new Vector2(0.25f*(iIndex+1),0.0f);
				uv[i*4+2]	=new Vector2(0.25f*iIndex,1.0f);
				uv[i*4+3]	=new Vector2(0.25f*iIndex,0.0f);
			}
			f.mesh.vertices	=	pos;
			f.mesh.uv		=	uv;
			
			int[] index	=	new int[6];
			for(int i=0;i<1;i++)
			{
				index[i*6]	=	i*4;
				index[i*6+1]	=	i*4+1;
				index[i*6+2]	=	i*4+2;
				index[i*6+3]	=	i*4+2;
				index[i*6+4]	=	i*4+1;
				index[i*6+5]	=	i*4+3;
				
			}
			f.mesh.triangles	=	index;
			Vector3 center = v*0.5f;
			v	= new Vector3(Mathf.Abs(v.x),Mathf.Abs(v.z),Mathf.Abs(v.z))*0.5f;
			f.mesh.bounds	=	new Bounds(center,v);
	}
	// Update is called once per frame
	void Update () {
		if(Target!=null)
		{
			Vector3 v	=	Target.transform.position	-	transform.position;
			
				//float fdis	=	v.sqrMagnitude;
			float f = (float)(Time.frameCount%100);//f(float)Random.Range(0,20);
			ModifyMesh(v,Mathf.Floor(f*0.1f));
				
			mr.enabled	=	true;
		}
		else
		{
			mr.enabled	=	false;
		}
	}
}
