using UnityEngine;
using System.Collections;

/// <summary>
/// 角色列表按钮(执行脚本)
/// </summary>
public class RoleSelButton : MonoBehaviour 
{
	public GameObject m_SelectCharWnd;	//< 角色选择窗口对象aa
	protected SelectChar m_SelectChar;	//< 角色选择窗口脚本对象aa
	
	public int			m_RoleID;		//< 角色在角色列表中的IDaa
	public UILabel		m_RoleName;		//< 角色名称UIaa
	public UILabel		m_RoleLevel;	//< 角色等级UIaa
	public UILabel		m_RoleRace;		//< 角色职业UIaa
	public UISprite		m_RolePic;		//< 角色头像UIaa
	public UISprite		m_RoleCreate;	//< 角色创建UIaa
	public UISprite		m_RoleSelect;	//< 角色选中高亮UIaa
	
	public SelectRole 	m_SelectRole;	//< 进入游戏按钮脚本对象aa
	
	protected bool m_isEmpty = true;
	
	protected int mGender = 0;		//< 图标对应的角色性别aa
	protected int mHairStyle = 0;	//< 图标对应的头发样式aa
	protected int mHairColor = 0;	//< 图标对应的头发颜色aa
	
	protected bool m_bSelected = false;	// 当前此项是否被选中
	
	
	void Awake()
	{
		if (m_SelectCharWnd)
			m_SelectChar = m_SelectCharWnd.GetComponent<SelectChar>();
		
		InitRole();
	}
	
	// 初始化按钮信息aa
	public void InitRole()
	{
		if(m_RoleSelect && m_bSelected==false)
			m_RoleSelect.gameObject.SetActive(false);
		
		if( m_RoleID<0 || m_RoleID>3 || SDNetGlobal.playerList[m_RoleID]==null )
		{
			m_isEmpty = true;
			if(m_RoleName)		m_RoleName.gameObject.SetActive(false);
			if(m_RoleLevel)		m_RoleLevel.gameObject.SetActive(false);
			if(m_RoleRace)		m_RoleRace.gameObject.SetActive(false);
			if(m_RolePic)		m_RolePic.gameObject.SetActive(false);
			if(m_RoleCreate)	m_RoleCreate.gameObject.SetActive(true);
			return;
		}
		
		m_isEmpty = false;
		if(m_RoleCreate)
			m_RoleCreate.gameObject.SetActive(false);
		if(m_RoleName)
		{
			m_RoleName.gameObject.SetActive(true);
			m_RoleName.text = SDNetGlobal.playerList[m_RoleID].mRoleName;
		}
		if(m_RoleLevel)
		{
			m_RoleLevel.gameObject.SetActive(true);
			m_RoleLevel.text = "Lv. " + SDNetGlobal.playerList[m_RoleID].mLevel;
		}
		
		// 职业aa
		if(m_RoleRace)
		{
			m_RoleRace.gameObject.SetActive(true);
			if(SDNetGlobal.playerList[m_RoleID].mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior)
				m_RoleRace.text = "战士";
			else if(SDNetGlobal.playerList[m_RoleID].mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue)
				m_RoleRace.text = "游侠";
			else if(SDNetGlobal.playerList[m_RoleID].mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Minister)
				m_RoleRace.text = "牧师";
			else if(SDNetGlobal.playerList[m_RoleID].mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Magic)
				m_RoleRace.text = "法师";
		}
		
		// 头像aa
		if (m_RolePic)	
		{
			m_RolePic.gameObject.SetActive(true);
			m_RolePic.spriteName = "";
			
			sdPlayerInfo kPlayInfo = SDNetGlobal.playerList[m_RoleID];
			if (kPlayInfo != null)
			{
				mGender = kPlayInfo.mGender;
				mHairStyle = kPlayInfo.mHairStyle;
				mHairColor = kPlayInfo.mSkinColor;
				
				LoadAvatarHeadAtlas(kPlayInfo.mGender, kPlayInfo.mHairStyle, kPlayInfo.mSkinColor);
			}
		}
	}
	
	// 显示未选中效果aa
	public void unSelect()
	{
		m_bSelected = false;
		if(m_RoleName)
			m_RoleName.color = new Color(255.0f/255.0f, 255.0f/255.0f, 213.0f/255.0f);
		if(m_RoleSelect)	
			m_RoleSelect.gameObject.SetActive(false);
	}
	
