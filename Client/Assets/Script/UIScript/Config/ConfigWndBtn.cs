using UnityEngine;
using System.Collections;

public class ConfigWndBtn : MonoBehaviour 
{
	static private UISlider mSlider = null;
	static private bool mGraphicReload = false;
	static public bool mTown = false;

	static private UISprite 	mGraphic1	= null;
	static private UISprite 	mGraphic2	= null;
	static private UISprite 	mGraphic3	= null;
	static private UISprite 	mControl1	= null;
	static private UISprite 	mControl2	= null;
	static private UISprite 	mCamera1	= null;
	static private UISprite 	mCamera2	= null;
	static private GameObject 	mConfigTab	= null;


	// Use this for initialization
	void Start () 
	{
		// Back
		if( gameObject.name == "btn_back" )
		{
			UISprite sp = gameObject.transform.FindChild("Background").FindChild("Sprite").GetComponent<UISprite>();
			if( mTown )
				sp.spriteName = "fhzh";
			else
				sp.spriteName = "fhsj";

			if( mConfigTab == null )
			{
				mConfigTab = GameObject.Find("ConfigTab");
			}
		}

		// Sound
		if( gameObject.name == "sl_volume" )
		{
			mSlider = GetComponent<UISlider>();
			EventDelegate.Add(mSlider.onChange, OnChange);
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Volume");
			if( s == "" ) s = "1.0";
			mSlider.value = float.Parse(s);
		}
		if( gameObject.name == "tg_mute" )
		{
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Mute");
			if( s == "" ) s = "0";
			if( s == "1" )
			{
				GetComponentInChildren<UISprite>().spriteName = "mute1";
				if( mSlider != null ) mSlider.enabled = false;
			}
			else
			{
				GetComponentInChildren<UISprite>().spriteName = "mute0";
			}
		}

		// Graphic
		if( gameObject.name == "tg_g_low" )
		{
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Graphic");
			mGraphic1 = gameObject.GetComponentInChildren<UISprite>();
			if( s == "0" )
				mGraphic1.spriteName = "btn1";
			else
				mGraphic1.spriteName = "btn1dis";
		}
		else if( gameObject.name == "tg_g_mid" )
		{
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Graphic");
			if( s == "" ) 
			{ 
				s = "1"; 
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Graphic","1"); 
			}
			mGraphic2 = gameObject.GetComponentInChildren<UISprite>();
			if( s == "1" )
				mGraphic2.spriteName = "btn1";
			else
				mGraphic2.spriteName = "btn1dis";
		}
		else if( gameObject.name == "tg_g_high" )
		{
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Graphic");
			mGraphic3 = gameObject.GetComponentInChildren<UISprite>();
			if( s == "2" )
				mGraphic3.spriteName = "btn1";
			else
				mGraphic3.spriteName = "btn1dis";
		}

		// Control
		if( gameObject.name == "tg_move1" )
		{
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Move");
			mControl1 = gameObject.GetComponentInChildren<UISprite>();
			if( s != "1" )
				mControl1.spriteName = "btn1";
			else
				mControl1.spriteName = "btn1dis";
		}
		else if( gameObject.name == "tg_move2" )
		{
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Move");
			mControl2 = gameObject.GetComponentInChildren<UISprite>();
			if( s == "1" )
				mControl2.spriteName = "btn1";
			else
				mControl2.spriteName = "btn1dis";
		}

		// Angle
		if( gameObject.name == "tg_camera1" )
		{
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Camera");
			mCamera1 = gameObject.GetComponentInChildren<UISprite>();
			if( s != "1" )
				mCamera1.spriteName = "btn1";
			else
				mCamera1.spriteName = "btn1dis";
		}
		else if( gameObject.name == "tg_camera2" )
		{
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Camera");
			mCamera2 = gameObject.GetComponentInChildren<UISprite>();
			if( s == "1" )
				mCamera2.spriteName = "btn1";
			else
				mCamera2.spriteName = "btn1dis";
		}
	}

