using UnityEngine;
using System.Collections;
using System;

public class sdUIPetRonghePnl : MonoBehaviour
{
	public GameObject m_preWnd = null;
	
	public GameObject m_txt_Sel0 = null;
	public GameObject m_txt_Sel1 = null;
	public GameObject m_txt_SelResult = null;
	public GameObject m_btnRonghe = null;
	public GameObject m_btnRongheShow = null;
	public GameObject m_btnRongheBack = null;

	public GameObject btnRhSel0 = null;
	public GameObject btnRhSel1 = null;
	public GameObject panModel0 = null;
	public GameObject panModel1 = null;
	public GameObject panModel2 = null;

	public GameObject panNew = null;
	public GameObject newName = null;
	public GameObject pstar0 = null;
	public GameObject pstar1 = null;
	public GameObject pstar2 = null;
	public GameObject pstar3 = null;
	public GameObject pstar4 = null;
	
	public UInt64 m_uuPetID0 = UInt64.MaxValue;
	public UInt64 m_uuPetID1 = UInt64.MaxValue;
	public int m_iPetIDNew = 0;
	
	static GameObject m_PetModel0;
	static GameObject m_PetModel1;
	static GameObject m_PetModelNew;
	
	static GameObject m_RongheEffect;
	
	public bool m_bBeginRonghe = false;

	static GameObject m_RongheSuccessEffect;
	bool bHasLoadRongheSuccessEffect = false;
	float fHasPlayRongheSuccessEffectTime = 0.0f;
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	float fTime = 0.0f;
	bool bNeedMove = false;
	float fRongheSuccessTime = 0.0f;
	void Update () 
	{
		if (m_bBeginRonghe)
		{
			fTime += Time.deltaTime;
			
			if (fTime>=0.8f)
			{
				if (m_PetModel0)
					m_PetModel0.SetActive(false);
		
				if (m_PetModel1)
					m_PetModel1.SetActive(false);
			}
			
			if (fTime>=3.0f)
			{
				Hashtable table = sdConfDataMgr.Instance().GetPetTemplate(m_iPetIDNew.ToString());
				if (table!=null)
				{
					string strName = table["Name"].ToString();
					ShowRonghePetResultModel();
				}
				//显示按钮：查看，返回..
				if (m_btnRongheShow)
					m_btnRongheShow.SetActive(true);
				if (m_btnRongheBack)
					m_btnRongheBack.SetActive(true);
				//加载融合成功特效..
				LoadRongheSuccessEffect();
				//状态重置..
				m_bBeginRonghe = false;
				fTime = 0.0f;
				bNeedMove = false;
				fRongheSuccessTime = 0.0f;
			}
		}

		if (bHasLoadRongheSuccessEffect)
		{
			fHasPlayRongheSuccessEffectTime += Time.deltaTime;
			if (fHasPlayRongheSuccessEffectTime>=1.5f)
			{
				//状态回归..
				bHasLoadRongheSuccessEffect = false;
				fHasPlayRongheSuccessEffectTime = 0.0f;
				bNeedMove = true;
				fRongheSuccessTime = 0.0f;
			}
		}

		if (bNeedMove)
		{
			fRongheSuccessTime+=Time.deltaTime;
			if (fRongheSuccessTime>0.4f)
			{
				bNeedMove = false;
				fRongheSuccessTime = 0.0f;
				if (m_RongheSuccessEffect!=null)
					m_RongheSuccessEffect.SetActive(false);

				//显示新宠物名字,星级..
				if (panNew)
				{
					panNew.SetActive(true);
					OnShowNewPetProp();
				}
			}
			else
			{
				int iY = (int)(650.0f*fRongheSuccessTime);
				if (m_RongheSuccessEffect!=null)
					m_RongheSuccessEffect.transform.localPosition = new Vector3(m_RongheSuccessEffect.transform.localPosition.x,
					                                                            iY,
					                                                            m_RongheSuccessEffect.transform.localPosition.z);
			}
		}
	}
	
	void OnClick()
    {

	}
	
	void OnDrag(Vector2 delta)
	{
		if( gameObject.name == "panModel0" )
		{
			if( m_PetModel0 == null ) return;
			m_PetModel0.transform.Rotate(0,-delta.x/2.0f,0);
		}
		else if( gameObject.name == "panModel1" )
		{
			if( m_PetModel1 == null ) return;
			m_PetModel1.transform.Rotate(0,-delta.x/2.0f,0);
		}
		else if( gameObject.name == "panModel2" )
		{
			if( m_PetModelNew == null ) return;
			m_PetModelNew.transform.Rotate(0,-delta.x/2.0f,0);
		}
	}
	
