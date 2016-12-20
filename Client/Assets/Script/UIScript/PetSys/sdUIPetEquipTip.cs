using System;
using UnityEngine;
using System.Collections;

public class sdUIPetEquipTip : MonoBehaviour 
{
	static private GameObject		m_preWnd			= null;
	
	public GameObject icon = null;
	public GameObject label_name = null;
	public GameObject Lb_defence_name = null;
	public GameObject Lb_defence_Num = null;
	
	public GameObject Lb_part = null;
	public GameObject label_level = null;
	public GameObject Lb_color = null;
	
	public GameObject label_desc = null;
	public GameObject label_price = null;
	
	public int m_iItemID = 0;
	bool hasAtlas = false;
	int iconId = -1;
	string strIcon = "";
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
//	void Update () 
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
			}
		}
	}
	
	void OnClick()
    {
		if ( gameObject.name=="btnClose")
		{
			if( sdUIPetControl.m_UIPetEquipTip != null )
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetEquipTip);
			
			if( m_preWnd )
				m_preWnd.SetActive(true);
		}
	}
	
	public void ActivePetEquipTip(GameObject PreWnd, int iID)
	{
		m_preWnd = PreWnd;
		hasAtlas = false;
		iconId = -1;
		strIcon = "";
		
		if (iID<=0) 
		{
			m_iItemID = 0;
			
			if (label_name)
				label_name.GetComponent<UILabel>().text = "物品ID错误";
			
			if (label_desc)
				label_desc.GetComponent<UILabel>().text = "物品ID错误";
			
			if (Lb_defence_name)
				Lb_defence_name.GetComponent<UILabel>().text = "生命";
			
			if (Lb_defence_Num)
				Lb_defence_Num.GetComponent<UILabel>().text = "0";
			
			if (Lb_part)
				Lb_part.GetComponent<UILabel>().text = "宠物装备";
			
			if (label_level)
				label_level.GetComponent<UILabel>().text = "使用等级 2";
			
			if (Lb_color)
			{
				Color PetEquipColor0 = new Color(214f/255f, 214f/255f, 214f/255f, 1f);
				Lb_color.GetComponent<UILabel>().color = PetEquipColor0;
				Lb_color.GetComponent<UILabel>().text = "普通";
			}
			
			if (label_price)
				label_price.GetComponent<UILabel>().text = "售价:0";

			return;
		}
		
		m_iItemID = iID;
		ReflashPetEquipTipUI();
	}
	
	public void ReflashPetEquipTipUI()
	{
		Color PetEquipColor0 = new Color(214f/255f, 214f/255f, 214f/255f, 1f);
		Color PetEquipColor1 = new Color(45f/255f, 210f/255f, 18f/255f, 1f);
		Color PetEquipColor2 = new Color(0f, 144f/255f, 1f, 1f);
		Color PetEquipColor3 = new Color(164f/255f, 84f/255f, 254f/255f, 1f);
		Color PetEquipColor4 = new Color(1f, 179f/255f, 15f/255f, 1f);
		
		Hashtable info = sdConfDataMgr.Instance().GetItemById(m_iItemID.ToString());
		if (info != null)
		{
			iconId = int.Parse(info["IconID"].ToString());
			strIcon = info["IconPath"].ToString();
			if (iconId >= 0)
			{
				sdConfDataMgr.Instance().LoadItemAtlas(iconId.ToString(), OnSetAtlas);
//				hasAtlas = false;
//				UIAtlas atlas = sdConfDataMgr.Instance().GetItemAtlas(iconId.ToString());
//				if (atlas!=null&&icon!=null)
//				{
//					icon.GetComponent<UISprite>().atlas = atlas;
//					icon.GetComponent<UISprite>().spriteName = strIcon;
//				}
			}
			
			string strName = info["ShowName"].ToString();
			if (label_name!=null)
				label_name.GetComponent<UILabel>().text = strName;
			
			string mainAtt = info["StringExtra3"].ToString();
			string strValue ="";
			if (mainAtt!="" && mainAtt!="0")
				strValue = sdConfDataMgr.Instance().GetShowStr(mainAtt);
			
			if (Lb_defence_name)
				Lb_defence_name.GetComponent<UILabel>().text = strValue;
			
			if (Lb_defence_Num)
			{
				if (strValue!="")
					Lb_defence_Num.GetComponent<UILabel>().text = info[mainAtt].ToString();
				else
					Lb_defence_Num.GetComponent<UILabel>().text = "";
			}
			
			if (Lb_part!=null)
				Lb_part.GetComponent<UILabel>().text = info["Name"].ToString();
			
			if (label_level!=null)
				label_level.GetComponent<UILabel>().text = "使用等级 " + info["NeedLevel"].ToString();
			
			int iQuility = int.Parse(info["Quility"].ToString());
			if (Lb_color!=null)
			{
				if (iQuility==1)
				{
					Lb_color.GetComponent<UILabel>().color = PetEquipColor0;
					Lb_color.GetComponent<UILabel>().text = "普通";
				}
				else if (iQuility==2)
				{
					Lb_color.GetComponent<UILabel>().color = PetEquipColor1;
					Lb_color.GetComponent<UILabel>().text = "优秀";
				}
				else if (iQuility==3)
				{
					Lb_color.GetComponent<UILabel>().color = PetEquipColor2;
					Lb_color.GetComponent<UILabel>().text = "精良";
				}
				else if (iQuility==4)
				{
					Lb_color.GetComponent<UILabel>().color = PetEquipColor3;
					Lb_color.GetComponent<UILabel>().text = "史诗";
				}
				else if (iQuility==5)
				{
					Lb_color.GetComponent<UILabel>().color = PetEquipColor4;
					Lb_color.GetComponent<UILabel>().text = "传说";
				}
				else 
				{
					Lb_color.GetComponent<UILabel>().color = PetEquipColor0;
					Lb_color.GetComponent<UILabel>().text = "普通";
				}
			}
			
			if (label_desc)
				label_desc.GetComponent<UILabel>().text = info["Description"].ToString();
			
			if (label_price)
				label_price.GetComponent<UILabel>().text = "售价:" + info["Value"].ToString();
		}
		else
		{
			if (label_name)
				label_name.GetComponent<UILabel>().text = "未知物品";
			
			if (label_desc)
				label_desc.GetComponent<UILabel>().text = "未知物品";
			
			if (Lb_defence_name)
				Lb_defence_name.GetComponent<UILabel>().text = "生命";
			
			if (Lb_defence_Num)
				Lb_defence_Num.GetComponent<UILabel>().text = "0";
			
			if (Lb_part)
				Lb_part.GetComponent<UILabel>().text = "未知物品";
			
			if (label_level)
				label_level.GetComponent<UILabel>().text = "使用等级 1";
			
			if (Lb_color)
			{
				Lb_color.GetComponent<UILabel>().color = PetEquipColor0;
				Lb_color.GetComponent<UILabel>().text = "普通";
			}
			
			if (label_price)
				label_price.GetComponent<UILabel>().text = "售价:0";
		}
	}
}

