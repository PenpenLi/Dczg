using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class sdDragAvatar : MonoBehaviour
{
	Vector2 lastDelta;
	void OnDrag(Vector2 delta)
	{
		GameObject	avatarcam	=	sdGameLevel.instance.avatarCamera.gameObject;
		if(avatarcam!=null){
			Quaternion	q	=	avatarcam.transform.GetChild(0).localRotation;
			q*=	Quaternion.AngleAxis(-delta.x*0.001f*360.0f,new Vector3(0,1,0));
			avatarcam.transform.GetChild(0).localRotation = q;
		}
	}
}