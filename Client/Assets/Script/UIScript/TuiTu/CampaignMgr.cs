using UnityEngine;
using System.Collections;

public class CampaignMgr : MonoBehaviour 
{
	public UILabel			m_CrystalShow;
	public UILabel			m_SpriteShow;
	public UISprite			m_Star1Show;
	public UISprite			m_Star2Show;
	public UISprite			m_Star3Show;
	public GameObject		m_StarTextShow;
	public int				m_BattleID		= 0;			// 战役ID.
	
	float 	m_LastUpdate;
	
	public BattleBoxItem	m_RewardBox1	= null;
	public BattleBoxItem	m_RewardBox2	= null;
	public BattleBoxItem	m_RewardBox3	= null;
	public int				m_BattleStars	= 0;
	
	private GameObject		m_RewardFX1		= null;
	private GameObject		m_RewardFX2		= null;
	private GameObject		m_RewardFX3		= null;
	private GameObject		m_LevelRewardWnd= null;
	
	private UIAtlas			m_Atlas			= null;
	private UISprite		m_CampaignBG	= null;
	private UISprite		m_RedLine		= null;
	private GameObject[]	m_LevelBtn		= new GameObject[6];
	private UILabel			m_title			= null;
	private ChooseDifficulty m_Difficulty1	= null;
	private ChooseDifficulty m_Difficulty2	= null;
	private ChooseDifficulty m_Difficulty3	= null;
	
	
	// Use this for initialization
	void Start () 
	{
		if( m_CampaignBG == null )
		{
			m_title			= GameObject.Find("lb_campaign_title").GetComponent<UILabel>();
			m_CampaignBG	= GameObject.Find("sp_campaign_bg").GetComponent<UISprite>();
			m_RedLine		= GameObject.Find("sp_line_red").GetComponent<UISprite>();
			for(int i=1;i<=6;i++)
				m_LevelBtn[i-1] = GameObject.Find("btn_level"+i);
			m_Difficulty1	= GameObject.Find("bt_difficulty1").GetComponent<ChooseDifficulty>();
			m_Difficulty2	= GameObject.Find("bt_difficulty2").GetComponent<ChooseDifficulty>();
			m_Difficulty3	= GameObject.Find("bt_difficulty3").GetComponent<ChooseDifficulty>();
		}

		/*
		sdConfDataMgr.Instance().Init(true);	// test
		m_BattleID = 11; // test
		ShowCampaign();
		*/
	}

	public UISprite GetCampaignBG()
	{
		return m_CampaignBG;
	}

