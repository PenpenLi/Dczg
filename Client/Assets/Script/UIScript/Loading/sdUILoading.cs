using UnityEngine;
using System.Collections;

public class sdUILoading : MonoBehaviour 
{
	static private UITexture	m_texLoading	= null;
	static private UISprite		m_spRotate		= null;
	static private UISprite		m_spBG			= null;
	static private UILabel		m_lbCityName	= null;
	static private GameObject 	m_oLoadingUI	= null;

	static private Texture		m_tex			= null;


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( m_spRotate!=null && m_spRotate.isVisible )
		{
			m_spRotate.gameObject.transform.Rotate(0,0,-360.0f*Time.deltaTime);
		}
	}
	
	static void Init()
	{
		if( m_oLoadingUI == null )
		{
			Object obj = Resources.Load("Loading/LoadingWnd");
			if( obj == null ) return;
			
			m_oLoadingUI = GameObject.Instantiate(obj) as GameObject;
			if( m_oLoadingUI == null ) return;
			GameObject Anchor = GameObject.Find("Anchor");
			if( Anchor ) m_oLoadingUI.transform.parent = Anchor.transform;
			m_oLoadingUI.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			m_oLoadingUI.transform.localPosition = new Vector3(0,0,0);
		
			GameObject o;
			o = GameObject.Find("TEX_LoadingPic");
			if(o) 
			{
				m_texLoading = o.GetComponentInChildren<UITexture>();
				m_texLoading.mainTexture = null;
			}
			o = GameObject.Find("SP_LoadingProgress");
			if(o) m_spRotate = o.GetComponentInChildren<UISprite>();
			o = GameObject.Find("SP_LoadingBG");
			if(o) m_spBG = o.GetComponentInChildren<UISprite>();
			o = GameObject.Find("LB_LoadingCityName");
			if(o) m_lbCityName = o.GetComponentInChildren<UILabel>();
		}
	}
	
	// 激活有图片的Loading界面.
	static bool bLoadingTex = false;
	public static bool LoadingTex() { return bLoadingTex; }
	public static void ActiveLoadingUI(int idx) 
	{ 
		if( idx == 0 )
			ActiveLoadingUI("worldmap", "鲁米纳大陆"); 
		else
			ActiveLoadingUI("maincity", "光明城鲁米纳"); 
	}
	public static void ActiveLoadingUI(string strLoadingTex, string strLevelName)
	{		
		Init();
		m_oLoadingUI.SetActive(true);
		
		// 不显示冒泡数值.
		GameObject bubble = GameObject.Find("@SelfBubble");
		if( bubble ) bubble.SetActive(false);
		
		if( m_spBG )		m_spBG.gameObject.SetActive(true);
		if( m_texLoading )	m_texLoading.gameObject.SetActive(true);
		if( m_spRotate )	m_spRotate.gameObject.SetActive(true);
		if( m_lbCityName )
		{
			m_lbCityName.gameObject.SetActive(true);
			m_lbCityName.text = strLevelName;
		}

		PreLoadTex();
	}
	
	public static void UnactiveLoadingUI()
	{
		Init();
		m_oLoadingUI.SetActive(false);
	}

	// 预加载Loading图.
	public static void PreLoadTex()
	{
		if( bLoadingTex || m_texLoading.mainTexture!=null ) return;

		// 先简单处理，随机出一张图.
		int iDS = Random.Range(1,3);
		ResLoadParams param = new ResLoadParams();
		param.info = Application.loadedLevelName;
		bLoadingTex = true;
		sdResourceMgr.Instance.LoadResourceImmediately("UI/LoadingTex/$LT_ds"+iDS+".png",LoadTexCallback,param,typeof(Texture)); 
	}
	
	// 激活无图片的小型Loading界面.
	public static void ActiveSmallLoadingUI(bool bActive)
	{
		Init();
		PreLoadTex();	// 预加载贴图.
		if( bActive )
		{
			m_oLoadingUI.SetActive(true);
			if( m_spBG )		m_spBG.gameObject.SetActive(false);
			if( m_texLoading )	m_texLoading.gameObject.SetActive(false);
			if( m_lbCityName )	m_lbCityName.gameObject.SetActive(false);
			if( m_spRotate )	
			{
				m_spRotate.gameObject.SetActive(true);
				m_spRotate.gameObject.transform.localPosition = new Vector3(0,0,0);
			}
		}
		else
		{
			m_spRotate.gameObject.transform.localPosition = new Vector3(0,-250f,0);
			m_oLoadingUI.SetActive(false);
		}
	}
	
	static void LoadTexCallback(ResLoadParams param,Object obj)
	{
		bLoadingTex = false;
		if( obj == null ) return;
		if( param.info != Application.loadedLevelName ) return;
		if( m_texLoading )
		{
			m_texLoading.mainTexture = obj as Texture;
			m_texLoading.color = new Color(1f,1f,1f,1f);
		}
	}
}
