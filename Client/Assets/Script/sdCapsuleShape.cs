using UnityEngine;
using System.Collections;
public	enum ShapeType
{
	eSphere,
	eCapsule
}
public	class sdCapsuleShape
{
	public	Vector3		point;
	public	Vector3		dir;
	public	float		length;
	public	float		radius;
	public	ShapeType	type;
	Vector2	GetPoint(Vector3 p)
	{
		Vector2 vOut	=	new Vector2();
		Vector3 v		=	p-point;
		vOut.x			=	Vector3.Dot(v,dir);
		float c			=	v.magnitude;
		vOut.y			=	Mathf.Sqrt(c*c-vOut.x*vOut.x);
		return vOut;
	}
	public	bool	IsIntersect(Vector3 p,float	r)
	{
		Vector3 v		=	p-point;
		float	fdot	=	Vector3.Dot(v,dir);
		float	dis		=	v.magnitude;
		float	maxDis	=	r+radius;
		if(fdot<=0.0f)
		{
			return dis	<	maxDis;
		}
		else if(fdot	>=	length)
		{
			return Vector3.Distance(point+dir*length,p)<maxDis;
		}
		else
		{
			return Mathf.Sqrt(dis*dis-fdot*fdot)<maxDis;
		}
	}
	public	bool	IsIntersect(sdCapsuleShape c)
	{
		Vector3	Y	=	c.dir;
		Vector3	X	=	dir;
		
		Vector3	vBegin	=	c.point-point;
		Vector3	vEnd	=	vBegin+c.dir*c.length;
		
		bool bIntersect	=	IsIntersect(vBegin+point,c.radius);
		if(bIntersect)
		{
			return true;
		}
		bIntersect	=	IsIntersect(vEnd+point,c.radius);
		if(bIntersect)
		{
			return true;
		}
		
		float fBeginDot	=	Vector3.Dot(vBegin,X);
		float fEndDot	=	Vector3.Dot(vEnd,X);
		float maxDis	=	radius+c.radius;
		
		if(	(fBeginDot	<=	-maxDis	&&	fEndDot	<=	-maxDis)||
			(fBeginDot	>=	length+maxDis	&&	fEndDot	>=	length+maxDis))
		{
			return false;
		}
		float	fXYDot	=	Vector3.Dot(Y,X);
		if(fXYDot>0.9999f || fXYDot <-0.9999f)
		{
			return false;
		}
		else
		{
			Vector3	Z	=	Vector3.Cross(Y,X);Z.Normalize();
			float	bDotZ	=	Vector3.Dot(vBegin,Z);
			if(Mathf.Abs(bDotZ)>length+maxDis)
			{
				return false;
			}
			Y	=	Vector3.Cross(X,Z);Y.Normalize();
			
			Vector2 begin_yz	=	new Vector2(
												Vector3.Dot(vBegin,Y),
												Vector3.Dot(vBegin,Z));
			Vector2 end_yz	=	new Vector2(
												Vector3.Dot(vEnd,Y),
												Vector3.Dot(vEnd,Z));
			
			Vector2	yz_dir	=	end_yz-begin_yz;
			float	be_length	=	yz_dir.magnitude;
			yz_dir.Normalize();
			float	ZeroDot		=	Vector2.Dot(-begin_yz,yz_dir);
			if(ZeroDot<=0)
			{
				return false;
			}
			else if(ZeroDot>=be_length)
			{
				return false;
			}
			else
			{
				Vector3 nearPoint	=	c.point	+	c.dir*c.length*ZeroDot/be_length;
				return IsIntersect(nearPoint,c.radius);
			}
		}
	}
	public	void	SetInfo(Collider collider)
	{
		if(collider.GetType() == typeof(CapsuleCollider))
		{
			CapsuleCollider	colli	=	(CapsuleCollider)collider;
				
			length	=	colli.height*0.5f;
			radius	=	colli.radius;
			point	=	colli.transform.TransformPoint(colli.center);
				
			if(colli.direction == 0)
			{
				dir	=	colli.transform.TransformDirection(new Vector3(-1,0,0));
			}
			else if(colli.direction == 1)
			{
				dir	=	colli.transform.TransformDirection(new Vector3(0,-1,0));
			}
			else if(colli.direction == 2)
			{
				dir	=	colli.transform.TransformDirection(new Vector3(0,0,-1));
			}
					
			point	-=	dir*length*0.5f;
			
			type	=	ShapeType.eCapsule;
		}
		else if(collider.GetType()==typeof(SphereCollider))
		{
			SphereCollider	colli	=	(SphereCollider)collider;
			point	=	colli.transform.TransformPoint(colli.center);
			radius	=	colli.radius;
			type	=	ShapeType.eSphere;
		}
			
	}
	//设置为连续碰撞检测..
	public	void	SetCCDInfo(Collider collider,Vector3 lastPosition)
	{
		if(collider.GetType() == typeof(CapsuleCollider))
		{
			SetInfo(collider);
		}
		else if(collider.GetType()==typeof(SphereCollider))
		{
			SphereCollider	colli	=	(SphereCollider)collider;
			point	=	colli.transform.TransformPoint(colli.center);
			radius	=	colli.radius;
			
			
			Vector3 vDir	=	lastPosition	-	point;
			length	=	vDir.magnitude;
			dir 	=	vDir.normalized;
			type	=	ShapeType.eCapsule;
		}
	}
	public	void	SetInfo(CharacterController colli)
	{
		length	=	colli.height-colli.radius*2.0f;
		radius	=	colli.radius;
		point	=	colli.transform.TransformPoint(colli.center);
			
		dir	=	colli.transform.TransformDirection(new Vector3(0,-1,0));
				
		point	-=	dir*length*0.5f;
		type	=	ShapeType.eCapsule;
	}
}