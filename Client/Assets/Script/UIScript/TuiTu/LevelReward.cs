using UnityEngine;
using System.Collections;

public class LevelReward : MonoBehaviour 
{
	public GameObject	m_btReward;
	public GameObject	m_spHaveReward;
	
	int  m_iItemAtlasID	= -1;
	bool m_bHasAtlas	= false;

	static uint m_uBoxID	= 0;
	
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
//	void Update () 
//	{
//
//	}

    void OnSetAtlas(ResLoadParams param, UnityEngine.Object obj)
	{
		if( m_iItemAtlasID>=0 && m_bHasAtlas==false )
		{
            UIAtlas atlas = obj as UIAtlas;
			if( atlas != null )
			{
				GameObject.Find("sp_rewarditem3").GetComponent<UISprite>().atlas = atlas;
				m_bHasAtlas = true;
			}
		}
	}
	
	// 显示奖励领取界面.
	public void ShowLevelRewardWnd(BattleBoxItem box, int stars)
	{
		m_uBoxID = box.BoxID;
		
		// 根据当前星值情况显示提示信息.
		GameObject obj = GameObject.Find("lb_levelreward");
		if( box.IsTake ) 
		{
			obj.GetComponent<UILabel>().text = "您已经领取了奖励宝箱。";
			obj.GetComponentInChildren<UISprite>().spriteName = "";
			GameObject.Find("lb_starneed").GetComponent<UILabel>().text = "";
		}
		else if( stars >= box.NeedStar )
		{
			obj.GetComponent<UILabel>().text = "恭喜您已经获得" + (stars>9?"":" ") + stars + "   ，请领取奖励宝箱！";
			UISprite spxx = obj.GetComponentInChildren<UISprite>();
			spxx.spriteName = "x";
			spxx.gameObject.transform.localPosition = new Vector3(-8.0f,4.0f,0);
			GameObject.Find("lb_starneed").GetComponent<UILabel>().text = "";
		}
		else
		{
			obj.GetComponent<UILabel>().text = "再获得         即可领取奖励宝箱，加油哦。";
			UISprite spxx = obj.GetComponentInChildren<UISprite>();
			spxx.spriteName = "x";
			spxx.gameObject.transform.localPosition = new Vector3(-114.0f,4.0f,0);
			GameObject.Find("lb_starneed").GetComponent<UILabel>().text = (box.NeedStar-stars).ToString();
		}
		
		// 物品奖励.
		GameObject.Find("lb_rewarditem1").GetComponent<UILabel>().text = box.NonMoney.ToString();
		GameObject.Find("lb_rewarditem2").GetComponent<UILabel>().text = box.NonCash.ToString();

		UILabel			lb = GameObject.Find("lb_rewarditem3").GetComponent<UILabel>();
		UISprite 		sp = GameObject.Find("sp_rewarditem3").GetComponent<UISprite>();
		DropoutButton 	bt = GameObject.Find("btn_rewarditem3").GetComponent<DropoutButton>();
		UISprite		sf = bt.transform.Find("sp_frame").GetComponent<UISprite>();
		if( box.Item1ID > 0 )
		{
			bt.transform.FindChild("Background").localScale = Vector3.one;
			Hashtable tab = sdConfDataMgr.Instance().GetItemById(box.Item1ID.ToString());
			if( tab != null )
			{
				bt.m_ItemID		= box.Item1ID.ToString();
				lb.text			= (string)tab["ShowName"]; if(box.Item1Count>1) lb.text+="x"+box.Item1Count;
				lb.color		= SDGlobal.QualityColor[int.Parse(tab["Quility"].ToString())];
				sp.spriteName	= (string)tab["IconPath"];	
				sf.spriteName	= "IconFrame" + tab["Quility"]; 
				m_bHasAtlas		= false;
				m_iItemAtlasID	= int.Parse((string)tab["IconID"]);
				sdConfDataMgr.Instance().LoadItemAtlas(m_iItemAtlasID.ToString(), OnSetAtlas);
			}
		}
		else
		{
			bt.m_ItemID		= "";
			lb.text			= "";
			sp.spriteName	= "";
			sf.spriteName	= "";
			bt.transform.FindChild("Background").localScale = Vector3.zero;
		}
		
		// 是否可用领奖的状态.
		if( box.IsTake )
		{
			m_btReward.SetActive(false);
			m_spHaveReward.SetActive(true);
		}
		else
		{
			m_btReward.SetActive(true);
			m_spHaveReward.SetActive(false);
			
			if( stars < box.NeedStar ) 
				m_btReward.GetComponent<UIButton>().enabled = false;
			else
				m_btReward.GetComponent<UIButton>().enabled = true;
		}
		
		this.gameObject.SetActive(true);
	}
	
	void OnClick()
	{
		if( gameObject.name == "btn_reward" )
		{
			if( gameObject.GetComponent<UIButton>().enabled )
			{
				// 检查背包格子数，目前暂定不能超过39个.
                if (sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, -1).Count >= sdGameItemMgr.Instance.maxBagNum)
				{
					sdUICharacter.Instance.ShowOkMsg("您的背包满了，不能领取奖励。",null);
					return;
				}
			
				sdLevelInfo.AskRewardBox(m_uBoxID);
				m_btReward.SetActive(false);
			}
			else
			{
				sdUICharacter.Instance.ShowMsgLine("您的星数不足，无法打开宝箱。",MSGCOLOR.Yellow);
			}
		}
	}
}
