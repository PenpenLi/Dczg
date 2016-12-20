using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetStrongPnl : MonoBehaviour
{
	public GameObject petOld = null;
	public GameObject petSelect = null;
	public GameObject petNew = null;

	public GameObject m_stGJOldTxt0 = null;
	public GameObject m_stFYOldTxt0 = null;
	public GameObject m_stSMOldTxt0 = null;

	public GameObject m_stArrow0 = null;
	public GameObject m_stArrow1 = null;
	public GameObject m_stArrow2 = null;

	public GameObject m_stGJOldTxt = null;
	public GameObject m_stFYOldTxt = null;
	public GameObject m_stSMOldTxt = null;

	public GameObject m_stGJNewTxt = null;
	public GameObject m_stFYNewTxt = null;
	public GameObject m_stSMNewTxt = null;

	public GameObject hint = null;
	public GameObject moneyNum = null;

	public GameObject panAnm = null;

	public GameObject m_preWnd = null;
	public UInt64 m_uuDBID;

	static uint m_nPetTemplateID = 0;
	static GameObject m_PetModel;
	Vector3 petPos = new Vector3(1.0f, 1.0f, 1.0f);
	Vector3 petTempPos = new Vector3(1.0f, 1.0f, 1.0f);

	//强化相关特效..
	public GameObject m_StrongEffect = null;
	bool bHasLoadStrongEffect = false;
	float fHasPlayStrongEffectTime = 0.0f;

	public GameObject m_StrongSuccessEffect = null;
	bool bHasLoadStrongSuccessEffect = false;
	float fHasPlayStrongSuccessEffectTime = 0.0f;

	public int m_iStrongMoney = 0;

	void Awake () 
	{
	}
	
	void Start () 
	{
	}

	int iAnimStep = 0;
	float fAnimTime = 0.0f;
	float fSuccessMoveTime = 0.0f;
	void Update ()
	{
		if (iAnimStep == 1)
		{
			if (fAnimTime<=0.3f)
			{
				fAnimTime += Time.deltaTime;
				petTempPos.x = petPos.x*fAnimTime*(1.0f/0.3f);
				petTempPos.y = petPos.y*fAnimTime*(1.0f/0.3f);
				petTempPos.z = petPos.z*fAnimTime*(1.0f/0.3f);
				if (m_PetModel)
				{
					if (m_PetModel.activeSelf==false)
						m_PetModel.SetActive(true);

					m_PetModel.transform.localScale = petTempPos;
				}
			}
			else
			{
				if (m_PetModel)
					m_PetModel.transform.localScale = petPos;
				iAnimStep = 2;
				fAnimTime = 0.0f;
			}
		}
		else if (iAnimStep == 2)
		{
			LoadStrongEffect();
			iAnimStep = 3;
		}
		else if (iAnimStep == 3)
		{
			if (bHasLoadStrongEffect==true)
			{
				fHasPlayStrongEffectTime += Time.deltaTime;
				if (fHasPlayStrongEffectTime>=2.0f)
				{
					if (m_StrongEffect)
						m_StrongEffect.SetActive(false);
					bHasLoadStrongEffect = false;
					fHasPlayStrongEffectTime = 0.0f;
					iAnimStep = 4;
				}
			}
		}
		else if (iAnimStep == 4)
		{
			LoadStrongSuccessEffect();
			iAnimStep = 5;
		}
		else if (iAnimStep == 5)
		{
			if (bHasLoadStrongSuccessEffect==true)
			{
				fHasPlayStrongSuccessEffectTime += Time.deltaTime;
				if (fHasPlayStrongSuccessEffectTime>=1.5f)
				{
					fSuccessMoveTime = 0.0f;
					bHasLoadStrongSuccessEffect = false;
					fHasPlayStrongSuccessEffectTime = 0.0f;
					iAnimStep = 6;
				}
			}
		}
		else if (iAnimStep == 6)
		{
			fSuccessMoveTime+=Time.deltaTime;
			if (fSuccessMoveTime>0.4f)
			{
				iAnimStep = 0;
				if (m_StrongSuccessEffect!=null)
					m_StrongSuccessEffect.SetActive(false);
				fSuccessMoveTime = 0.0f;
				panAnm.SetActive(false);
				sdGameLevel.instance.petCamera.enabled = false;
			}
			else
			{
				int iY = (int)(650.0f*fSuccessMoveTime);
				if (m_StrongSuccessEffect!=null)
					m_StrongSuccessEffect.transform.localPosition = new Vector3(m_StrongSuccessEffect.transform.localPosition.x,
				                                                            iY,
				                                                            m_StrongSuccessEffect.transform.localPosition.z);

				float fAlpha = 0.4f-fSuccessMoveTime;
				if (fAlpha<0.0f)
					fAlpha = 0.0f;
				fAlpha = fAlpha/0.4f;
				this.transform.FindChild("panAnm").transform.FindChild("PetView").GetComponent<UITexture>().alpha = fAlpha;
				this.transform.FindChild("panAnm").transform.FindChild("bgGrey").GetComponent<UISprite>().alpha = fAlpha;
			}
		}
	}
	
	void OnClick()
    {
	}
	
	public void ActivePetStrongPnl(GameObject PreWnd, UInt64 uuSelfDBID)
	{
		m_preWnd = PreWnd;
		m_uuDBID = uuSelfDBID;
		//DestroyPetModel();
		LoadPetModel();
		ReflashPetStrongIcon();
		panAnm.SetActive(false);
	}

	public void DestroyPetModel()
	{
		if( m_PetModel )
			Destroy(m_PetModel);
		m_PetModel = null;
	}

	public void LoadPetModel()
	{
		// 载入宠物形象..
		if (m_uuDBID!=UInt64.MaxValue)
		{
			SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;

			m_nPetTemplateID = Info.m_uiTemplateID;
			DestroyPetModel();
			
			// 载入模型..
			ResLoadParams param = new ResLoadParams();
			param.pos = new Vector3(0.0f,-140.0f,-200.0f);
			param.rot.Set(0,180.0f,0,0);
			param.scale = new Vector3(180.0f,180.0f,180.0f);
			string strPath = Info.m_strRes.Replace(".prefab","_UI.prefab");
			sdResourceMgr.Instance.LoadResource(strPath, PetLoadInstantiate, param);
		}
	}

	public void PetLoadInstantiate(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) 
			return;
		
		m_PetModel = GameObject.Instantiate(obj) as GameObject;
		m_PetModel.name = "PetStrongModel";
		m_PetModel.transform.parent = sdGameLevel.instance.petCamera.transform;
		
		m_PetModel.transform.localPosition = new Vector3(0.0f, -1.00f, 5.0f);
		m_PetModel.transform.localRotation = Quaternion.AngleAxis(180.0f, Vector3.up);
		m_PetModel.transform.localScale *= 1.5f;

		//播放一下idle01的动画..
		sdConfDataMgr.Instance().OnModelPlayIdleAnm(m_PetModel);
		m_PetModel.SetActive(true);
		petPos = m_PetModel.transform.localScale;
		
		int petLayer = LayerMask.NameToLayer("PetNode");
		Transform[] renderData = m_PetModel.GetComponentsInChildren<Transform>();
		foreach(Transform t in renderData)
		{
			t.gameObject.layer = petLayer;
		}
		
		this.transform.FindChild("panAnm").transform.FindChild("PetView").GetComponent<UITexture>().mainTexture = 
			sdGameLevel.instance.petTexture;
		this.transform.FindChild("panAnm").transform.FindChild("PetView").GetComponent<UITexture>().alpha = 1.0f;
		this.transform.FindChild("panAnm").transform.FindChild("bgGrey").GetComponent<UISprite>().alpha = 1.0f;
		sdGameLevel.instance.petCamera.enabled = false;
	}

	public void ReflashPetStrongIcon()
	{
		m_iStrongMoney = 0;

		if (petOld)
		{
			sdUIPetStrongCard card = petOld.GetComponent<sdUIPetStrongCard>();
			if (card)
			{
				card.m_iIconType = 0;
				card.ReflashPetIconUI(m_uuDBID);
			}
		}

		if (petSelect)
		{
			sdUIPetStrongCard card = petSelect.GetComponent<sdUIPetStrongCard>();
			if (card)
			{
				card.m_iIconType = 0;
				card.ReflashPetIconUI(sdNewPetMgr.Instance.m_uuPetStrongSelectID);
			}
		}
		
		if (petNew)
		{
			sdUIPetStrongCard card = petNew.GetComponent<sdUIPetStrongCard>();
			if (card)
			{
				card.m_iIconType = 1;
				card.ReflashPetIconUI(m_uuDBID);
			}
		}
		
		//属性变化的UI显示..
		SClientPetInfo Info = null;
		if (m_uuDBID==UInt64.MaxValue)
			return;
		
		Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
		if (Info == null)
			return;
		
		if (Info.m_iUp==3)
		{
			if (m_stArrow0)
				m_stArrow0.SetActive(false);
			
			if (m_stArrow1)
				m_stArrow1.SetActive(false);
			
			if (m_stArrow2)
				m_stArrow2.SetActive(false);

			//老属性..
			if (m_stGJOldTxt0)
			{
				float fGj = sdNewPetMgr.Instance.GetPetAttCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fGj + Info.m_TemplateProperty.m_iAtkDmgMax;
				m_stGJOldTxt0.GetComponent<UILabel>().text = iTemp.ToString();
			}
			
			if (m_stFYOldTxt0)
			{
				float fFy = sdNewPetMgr.Instance.GetPetDefCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fFy + Info.m_TemplateProperty.m_iDef;
				m_stFYOldTxt0.GetComponent<UILabel>().text = iTemp.ToString();
			}
			
			if (m_stSMOldTxt0)
			{
				float fSm = sdNewPetMgr.Instance.GetPetHPCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fSm + Info.m_TemplateProperty.m_iMaxHP;
				m_stSMOldTxt0.GetComponent<UILabel>().text = iTemp.ToString();
			}

			//新老属性对比..
			if (m_stGJOldTxt)
			{
				float fGj = sdNewPetMgr.Instance.GetPetAttCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fGj + Info.m_TemplateProperty.m_iAtkDmgMax;
				m_stGJOldTxt.GetComponent<UILabel>().text = iTemp.ToString();
			}
			
			if (m_stFYOldTxt)
			{
				float fFy = sdNewPetMgr.Instance.GetPetDefCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fFy + Info.m_TemplateProperty.m_iDef;
				m_stFYOldTxt.GetComponent<UILabel>().text = iTemp.ToString();
			}
			
			if (m_stSMOldTxt)
			{
				float fSm = sdNewPetMgr.Instance.GetPetHPCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fSm + Info.m_TemplateProperty.m_iMaxHP;
				m_stSMOldTxt.GetComponent<UILabel>().text = iTemp.ToString();
			}

			//新老属性对比..
			if (m_stGJNewTxt)
				m_stGJNewTxt.SetActive(false);
			
			if (m_stFYNewTxt)
				m_stFYNewTxt.SetActive(false);
			
			if (m_stSMNewTxt)
				m_stSMNewTxt.SetActive(false);

			//消耗金钱..
			if (hint)
			{
				hint.GetComponent<UILabel>().text = "强化等级已满";
			}

			if (moneyNum)
			{
				moneyNum.GetComponent<UILabel>().text = "0";
			}
		}
		else
		{
			if (m_stArrow0)
				m_stArrow0.SetActive(true);
			
			if (m_stArrow1)
				m_stArrow1.SetActive(true);
			
			if (m_stArrow2)
				m_stArrow2.SetActive(true);

			//老属性..
			if (m_stGJOldTxt0)
			{
				float fGj = sdNewPetMgr.Instance.GetPetAttCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fGj + Info.m_TemplateProperty.m_iAtkDmgMax;
				m_stGJOldTxt0.GetComponent<UILabel>().text = iTemp.ToString();
			}
			
			if (m_stFYOldTxt0)
			{
				float fFy = sdNewPetMgr.Instance.GetPetDefCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fFy + Info.m_TemplateProperty.m_iDef;
				m_stFYOldTxt0.GetComponent<UILabel>().text = iTemp.ToString();
			}
			
			if (m_stSMOldTxt0)
			{
				float fSm = sdNewPetMgr.Instance.GetPetHPCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fSm + Info.m_TemplateProperty.m_iMaxHP;
				m_stSMOldTxt0.GetComponent<UILabel>().text = iTemp.ToString();
			}

			//新老属性对比..
			if (m_stGJOldTxt)
			{
				float fGj = sdNewPetMgr.Instance.GetPetAttCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fGj + Info.m_TemplateProperty.m_iAtkDmgMax;
				m_stGJOldTxt.GetComponent<UILabel>().text = iTemp.ToString();
			}
			
			if (m_stFYOldTxt)
			{
				float fFy = sdNewPetMgr.Instance.GetPetDefCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fFy + Info.m_TemplateProperty.m_iDef;
				m_stFYOldTxt.GetComponent<UILabel>().text = iTemp.ToString();
			}
			
			if (m_stSMOldTxt)
			{
				float fSm = sdNewPetMgr.Instance.GetPetHPCoe(Info.m_uiTemplateID, Info.m_iUp, Info.m_iLevel);
				int iTemp = (int)fSm + Info.m_TemplateProperty.m_iMaxHP;
				m_stSMOldTxt.GetComponent<UILabel>().text = iTemp.ToString();
			}

			//新老属性对比..
			if (m_stGJNewTxt)
			{
				float fTemp = sdNewPetMgr.Instance.GetPetAttCoe(Info.m_uiTemplateID, Info.m_iUp+1, Info.m_iLevel);
				int iTemp = (int)fTemp + Info.m_TemplateProperty.m_iAtkDmgMax;
				m_stGJNewTxt.GetComponent<UILabel>().text = iTemp.ToString();
				m_stGJNewTxt.SetActive(true);
			}
			
			if (m_stFYNewTxt)
			{
				float fTemp = sdNewPetMgr.Instance.GetPetDefCoe(Info.m_uiTemplateID, Info.m_iUp+1, Info.m_iLevel);
				int iTemp = (int)fTemp + Info.m_TemplateProperty.m_iDef;
				m_stFYNewTxt.GetComponent<UILabel>().text = iTemp.ToString();
				m_stFYNewTxt.SetActive(true);
			}
			
			if (m_stSMNewTxt)
			{
				float fTemp = sdNewPetMgr.Instance.GetPetHPCoe(Info.m_uiTemplateID, Info.m_iUp+1, Info.m_iLevel);
				int iTemp = (int)fTemp + Info.m_TemplateProperty.m_iMaxHP;
				m_stSMNewTxt.GetComponent<UILabel>().text = iTemp.ToString();
				m_stSMNewTxt.SetActive(true);
			}

			//消耗金钱..
			if (hint)
			{
				int iUp = Info.m_iUp;
				int iAbility = Info.m_iAbility;
				string strTemp = "";
				if (iAbility==1)
					strTemp = "放入任意一星";
				else if (iAbility==2)
					strTemp = "放入任意二星";
				else if (iAbility==3)
					strTemp = "放入任意三星";
				else if (iAbility==4)
					strTemp = "放入任意四星";
				else if (iAbility==5)
					strTemp = "放入任意五星";
				else
					strTemp = "放入任意一星";

				string strTemp2 ="";
				if (iUp==0)
					strTemp2 = "未强化战魂";
				else
					strTemp2 = string.Format("+{0}战魂", iUp);
				hint.GetComponent<UILabel>().text = strTemp+strTemp2;
			}
			
			if (moneyNum)
			{
				int iMoney = 0;
				Hashtable Info2 = sdConfDataMgr.Instance().GetPetTemplate(Info.m_uiTemplateID.ToString());
				if (Info2 != null)
				{
					iMoney = int.Parse(Info2["UpMoneyCoe"].ToString());
					iMoney = iMoney*(Info.m_iUp+1);
				}

				m_iStrongMoney = iMoney;
				moneyNum.GetComponent<UILabel>().text = iMoney.ToString();
			}
		}
	}
	
	public void SendPetStrongMsg()
	{
		if (m_uuDBID!=UInt64.MaxValue && sdNewPetMgr.Instance.m_uuPetStrongSelectID!=UInt64.MaxValue
		    && m_uuDBID!=sdNewPetMgr.Instance.m_uuPetStrongSelectID)
		{
			sdPetMsg.Send_CS_PET_UP_REQ(m_uuDBID, sdNewPetMgr.Instance.m_uuPetStrongSelectID);
		}
	}

	public void OnClickStrongOk()
	{
		//属性变化的UI显示..
		SClientPetInfo Info = null;
		if (m_uuDBID==UInt64.MaxValue)
			return;
		
		Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
		if (Info == null)
			return;
		
		if (Info.m_iUp>=sdNewPetMgr.MAX_PET_STRONG_LEVEL)
		{
			sdUICharacter.Instance.ShowOkMsg("该战魂已经强化到最高级别", null);
			return;
		}

		if (sdNewPetMgr.Instance.m_uuPetStrongSelectID == UInt64.MaxValue)
		{
			sdUICharacter.Instance.ShowOkMsg("请选择一个战魂作为强化材料", null);
			return;
		}

		//金钱不足判断..
		uint uiMyMoney = 0;
		sdGameLevel level = sdGameLevel.instance;
		if (level != null)
		{
			if (level.mainChar != null)
			{
				Hashtable prop = level.mainChar.GetProperty();
				uiMyMoney = (uint)prop["NonMoney"];
			}
		}
		if (m_iStrongMoney>uiMyMoney)
		{
			sdUICharacter.Instance.ShowOkMsg("没有足够的金币", null);
			return;
		}

		SendPetStrongMsg();
		sdNewPetMgr.Instance.m_uuPetStrongSelectID = UInt64.MaxValue;
		ReflashPetStrongIcon();
	}

	public void OnStrongSuccess()
	{
		panAnm.SetActive(true);
		if (m_PetModel)
			m_PetModel.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
		
		iAnimStep = 1;
		fAnimTime = 0.0f;
		sdGameLevel.instance.petCamera.enabled = true;
	}

	public void LoadStrongEffect()
	{
		if( m_StrongEffect )
			Destroy(m_StrongEffect);
		m_StrongEffect = null;
		
		bHasLoadStrongEffect = false;
		fHasPlayStrongEffectTime = 0.0f;
		
		string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwuqianghua/Fx_Lingquqianghua.prefab";
		ResLoadParams param = new ResLoadParams();
		param.info = "StrongEffect";
		sdResourceMgr.Instance.LoadResource(prefabname,OnLoadStrongPrefab,param);
	}
	
	public void OnLoadStrongPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null)
		{
			bHasLoadStrongEffect = false;
			fHasPlayStrongEffectTime = 0.0f;

			iAnimStep = 6;
			fSuccessMoveTime = 1.0f;
			return;
		}
		
		if (param.info == "StrongEffect")
		{
			m_StrongEffect = GameObject.Instantiate(obj,gameObject.transform.position,gameObject.transform.transform.rotation) as GameObject;
			m_StrongEffect.transform.parent = gameObject.transform;
			m_StrongEffect.transform.localPosition = new Vector3(0.0f, 0.0f, -500.0f);
			m_StrongEffect.transform.localRotation = Quaternion.identity;
			m_StrongEffect.transform.localScale	=	Vector3.one;
			m_StrongEffect.SetActive(true);
			bHasLoadStrongEffect = true;
			fHasPlayStrongEffectTime = 0.0f;
		}
	}

	public void LoadStrongSuccessEffect()
	{
		if( m_StrongSuccessEffect )
			Destroy(m_StrongSuccessEffect);
		m_StrongSuccessEffect = null;
		
		bHasLoadStrongSuccessEffect = false;
		fHasPlayStrongSuccessEffectTime = 0.0f;
		
		string prefabname = "Effect/MainChar/FX_UI/$Fx_Levelup/Fx_Shengji_001_prefab.prefab";
		ResLoadParams param = new ResLoadParams();
		param.info = "StrongSuccessEffect";
		sdResourceMgr.Instance.LoadResource(prefabname,OnLoadStrongSuccessPrefab,param);
	}
	
	public void OnLoadStrongSuccessPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null)
		{
			bHasLoadStrongSuccessEffect = false;
			fHasPlayStrongSuccessEffectTime = 0.0f;

			iAnimStep = 6;
			fSuccessMoveTime = 1.0f;
			return;
		}
		
		if (param.info == "StrongSuccessEffect")
		{
			m_StrongSuccessEffect = GameObject.Instantiate(obj,gameObject.transform.position,gameObject.transform.transform.rotation) as GameObject;
			m_StrongSuccessEffect.transform.parent = gameObject.transform;
			m_StrongSuccessEffect.transform.localPosition = new Vector3(0.0f, 0.0f, -500.0f);
			m_StrongSuccessEffect.transform.localRotation = Quaternion.identity;
			m_StrongSuccessEffect.transform.localScale	= new Vector3(300.0f, 300.0f, 300.0f);

			GameObject gObj = null;
			gObj = m_StrongSuccessEffect.transform.FindChild("Fx_Qianghuachenggong_001").gameObject;
			if (gObj!=null) gObj.SetActive(true);
			gObj = m_StrongSuccessEffect.transform.FindChild("Fx_Ronghechenggong_001").gameObject;
			if (gObj!=null) gObj.SetActive(false);
			gObj = m_StrongSuccessEffect.transform.FindChild("Fx_Jiesuochenggong_001").gameObject;
			if (gObj!=null) gObj.SetActive(false);
			gObj = m_StrongSuccessEffect.transform.FindChild("Fx_Shengjichenggong_001").gameObject;
			if (gObj!=null) gObj.SetActive(false);

			m_StrongSuccessEffect.SetActive(true);
			bHasLoadStrongSuccessEffect = true;
			fHasPlayStrongSuccessEffectTime = 0.0f;
		}
	}
}