	public void ActivePetRonghePnl(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
		OnActivePnlSetRadioButton();
		ResetPetRongheUI();
		ShowRonghePetSelectLeftModel();
		ShowRonghePetSelectRightModel();
	}

	public void OnActivePnlSetRadioButton()
	{
		sdRadioButton[] list = gameObject.GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (btn.gameObject.name=="RbRh")
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
		}
	}
	
	public void ResetPetRongheUI()
	{
		m_uuPetID0 = UInt64.MaxValue;
		m_uuPetID1 = UInt64.MaxValue;
		m_iPetIDNew = 0;
		m_bBeginRonghe = false;

		fTime = 0.0f;
		bHasLoadRongheSuccessEffect = false;
		fHasPlayRongheSuccessEffectTime = 0.0f;
		
		if (m_txt_Sel0)
		{
			UILabel txtLable = m_txt_Sel0.GetComponent<UILabel>();
			if (txtLable)
				txtLable.text = "选择战魂";
			
			sdNewPetMgr.SetLabelColorByAbility(4, m_txt_Sel0);
		}
		
		if (m_txt_Sel1)
		{
			UILabel txtLable = m_txt_Sel1.GetComponent<UILabel>();
			if (txtLable)
				txtLable.text = "选择战魂";
			
			sdNewPetMgr.SetLabelColorByAbility(4, m_txt_Sel1);
		}
		
		if (m_txt_SelResult)
		{
			UILabel txtLable = m_txt_SelResult.GetComponent<UILabel>();
			if (txtLable)
				txtLable.text = "任意两只四星以下相同星级的+3战魂,可通过融合随机获得一只星级+1的战魂";
		}
		
		if( m_PetModel0 )
			m_PetModel0.SetActive(false);
		
		if( m_PetModel1 )
			m_PetModel1.SetActive(false);
		
		if( m_PetModelNew )
			m_PetModelNew.SetActive(false);
		
		if (btnRhSel0)
			btnRhSel0.SetActive(true);
		
		if (btnRhSel1)
			btnRhSel1.SetActive(true);

		if (panNew)
			panNew.SetActive(false);

		if (m_btnRonghe)
			m_btnRonghe.SetActive(true);
		if (m_btnRongheShow)
			m_btnRongheShow.SetActive(false);
		if (m_btnRongheBack)
			m_btnRongheBack.SetActive(false);
		
		if( m_RongheEffect )
			Destroy(m_RongheEffect);
		m_RongheEffect = null;

		if( m_RongheSuccessEffect )
			Destroy(m_RongheSuccessEffect);
		m_RongheSuccessEffect = null;
	}
	
	public void OnClickRongheBtn()
	{
		if (m_txt_Sel0)
		{
			UILabel txtLable = m_txt_Sel0.GetComponent<UILabel>();
			if (txtLable)
				txtLable.text = "";
		}
		
		if (m_txt_Sel1)
		{
			UILabel txtLable = m_txt_Sel1.GetComponent<UILabel>();
			if (txtLable)
				txtLable.text = "";
		}
		
		if (m_txt_SelResult)
		{
			UILabel txtLable = m_txt_SelResult.GetComponent<UILabel>();
			if (txtLable)
				txtLable.text = "";
		}
		
		if (btnRhSel0)
			btnRhSel0.SetActive(false);
		
		if (btnRhSel1)
			btnRhSel1.SetActive(false);

		if (m_btnRonghe)
			m_btnRonghe.SetActive(false);
	}
	
	public void ShowRonghePetSelectLeftModel()
	{
		SClientPetInfo Info = null;
		
		if (m_uuPetID0 != UInt64.MaxValue)
		{
			Info = sdNewPetMgr.Instance.GetPetInfo(m_uuPetID0);
			if (Info != null)
			{
				// 载入宠物形象..
				if( m_PetModel0 ) Destroy(m_PetModel0);
				m_PetModel0 = null;
				ResLoadParams param = new ResLoadParams();
				param.pos = new Vector3(0.0f,-140.0f,-200.0f);
				param.rot.Set(0,180.0f,0,0);
				param.scale = new Vector3(180.0f,180.0f,180.0f);
				string strPath = Info.m_strRes.Replace(".prefab","_UI.prefab");
				sdResourceMgr.Instance.LoadResource(strPath, LeftPetLoadInstantiate, param);
				//宠物名字..
				if (m_txt_Sel0)
				{
					sdNewPetMgr.SetLabelColorByAbility(Info.m_iAbility, m_txt_Sel0);
					UILabel txtLable = m_txt_Sel0.GetComponent<UILabel>();
					if (txtLable)
						txtLable.text = Info.m_strName;
				}
				
				if (btnRhSel0)
					btnRhSel0.SetActive(false);
			}
		}
		
		if (m_txt_SelResult!=null)
		{
			UILabel txtLable = m_txt_SelResult.GetComponent<UILabel>();
			if (txtLable)
			{
				if (m_uuPetID0==UInt64.MaxValue && m_uuPetID1==UInt64.MaxValue)
				{
					txtLable.text = "任意两只四星以下相同星级的+3战魂,可通过融合随机获得一只星级+1的战魂";
				}
				else if ((m_uuPetID0==UInt64.MaxValue && m_uuPetID1!=UInt64.MaxValue)
					|| (m_uuPetID0!=UInt64.MaxValue && m_uuPetID1==UInt64.MaxValue) )
					txtLable.text = "请选择另一个需要融合的战魂";
				else if (m_uuPetID0!=UInt64.MaxValue && m_uuPetID1!=UInt64.MaxValue)
					txtLable.text = "请点击融合按钮进行融合";
			}
		}
	}
	
	public void ShowRonghePetSelectRightModel()
	{
		SClientPetInfo Info = null;
		if (m_uuPetID1 != UInt64.MaxValue)
		{
			Info = sdNewPetMgr.Instance.GetPetInfo(m_uuPetID1);
			if (Info != null)
			{
				// 载入宠物形象..
				if( m_PetModel1 ) Destroy(m_PetModel1);
				m_PetModel1 = null;
				ResLoadParams param = new ResLoadParams();
				param.pos = new Vector3(0.0f,-140.0f,-200.0f);
				param.rot.Set(0,180.0f,0,0);
				param.scale = new Vector3(180.0f,180.0f,180.0f);
				string strPath = Info.m_strRes.Replace(".prefab","_UI.prefab");
				sdResourceMgr.Instance.LoadResource(strPath, RightPetLoadInstantiate, param);
				//宠物名字..
				if (m_txt_Sel1)
				{
					sdNewPetMgr.SetLabelColorByAbility(Info.m_iAbility, m_txt_Sel1);
					UILabel txtLable = m_txt_Sel1.GetComponent<UILabel>();
					if (txtLable)
						txtLable.text = Info.m_strName;
				}
				
				if (btnRhSel1)
					btnRhSel1.SetActive(false);
			}
		}
		
		if (m_txt_SelResult!=null)
		{
			UILabel txtLable = m_txt_SelResult.GetComponent<UILabel>();
			if (txtLable)
			{
				if (m_uuPetID0==UInt64.MaxValue && m_uuPetID1==UInt64.MaxValue)
				{
					txtLable.text = "任意两只四星以下相同星级的+3战魂,可通过融合随机获得一只星级+1的战魂";
				}
				else if ((m_uuPetID0==UInt64.MaxValue && m_uuPetID1!=UInt64.MaxValue)
					|| (m_uuPetID0!=UInt64.MaxValue && m_uuPetID1==UInt64.MaxValue) )
					txtLable.text = "请选择另一个需要融合的战魂";
				else if (m_uuPetID0!=UInt64.MaxValue && m_uuPetID1!=UInt64.MaxValue)
					txtLable.text = "请点击融合按钮进行融合";
			}
		}
	}
	
	public void ShowRonghePetResultModel()
	{
		if (m_iPetIDNew>0)
		{
			Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(m_iPetIDNew.ToString());
			if (info != null)
			{
				// 载入宠物形象..
				if( m_PetModelNew ) Destroy(m_PetModelNew);
				m_PetModelNew = null;
				ResLoadParams param = new ResLoadParams();
				param.pos = new Vector3(0.0f,-240.0f,-200.0f);
				param.rot.Set(0,180.0f,0,0);
				param.scale = new Vector3(180.0f,180.0f,180.0f);
				string strPath = (info["Res"].ToString()).Replace(".prefab","_UI.prefab");
				sdResourceMgr.Instance.LoadResource(strPath, CenterPetLoadInstantiate, param);
			}
		}
	}
	
	public void LeftPetLoadInstantiate(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) return;
		m_PetModel0 = GameObject.Instantiate(obj) as GameObject;
		m_PetModel0.name = "panModel0";
		m_PetModel0.transform.parent = panModel0.transform;
		m_PetModel0.transform.localPosition = param.pos;
		
		if (m_PetModel0!=null)
		{
			if (m_uuPetID0 != UInt64.MaxValue)
				m_PetModel0.SetActive(true);
			else
				m_PetModel0.SetActive(false);
		}
		
		if (m_PetModel1!=null)
		{
			if (m_uuPetID1 != UInt64.MaxValue)
				m_PetModel1.SetActive(true);
			else
				m_PetModel1.SetActive(false);
		}
	}
	
	public void RightPetLoadInstantiate(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) return;
		m_PetModel1 = GameObject.Instantiate(obj) as GameObject;
		m_PetModel1.name = "panModel1";
		m_PetModel1.transform.parent = panModel1.transform;
		m_PetModel1.transform.localPosition = param.pos;
		
		if (m_PetModel0!=null)
		{
			if (m_uuPetID0 != UInt64.MaxValue)
				m_PetModel0.SetActive(true);
			else
				m_PetModel0.SetActive(false);
		}
		
		if (m_PetModel1!=null)
		{
			if (m_uuPetID1 != UInt64.MaxValue)
				m_PetModel1.SetActive(true);
			else
				m_PetModel1.SetActive(false);
		}
	}
	
	public void CenterPetLoadInstantiate(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) return;
		m_PetModelNew = GameObject.Instantiate(obj) as GameObject;
		m_PetModelNew.name = "panModel2";
		m_PetModelNew.transform.parent = panModel2.transform;
		m_PetModelNew.transform.localPosition = param.pos;
		m_PetModelNew.SetActive(true);
	}
	
	public void LoadRongheEffect()
	{
		if( m_RongheEffect )
			Destroy(m_RongheEffect);
		m_RongheEffect = null;
		
		string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwuronghe/Fx_chongwuronghe.prefab";
		ResLoadParams param = new ResLoadParams();
		param.info = "RongheEffect";
		sdResourceMgr.Instance.LoadResource(prefabname,OnLoadPrefab,param);
	}
	
	public void OnLoadPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null) return;
		if (param.info == "RongheEffect")
		{
			m_RongheEffect = GameObject.Instantiate(obj) as GameObject;
			m_RongheEffect.transform.parent = gameObject.transform;
			m_RongheEffect.transform.localPosition = Vector3.zero;
			m_RongheEffect.transform.localScale = new Vector3(100,100,100);
		}
	}
	
	public void ShowHideModel(bool bShow)
	{
		if (bShow==true)
		{
			if (m_PetModel0 && m_uuPetID0!=UInt64.MaxValue)
				m_PetModel0.SetActive(true);
			
			if (m_PetModel1 && m_uuPetID1!=UInt64.MaxValue)
				m_PetModel1.SetActive(true);

			if (m_PetModelNew && m_iPetIDNew>0)
				m_PetModelNew.SetActive(true);
		}
		else
		{
			if (m_PetModel0)
				m_PetModel0.SetActive(false);
			
			if (m_PetModel1)
				m_PetModel1.SetActive(false);

			if (m_PetModelNew)
				m_PetModelNew.SetActive(false);
		}
	}

	public void LoadRongheSuccessEffect()
	{
		if( m_RongheSuccessEffect )
			Destroy(m_RongheSuccessEffect);
		m_RongheSuccessEffect = null;
		
		bHasLoadRongheSuccessEffect = false;
		fHasPlayRongheSuccessEffectTime = 0.0f;
		
		string prefabname = "Effect/MainChar/FX_UI/$Fx_Levelup/Fx_Shengji_001_prefab.prefab";
		ResLoadParams param = new ResLoadParams();
		param.info = "RongheSuccessEffect";
		sdResourceMgr.Instance.LoadResource(prefabname,OnLoadRongheSuccessPrefab,param);
	}
	
	public void OnLoadRongheSuccessPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null)
		{
			bHasLoadRongheSuccessEffect = false;
			fHasPlayRongheSuccessEffectTime = 0.0f;
			return;
		}
		
		if (param.info == "RongheSuccessEffect")
		{
			m_RongheSuccessEffect = GameObject.Instantiate(obj,gameObject.transform.position,gameObject.transform.transform.rotation) as GameObject;
			m_RongheSuccessEffect.transform.parent = gameObject.transform;
			m_RongheSuccessEffect.transform.localPosition = new Vector3(0.0f, 0.0f, -500.0f);
			m_RongheSuccessEffect.transform.localRotation = Quaternion.identity;
			m_RongheSuccessEffect.transform.localScale	= new Vector3(300.0f, 300.0f, 300.0f);

			GameObject gObj = null;
			gObj = m_RongheSuccessEffect.transform.FindChild("Fx_Qianghuachenggong_001").gameObject;
			if (gObj!=null) gObj.SetActive(false);
			gObj = m_RongheSuccessEffect.transform.FindChild("Fx_Ronghechenggong_001").gameObject;
			if (gObj!=null) gObj.SetActive(true);
			gObj = m_RongheSuccessEffect.transform.FindChild("Fx_Jiesuochenggong_001").gameObject;
			if (gObj!=null) gObj.SetActive(false);
			gObj = m_RongheSuccessEffect.transform.FindChild("Fx_Shengjichenggong_001").gameObject;
			if (gObj!=null) gObj.SetActive(false);

			m_RongheSuccessEffect.SetActive(true);
			bHasLoadRongheSuccessEffect = true;
			fHasPlayRongheSuccessEffectTime = 0.0f;
		}
	}

	public void OnShowNewPetProp()
	{
		Hashtable table = sdConfDataMgr.Instance().GetPetTemplate(m_iPetIDNew.ToString());
		if (table!=null)
		{
			string strName = table["Name"].ToString();
			int iAbility = int.Parse(table["Ability"].ToString());
			if (newName)
			{
				sdNewPetMgr.SetLabelColorByAbility(iAbility, newName);
				newName.GetComponent<UILabel>().text = strName;
			}

			SetPetStar(iAbility);
		}
	}

	public void SetPetStar(int iStar)
	{
		//星级..
		if (pstar0==null || pstar1==null || pstar2==null || pstar3==null || pstar4==null)
			return;
		
		float fWidth = (float)pstar0.GetComponent<UISprite>().width*1.0f;
		
		if (iStar==1)
		{
			pstar0.SetActive(false);
			pstar1.SetActive(false);
			pstar2.SetActive(true);
			pstar3.SetActive(false);
			pstar4.SetActive(false); 
			
			pstar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, pstar2.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar2.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==2)
		{
			pstar0.SetActive(false);
			pstar1.SetActive(false);
			pstar2.SetActive(true);
			pstar3.SetActive(true);
			pstar4.SetActive(false);
			
			pstar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, pstar2.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar2.GetComponent<UISprite>().transform.localPosition.z);
			pstar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, pstar3.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==3)
		{
			pstar0.SetActive(false);
			pstar1.SetActive(true);
			pstar2.SetActive(true);
			pstar3.SetActive(true);
			pstar4.SetActive(false);
			
			pstar1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, pstar1.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar1.GetComponent<UISprite>().transform.localPosition.z);
			pstar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, pstar2.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar2.GetComponent<UISprite>().transform.localPosition.z);
			pstar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, pstar3.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==4)
		{
			pstar0.SetActive(false);
			pstar1.SetActive(true);
			pstar2.SetActive(true);
			pstar3.SetActive(true);
			pstar4.SetActive(true);
			
			pstar1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*0.5f, pstar1.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar1.GetComponent<UISprite>().transform.localPosition.z);
			pstar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, pstar2.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar2.GetComponent<UISprite>().transform.localPosition.z);
			pstar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, pstar3.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar3.GetComponent<UISprite>().transform.localPosition.z);
			pstar4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.5f, pstar4.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==5)
		{
			pstar0.SetActive(true);
			pstar1.SetActive(true);
			pstar2.SetActive(true);
			pstar3.SetActive(true);
			pstar4.SetActive(true);
			
			pstar1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, pstar1.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar1.GetComponent<UISprite>().transform.localPosition.z);
			pstar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, pstar2.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar2.GetComponent<UISprite>().transform.localPosition.z);
			pstar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, pstar3.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar3.GetComponent<UISprite>().transform.localPosition.z);
			pstar4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(pstar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*4.0f, pstar4.GetComponent<UISprite>().transform.localPosition.y, 
				 pstar4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else
		{
			pstar0.SetActive(true);
			pstar1.SetActive(false);
			pstar2.SetActive(false);
			pstar3.SetActive(false);
			pstar4.SetActive(false);
		}
	}
}