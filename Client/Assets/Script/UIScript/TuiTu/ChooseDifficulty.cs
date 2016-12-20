using UnityEngine;
using System.Collections;

public class ChooseDifficulty : MonoBehaviour 
{
	public GameObject		m_CampaignWnd;
	public UISprite			m_spStar;
	public UISprite			m_OtherDifficulty1;
	public UISprite			m_OtherDifficulty2;
	public GameObject		m_OtherDifficultyFrame1	= null;
	public GameObject		m_OtherDifficultyFrame2 = null;
	public TweenAlpha		m_Chain1;
	public TweenAlpha		m_Chain2;
	public GameObject		m_Bg;
	public GameObject		m_Eye1;
	public GameObject		m_Eye2;
	public TweenPosition	m_Effect;

	private int m_iDifficulty = 1;
	private bool m_bValid = false;
	private CampaignMgr m_CampaignMgr = null;
	private UIPanel	m_CullPanel = null;

	static bool m_bAni = false;


	// Use this for initialization
	void Start () 
	{
		if( gameObject.name == "bt_difficulty1" )
			m_iDifficulty = 1;
		else if( gameObject.name == "bt_difficulty2" )
			m_iDifficulty = 2;
		else if( gameObject.name == "bt_difficulty3" )
			m_iDifficulty = 3;

		m_CampaignMgr = m_CampaignWnd.transform.FindChild("CullPanel/CampaignPanel/sp_campaign/sp_bottom/sp_spirit/bt_addsprite").GetComponent<CampaignMgr>();
		m_CullPanel = m_CampaignWnd.transform.FindChild("CullPanel").GetComponent<UIPanel>();
		m_CullPanel.clipping = UIDrawCall.Clipping.None;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void UpdateDifficulty()
	{
		if( sdLevelInfo.levelInfos == null ) return;

		StartTuiTu lvl = m_CampaignWnd.transform.FindChild("CullPanel/CampaignPanel/btn_level1").GetComponent<StartTuiTu>();
		int iLevelID = lvl.GetLevelID()/100*100+10+m_iDifficulty;

		for(int i=0; i<sdLevelInfo.levelInfos.Length; i++)
		{
			if( sdLevelInfo.levelInfos[i].levelID == iLevelID )
			{
				if( sdLevelInfo.levelInfos[i].valid )
				{
					// 此难度已经激活.
					m_spStar.spriteName = "";
					m_bValid = true;
				}
				else
				{
					// 此难度还未激活.
					m_spStar.spriteName = "lock";
					m_bValid = false;
				}
				break;
			}
		}
	}
	
	public void OnClick()
	{
		if( m_bValid == false ) 
		{
			if( gameObject.name == "bt_difficulty2" )
				sdUICharacter.Instance.ShowMsgLine("完成此战役所有普通难度关卡，就可解锁精英难度。",MSGCOLOR.Yellow);
			else if( gameObject.name == "bt_difficulty3" )
				sdUICharacter.Instance.ShowMsgLine("完成此战役所有精英难度关卡，就可解锁专家难度。",MSGCOLOR.Yellow);
			return;
		}

		// 避免重复点击.
		if( gameObject.name == "bt_difficulty1" && m_OtherDifficulty1.spriteName == "jy1.png" && m_OtherDifficulty2.spriteName == "zj1.png" ) return;
		else if( gameObject.name == "bt_difficulty2" && m_OtherDifficulty1.spriteName == "pt1" && m_OtherDifficulty2.spriteName == "zj1.png" ) return;
		else if( gameObject.name == "bt_difficulty3" && m_OtherDifficulty1.spriteName == "pt1" && m_OtherDifficulty2.spriteName == "jy1.png" ) return;

		/*
		// 动画期间不能再切换.
		if( m_bAni ) return;

		// 播放闪白的动画效果.
		m_CullPanel.clipping = UIDrawCall.Clipping.AlphaClip;
		m_Chain1.PlayForward();
		m_Chain2.PlayForward();
		m_Bg.SetActive(true);
		m_Eye1.SetActive(false);
		m_Eye2.SetActive(false);
		m_CampaignMgr.ShowBoxFX(false,null);

		m_Effect.PlayForward();
		EventDelegate.Add(m_Effect.onFinished, onFinished);
		bPlayForward = true;
		m_bAni = true;
		*/

		SelectDifficulty();
	}

	bool bPlayForward = true;
	void onFinished()
	{
		if( bPlayForward == false )
		{
			m_CullPanel.clipping = UIDrawCall.Clipping.None;
			m_Chain1.PlayReverse();
			m_Chain2.PlayReverse();
			m_Bg.SetActive(false);
			m_Eye1.SetActive(true);
			m_Eye2.SetActive(true);
			m_CampaignMgr.ShowBoxFX(true,null);

			bPlayForward = true;
			m_bAni = false;
			EventDelegate.Remove(m_Effect.onFinished, onFinished);
			return;
		}

		SelectDifficulty();

		// 反向播放..
		m_Effect.PlayReverse();
		bPlayForward = false;
	}

	public void SelectDifficulty()
	{
		// 根据难度选择，设定关卡ID..
		GameObject obj = GameObject.Find("sp_newIcon");
		if( obj != null ) obj.transform.localScale = Vector3.zero;
		obj = GameObject.Find("sp_line_red");
		if( obj != null ) obj.GetComponent<UISprite>().fillAmount = 0;
		
		for(int i=1;i<=6;i++)
		{
			StartTuiTu lvl = m_CampaignWnd.transform.FindChild("CullPanel/CampaignPanel/btn_level"+i).GetComponent<StartTuiTu>();
			lvl.m_iDifficulty = m_iDifficulty;
			lvl.UpdateLevelButton();
		}
		
		// 记录此战役选择的难度.
		sdLevelInfo.battleInfos[ sdLevelInfo.BattleInfoID(m_CampaignMgr.m_BattleID) ].difficulty = m_iDifficulty;
		
		// 更改难度边框..
		if( gameObject.name == "bt_difficulty1" )
		{
			gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "pt2";
			m_OtherDifficulty1.spriteName = "jy1.png";
			m_OtherDifficulty2.spriteName = "zj1.png";
			m_OtherDifficultyFrame1.SetActive(false);
			m_OtherDifficultyFrame2.SetActive(false);
		}
		else if( gameObject.name == "bt_difficulty2" )
		{
			m_OtherDifficulty1.spriteName = "pt1";
			gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "jy2.png";
			m_OtherDifficulty2.spriteName = "zj1.png";
			m_OtherDifficultyFrame1.SetActive(true);
			m_OtherDifficultyFrame2.SetActive(false);
		}
		else if( gameObject.name == "bt_difficulty3" )
		{
			m_OtherDifficulty1.spriteName = "pt1";
			m_OtherDifficulty2.spriteName = "jy1.png";
			gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "zj2.png";
			m_OtherDifficultyFrame1.SetActive(false);
			m_OtherDifficultyFrame2.SetActive(true);
		}
	}

	public bool IsAni()
	{
		return m_bAni;
	}
}