	public static void UpdateAngleAndControl()
	{
		if( mControl1 == null ) return;

		string s = sdConfDataMgr.Instance().GetSetting("CFG_Move");
		if( s != "1" )
		{
			mControl1.spriteName = "btn1";
			mControl2.spriteName = "btn1dis";
		}
		else
		{
			mControl1.spriteName = "btn1dis";
			mControl2.spriteName = "btn1";
		}

		s = sdConfDataMgr.Instance().GetSetting("CFG_Camera");
		if( s != "1" )
		{
			mCamera1.spriteName = "btn1";
			mCamera2.spriteName = "btn1dis";
		}
		else
		{
			mCamera1.spriteName = "btn1dis";
			mCamera2.spriteName = "btn1";
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnClick()
	{
		if( gameObject.name == "btn_close" )
		{
			Time.timeScale = 1.0f;
			mConfigTab.GetComponent<UIDraggablePanel>().ResetPosition();
			sdUICharacter.Instance.HideConfigWnd();
			sdConfDataMgr.Instance().SetSetting("","");
			if( mGraphicReload ) 
			{
				sdConfDataMgr.Instance().ApplyGraphicConfig(false);
				mGraphicReload = false;
			}
		}
		else if( gameObject.name == "btn_ok" )
		{
            Time.timeScale = 1.0f;
			mConfigTab.GetComponent<UIDraggablePanel>().ResetPosition();
			sdUICharacter.Instance.HideConfigWnd();
			sdConfDataMgr.Instance().SetSetting("","");
			if( mGraphicReload ) 
			{
				sdConfDataMgr.Instance().ApplyGraphicConfig(false);
				mGraphicReload = false;
			}
		}
		else if( gameObject.name == "btn_closeF" || gameObject.name == "btn_okF" )		// 战斗config
		{
			Time.timeScale = 1.0f;
			sdUICharacter.Instance.HideConfigWnd();
			sdConfDataMgr.Instance().SetSetting("","");
		}
		else if( gameObject.name == "btn_ConfigTab" )
		{
		}
		else if( gameObject.name == "btn_AccountTab" )
		{
			sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"),MSGCOLOR.Yellow);
		}
		else if( gameObject.name == "btn_AboutTab" )
		{
			sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"),MSGCOLOR.Yellow);
		}
		else if( gameObject.name == "tg_mute" )
		{
			UISprite sp = GetComponentInChildren<UISprite>();
			if( sp.spriteName == "mute0" )
			{
				sp.spriteName = "mute1";
				AudioListener.pause = true;
				mSlider.enabled = false;
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Mute","1");
			}
			else
			{
				sp.spriteName = "mute0";
				AudioListener.pause = false;
				mSlider.enabled = true;
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Mute","0");
			}
		}
		else if( gameObject.name == "tg_g_low" )
		{
			if( sdConfDataMgr.Instance().GetSetting("CFG_Graphic") != "0" )
			{
				mGraphic1.spriteName = "btn1";
				mGraphic2.spriteName = "btn1dis";
				mGraphic3.spriteName = "btn1dis";
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Graphic","0");
				mGraphicReload = true;
			}
		}
		else if( gameObject.name == "tg_g_mid" )
		{
			if( sdConfDataMgr.Instance().GetSetting("CFG_Graphic") != "1" )
			{
				mGraphic1.spriteName = "btn1dis";
				mGraphic2.spriteName = "btn1";
				mGraphic3.spriteName = "btn1dis";
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Graphic","1");
				mGraphicReload = true;
			}
		}
		else if( gameObject.name == "tg_g_high" )
		{
			if( sdConfDataMgr.Instance().GetSetting("CFG_Graphic") != "2" )
			{
				mGraphic1.spriteName = "btn1dis";
				mGraphic2.spriteName = "btn1dis";
				mGraphic3.spriteName = "btn1";
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Graphic","2");
				mGraphicReload = true;
			}
		}
		else if( gameObject.name == "tg_move1" )
		{
			if( sdConfDataMgr.Instance().GetSetting("CFG_Move") == "1" )
			{
				mControl1.spriteName = "btn1";
				mControl2.spriteName = "btn1dis";
				GameObject.Find("@GameLevel").GetComponent<sdGameLevel>().AutoMode = true;
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Move","0");

				GameObject btn = GameObject.Find("btn_controlmode");
				if( btn != null ) 
					btn.GetComponentInChildren<UISprite>().spriteName = "cz1";
			}
		}
		else if( gameObject.name == "tg_move2" )
		{
			if( sdConfDataMgr.Instance().GetSetting("CFG_Move") != "1" )
			{
				mControl1.spriteName = "btn1dis";
				mControl2.spriteName = "btn1";
				GameObject.Find("@GameLevel").GetComponent<sdGameLevel>().AutoMode = false;
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Move","1");

				GameObject btn = GameObject.Find("btn_controlmode");
				if( btn != null ) 
					btn.GetComponentInChildren<UISprite>().spriteName = "cz2";
			}
		}
		else if( gameObject.name == "tg_camera1" )
		{
			if( sdConfDataMgr.Instance().GetSetting("CFG_Camera") == "1" )
			{
				mCamera1.spriteName = "btn1";
				mCamera2.spriteName = "btn1dis";
				if( sdGameLevel.instance.levelType==sdGameLevel.LevelType.MainCity || sdGameLevel.instance.levelType==sdGameLevel.LevelType.Fight || sdGameLevel.instance.levelType==sdGameLevel.LevelType.None )
					sdGameLevel.instance.mainCamera.ChangeCameraDistance();
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Camera","0");

				GameObject btn = GameObject.Find("btn_anglemode");
				if( btn != null ) 
					btn.GetComponentInChildren<UISprite>().spriteName = "sj1";
			}
		}
		else if( gameObject.name == "tg_camera2" )
		{
			if( sdConfDataMgr.Instance().GetSetting("CFG_Camera") != "1" )
			{
				mCamera1.spriteName = "btn1dis";
				mCamera2.spriteName = "btn1";
				if( sdGameLevel.instance.levelType==sdGameLevel.LevelType.MainCity || sdGameLevel.instance.levelType==sdGameLevel.LevelType.Fight || sdGameLevel.instance.levelType==sdGameLevel.LevelType.None )
					sdGameLevel.instance.mainCamera.ChangeCameraDistance();
				sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Camera","1");

				GameObject btn = GameObject.Find("btn_anglemode");
				if( btn != null ) 
					btn.GetComponentInChildren<UISprite>().spriteName = "sj2";
			}
		}
		else if( gameObject.name == "btn_quit" )
		{
			sdMsgBox.OnConfirm quit = new sdMsgBox.OnConfirm(OnQuitGame);
			sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("QuitGameConfirm"), quit, null);
		}
		else if( gameObject.name == "btn_back" )
		{
            Time.timeScale = 1.0f;
			sdConfDataMgr.Instance().SetSetting("","");
			if( mTown )
			{
				sdMsgBox.OnConfirm relogin = new sdMsgBox.OnConfirm(OnBackChar);
				sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("ReturnCharComfirm"), relogin,null);
			}
			else
			{
				sdMsgBox.OnConfirm town = new sdMsgBox.OnConfirm(OnBackTown);
				sdUICharacter.Instance.ShowOkCanelMsg( sdConfDataMgr.Instance().GetShowStr("BackTown") ,town,null);
			}
		}
		else if( gameObject.name == "btn_default" )
		{
			sdMsgBox.OnConfirm btn_default = new sdMsgBox.OnConfirm(OnDefault);
			sdUICharacter.Instance.ShowOkCanelMsg("确定要恢复默认设置吗？",btn_default,null);
		}
		else if( gameObject.name == "tg_head" )
		{
			sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"),MSGCOLOR.Yellow);
		}
		else if( gameObject.name == "tg_username" )
		{
			sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"),MSGCOLOR.Yellow);
		}
		else if( gameObject.name == "tg_spirit" )
		{
			sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"),MSGCOLOR.Yellow);
		}
		else if( gameObject.name == "tg_boss" )
		{
			sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"),MSGCOLOR.Yellow);
		}
		else if( gameObject.name == "tg_tianguai" )
		{
			sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"),MSGCOLOR.Yellow);
		}
		else if( gameObject.name == "tg_pvp" )
		{
			sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"),MSGCOLOR.Yellow);
		}
	}

