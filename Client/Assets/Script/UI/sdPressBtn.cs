
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdPressBtn : MonoBehaviour
{
	bool isBig = false;
	public float num = 1.1f;
	
	void OnPress(bool isDown)
	{
		if (!isDown) 
		{
			gameObject.transform.localScale /= num;
			isBig = false;
			return;
		}
		
		if (!isBig)
		{
			gameObject.transform.localScale *= num;
			isBig = true;
		}
	}
}