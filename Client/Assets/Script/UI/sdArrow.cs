using UnityEngine;
using System.Collections;
using System;

public class sdArrow : MonoBehaviour
{
	public bool isSlider = false;
	float time = 0;
	bool isAdd = true;

	public GameObject beginObj = null;
	public GameObject endObj = null;

	bool isMove = true;
	public void StopMove()
	{
		isMove = false;
	}

	void Update()
	{
		if (!isMove) return;

		time += 0.02f;
		if (time > 0.1f)
		{
			time = 0;
			if (isSlider)
			{
				Vector3 pos = transform.localPosition;
				pos.x += 20;
				transform.localPosition = pos;
				if (pos.x >= endObj.transform.localPosition.x+10)
				{
					Vector3 beginPos = beginObj.transform.localPosition;
					pos.x = beginPos.x;
					transform.localPosition = pos;
				}
			}
			else
			{
				Quaternion r = transform.localRotation;
				if (isAdd)
				{
					r.z += 5f/180f;
				}
				else
				{
					r.z -= 5f/180f;
				}
				
				if (r.z >= 15f/180f)
				{
					isAdd = false;
				}
				else if (r.z <= 0)
				{
					isAdd = true;
				}
				transform.rotation = r;
			}
		}
	}
}
