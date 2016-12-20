using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class sdGuideExtra : MonoBehaviour
{	
	public GameObject arrow = null;
	public GameObject parentPanel = null;
	Vector3 lastPos = Vector3.zero;

	public List<EventDelegate> onFinish = new List<EventDelegate>();
	
	void Start()
	{
		Time.timeScale = 0;
	}

	void OnPress(bool flag)
	{
		if (flag == true)
		{
			lastPos = Input.mousePosition;
		}
		else
		{
			Vector3 pos = Input.mousePosition;
			if (pos.x - lastPos.x > 50)
			{
				sdGameLevel.instance.mainChar.CastSkill(1002);
				sdUICharacter.Instance.HideGuidRoll();

				Time.timeScale = 1f;
				EventDelegate.Execute(onFinish);
				onFinish.Clear();
			}
		}
	}
}
