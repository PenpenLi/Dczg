using UnityEngine;
using System.Collections;

public class PickupMoney : MonoBehaviour {
	uint	money		=	0;
	float	FadeInTime	=	0.0f;
	float	HoldTime	=	2.0f;
	float	FadeOutTime	=	2.0f;
	
	float	fCurrent	=	5.0f;
	public	Vector2	pos			=	new Vector2(200,200);
	GUITexture	icon	=	null;
	private bool mVisible = false;
	// Use this for initialization
	void Start () {
		//Find ICON
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
			money				=	0;
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
		Color tex			=	GetComponent<GUITexture>().color;
		tex.a 				= fAlpha;
		GetComponent<GUITexture>().color	=	tex;
		tex				=	GetComponent<GUIText>().color;
		tex.a 			= fAlpha;
		GetComponent<GUIText>().color	=	tex;
		if(icon!=null){
			tex			=	icon.color;
			tex.a 		= fAlpha;
			icon.color	=	tex;
		}
		
		Vector2	v	=	pos;
		
		GetComponent<GUITexture>().pixelInset			=	new Rect(v.x,v.y,301,68);
		GetComponent<GUIText>().pixelOffset				=	v+new Vector2(80,45);
		if(icon!=null){
			icon.pixelInset	=	new Rect(v.x+5,v.y+5,58,58);
		}
	}
	void	Reset(){
		money	=	0;
	}
	public	void	Show(uint uiMoney){
		mVisible = true;
		if(fCurrent	>	FadeInTime+HoldTime+FadeOutTime){
			GetComponent<GUIText>().enabled		=	true;
			GetComponent<GUITexture>().enabled	=	true;
			if(icon!=null){
				icon.enabled	=	true;
			}
			money	=	uiMoney;
		}else{
			money	+=	uiMoney;
		}
		GetComponent<GUIText>().text	=	money.ToString()+"金币获得";
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
		money				=	0;
	}
}
