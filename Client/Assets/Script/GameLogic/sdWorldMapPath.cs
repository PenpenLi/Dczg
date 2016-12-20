using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdWorldMapPath : MonoBehaviour 
{
	public GameObject	MainCityStart	= null;
	public GameObject	MainCityEnd		= null;
	public GameObject	MainCityAnim	= null;
	public GameObject	MainCityEffect1	= null;
	public GameObject	MainCityEffect2	= null;
	public GameObject	FxDianji		= null;

	public GameObject[] LevelStar		= new GameObject[16*3];	// 关卡星星..

    static public List<EventDelegate> onFinish = new List<EventDelegate>();

	static float		fShowTime0		= 0.0f;		// 等待场景资源载入时间.
	static float		fShowTime1		= 1.0f;		// 镜头推近时间.
	static float		fShowTime2		= 2.0f;		// 道路连接动画时间.
	static float		fShowTime3		= 7.5f;		// 城堡升起时间.
	static float		fShowTime4		= 1.5f;		// 名字渐变时间.
	static float		fShowTime5		= 1.0f;		// 镜头拉远时间。
		
	static bool			bInited			= false;
	static int 			mapShowStep		= -1;
	static float 		fCurrent		= 1000.0f;
	static Renderer[] 	roadArray		= null;
	static GameObject 	levelText		= null;
	static int 			currentIndex	= -1;
	static AudioClip	AudioClipWin	= null;
	static AudioClip	AudioClipMC		= null;
	static AudioClip	AudioClipOld	= null;
	static public GameObject	TownUI	= null;
	static Vector3		CameraPos0;
	static Vector3		CameraPos1;

	int mLevel = 0;
	
	
	void Start () 
	{
		bInited = false;
	}
	
	
	void Update () 
	{
		if( bInited == false )
		{
			Hashtable data = sdGlobalDatabase.Instance.globalData;
			if( data.ContainsKey("OpenLevel_Index") )
			{
				bInited = true;
				int	 level	= (int)data["OpenLevel_Index"];
				bool first	= (int)data["OpenLevel_FirstTime"] == 1;
				data["OpenLevel_FirstTime"] = 0;
				SetLevelState(level,first);
			}
		}
		
		if( mapShowStep >= 0 )
		{
			if( mapShowStep == 0 )
			{
				fCurrent += Time.deltaTime;
				if( fCurrent > fShowTime0 )
				{
					fCurrent = 0;
					mapShowStep = 1;
				}
				if( FxDianji.activeSelf && FxDianji.transform.localPosition.x<300.0f )
				{
					Vector3 v = FxDianji.transform.localPosition;
					FxDianji.transform.localPosition = new Vector3(v.x+1000.0f,v.y,v.z);
				}
			}
			else if( mapShowStep == 1 )
			{
				// 镜头推近.
				fCurrent += Time.deltaTime;
				if( fCurrent <= fShowTime1 )
				{
					float f	= (fCurrent*fCurrent) / (fShowTime1*fShowTime1);
					float x = CameraPos0.x + (CameraPos1.x-CameraPos0.x)*f;
					float y = CameraPos0.y + (CameraPos1.y-CameraPos0.y)*f;
					float z = CameraPos0.z + (CameraPos1.z-CameraPos0.z)*f;
					sdGameLevel.instance.mainCamera.transform.localPosition = new Vector3(x,y,z);
				}
				else
				{
					sdGameLevel.instance.mainCamera.transform.localPosition = CameraPos1;
					fCurrent = 0;
					mapShowStep = 2;
					currentIndex = -1;
				}
			}
			else if( mapShowStep == 2 )
			{
				// 道路动画.
				fCurrent += Time.deltaTime;
				if( fCurrent <= fShowTime2 )
				{
					if( roadArray == null )
					{
						fCurrent = 1000.0f;
					}
					else
					{
						float f	= (fCurrent/fShowTime2) * (float)roadArray.Length;
						int ShowCount =	(int)f;
						if( ShowCount != currentIndex )
						{
							currentIndex = ShowCount;
							roadArray[currentIndex].enabled = true;
						}
					}
				}	
				else
				{
					int level = (int)sdGlobalDatabase.Instance.globalData["OpenLevel_Index"];
					if( level == 20 )	// 判断是否是城堡
					{
						mapShowStep = 3;
						if( MainCityStart	) MainCityStart.SetActive(false);
						if( MainCityAnim	) MainCityAnim.SetActive(true);
					}
					else
						mapShowStep = 4;
					fCurrent = 0;
				}
			}
			else if( mapShowStep == 3 )
			{
				// 城堡升起动画.
				fCurrent += Time.deltaTime;
				if( fCurrent <= fShowTime3 )
				{
					//float f	= fCurrent / fShowTime3;
				}
				else
				{
					if( MainCityStart	) MainCityStart.SetActive(false);
					if( MainCityEnd		) MainCityEnd.SetActive(true);
					if( MainCityAnim	) MainCityAnim.SetActive(false);
					if( MainCityEffect1 ) MainCityEffect1.SetActive(true);
					if( MainCityEffect2 ) MainCityEffect2.SetActive(true);
					fCurrent = 0;
					mapShowStep = 4;
				}
			}
			else if( mapShowStep == 4 )
			{
				// 关卡名称显示.
				fCurrent += Time.deltaTime;
				if( fCurrent <= fShowTime4 )
				{
					float f	= fCurrent / fShowTime4;
					if( levelText.activeSelf == false ) levelText.SetActive(true);
					levelText.GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f,f);
				}
				else
				{
					levelText.GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f,1.0f);
					fCurrent = 0;
					mapShowStep = 5;
				}
			}
			else if( mapShowStep == 5 )
			{
				// 镜头拉回原位.
				fCurrent += Time.deltaTime;
				if( fCurrent <= fShowTime5 )
				{
					float f	= (fCurrent*fCurrent) / (fShowTime5*fShowTime5);
					float x = CameraPos1.x + (CameraPos0.x-CameraPos1.x)*f;
					float y = CameraPos1.y + (CameraPos0.y-CameraPos1.y)*f;
					float z = CameraPos1.z + (CameraPos0.z-CameraPos1.z)*f;
					sdGameLevel.instance.mainCamera.transform.localPosition = new Vector3(x,y,z);
				}
				else
				{
					sdGameLevel.instance.mainCamera.transform.localPosition = CameraPos0;
					sdGameLevel.instance.WordMapCameraPos = CameraPos0;
					mapShowStep = 6;

					if( FxDianji.activeSelf && FxDianji.transform.localPosition.x>300.0f )
					{
						Vector3 v = FxDianji.transform.localPosition;
						FxDianji.transform.localPosition = new Vector3(v.x-1000.0f,v.y,v.z);
					}
				}
			}
			else if(mapShowStep == 6)
			{
				string strEnter = sdConfDataMgr.Instance().GetSetting(mLevel.ToString() + "_firstenter");
				if(strEnter.Length == 0 && 
					(Application.platform == RuntimePlatform.Android || 
					Application.platform == RuntimePlatform.IPhonePlayer))
				{
					GameObject mainCamera = GameObject.Find("@MainCamera");
					if(mainCamera != null)
					{
						sdMovieVideo movie = mainCamera.AddComponent<sdMovieVideo>();
						if(movie != null)
						{
							Hashtable table = sdConfDataMgr.Instance().GetTable("moviebattle");
							if(table != null)
							{
								if(table.ContainsKey(mLevel.ToString()))
								{
									mapShowStep = -1;
									AudioSource audio = sdGameLevel.instance.gameObject.GetComponent<AudioSource>();
									if( audio != null ) 
										audio.Stop();									
									Hashtable data = table[mLevel.ToString()] as Hashtable;
									movie.PlayMovie((string)data["filename"]);
								}
							}
						}
					}
					sdConfDataMgr.Instance().SetSetting(mLevel.ToString() + "_firstenter", "0");		
				}
				mapShow();
			}
		}
	}
	
	void mapShow()
	{
		mapShowStep = -1;				
		AudioSource audio = sdGameLevel.instance.gameObject.GetComponent<AudioSource>();
		if( audio != null ) 
		{
			audio.Stop();
			audio.clip = AudioClipOld;
			audio.Play();
		}					
		if( TownUI != null && TownUI.activeSelf == false) 
		{
			TownUI.SetActive(true);

            sdTown town = TownUI.GetComponent<sdTown>();
            if (town != null)
            {
                town.CheckSystemLock();
            }
		}

        if (onFinish.Count > 0)
        {
            EventDelegate.Execute(onFinish);
            onFinish.Clear();
        }
       
	}
	// 设置战役连接开启状态.
	void SetLevelState(int level,bool bFirst)
	{
		Hashtable table	= sdConfDataMgr.Instance().GetTable("worldmappath");
		
		// 大于当前开启战役的后续战役连接都关闭.
		foreach(DictionaryEntry de in table)
		{
			int index =	int.Parse(de.Key as string);
			if( index > level )
			{
				Hashtable t = de.Value as Hashtable;
				string  road	=	t["road"] as string;
				if(road.Length>0)
				{
					GameObject obj = GameObject.Find(road);
					if(obj!=null) obj.SetActive(false);
				}
				string  text	=	t["text"] as string;
				if(text.Length>0)
				{
					GameObject obj = GameObject.Find(text);
					if(obj!=null) obj.SetActive(false);
				}
			}
			else
			{
				int bid = sdLevelInfo.BattleInfoID(index);
				int stars = sdLevelInfo.battleInfos[bid].starCount;
				if( stars >= 54 )
					LevelStar[(bid-1)*3+2].SetActive(true);
				else if( stars >= 36 )
					LevelStar[(bid-1)*3+1].SetActive(true);
				else if( stars >= 18 )
					LevelStar[(bid-1)*3+0].SetActive(true);
			}
		}
		
		// 判断主城是否要显示完整状态.
		if( level>20 || (level==20 && !bFirst) )
		{
			if( MainCityStart	) MainCityStart.SetActive(false);
			if( MainCityEnd		) MainCityEnd.SetActive(true);
			if( MainCityAnim	) MainCityAnim.SetActive(false);
			if( MainCityEffect1 ) MainCityEffect1.SetActive(true);
			if( MainCityEffect2 ) MainCityEffect2.SetActive(true);
		}
		else
		{
			if( MainCityStart	) MainCityStart.SetActive(true);
			if( MainCityEnd		) MainCityEnd.SetActive(false);
			if( MainCityAnim	) MainCityAnim.SetActive(false);
			if( MainCityEffect1 ) MainCityEffect1.SetActive(false);
			if( MainCityEffect2 ) MainCityEffect2.SetActive(false);
		}
		
		mLevel = level;
		if(bFirst)
		{
			ActiveLevel(level);
		}

	}
		
	// 先关闭当前首次显示关卡的连接，在update中逐个打开.
	static void ActiveLevel(int level)
	{
		Hashtable table	=	sdConfDataMgr.Instance().GetTable("worldmappath");
		Hashtable t		=	table[level.ToString()] as Hashtable;
		
		string  road	=	t["road"] as string;
		if(road.Length>0)
		{
			GameObject obj = GameObject.Find(road);
			//obj.SetActive(false);
			roadArray =	obj.transform.GetComponentsInChildren<Renderer>();
			foreach(Renderer r in roadArray)
			{
				r.enabled	=	false;
			}
		}
		string  text	=	t["text"] as string;
		if(text.Length>0)
		{
			levelText = GameObject.Find(text);
			levelText.SetActive(false);
		}
		
		CameraPos0 = new Vector3(float.Parse((string)t["x0"]),float.Parse((string)t["y0"]),float.Parse((string)t["z0"]));
		CameraPos1 = new Vector3(float.Parse((string)t["x1"]),float.Parse((string)t["y1"]),float.Parse((string)t["z1"]));
		sdGameLevel.instance.mainCamera.transform.localPosition = CameraPos0;
		
		// 在update中显示连接过程.
		PlayWinAudio(level);
	}
	
	static void PlayWinAudio(int level)
	{
		bool b;
		if( level == 20 )
			b = AudioClipMC==null;
		else
			b = AudioClipWin==null;
		
		if( b )
		{
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = level;
			if( level == 20 )
				sdResourceMgr.Instance.LoadResource("Music/$win_auto_maincity.ogg",LoadAudio,param);
			else
				sdResourceMgr.Instance.LoadResource("Music/$win_auto_short.ogg",LoadAudio,param);
		}
		else
		{
			AudioSource audio = sdGameLevel.instance.gameObject.GetComponent<AudioSource>();
			if( audio != null )
			{
				audio.Stop();
				AudioClipOld = audio.clip;
				if( level == 20 )
					audio.clip = AudioClipMC;
				else
					audio.clip = AudioClipWin;
				audio.Play();
			}
			
			TownUI = GameObject.Find("$TownUi");
			if( TownUI != null ) TownUI.SetActive(false);
			
			mapShowStep		= 0;
			fCurrent		= 0;
		}
	}
	
	static void LoadAudio(ResLoadParams param,UnityEngine.Object obj)
	{
		int level = (int)param.userdata0;
		if( level == 20 )
			AudioClipMC	 = obj as AudioClip;
		else
			AudioClipWin = obj as AudioClip;
		PlayWinAudio(level);
	}
	
	// 设置当前开启的战役，并制定是否首次开启，如果首次开启则播放动画，每次打开世界地图只能调用此函数一次，后续调用会导致崩溃.
	public static void SetLevel(int level,bool firstTime)
	{
		Hashtable data = sdGlobalDatabase.Instance.globalData;
		if( data.ContainsKey("OpenLevel_FirstTime") && (int)data["OpenLevel_FirstTime"]==1 ) return;
		data["OpenLevel_Index"]		= level;
		data["OpenLevel_FirstTime"]	= firstTime ? 1 : 0;
		bInited = false;
	}
}
