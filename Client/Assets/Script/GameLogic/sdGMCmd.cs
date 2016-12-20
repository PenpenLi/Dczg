using UnityEngine;
using System.Collections;

public class sdGMCmd : MonoBehaviour 
{
	protected static bool	m_bShowFps		= false;
	protected float			m_fFps			= 0;
	protected float			m_fFpsTime		= 0;
	protected int			m_iFpsCount		= 0;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( m_bShowFps )
		{
			m_fFpsTime += Time.deltaTime;
			m_iFpsCount++;
			if( m_fFpsTime >= 1.0f )
			{
				m_fFps = (float)m_iFpsCount / m_fFpsTime;
				m_fFpsTime = 0;
				m_iFpsCount = 0;
			}
		}
	}
	
	void OnGUI()
	{
		if( m_bShowFps )
		{
			GUI.Label(new Rect(0,0,100,20),m_fFps.ToString("0"));
		}
	}
	
	public static bool GMCommand(string cmd)
	{
		if( cmd.Length < 5 ) return false;
		if( cmd[0] != '#' ) return false;

		bool reload = false;
		string[] cmds = cmd.Split(new char[] {' '});
		if( cmds[0] == "#fps" )
		{
			if( cmds[1] == "1" ) m_bShowFps = true; else m_bShowFps = false; 
		}
		else if( cmds[0] == "#screen" )
		{
			Screen.SetResolution(int.Parse(cmds[1]),int.Parse(cmds[2]),true);	// 分辨率..
		}
		else if( cmds[0] == "#aa" )
		{
			QualitySettings.antiAliasing = int.Parse(cmds[1]);					// 抗锯齿 0 2 4 8
			reload = true;
		}
		else if( cmds[0] == "#tex" )
		{
			QualitySettings.masterTextureLimit = int.Parse(cmds[1]);			// 贴图尺寸 0=Full 1=Half
			reload = true;
		}
		else if( cmds[0] == "#frame" )
		{
			Application.targetFrameRate = int.Parse(cmds[1]);					// 帧速限制 -1 为无限制.
		}
		else if( cmds[0] == "#maincity" )										// 开启主城.
		{
			int LevelID = 21011;
			for(int i=0;i<sdLevelInfo.levelInfos.Length;i++)
			{
				if( sdLevelInfo.levelInfos[i].levelID == LevelID )
				{
					sdLevelInfo.levelInfos[i].valid = true;						
					break;							
				}
			}
		}
		else if( cmds[0] == "#battle" )											// 测试开启战役.
		{
			sdWorldMapPath.SetLevel(int.Parse(cmds[1]),true);
		}
		else if( cmds[0] == "#sys" )											// 解锁系统.
		{
			sdConfDataMgr.Instance().SetSetting(cmds[1],"1");
			GameObject.Find("$TownUi").GetComponent<sdTown>().mCheckSystemLock = true;
		}
		else if( cmds[0] == "#info" )											// 解锁系统.
		{
			string info = 
				"w:"+Screen.width+" h:"+Screen.height+" dpi:"+Screen.dpi+
				" TL:"+QualitySettings.masterTextureLimit+" FR:"+Application.targetFrameRate+
				" Device:"+sdConfDataMgr.Instance().GetSetting("DeviceMode");
			sdUICharacter.Instance.ShowOkMsg(info,null);
		}

		// 重置显示质量控制.
		if( reload )
		{
			int i = QualitySettings.GetQualityLevel();
			QualitySettings.SetQualityLevel(i,true);
		}

		return true;
	}
}
