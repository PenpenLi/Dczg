using System.Collections;
using UnityEngine;
using System.Collections.Generic;

class sdSkillIcon: MonoBehaviour
{
	public string skillId = "";
	private Vector3 scale = new Vector3();
	private Vector3 size = new Vector3();
	private Vector3 parentPos = new Vector3();
	bool hasAtlas = false;
	
	void Start()
	{
		scale = gameObject.transform.localScale;
		size = gameObject.GetComponent<BoxCollider>().size;
		parentPos = transform.parent.localPosition;
	}
	
	void Update()
	{
		if (!hasAtlas)	
		{
			if (sdConfDataMgr.Instance().skilliconAtlas != null)
			{
				GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().skilliconAtlas;
				hasAtlas = true;
			}
		}
		
		Vector4 range = transform.parent.GetComponent<UIPanel>().clipRange;
		float pos = gameObject.transform.localPosition.x;
		float delt = Mathf.Abs(pos - range.x);
		float temp = delt/(float)(size.x*1.5);
		if (temp > 1) temp = 1;
		float newscale = (float)((1-temp)*0.4+0.6);
		gameObject.GetComponent<UISprite>().transform.localScale = scale*newscale;
		gameObject.GetComponent<BoxCollider>().size = size/newscale;
	}
	
	bool isDrag = false;
	
	void OnDrag(Vector2 delta)
	{
		isDrag = true;
	}
	
	public void Finish()
	{
		sdUICharacter.Instance.ShowSkillInfo(skillId);
		transform.parent.GetComponent<sdDragPanel>().onDragFinished -= new sdDragPanel.OnDragFinished(Finish);
	}
	
	void OnPress(bool isPressed)
	{
		if (!isPressed)
		{
			if (isDrag)
			{
				sdSkillIcon nearIcon = null;
				float min = 0;
				sdSkillIcon[] list = transform.parent.GetComponentsInChildren<sdSkillIcon>();
				Vector4 range = transform.parent.GetComponent<UIPanel>().clipRange;
				foreach(sdSkillIcon item in list)
				{
					float pos = item.gameObject.transform.localPosition.x;
					float delt = Mathf.Abs(pos - range.x);	
					if (nearIcon == null)
					{
						min = delt;
						nearIcon = item;
					}
					else
					{
						if (delt < min)
						{
							min = delt;
							nearIcon = item;
						}
					}
				}
				
				if (nearIcon != null) 
				{
					transform.parent.GetComponent<sdDragPanel>().onDragFinished += new sdDragPanel.OnDragFinished(nearIcon.Finish);
					transform.parent.GetComponent<sdDragPanel>().SetMove(nearIcon.transform.localPosition.x);
				}
				isDrag = false;
			}
			else
			{
				if (gameObject.transform.localPosition.x == transform.parent.GetComponent<UIPanel>().clipRange.x)
				{
					sdUICharacter.Instance.ShowTip(TipType.Skill, skillId);
				}
				else
				{
					transform.parent.GetComponent<sdDragPanel>().onDragFinished += new sdDragPanel.OnDragFinished(Finish);
					transform.parent.GetComponent<sdDragPanel>().SetMove(gameObject.transform.localPosition.x);
				}
			}
		}
		else
		{
			
		}
	}
}
