using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour {
	float	FadeInTime	=	0.0f;
	float	HoldTime	=	2.0f;
	float	FadeOutTime	=	2.0f;
	float	fCurrent	=	5.0f;
	
	public	Vector2	pos			=	new Vector2(200,200);
	int 	itemIndex			=	0;
	GUITexture	icon	=	null;
	private  bool    mVisible = false;
	// Use this for initialization
	void Start () {
		for(int i=0;i<transform.childCount;i++)
		{
			Transform child	=	transform.GetChild(i);
			if(child.gameObject.name	==	"icon"){
				icon	=	child.gameObject.GetComponent<GUITexture>();
				if(icon!=null){
					icon.enabled	=	false;
					break;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!mVisible)
			return;
		fCurrent+=Time.deltaTime;
		
		if(fCurrent	>	FadeInTime+HoldTime+FadeOutTime){
			GetComponent<GUIText>().enabled		=	false;
			GetComponent<GUITexture>().enabled	=	false;
			return;
		}
		
		float fAlpha	=	1.0f;
		if(fCurrent	<	FadeInTime){
			fAlpha	=	fCurrent/FadeInTime;
		}else if(fCurrent	<	FadeInTime+HoldTime){
			
		}else if(fCurrent	<	FadeInTime+HoldTime+FadeOutTime){
			fAlpha	=	1.0f - (fCurrent-FadeInTime-HoldTime)/FadeOutTime;
		}
		if(fAlpha<0.0f){
			fAlpha	=	0.0f;
		}
		Color tex	=	GetComponent<GUITexture>().color;
		tex.a = fAlpha;
		GetComponent<GUITexture>().color	=	tex;
		tex	=	GetComponent<GUIText>().color;
		tex.a = fAlpha;
		GetComponent<GUIText>().color	=	tex;
		if(icon!=null){
			tex	=	icon.color;
			tex.a = fAlpha;
			icon.color	=	tex;
		}

		Vector2	v	=	pos	+	new Vector2(0,(1+itemIndex)*-68.0f);
		
		GetComponent<GUITexture>().pixelInset	=	new Rect(v.x,v.y,301,68);
		GetComponent<GUIText>().pixelOffset				=	v+new Vector2(80,45);
		if(icon!=null){
			icon.pixelInset	=	new Rect(v.x+2,v.y+2,64,64);
		}
	}
	public	void	Show(string itemname,Color c){
		mVisible = true;
		GetComponent<GUIText>().enabled		=	true;
		GetComponent<GUITexture>().enabled	=	true;
		if(icon!=null){
			icon.enabled	=	true;
			//icon.texture	=	Resources.Load("1") as Texture;
		}
		c.a = 1.0f;
		GetComponent<GUIText>().color	=	c;
		GetComponent<GUIText>().text	=	itemname.ToString();
		fCurrent	=	0.0f;
	}
	public	void	Hide(){
		mVisible = false;
		fCurrent			=	FadeInTime+HoldTime+FadeOutTime;
		GetComponent<GUIText>().enabled		=	false;
		GetComponent<GUITexture>().enabled	=	false;
		if(icon!=null){
			icon.enabled	=	false;
		}
	}
	public	void	ResetPos(int idx){
		itemIndex	=	idx;
	}
	public	bool	IsDead(){
		return fCurrent	>	FadeInTime+HoldTime+FadeOutTime;
	}
}
