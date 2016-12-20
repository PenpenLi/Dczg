
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class sdMaskPanel : MonoBehaviour
{	
	public UISprite bg = null;
	public sdRoleWndButton clickBtn = null;
	public sdRoleWndButton targetBtn = null;
	public GameObject arrow = null;
	public GameObject round = null;

	GameObject copyItem = null;
	GameObject tempItem = null;

    void Start()
    {
        sdUICharacter.Instance.SetMaskPanel(this);
        transform.parent = transform.parent.parent;
        gameObject.SetActive(false);
    }

	void OnCopy()
	{
// 		copyItem = GameObject.Instantiate(tempItem) as GameObject;
// 		if (copyItem.GetComponent<sdSlotIcon>() !=  null)
// 		{
// 			copyItem.GetComponent<sdSlotIcon>().index = -1;
// 		}
// 		copyItem.transform.parent = targetBtn.gameObject.transform.parent;
// 		copyItem.AddComponent<sdCopyItem>();
//         if (copyItem.GetComponent<sdShortCutIcon>() != null || copyItem.GetComponentInChildren<sdShortCutIcon>() != null)
// 		{
// 			copyItem.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
// 		}
// 		else
// 		{
// 			copyItem.transform.localScale = new Vector3(1f,1f,1f);
// 		}
// 		
// 		copyItem.transform.position = tempItem.transform.position;
        sdSlotIcon icon = tempItem.GetComponent<sdSlotIcon>();
        if (icon != null && icon.panel == PanelType.Panel_Bag)
        {
            Transform pic = tempItem.transform.FindChild("icon");
            if (pic != null)
            {
                 targetBtn.transform.position = pic.position;
            }
        }
        else
        {
            targetBtn.transform.position = tempItem.transform.position;
        }

		if (tempItem.GetComponent<BoxCollider>() == null)
		{
			targetBtn.GetComponent<BoxCollider>().size = new Vector3(2000,2000,1);
		}
		else
		{
			targetBtn.GetComponent<BoxCollider>().size = tempItem.GetComponent<BoxCollider>().size;
		}
        
		gameObject.SetActive(true);
	}

	public void ShowPanel(bool hasBg, GameObject obj)
	{
		if (hasBg)
		{
			bg.spriteName = "TipsSystem_Background";
		}
		else
		{
			bg.spriteName = "";
		}

		if (obj != null)
		{
			if (obj.layer != LayerMask.NameToLayer("NGUI"))
			{
				sdUICharacter.Instance.ShowSceneMask(obj);
				targetBtn.gameObject.SetActive(false);
				targetBtn.GetComponent<BoxCollider>().size = new Vector3(275, 275, 0);
				gameObject.SetActive(true);
			}
			else
			{
				targetBtn.gameObject.SetActive(true);
				tempItem = obj;
                if (obj.GetComponent<sdSlotIcon>() != null && obj.GetComponent<sdSlotIcon>().panel != PanelType.Panel_ItemUp &&
				    (obj.GetComponent<sdSlotIcon>().itemid == "0" || obj.GetComponent<sdSlotIcon>().itemid == ""))
				{
					EventDelegate finish = new EventDelegate(OnCopy);
					obj.GetComponent<sdSlotIcon>().onLoad.Add(finish);
				}
				else
				{
					OnCopy();
				}
                Time.timeScale = 0f;
			}
		}
		else
		{
            targetBtn.gameObject.SetActive(false);
            gameObject.SetActive(true);
			//if (arrow != null)arrow.SetActive(false);
		}

		
	}

	float time = 0;

	bool isAddRound = true;
	bool isAddArrow = false;

	void Update()
	{
        if (tempItem != null && targetBtn != null)
        {
            //copyItem.transform.position = tempItem.transform.position;
            sdSlotIcon icon = tempItem.GetComponent<sdSlotIcon>();
            if (icon != null && icon.panel == PanelType.Panel_Bag)
            {
                Transform pic = tempItem.transform.FindChild("icon");
                if (pic != null)
                {
                    targetBtn.transform.position = pic.position;
                }
            }
            else
            {
                targetBtn.transform.position = tempItem.transform.position;
            }
        }

		if (arrow == null) return;

		time += 0.02f;
		if (time > 0.1)
		{
			time = 0;
			Vector3 scale = round.transform.localScale;
			Vector3 pos = arrow.transform.localPosition;
			if (isAddRound)
			{
				scale.x += 0.033f;
				scale.y += 0.033f;
				if (scale.x >= 0.7)
				{
					scale.x = 0.7f;
					scale.y = 0.7f;
					isAddRound = false;
				}
			}
			else
			{
				scale.x -= 0.033f;
				scale.y -= 0.033f;
				if (scale.x <= 0.6)
				{
					scale.x = 0.6f;
					scale.y = 0.6f;
					isAddRound = true;
				}
			}

			if (isAddArrow)
			{
				pos.y += 4f;
				pos.x -= 3f;
				if (pos.y >= -25f)
				{
					pos.y = -25f;
					isAddArrow = false;
				}
			}
			else
			{
				pos.y -= 4f;
				pos.x += 3f;
				if (pos.y <= -40f)
				{
					pos.y = -40f;
					isAddArrow = true;
				}
			}

			arrow.transform.localPosition = pos;
			round.transform.localScale = scale;
		}
	}

	public void HidePanel()
	{
		targetBtn.transform.FindChild("point").GetComponent<UISprite>().spriteName = "bg2(1)";
		gameObject.SetActive(false);	
		sdUICharacter.Instance.HideSceneMask();
		//if (copyItem != null) GameObject.Destroy(copyItem);
		Time.timeScale = 1f;
	}

	public void HideMaskPoint()
	{
		targetBtn.transform.FindChild("point").GetComponent<UISprite>().spriteName = "";
	}
}
