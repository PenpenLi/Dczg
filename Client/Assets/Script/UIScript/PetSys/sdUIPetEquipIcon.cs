using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdUIPetEquipIcon : MonoBehaviour
{
	public int index = 0;
	private UInt64 equipUID = UInt64.MaxValue;
	
	public GameObject activePic = null;
	public GameObject icon = null;
	public GameObject lb_name = null;
	public GameObject lb_point = null;
	public GameObject bg = null;
	public GameObject iconbg = null;
	public bool bChangeEquipWnd = false;
	
	public bool bSelect = false;
	bool hasAtlas = false;
	int iconId = -1;
	string strIcon = "";
	
	public UInt64 GetId()
	{
		return equipUID;
	}
	
//	void Update()
//	{
//		if (!hasAtlas)
//		{
//			if (iconId >= 0)
//			{
//				UIAtlas atlas = sdConfDataMgr.Instance().GetItemAtlas(iconId.ToString());
//				if (atlas!=null&&icon!=null)
//				{
//					icon.GetComponent<UISprite>().atlas = atlas;
//					icon.GetComponent<UISprite>().spriteName = strIcon;
//					hasAtlas = true;
//				}
//			}
//		}
//	}

    void OnSetAtlas(ResLoadParams param, UnityEngine.Object obj)
	{
		if (iconId >= 0)
		{
            UIAtlas atlas = obj as UIAtlas;
			if (atlas!=null&&icon!=null)
			{
				icon.GetComponent<UISprite>().atlas = atlas;
				icon.GetComponent<UISprite>().spriteName = strIcon;
				hasAtlas = true;
			}
		}
	}
	
	void OnClick()
	{
		if (gameObject)
		{
			//换装备界面武器防具等，同时只能选中一件..
			if (bSelect==false && equipUID!=UInt64.MaxValue) 
			{
				sdGamePetItem petItem = sdNewPetMgr.Instance.getPetItem(equipUID);
				if (petItem!=null)
				{
					GameObject wnd = sdGameLevel.instance.NGUIRoot;
					if (wnd)
					{
						sdUIPetChangeEquipPnl cePnl = wnd.GetComponentInChildren<sdUIPetChangeEquipPnl>();
						if (cePnl)
							cePnl.SetAllItemUnSelectByCharacter(petItem.iCharacter);
					}
				}
			}

			bSelect = !bSelect;
			ReflashSelectUI();
		}
	}
	
	public void SetIdAndReflashUI(UInt64 id)
	{
		if (id==UInt64.MaxValue) 
		{
			equipUID = UInt64.MaxValue;
			gameObject.SetActive(false);
			hasAtlas = false;
			iconId = -1;
			strIcon = "";
			
			if (iconbg)
				iconbg.GetComponent<sdUIPetEquipButtonClick>().m_iEquipID = 0;
			
			return;
		}
		
		if (iconbg)
			iconbg.GetComponent<sdUIPetEquipButtonClick>().m_iEquipID = 0;
		
		gameObject.SetActive(true);
		equipUID = id;
		bSelect = false;
		ReflashSelectUI();
		
		Color PetEquipColor0 = new Color(214f/255f, 214f/255f, 214f/255f, 1f);
		Color PetEquipColor1 = new Color(45f/255f, 210f/255f, 18f/255f, 1f);
		Color PetEquipColor2 = new Color(0f, 144f/255f, 1f, 1f);
		Color PetEquipColor3 = new Color(164f/255f, 84f/255f, 254f/255f, 1f);
		Color PetEquipColor4 = new Color(1f, 179f/255f, 15f/255f, 1f);
		sdGamePetItem petItem = sdNewPetMgr.Instance.getPetItem(equipUID);
		if (petItem!=null)
		{
			Hashtable info = sdConfDataMgr.Instance().GetItemById(petItem.templateID.ToString());
			if (info != null)
			{
				iconId = int.Parse(info["IconID"].ToString());
				strIcon = info["IconPath"].ToString();
				if (iconId >= 0)
				{
					sdConfDataMgr.Instance().LoadItemAtlas(iconId.ToString(), OnSetAtlas);
//					hasAtlas = false;
//					UIAtlas atlas = sdConfDataMgr.Instance().GetItemAtlas(iconId.ToString());
//					if (atlas!=null&&icon!=null)
//					{
//						icon.GetComponent<UISprite>().atlas = atlas;
//						icon.GetComponent<UISprite>().spriteName = strIcon;
//					}
				}
				
				string strName = info["ShowName"].ToString();
				int iQuility = int.Parse(info["Quility"].ToString());
				if (lb_name)
				{
					if (iQuility==1)
						lb_name.GetComponent<UILabel>().color = PetEquipColor0;
					else if (iQuility==2)
						lb_name.GetComponent<UILabel>().color = PetEquipColor1;
					else if (iQuility==3)
						lb_name.GetComponent<UILabel>().color = PetEquipColor2;
					else if (iQuility==4)
						lb_name.GetComponent<UILabel>().color = PetEquipColor3;
					else if (iQuility==5)
						lb_name.GetComponent<UILabel>().color = PetEquipColor4;
					else 
						lb_name.GetComponent<UILabel>().color = PetEquipColor0;
					
					lb_name.GetComponent<UILabel>().text = strName;
				}
				
				int score = sdConfDataMgr.Instance().GetItemScore(petItem.templateID.ToString(), 0);
				if (lb_point!= null)
				{
					string txt = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("Score"), score.ToString());
					lb_point.GetComponent<UILabel>().text = txt;
				}
				
				if (iconbg)
					iconbg.GetComponent<sdUIPetEquipButtonClick>().m_iEquipID = petItem.templateID;
			}
		}
	}
	
	public void ReflashSelectUI()
	{
		if (activePic)
			activePic.SetActive(bSelect);
		
		if (bg)
		{
			if (bSelect)
				bg.GetComponent<UISprite>().spriteName = "bg_list_click";
			else
                bg.GetComponent<UISprite>().spriteName = "List_bg";
		}
	}
}
