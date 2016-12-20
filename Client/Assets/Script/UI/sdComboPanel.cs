
using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class sdComboPanel : MonoBehaviour
{	
	public GameObject combo;
	public GameObject arrow;
	public GameObject btn;
	
	public float lifeTime = 0.5f;
	bool needEnd = false;
	float existTime = 0;
	float changeTime = 0;
	
	public void SetIcon(int cur, int max)
	{
		string iconName = string.Format("{0}{1}", max, cur+1);
		combo.GetComponent<UISprite>().spriteName = iconName;
		if (cur + 1 == max)
		{
			needEnd = true;
			existTime = 0;
		}
	}
	
	int range = 0;
	void Update()
	{
		if (needEnd)
		{
			existTime += Time.deltaTime;
			if (existTime >= lifeTime)
			{
				needEnd = false;
				gameObject.SetActive(false);	
			}
		}
		
		if (arrow == null || btn == null || combo == null) return;
		Vector3 pos = arrow.transform.localPosition;
		Vector3 pos1 = btn.transform.localPosition;
		changeTime += Time.deltaTime;
		if (changeTime >= 0.1)
		{
			if (range >= 0)
			{
				pos.y += 1.67f;
				pos1.y -= 1.67f;
			}
			else if (range < 0)
			{
				pos.y -= 1.67f;
				pos1.y += 1.67f;
			}
			
			range++;
			if (range >= 3) range = -range;
			
			arrow.transform.localPosition = pos;
			btn.transform.localPosition = pos1;
			changeTime = 0;
		}
	}
}