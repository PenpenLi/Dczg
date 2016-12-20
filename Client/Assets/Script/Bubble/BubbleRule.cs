using UnityEngine;
using System.Collections;

public	class BubbleRule{
	public	float	FadeInTime	=	1.0f;
	public 	float 	HoldTime	=	1.0f;
	public	float	FadeOutTime	=	1.0f;
	public	float	MaxScale	=	1.0f;
	public	float	Scale		=	0.8f;
	public	float	ShakeTime	=	0.0f;
	public	Vector3	Velocity	=	new Vector3(-0.3f,1,0);
	public	string	PreText		=	"";
	public	string	PostText	=	"";
	public	float fScaleTime	=	0.1f;
	public 	float 	fOrginScale	=	0.075f;
	public	float	fOrginSpeed	=	0.75f;
	public  Bubble.BubbleType bubbleType = Bubble.BubbleType.eBT_BaseHurt;
	public	BubbleRule(){
		Velocity	*=	fOrginSpeed;//BubbleSystem.system.GetUp()*
	}
	public	virtual	float	CalcLerp(float	fCurrent)
	{
		if(fCurrent	<	FadeInTime){
			return fCurrent/FadeInTime;
		}else if(fCurrent <	FadeInTime+HoldTime){
			return 1.0f;
		}else if(fCurrent <	FadeInTime+HoldTime+FadeOutTime){
			return 1.0f - (fCurrent-FadeInTime-HoldTime)/FadeOutTime;
		}else{
			return -1.0f;
		}
	}
	public	virtual	float	CalcScale(float	fCurrent)
	{
		if(fCurrent	<	FadeInTime){
			return fOrginScale*MaxScale*fCurrent/FadeInTime;
		}else if(fCurrent	<	FadeInTime+fScaleTime){
			return fOrginScale*Mathf.Lerp(MaxScale,Scale,(fCurrent-FadeInTime)/fScaleTime);
		}else{
			return fOrginScale*Scale;
		}
	}
	public	virtual	void	CalcPosition(Transform obj,float	fCurrent)
	{
		if(fCurrent	>	FadeInTime+HoldTime){
			obj.localPosition +=	Velocity*Time.deltaTime;
		}
	}
	public	virtual	bool	IsDead(float	fCurrent){
		return fCurrent > (FadeInTime+HoldTime+FadeOutTime);
	}
	public void	SetSelf(){
		if(bubbleType == Bubble.BubbleType.eBT_AddSp)
			Velocity	=	new Vector3(-0.5f,0.5f,0)*fOrginSpeed;
		else
			Velocity	=	new Vector3(0.5f,0.5f,0)*fOrginSpeed;
	
	}
}
public	class TargetRule	:	BubbleRule
{
	public TargetRule(){
		FadeInTime	=	0.1f;
		HoldTime	=	0.3f;
		FadeOutTime	=	0.5f;		
	}
}
public	class CriticalHurtRule	:	TargetRule
{
	Vector3 vTemp;
	float	fOffset;
	float	fShakeSpeed;
	public CriticalHurtRule(){
		FadeInTime	=	0.3f;
		HoldTime	=	0.7f;
		FadeOutTime	=	0.3f;
		MaxScale		=	1.8f;
		Scale			=	1.2f;
		ShakeTime		=	0.0f;
		fScaleTime		=	0.05f;
		vTemp			=	Vector3.zero;
		fOffset			=	Random.Range(0.0f,1.0f);
		fShakeSpeed		=	150;
	}
	
	public	override	float	CalcScale(float	fCurrent)
	{		
		float fScale	=	base.CalcScale(fCurrent);
		if(fCurrent	>	ShakeTime+FadeInTime || fCurrent < FadeInTime)
		{	
			return fScale;
		}
		float fLerp	=	1.0f	-	(fCurrent-FadeInTime)/ShakeTime;
		return fScale*(1.0f+Mathf.Sin(Time.time*fShakeSpeed)*0.3f*fLerp);
	}
	public	override	void	CalcPosition(Transform obj,float	fCurrent)
	{
		if(fCurrent	>	FadeInTime+HoldTime){
			obj.localPosition +=	Velocity*Time.deltaTime;
		}
	}

}
public	class UIRule	:	BubbleRule
{
	public	UIRule()
	{
		Velocity	=	Vector3.zero;
		FadeInTime	=	0.0f;
		HoldTime	=	2.0f;
		FadeOutTime	=	2.0f;
	}
}

public	class 	ExpRule	:	BubbleRule
{
	public	ExpRule()
	{
		Velocity	=	Vector3.zero;
		FadeInTime	=	0.1f;
		HoldTime	=	0.0f;
		FadeOutTime	=	1.0f;
	}
}

