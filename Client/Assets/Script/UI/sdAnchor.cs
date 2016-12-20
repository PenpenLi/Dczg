using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdAnchor : MonoBehaviour
{
	public float standard_width = 1280;
	public float standard_height = 720;
	
	void Start ()
	{
		float fRadio = (standard_width/standard_height) / ((float)Screen.width/(float)Screen.height);
		UIAnchor anchor = gameObject.GetComponent<UIAnchor>();
		if (anchor != null)
		{
			anchor.relativeOffset.y = anchor.relativeOffset.y/fRadio;
		}
	}

}
