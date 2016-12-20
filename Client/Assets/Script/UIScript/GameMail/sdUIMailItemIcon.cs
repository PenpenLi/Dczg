using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdUIMailItemIcon : MonoBehaviour
{
	public GameObject item = null;
	public GameObject icon = null;
	public GameObject paper = null;
	public GameObject cover = null;
	public GameObject lbname = null;
	public GameObject count = null;
	public bool bCanClick = false;

	public int itemID = 0;
	int iconId = -1;

	void Update()
	{
	}

	void OnClick()
	{
		if (gameObject.activeSelf==true && bCanClick==true && itemID>0)
		{
			Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(itemID.ToString());
			if (itemInfo!=null)
			{
				int iClass = int.Parse(itemInfo["Class"].ToString());
				int iSubClass = int.Parse(itemInfo["SubClass"].ToString());
				int iQuility = int.Parse(itemInfo["Quility"].ToString());

				//物品是宠物..
				if (iClass==(int)GameItemClassType.Game_Item_Class_Pet)
				{
					int iExpend = int.Parse(itemInfo["Expend"].ToString());
					sdUIPetControl.Instance.ActivePetZuheSmallTip(null, iExpend, 0, 1);
				}
				else
				{
					sdUICharacter.Instance.ShowTip(TipType.TempItem, itemID.ToString());
				}
			}
		}
	}
	
	void OnSetAtlas(ResLoadParams param, UnityEngine.Object obj)
	{
		if (iconId >= 0)
		{
			UIAtlas atlas = obj as UIAtlas;
			if (atlas!=null && icon!=null)
			{
				icon.GetComponent<UISprite>().atlas = atlas;
			}
		}
	}

	void OnSetPetAtlas()
	{
		if (icon!=null)		
			icon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
	}

	public void SetIdAndReflashUI(int id, int iCount)
	{
		if (id<=0)
		{
			itemID = 0;
			iconId = -1;
			gameObject.SetActive(false);
			return;
		}
		
		gameObject.SetActive(true);
		itemID = id;
		iconId = -1;
		
		Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(itemID.ToString());
		if (itemInfo!=null)
		{
			int iClass = int.Parse(itemInfo["Class"].ToString());
			int iSubClass = int.Parse(itemInfo["SubClass"].ToString());
			int iQuility = int.Parse(itemInfo["Quility"].ToString());

			if (icon!=null)
			{
				//物品是宠物..
				if (iClass==(int)GameItemClassType.Game_Item_Class_Pet)
				{
					int iPetId = int.Parse(itemInfo["Expend"].ToString());
					Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(iPetId.ToString());
					if (petInfo!=null)
					{
						icon.GetComponent<UISprite>().spriteName = petInfo["Icon"].ToString();
						if( sdConfDataMgr.Instance().PetAtlas != null )
							icon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
						else
							sdConfDataMgr.Instance().LoadPetAtlas( new EventDelegate(OnSetPetAtlas) );
					}
				}
				else
				{
					icon.GetComponent<UISprite>().spriteName = itemInfo["IconPath"].ToString();
					iconId = int.Parse(itemInfo["IconID"].ToString());
					sdConfDataMgr.Instance().LoadItemAtlas(iconId.ToString(), OnSetAtlas);
				}
			}

			if (paper)
			{
				//宠物碎片..
				if (iClass==(int)GameItemClassType.Game_Item_Class_Pet && iSubClass==2)
					paper.GetComponent<UISprite>().spriteName = "IconFrame0-cp";
				else
					paper.GetComponent<UISprite>().spriteName = "IconFrame0";
			}

			if (iQuility==1)
			{
				if (cover)
					cover.GetComponent<UISprite>().spriteName = "IconFrame1";
				
				if (lbname!=null)
				{
					lbname.GetComponent<UILabel>().text = itemInfo["ShowName"].ToString();
					lbname.GetComponent<UILabel>().color = new Color(255f/255f, 255f/255f, 255f/255f, 1f);
				}
			}
			else if (iQuility==2)
			{
				if (cover)
					cover.GetComponent<UISprite>().spriteName = "IconFrame2";
				
				if (lbname!=null)
				{
					lbname.GetComponent<UILabel>().text = itemInfo["ShowName"].ToString();
					lbname.GetComponent<UILabel>().color = new Color(45f/255f, 210f/255f, 18f/255f, 1f);
				}
			}
			else if (iQuility==3)
			{
				if (cover)
					cover.GetComponent<UISprite>().spriteName = "IconFrame3";
				
				if (lbname!=null)
				{
					lbname.GetComponent<UILabel>().text = itemInfo["ShowName"].ToString();
					lbname.GetComponent<UILabel>().color = new Color(0f, 144f/255f, 1f, 1f);
				}
			}
			else if (iQuility==4)
			{
				if (cover)
					cover.GetComponent<UISprite>().spriteName = "IconFrame4";
				
				if (lbname!=null)
				{
					lbname.GetComponent<UILabel>().text = itemInfo["ShowName"].ToString();
					lbname.GetComponent<UILabel>().color = new Color(164f/255f, 84f/255f, 254f/255f, 1f);
				}
			}
			else if (iQuility==5)
			{
				if (cover)
					cover.GetComponent<UISprite>().spriteName = "IconFrame5";
				
				if (lbname!=null)
				{
					lbname.GetComponent<UILabel>().text = itemInfo["ShowName"].ToString();
					lbname.GetComponent<UILabel>().color = new Color(1f, 179f/255f, 15f/255f, 1f);
				}
			}
			else
			{
				if (cover)
					cover.GetComponent<UISprite>().spriteName = "IconFrame1";
				
				if (lbname!=null)
				{
					lbname.GetComponent<UILabel>().text = itemInfo["ShowName"].ToString();
					lbname.GetComponent<UILabel>().color = new Color(214f/255f, 214f/255f, 214f/255f, 1f);
				}
			}

			if (count)
			{
				if (iCount<=1)
					count.GetComponent<UILabel>().text = "";
				else
					count.GetComponent<UILabel>().text = iCount.ToString();
			}
		}
	}
}
