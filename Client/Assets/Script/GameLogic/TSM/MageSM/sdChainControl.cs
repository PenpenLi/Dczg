using UnityEngine;
using System.Collections;

public class sdChainControl : MonoBehaviour {
	GameObject[] 	obj	=	new GameObject[3];
	sdMainChar		mc	=	null;
	GameObject[]	targetObj	=	null;
	// Use this for initialization
	void Awake () {
		obj[0]	=	transform.FindChild("Fx_Lighting0").gameObject;
		obj[1]	=	transform.FindChild("Fx_Lighting1").gameObject;
		obj[2]	=	transform.FindChild("Fx_Lighting2").gameObject;
	
		ActiveObj(0,false);
		ActiveObj(1,false);
		ActiveObj(2,false);
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(mc==null)
		{
			mc	=	sdGameLevel.instance.mainChar;
			
		}
		if(mc!=null)
		{
				//transform.parent = ;
				transform.position	=	mc.transform.position+new Vector3(0,1,0);
		}
		if(targetObj!=null)
		{
			for(int i=0;i<targetObj.Length;i++)
			{
				obj[i].transform.position	=	targetObj[i].transform.position+new Vector3(0,1,0);
			}
		}
		
		
	}
	
	public	void	SetPositionArray(GameObject[] posArray)
	{
		targetObj	=	posArray;
		if(posArray==null)
		{
			ActiveObj(0,false);
			ActiveObj(1,false);
			ActiveObj(2,false);
		}
		else
		{
			
			for(int i=0;i<3;i++)
			{
				
				if(i<posArray.Length )
				{
				
					ActiveObj(i,true);
					//obj[i].transform.position	=	posArray[i].transform.position;
				}
				else
				{
					ActiveObj(i,false);
				}
			}
		}
	}
	void	ActiveObj(int i,bool bActive)
	{
		if(obj[i]!=null)
		{
			obj[i].SetActive(bActive);
		}
	}
}
