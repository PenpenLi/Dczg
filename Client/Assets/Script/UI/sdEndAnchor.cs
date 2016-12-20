using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdEndAnchor : MonoBehaviour
{
	void OnPress(bool isDown)
	{
		if (isDown)
		{
			if (gameObject.transform.parent.GetComponent<UIAnchor>() != null && gameObject.transform.parent.GetComponent<UIAnchor>().enabled)
			{
			    gameObject.transform.parent.GetComponent<UIAnchor>().enabled = false;
				Vector4 vec = gameObject.transform.parent.GetComponent<UIPanel>().clipRange;
				vec.x = (float)-76.5;
				gameObject.transform.parent.GetComponent<UIPanel>().clipRange = vec;
			}
			

		}
	}
}
