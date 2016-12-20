using UnityEngine;
using System.Collections;

/// <summary>
/// 选择角色窗口(执行脚本)
/// 1.更新预览窗口
/// </summary>
public class SelectChar : MonoBehaviour 
{	
	public GameObject m_SelectRoleWndObject;//< 进入游戏按钮对象aa		
	protected SelectRole mSelectRole;		//< 进入游戏按钮脚本对象aa

	protected GameObject mLoginCharObject;	//< 预览窗口预览角色对象aa
	public sdLoginChar mLoginChar;		//< 预览窗口预览角色脚本对象aa
	protected SpinObject mSpinObject; 		//< 主窗口旋转预览角色脚本对象aa
	
	void Awake()
	{
		mSpinObject = this.gameObject.GetComponent<SpinObject>();
		
		if (m_SelectRoleWndObject)
			mSelectRole = m_SelectRoleWndObject.GetComponent<SelectRole>();
		
		// 根据分辨率调整显示...
		GameObject obj = GameObject.Find("sp_link");
		if( obj != null )
		{
			float r = 1.777f / ((float)Screen.width/(float)Screen.height) - 1.0f;
			float y = (1280.0f / (float)Screen.width * (float)Screen.height - 720.0f) / 2.0f + 1000.0f;	// 16:9下的y为1000，4:3下的y为1120
			obj.GetComponent<TweenPosition>().from.y	= y;
			obj.GetComponent<TweenPosition>().to.y		= y - 612.0f - 240.0f*r;	// 对4:3屏幕，r=0.33，锁链长度一共会增长80
		}
		// 对应3:2以上比例的屏幕，把主角圆盘右移一些...
		obj = GameObject.Find("city_construct_halidom02");
		if( obj != null )
		{
			if( ((float)Screen.width/(float)Screen.height) <= 1.5 )
				obj.transform.localPosition = new Vector3(-0.5f,obj.transform.localPosition.y,obj.transform.localPosition.z);
		}
		
		// 如果一个角色都没创建过，则自动跳到角色创建界面.
		if( SDNetGlobal.playerList[0]==null && SDNetGlobal.playerList[1]==null && SDNetGlobal.playerList[2]==null && SDNetGlobal.playerList[3]==null )
		{
			mSelectRole.CreateChar();
		}
	}

	void Start () 
	{
		
	}

	void Update () 
	{

	}
	
	void FixedUpdate()
	{
		if (mLoginChar)
			mLoginChar.tickFrame();
	}
		
	// 进入选角色界面aa
	public void EnterSelectUI()
	{
		this.gameObject.SetActive(true);
		if( mLoginCharObject != null )
		{
			mLoginCharObject.SetActive(true);
            if (mLoginChar) 
                mLoginChar.PlayShow();
		}
		
		mSelectRole.EnterSelectUI();
		
		GameObject obj = GameObject.Find("sp_link");
		if( obj!=null && obj.GetComponent<TweenPosition>().enabled==false )
		{
			obj.GetComponent<TweenPosition>().PlayForward();
		}
	}
	
	// 离开选角色界面aa
	public void LeaveSelectUI()
	{
		this.gameObject.SetActive(false);
		if( mLoginCharObject != null )
			mLoginCharObject.SetActive(false);
	}

	// 进入离开选角色界面流程aa
	public void StartLeaveSelectUI()
	{
		GameObject obj = GameObject.Find("sp_link");
		if( obj != null )
		{
			obj.GetComponent<TweenPosition>().PlayReverse();
		}
	}
	
	// 判断离开选角色界面流程是否完成aa
	public bool IsLeaveSelectUIFinish()
	{
		GameObject obj = GameObject.Find("sp_link");
		if( obj != null )
			return !(obj.GetComponent<TweenPosition>().enabled);
		else
			return true;
	}
	
	// 指定角色被选中aa
	public void DoSelect(int iRoleID)
	{
		if (iRoleID < 0 || iRoleID > 4)
		{
			if (mLoginCharObject != null)
				mLoginCharObject.SetActive(false);
				
			return;	
		}
		
		sdPlayerInfo kPlayerInfo = SDNetGlobal.playerList[iRoleID];
		if (kPlayerInfo == null)
		{
			if (mLoginCharObject != null)
				mLoginCharObject.SetActive(false);
			return;
		}
		
		if (mLoginChar != null)
		{
			if (kPlayerInfo.mDBID == mLoginChar.DBID)
			{
				mLoginCharObject.SetActive(true);
                mLoginChar.PlayShow();
				return;
			}
		}
		
		// 销毁先前预览角色aa
		if (mLoginCharObject != null)
		{	
			if (mSpinObject)
				mSpinObject.m_Target = null;
		
			GameObject.Destroy(mLoginCharObject);
			
			mLoginChar = null;	
			mLoginCharObject = null;
		}
		
		// 创建预览角色aa
		GameObject obj = GameObject.Find("city_construct_halidom02");
		mLoginCharObject = new GameObject();
		mLoginCharObject.name = "@LoginChar";
		mLoginCharObject.transform.parent = GameObject.Find("SelectSceneWnd").transform;
		mLoginCharObject.transform.localPosition = new Vector3(obj.transform.localPosition.x, -0.76f, obj.transform.localPosition.z);	
		mLoginCharObject.transform.localRotation = Quaternion.Euler(0.0f, 158.1343f, 0.0f);
		mLoginCharObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);	
		mLoginCharObject.AddComponent<AudioSource>();
		// 
		sdGameActorCreateInfo kInfo = new sdGameActorCreateInfo();
		kInfo.mDBID = kPlayerInfo.mDBID;
		kInfo.mGender = kPlayerInfo.mGender;
		kInfo.mHairStyle = kPlayerInfo.mHairStyle;
		kInfo.mSkinColor = kPlayerInfo.mSkinColor;
		kInfo.mBaseJob = kPlayerInfo.mBaseJob;
		kInfo.mJob = kPlayerInfo.mJob;
		kInfo.mLevel = kPlayerInfo.mLevel;
		
		mLoginChar = mLoginCharObject.AddComponent<sdLoginChar>();
		mLoginChar.SetVisiable_WhenLoading(false);
		mLoginChar.init(kInfo);
		mLoginChar.updateAvatar(kPlayerInfo.mEquipID, kPlayerInfo.mEquipCount);
        mLoginChar.RequestEnd();
		
		
		// 设置给旋转脚本aa
		if (mSpinObject)
			mSpinObject.m_Target = mLoginChar;
	}
}