	// 显示选中效果aa
	public void doSelect()
	{
		m_bSelected = true;
			
		if(m_RoleName)
		{
			m_RoleName.color = new Color(1.0f,1.0f,0.3f,1.0f);
		}
		
		if(m_RoleSelect)	
			m_RoleSelect.gameObject.SetActive(true);
	}
	
	void OnClick()
	{
		// 当前已经选中此项目了，且正在播放SHOW动作...
		if( m_bSelected && m_SelectChar.mLoginChar!=null && m_SelectChar.mLoginChar.IsShowAnimPlaying() )
			return;
		
		if( m_isEmpty )
		{
			if(m_SelectRole)
				m_SelectRole.CreateChar();			//< 进入角色创建界面aa
		}
		else
		{
			// 如果别的条目正在播放SHOW动作，则停止之前项的特效..
			if( m_SelectChar.mLoginChar!=null && m_SelectChar.mLoginChar.IsShowAnimPlaying() ) 
			{
				GameObject EffectNode = sdGameLevel.instance.effectNode;
				if( EffectNode != null )
				{
					sdAutoDestory[] effects = EffectNode.GetComponentsInChildren<sdAutoDestory>();
					foreach(sdAutoDestory e in effects)
					{
						e.Life = 0;
					}
				}
			}
			
			doSelect();								//< 显示选中效果aa
			if(m_SelectRole)
				m_SelectRole.doSelect(m_RoleID);	//< 选中角色aa
		}
	}
	
	// 加载主角头像图集aa
	protected void LoadAvatarHeadAtlas(int iGender, int iHairStyle, int iHairColor)
	{	
		int iHairStyleIndex = 0;
		if (iGender == 1)
		{
			iHairStyleIndex = iHairStyle+1;		//< HairStyle: 0,1,2,3,4,5,6,7
												//< HairIndex: 1,2,3,4,5,6,7,8
		}
		else
		{
			iHairStyleIndex = 8 - iHairStyle;	//< HairStyle: 0,1,2,3,4,5,6,7
												//< HairIndex: 8,7,6,5,4,3,2,1		
		}
		
		string kAssertPath = string.Format("UI/CharHair/$mhair{0}/mhair{0}_prefab.prefab", iHairStyleIndex); 
		if (iGender == 1)
			kAssertPath = string.Format("UI/CharHair/$whair{0}/whair{0}_prefab.prefab", iHairStyleIndex); 


		ResLoadParams kParam = new ResLoadParams();
		kParam.userdata0 = iGender;
		kParam.userdata1 = iHairStyle;
		kParam.userdata2 = iHairColor;
		sdResourceMgr.Instance.LoadResource(
			kAssertPath, 
			NotifyAvatarHeadAtlas, 
			kParam,
			typeof(UIAtlas));
	}

	// 加载主角头像图集回调aa
	protected void NotifyAvatarHeadAtlas(ResLoadParams kParam, UnityEngine.Object kObj)
	{		
		if (kObj == null)
			return;
		
		UIAtlas kAtlas = kObj as UIAtlas;
		
		int iGender = (int)kParam.userdata0;
		int iHairStyle = (int)kParam.userdata1;
		int iHairColor = (int)kParam.userdata2;
		
		int iHairStyleIndex = 0;
		if (iGender == 1)
		{
			iHairStyleIndex = iHairStyle+1;		//< HairStyle: 0,1,2,3,4,5,6,7
												//< HairIndex: 1,2,3,4,5,6,7,8
		}
		else
		{
			iHairStyleIndex = 8 - iHairStyle;	//< HairStyle: 0,1,2,3,4,5,6,7
												//< HairIndex: 8,7,6,5,4,3,2,1		
		}
		
		int iHairColorIndex = iHairColor+1;	//< 图标索引:
		string kSpirteName = string.Format("{0}-{1}", iHairStyleIndex, iHairColorIndex);
		if (m_RolePic && (mGender == iGender) && (mHairStyle == iHairStyle) && (mHairColor == iHairColor))	
		{
			m_RolePic.atlas = kAtlas;
			m_RolePic.spriteName = kSpirteName;	
		}
	}
}