	void OnChange()
	{
		if( gameObject.name == "sl_volume" )
		{
			AudioListener.volume = mSlider.value;
			sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Volume",mSlider.value.ToString());
		}
	}

	void OnQuitGame()
	{
		Application.Quit();
	}

	void OnBackChar()
	{
		CliProto.GSPKG_LEAVEGAME_REQ req = new CliProto.GSPKG_LEAVEGAME_REQ();
		req.m_Type = CliProto.GS_LOGOUT_QUIT;
		SDNetGlobal.SendMessage(req);
	}

	void OnBackTown()
	{
        if(sdGameLevel.instance.levelType == sdGameLevel.LevelType.PVP)
        {
            sdPVPManager.Instance.KillMe(null);
            sdUICharacter.Instance.HideConfigWnd(true);
        }
		else if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.WorldBoss
		         ||sdGameLevel.instance.levelType == sdGameLevel.LevelType.LapBoss)
		{
			//退出世界Boss场景..
			sdActGameMsg.Send_CS_WB_RESULT_REQ(1);
			sdUICharacter.Instance.HideConfigWnd(true);
			sdUICharacter.Instance.TuiTu_To_WorldMap();
		}
        else
        {
            CliProto.CS_LEVEL_RESULT_NTF refMSG = new CliProto.CS_LEVEL_RESULT_NTF();
            refMSG.m_Result = 1; // 主动放弃当前关卡.
            SDNetGlobal.SendMessage(refMSG);


            sdUICharacter.Instance.HideConfigWnd(true);
            sdUICharacter.Instance.TuiTu_To_WorldMap();	    
        }
        
	}

	void OnDefault()
	{
		GameObject.Find("sl_volume").GetComponent<UISlider>().value = 1f;
		GameObject.Find("sl_volume").GetComponent<UISlider>().enabled = true;
		AudioListener.volume = 1f;
		sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Volume","1.0");

		GameObject.Find("tg_mute").GetComponentInChildren<UISprite>().spriteName = "mute0";
		AudioListener.pause = false;
		sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Mute","0");

		GameObject.Find("tg_move1").GetComponentInChildren<UISprite>().spriteName = "btn1";
		GameObject.Find("tg_move2").GetComponentInChildren<UISprite>().spriteName = "btn1dis";
		GameObject.Find("@GameLevel").GetComponent<sdGameLevel>().AutoMode = true;
		sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Move","0");

		if( GameObject.Find("tg_camera1").GetComponentInChildren<UISprite>().spriteName == "btn1dis" )
		{
			GameObject.Find("tg_camera1").GetComponentInChildren<UISprite>().spriteName = "btn1";
			GameObject.Find("tg_camera2").GetComponentInChildren<UISprite>().spriteName = "btn1dis";
			sdGameLevel.instance.mainCamera.ChangeCameraDistance();
		}
		sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Camera","0");

		sdConfDataMgr.Instance().SetSetting("CFG_Graphic","1");
		if( GameObject.Find("tg_g_mid").GetComponentInChildren<UISprite>().spriteName == "btn1dis" )
		{
			GameObject.Find("tg_g_low").GetComponentInChildren<UISprite>().spriteName = "btn1dis";
			GameObject.Find("tg_g_mid").GetComponentInChildren<UISprite>().spriteName = "btn1";
			GameObject.Find("tg_g_high").GetComponentInChildren<UISprite>().spriteName = "btn1dis";
			sdConfDataMgr.Instance().ApplyGraphicConfig(false);
		}
	}
}
