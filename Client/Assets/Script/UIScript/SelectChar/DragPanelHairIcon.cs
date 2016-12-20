using UnityEngine;
using System.Collections;

/// <summary>
/// 拖曳面板的头发样式图标(执行脚本)aa
/// </summary>
public class DragPanelHairIcon : DragPanelIcon
{
	public GameObject m_CreareCharWndObject;	//< 创建角色窗口aa
	protected CreateChar mCreateChar;			//< 创建角色窗口脚本对象aa
	
	protected int mGender = 0;		//< 图标对应的角色性别aa
	public int Gender
	{
		get { return mGender;}
		set { mGender = value;}	
	}
	
	protected int mHairStyle = 0;	//< 图标对应的头发样式aa
	public int HairStyle
	{
		get { return mHairStyle;}
		set { mHairStyle = value;}	
	}
	
	protected int mHairColor = 0;	//< 图标对应的头发颜色aa
	public int HairColor
	{
		get { return mHairColor;}
		set { mHairColor = value;}	
	}
	
	protected bool mLoadReqing = false;	//< 请求加载标记
	protected int mCurGender = -1;
	protected int mCurHairStyle = -1;
	protected int mCurHairColor = -1;
	
	// Use this for initialization
	protected override void Start ()
	{
		base.Start();
		
		if (m_CreareCharWndObject)
			mCreateChar = m_CreareCharWndObject.GetComponent<CreateChar>();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update();
		
		if (this.gameObject.activeInHierarchy)
		{
			UISprite kSprite = this.gameObject.GetComponent<UISprite>();
			if (kSprite.atlas)
			{
				if (mCurGender != mGender || mCurHairStyle != mHairStyle || mCurHairColor != mHairColor)
				{
					UpdateHairIcon(mGender, mHairStyle, mHairColor);
				}
			}
			else
			{
				UpdateHairIcon(mGender, mHairStyle, mHairColor);
			}
		}
	}
	
	// 设置图集aa
	protected void UpdateHairIcon(int iGender, int iHairStyle, int iHairColor)
	{
		if (mCurGender == mGender && mCurHairStyle == mHairStyle && mCurHairColor == mHairColor)
			return;
		
		if (mCreateChar == null)
			return;
		
		UIAtlas kAtlas = mCreateChar.GetHairAtlas(iGender, iHairStyle);
		if (kAtlas == null)
			return;
		
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
		
		int iHairColorIndex = iHairColor+1;

		string kSpirteName = string.Format("{0}-{1}", iHairStyleIndex, iHairColorIndex);
		UISprite kUISprite = this.gameObject.GetComponent<UISprite>();	
		if (kUISprite)
		{
			kUISprite.atlas = kAtlas;
			kUISprite.spriteName = kSpirteName;
			
			mCurGender = iGender;
			mCurHairStyle = iHairStyle;
			mCurHairColor = iHairColor;
		}
	}
	
	
/*	
	// 请求加载头像图集aa
	protected void LoadAtlas(int iGender, int iHairStyle, int iHairColor)
	{
		if (mLoadReqing)				//< 避免重复发请求aa
			return;
		
		int iHairIndex = 0;
		if (mGender == 1)
		{
			iHairIndex = iHairStyle+1;	//< HairStyle: 0,1,2,3,4,5,6,7
										//< HairIndex: 1,2,3,4,5,6,7,8
		}
		else
		{
			iHairIndex = iHairStyle+2;	//< 图集的命名有问题,男性向后偏移了一个值aa
			if (iHairIndex > 8)			//< HairStyle: 0,1,2,3,4,5,6,7
				iHairIndex = 1;			//< HairIndex: 2,3,4,5,6,7,8,1
		}
	
		string kAssertPath = string.Format("Assets/UI/CharHair/$mhair{0}/mhair{0}_prefab.prefab", iHairIndex); 
		if (iGender == 1)
			kAssertPath = string.Format("Assets/UI/CharHair/$whair{0}/whair{0}_prefab.prefab", iHairIndex); 

		int iWaitIndex = 0;	
		ResLoadParams kParam = new ResLoadParams();
		kParam.userdata0 = iGender;
		kParam.userdata1 = iHairStyle;
		kParam.userdata2 = iHairColor;
		BundleGlobal.Instance.LoadResource(ref iWaitIndex,
			kAssertPath, 
			typeof(UIAtlas), 
			NotifyHeadAtlas, 
			kParam);
	}
	
	// 头像图集回调aa
	protected void NotifyHeadAtlas(ResLoadParams kParam, UnityEngine.Object kObj)
	{		
		if (kObj == null)
			return;
		
		UIAtlas kAtlas = kObj as UIAtlas;
		
		int iGender = (int)kParam.userdata0;
		int iHairStyle = (int)kParam.userdata1;
		int iHairColor = (int)kParam.userdata2;
		
		int iHairStyleIndex = 0;
		if (mGender == 1)
		{
			iHairStyleIndex = iHairStyle+1;	//< HairStyle: 0,1,2,3,4,5,6,7
											//< HairIndex: 1,2,3,4,5,6,7,8
		}
		else
		{
			iHairStyleIndex = iHairStyle+2;	//< 图集的命名有问题,男性向后偏移了一个值aa
			if (iHairStyleIndex > 8)		//< HairStyle: 0,1,2,3,4,5,6,7
				iHairStyleIndex = 1;		//< HairIndex: 2,3,4,5,6,7,8,1
		}
		
		int iHairColorIndex = iHairColor+1;

		string kSpirteName = string.Format("{0}-{1}", iHairStyleIndex, iHairColorIndex);
		UISprite kUISprite = this.gameObject.GetComponent<UISprite>();	
		if (kUISprite)
		{
			kUISprite.atlas = kAtlas;
			kUISprite.spriteName = kSpirteName;
			
			mCurGender = iGender;
			mCurHairStyle = iHairStyle;
			mCurHairColor = iHairColor;
		}
		
		mLoadReqing = false;
	}
*/
}