	public void ShowCampaign(int iBattleID)
	{
		// 初始化关卡贴图..
		if( iBattleID != m_BattleID ) 
		{
			m_BattleID = iBattleID;
			string name = string.Format("UI/LevelUI/$Campaign_{0}/Campaign_{0}.prefab", m_BattleID);
			ResLoadParams param = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource(name, OnLoadAtlas, param, typeof(UIAtlas));
		}

		int biID = sdLevelInfo.BattleInfoID(m_BattleID);	// 从11,12,21,31的BattleID格式转换陈1~16的模式..
		if( sdLevelInfo.battleInfos[biID] == null ) return;

		m_BattleStars = sdLevelInfo.battleInfos[biID].starCount;
		m_CrystalShow.text = m_BattleStars + " / 54";
		if( m_BattleStars < 18 )
		{
			m_Star1Show.spriteName = "star0";
			m_Star2Show.spriteName = "star0";
			m_Star3Show.spriteName = "star0";
			m_StarTextShow.SetActive(false);
		}
		else if( m_BattleStars < 36 )
		{
			m_Star1Show.spriteName = "star1";
			m_Star2Show.spriteName = "star0";
			m_Star3Show.spriteName = "star0";
			m_StarTextShow.SetActive(false);
		}
		else if( m_BattleStars < 54 )
		{
			m_Star1Show.spriteName = "star1";
			m_Star2Show.spriteName = "star1";
			m_Star3Show.spriteName = "star0";
			m_StarTextShow.SetActive(false);
		}
		else
		{
			m_Star1Show.spriteName = "star1";
			m_Star2Show.spriteName = "star1";
			m_Star3Show.spriteName = "star1";
			m_StarTextShow.SetActive(true);
		}

		// 显示宝箱状态..
		sdMainChar mc = sdGameLevel.instance.mainChar;
		if( mc != null )
		{
			int job = (int)mc.BaseJob;
			if( job == (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior )			job = 1;
			else if( job == (int)HeaderProto.ERoleJob.ROLE_JOB_Magic )		job = 2;
			else if( job == (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue )		job = 3;
			else if( job == (int)HeaderProto.ERoleJob.ROLE_JOB_Minister )	job = 4;
			else 															job = 0;
			
			m_RewardBox1 = sdLevelInfo.battleInfos[biID].rewardBox[0,job];
			if( m_RewardBox1 == null ) m_RewardBox1 = sdLevelInfo.battleInfos[biID].rewardBox[0,0];
			m_RewardBox2 = sdLevelInfo.battleInfos[biID].rewardBox[1,job];
			if( m_RewardBox2 == null ) m_RewardBox2 = sdLevelInfo.battleInfos[biID].rewardBox[1,0];
			m_RewardBox3 = sdLevelInfo.battleInfos[biID].rewardBox[2,job];
			if( m_RewardBox3 == null ) m_RewardBox3 = sdLevelInfo.battleInfos[biID].rewardBox[2,0];
		}
		
		if(m_RewardFX1==null) m_RewardFX1 = GameObject.Find("Fx_lingjing01");
		UISprite sp = GameObject.Find("sp_reward1").GetComponent<UISprite>();
		if( m_RewardBox1!=null && m_BattleStars>=m_RewardBox1.NeedStar )
		{
			if( m_RewardBox1.IsTake == true )
			{
				sp.spriteName = "xz1-3";
				m_RewardFX1.SetActive(false);
			}
			else
			{
				sp.spriteName = "xz1-2";
				m_RewardFX1.SetActive(true);
			}
		}
		else 
		{
			sp.spriteName = "xz1";
			m_RewardFX1.SetActive(false);
		}
		
		if(m_RewardFX2==null) m_RewardFX2 = GameObject.Find("Fx_lingjing02");
		sp = GameObject.Find("sp_reward2").GetComponent<UISprite>();
		if( m_RewardBox2!=null && m_BattleStars>=m_RewardBox2.NeedStar )
		{
			if( m_RewardBox2.IsTake == true )
			{
				sp.spriteName = "xz2-3";
				m_RewardFX2.SetActive(false);
			}
			else
			{
				sp.spriteName = "xz2-2";
				m_RewardFX2.SetActive(true);
			}
		}
		else 
		{
			sp.spriteName = "xz2";
			m_RewardFX2.SetActive(false);
		}
		
		if(m_RewardFX3==null)  m_RewardFX3 = GameObject.Find("Fx_lingjing03");
		sp = GameObject.Find("sp_reward3").GetComponent<UISprite>();
		if( m_RewardBox3!=null && m_BattleStars>=m_RewardBox3.NeedStar )
		{
			if( m_RewardBox3.IsTake == true )
			{
				sp.spriteName = "xz3-3";
				m_RewardFX3.SetActive(false);
			}
			else
			{
				sp.spriteName = "xz3-2";
				m_RewardFX3.SetActive(true);
			}
		}
		else 
		{
			sp.spriteName = "xz3";
			m_RewardFX3.SetActive(false);
		}
	}

	void OnLoadAtlas(ResLoadParams res, UnityEngine.Object obj)
	{
		if (obj == null) return;
		m_Atlas = obj as UIAtlas;
		
		if( m_CampaignBG != null )
		{
			Hashtable tb = sdConfDataMgr.Instance().GetTable("worldmappath");
			Hashtable t = tb[m_BattleID.ToString()] as Hashtable;

			// 战役名.
			int biID = sdLevelInfo.BattleInfoID(m_BattleID);
			m_title.text = biID.ToString() + ". " + t["ct"] as string;

			// 背景和红线..
			m_CampaignBG.atlas = m_Atlas;
			m_CampaignBG.spriteName = "bg1";
			m_RedLine.atlas = m_Atlas;
			m_RedLine.spriteName = "route1";
			m_RedLine.transform.localPosition = new Vector3( int.Parse(t["rx"] as string), int.Parse(t["ry"] as string), 0 );
			m_RedLine.width = int.Parse(t["rw"] as string);
			m_RedLine.height = int.Parse(t["rh"] as string);

			// 按钮..
			for(int i=0;i<6;i++)
			{
				int j = i+1;
				m_LevelBtn[i].transform.localPosition = new Vector3( int.Parse(t["b"+j+"x"] as string), int.Parse(t["b"+j+"y"] as string), 0 );
				UISprite sp = m_LevelBtn[i].transform.FindChild("sp_level_bg").GetComponent<UISprite>();
				sp.atlas = m_Atlas;
				sp.spriteName = t["b"+j] as string;
				if( j == 6 )
				{
					sp.width = int.Parse( t["b"+j+"w"] as string );
					sp.height = int.Parse( t["b"+j+"h"] as string );
				}

				m_LevelBtn[i].GetComponent<StartTuiTu>().m_iDifficulty = 1;
				m_LevelBtn[i].GetComponent<StartTuiTu>().UpdateLevelButton();
			}

			// 难度判断
			m_Difficulty1.UpdateDifficulty();
			m_Difficulty2.UpdateDifficulty();
			m_Difficulty3.UpdateDifficulty();

			int d = sdLevelInfo.battleInfos[ sdLevelInfo.BattleInfoID(m_BattleID) ].difficulty;
			if( d == 1 )
				m_Difficulty1.SelectDifficulty();
			else if( d == 2 )
				m_Difficulty2.SelectDifficulty();
			else if( d == 3 )
				m_Difficulty3.SelectDifficulty();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( (Time.time - m_LastUpdate) > 3.0f )
		{
			m_LastUpdate = 	Time.time;
			sdMainChar mc = sdGameLevel.instance.mainChar;
			if( mc != null ) 
				m_SpriteShow.text = mc.Property["EP"] +" / "+ mc.Property["MaxEP"];
		}
		
		// 特效没有层级关系，如果上层没有遮盖的UI，就要把特效再显示出来.
		if( m_LevelRewardWnd )
		{
			if( m_LevelRewardWnd.activeSelf == false )
				ShowBoxFX(true,null);
		}
		
		// 判断是否要更新显示数据.
		if( sdLevelInfo.NeedReflash )
		{
			sdLevelInfo.NeedReflash = false;
			ShowCampaign(m_BattleID);
		}
	}
	
	void OnClick()
	{
	}
	
	public void ShowBoxFX(bool bShow,GameObject LevelRewardWnd)
	{
		m_LevelRewardWnd = LevelRewardWnd;
		if( bShow )
		{
			if(m_RewardFX1) m_RewardFX1.transform.localPosition = new Vector3(0,0,0);
			if(m_RewardFX2) m_RewardFX2.transform.localPosition = new Vector3(0,0,0);
			if(m_RewardFX3) m_RewardFX3.transform.localPosition = new Vector3(0,0,0);
		}
		else
		{
			if(m_RewardFX1) m_RewardFX1.transform.localPosition = new Vector3(2000.0f,0,0);
			if(m_RewardFX2) m_RewardFX2.transform.localPosition = new Vector3(2000.0f,0,0);
			if(m_RewardFX3) m_RewardFX3.transform.localPosition = new Vector3(2000.0f,0,0);
		}
	}

	public bool IsAni()
	{
		if( m_Difficulty1.IsAni() || m_Difficulty2.IsAni() || m_Difficulty3.IsAni() )
			return true;
		else
			return false;
	}
}
