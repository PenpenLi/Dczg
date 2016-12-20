using UnityEngine;
using System.Collections;

public class StartTuiTu : MonoBehaviour 
{
	public int			m_iBtnIdx;
	public GameObject	m_wndCampaign;
	public GameObject	m_objLockIcon;
	public GameObject	m_objNewIcon;
	public GameObject	m_objRedLine;
	public UILabel		m_lbLevelName;
	public UISprite		m_objStar1;
	public UISprite		m_objStar2;
	public UISprite		m_objStar3;
	public CampaignMgr	m_CampaignMgr;
	public int			m_iDifficulty	= 1;
	
	private bool 		m_bButtonValid	= false;
	private int			m_iLevelSprite;
	private int			m_iLevelTimes;
	
	
	void Awake()
	{
        if (m_objRedLine != null)
        {
            UISprite sp = m_objRedLine.GetComponentInChildren<UISprite>();
            if (sp != null)
            {
                sp.fillAmount = 0.0f;
            }
        }
	}

	public int GetLevelID()
	{
		return m_CampaignMgr.m_BattleID*1000 + m_iBtnIdx*10 + m_iDifficulty;
	}

	public void UpdateLevelButton()
	{
		if( sdLevelInfo.levelInfos == null ) return;
		int levelID = GetLevelID();
		for(int i=0; i < sdLevelInfo.levelInfos.Length; i++)
		{
			if( sdLevelInfo.levelInfos[i].levelID == levelID )
			{
				// 从配置文件读取关卡名称.
				string name = (string)sdLevelInfo.levelInfos[i].levelProp["ShowName"];
				if( name != "" ) m_lbLevelName.text = name;
				
				if( sdLevelInfo.levelInfos[i].valid )
				{
					m_bButtonValid = true;
					if(m_objLockIcon!=null) m_objLockIcon.SetActive(false);
					
					// setup red line
					if( m_objRedLine != null )
					{
						UISprite sp = m_objRedLine.GetComponentInChildren<UISprite>();
						if( sp != null )
						{
							if( gameObject.name=="btn_level6" )		 { if(sp.fillAmount<1.0f) sp.fillAmount=1.0f; }
							else if( gameObject.name=="btn_level5" ) { if(sp.fillAmount<0.8f) sp.fillAmount=0.8f; }
							else if( gameObject.name=="btn_level4" ) { if(sp.fillAmount<0.6f) sp.fillAmount=0.6f; }
							else if( gameObject.name=="btn_level3" ) { if(sp.fillAmount<0.4f) sp.fillAmount=0.4f; }
                            else if( gameObject.name=="btn_level2" ) { if(sp.fillAmount<0.2f) sp.fillAmount=0.2f; }
						}
						
						if( sdLevelInfo.levelInfos[i].crystal == 0 )
						{
							m_objNewIcon.transform.parent = gameObject.transform;
							m_objNewIcon.transform.localPosition = new Vector3(-80.0f,40.0f,0);
							m_objNewIcon.transform.localScale = Vector3.one;
						}
					}
					
					// setup star
					m_objStar1.gameObject.SetActive(true);
					if( sdLevelInfo.levelInfos[i].crystal <= 0 ) 
					{
						m_objStar1.spriteName = "star0";
						m_objStar2.spriteName = "star0";
						m_objStar3.spriteName = "star0";
					}
					else if( sdLevelInfo.levelInfos[i].crystal == 1 ) 
					{
						m_objStar1.spriteName = "star1";
						m_objStar2.spriteName = "star0";
						m_objStar3.spriteName = "star0";
					}
					else if( sdLevelInfo.levelInfos[i].crystal == 2 ) 
					{
						m_objStar1.spriteName = "star1";
						m_objStar2.spriteName = "star1";
						m_objStar3.spriteName = "star0";
					}
					else
					{
						m_objStar1.spriteName = "star1";
						m_objStar2.spriteName = "star1";
						m_objStar3.spriteName = "star1";
					}
				}
				else
				{
					if(m_objLockIcon!=null) m_objLockIcon.SetActive(true);
					m_objStar1.gameObject.SetActive(false);
					m_bButtonValid = false;
				}
			}
		}
	}
	
	// Use this for initialization
	void Start () 
	{
        //UpdateLevelButton();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnClick()
	{
		if(!m_bButtonValid) 
		{
			sdUICharacter.Instance.ShowMsgLine("此关卡还未解锁。",MSGCOLOR.Yellow);
			return;
		}
		
		// 检查背包格子数.
        if (sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, -1).Count >= sdGameItemMgr.Instance.maxBagNum)
		{
			sdUICharacter.Instance.ShowOkMsg("您的背包满了，不能进入战斗。",null);
			return;
		}

		// 检查宠物数.
		if( sdNewPetMgr.Instance.GetMyPetCount() > sdNewPetMgr.Instance.GetMyPetMaxCount() )
		{
			sdUICharacter.Instance.ShowOkMsg("您的宠物过多，不能进入战斗。",null);
			return;
		}
		
		sdUICharacter.Instance.ShowLevelPrepare( GetLevelID(), gameObject.transform.localPosition.x, gameObject.transform.localPosition.y );
	}
}
