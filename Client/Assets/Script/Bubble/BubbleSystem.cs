using UnityEngine;
using System.Collections;

public class BubbleSystem : MonoBehaviour{
	
	PickupMoney		money	=	null;
	PickupItem[]	item;
	GameObject		self	=	null;
	public	static	BubbleSystem	system	=	null;
	GameObject		mainCam	=	null;
	Camera			uiCam	=	null;
	
	static	PickupMoney	PickupMoney(uint uiMoney){
		GameObject	obj = GameObject.Instantiate(Resources.Load("Bubble/PickupMoney")) as GameObject;
		PickupMoney	p	= obj.GetComponent<PickupMoney>();
		
		return p;
	}
	static	PickupItem	PickupItem(string	name,Color c){
		GameObject	obj = GameObject.Instantiate(Resources.Load("Bubble/PickupItem")) as GameObject;
		PickupItem	p	= obj.GetComponent<PickupItem>();
		
		return p;
	}
	// Use this for initialization
	public	void	Start() {
		
		this.transform.parent = sdGameLevel.instance.transform;
				
		money	=	PickupMoney(0);
		money.Hide();
		money.transform.parent	=	transform;		
		
		item	=	new PickupItem[3];
		for(int i=0;i<3;i++)
		{
			item[i]	=	PickupItem("",Color.white);
			item[i].Hide();
			item[i].transform.parent	=	transform;			
		}
		mainCam			=	sdGameLevel.instance.mainCamera.gameObject;
		uiCam			=	sdGameLevel.instance.UICamera;
		
		self			=	new GameObject();
		self.name		=	"@SelfBubble";
		if(uiCam!=null){
			self.transform.parent	=	uiCam.transform;
		}
		self.transform.localPosition	=	Vector3.zero;//new Vector3(75,-250.0f,0);
		self.layer	=	11;
		system	=	this;
	}
	public	Vector3 	WorldToScreen(Vector3 v)
	{
		if(mainCam!=null)
		{
			Camera cam	=	mainCam.GetComponent<Camera>();
			if(cam!=null)
			{
				return cam.WorldToViewportPoint(v);
			}
		}
		return Vector3.zero;
	}
	public	Transform	GetUICamera(){
		if(uiCam!=null)
		{
			return uiCam.transform;
		}
		return null;
	}
	public	Transform	GetTransform(){
		return	transform;
	}
	public	Transform	GetSelfTransform(){
		return self.transform;
	}
	public	Vector3		GetUp(){
		return mainCam.transform.up;
	}
	// Update is called once per frame
	public	void 	Update () {
		Sort();
		
		for(int i=0;i<3;i++){
			item[i].ResetPos(i);
		}
		/*
		Vector2 v	=	Vector2.zero;
		if(!item[0].IsDead()){
			v	=	item[0].pos;
			if(Vector2.Distance(v,Vector2.zero)<0.001f){
				v	=	Vector2.zero;
			}else{
				v.y	-=	Time.deltaTime;
			}
		}

		for(int i=0;i<3;i++){
			item[i].pos	=	v;
		}
		*/
	}
	void	Sort(){
		int iNumAlive = 0;
		int iDead	  = 2;
		PickupItem[]	itemarray	=	new PickupItem[3];
		for(int i=0;i<3;i++){
			if(!item[i].IsDead()){
				itemarray[iNumAlive]=	item[i];
				iNumAlive++;
			}else{
				itemarray[iDead]	=	item[i];
				iDead--;
			}
		}
		item	=	itemarray;
	}
	public	void	MoneyBubble(uint uiMoney){
		money.Show(uiMoney);
	}
	public	void	ItemBubble(string	name,float r,float g,float b){
		
		Sort();
		
		int iNewIndex	=	0;
		bool bFind	=	false;
		for(int i=0;i<3;i++){
			if(item[i].IsDead()){
				item[i].Show(name,new Color(r,g,b,1));
				iNewIndex	=	i;
				bFind	=	true;
				break;
			}
		}
		if(!bFind){
			PickupItem	it	=	item[0];
			item[0]	=	item[1];
			item[1]	=	item[2];
			item[2]	=	it;
			it.Show(name,new Color(r,g,b,1));
			iNewIndex	=	2;
		}
		
		
		//item[2].Show(name,Color.yellow);
	}
}
