using UnityEngine;
using System.Collections;

public class ChooseLevelReward : MonoBehaviour 
{
	public CampaignMgr	m_CampaignMgr;
	
	private static GameObject	m_LevelRewardWnd		= null;
	private static bool			m_isLoadLevelRewardWnd	= false;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
	
	}
	
	void OnClick()
	{
		ShowLevelReward(gameObject.name);
	}
	
	void ShowLevelReward(string boxidx)
	{
		if( m_LevelRewardWnd == null ) 
		{
			if(m_isLoadLevelRewardWnd) return;
			ResLoadParams param = new ResLoadParams();
			param.info = boxidx;
			sdResourceMgr.Instance.LoadResource("UI/LevelUI/$LevelRewardWnd.prefab", LoadWnd, param);
			m_isLoadLevelRewardWnd = true;
		}
		else
		{
			WndAni.ShowWndAni(m_LevelRewardWnd,false,"sp_grey");
			//m_LevelRewardWnd.SetActive(true);
			m_CampaignMgr.ShowBoxFX(false,m_LevelRewardWnd);
			if( boxidx == "bt_reward1" )
				m_LevelRewardWnd.GetComponent<LevelReward>().ShowLevelRewardWnd(m_CampaignMgr.m_RewardBox1,m_CampaignMgr.m_BattleStars);
			else if( boxidx == "bt_reward2" )
				m_LevelRewardWnd.GetComponent<LevelReward>().ShowLevelRewardWnd(m_CampaignMgr.m_RewardBox2,m_CampaignMgr.m_BattleStars);
			else if( boxidx == "bt_reward3" )
				m_LevelRewardWnd.GetComponent<LevelReward>().ShowLevelRewardWnd(m_CampaignMgr.m_RewardBox3,m_CampaignMgr.m_BattleStars);
		}
	}
	
	void LoadWnd(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) return;
		
		m_LevelRewardWnd = GameObject.Instantiate(obj) as GameObject;
		m_LevelRewardWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_LevelRewardWnd.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_LevelRewardWnd.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		
		m_isLoadLevelRewardWnd = false;
		ShowLevelReward( param.info );
	}
}
