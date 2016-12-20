using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetLevelPnl : MonoBehaviour 
{
	//根据宠物实例ID显示宠物属性..
	public UInt64 m_uuDBID = UInt64.MaxValue;
	
	//左侧UI..
	public GameObject m_picType = null;
	public GameObject m_lbName = null;
	public GameObject m_lbStrong = null;
	public GameObject m_lbValue = null;

	//升级panel..
	//经验条..
	public GameObject m_lbLevel = null;
	public GameObject m_lbExp = null;
	public GameObject m_expBar = null;
	//基础属性..
	public GameObject m_lbv3 = null;
	public GameObject m_lbv4 = null;
	public GameObject m_lbv5 = null;
	//其他属性..
	public GameObject		m_lpCostVTxt		= null;
	public GameObject		m_lpGetExpVTxt		= null;
	public GameObject		m_lpMaxNeedExpVTxt	= null;
	public GameObject		m_lppet0			= null;
	public GameObject		m_lppet1			= null;
	public GameObject		m_lppet2			= null;
	public GameObject		m_lppet3			= null;
	public GameObject		m_lppet4			= null;
	public GameObject		m_lppet5			= null;
	public GameObject		m_lppet6			= null;
	public GameObject		m_lppet7			= null;

	//升级播放特效时的遮罩..
	public GameObject 		m_panEnd = null;

	//其他数据..
	public GameObject		m_preWnd			= null;
	static uint m_nPetTemplateID = 0;
	static int m_iPetUp = 0;
	static GameObject m_PetModel;

	//升级特效相关..
	public GameObject m_LevelEffect0 = null;
	public GameObject m_LevelEffect1 = null;
	public GameObject m_LevelEffect2 = null;
	public GameObject m_LevelEffect3 = null;
	public GameObject m_LevelEffect4 = null;
	public GameObject m_LevelEffect5 = null;
	public GameObject m_LevelEffect6 = null;
	public GameObject m_LevelEffect7 = null;
	
	public int m_iLevelUpingSetp = 0;
	
	public GameObject m_LevelUpHitEffect = null;
	public GameObject m_LevelFinalEffect = null;

	//升级时经验条变动效果..
	public int m_iFirstCost;
	public UInt64 m_ulFirstGiveExp;
	public UInt64 m_ulFirstNeedExp;
	public int m_iFirstLevel;
	public float m_fFirstLineValue;
	
	public int m_iLastCost;
	public UInt64 m_ulLastGiveExp;
	public UInt64 m_ulLastNeedExp;
	public int m_iLastLevel;
	public float m_fLastLineValue;
	public bool m_bBeginLineMove;
	const float m_fPerMoveSpeed = 2.0f;

	//选完升级材料的属性闪烁效果..
	public int m_iFlashBz = 0; //0不需要闪烁,1 需要闪烁, 2需要还原...
	public int m_iFLevel1 = 0;
	public int m_iFExp1 = 0;
	public float m_fFExp1 = 0.0f;
	public int m_iAttack1 = 0;
	public int m_iDef1 = 0;
	public int m_iHp1 = 0;
	public int m_iFLevel2 = 0;
	public int m_iFExp2 = 0;
	public float m_fFExp2 = 0.0f;
	public int m_iAttack2 = 0;
	public int m_iDef2 = 0;
	public int m_iHp2 = 0;
	public int m_iAddAttack = 0;
	public int m_iAddDef = 0;
	public int m_iAddHp = 0;
	float fFlashTime = 0.0f;
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	bool bHasLoadEffect = false;
	float fHasPlayEffectTime = 0.0f;
	
	bool bHasLoadLevelUpEffect0 = false;
	float fHasPlayLevelUpEffectTime0 = 0.0f;
	
	bool bHasLoadLevelUpEffect1 = false;
	float fHasPlayLevelUpEffectTime1 = 0.0f;
	
	bool bHasLoadLevelUpEffect2 = false;
	float fHasPlayLevelUpEffectTime2 = 0.0f;
	
	bool bHasLoadLevelUpEffect3 = false;
	float fHasPlayLevelUpEffectTime3 = 0.0f;
	
	bool bHasLoadLevelUpEffect4 = false;
	float fHasPlayLevelUpEffectTime4 = 0.0f;
	
	bool bHasLoadLevelUpEffect5 = false;
	float fHasPlayLevelUpEffectTime5 = 0.0f;
	
	bool bHasLoadLevelUpEffect6 = false;
	float fHasPlayLevelUpEffectTime6 = 0.0f;
	
	bool bHasLoadLevelUpEffect7 = false;
	float fHasPlayLevelUpEffectTime7 = 0.0f;
	
	int iPlayEffectNum = 0;
	
	bool bHasLoadHitEffect = false;
	float fHasPlayHitEffectTime = 0.0f;
	
	bool bHasLoadFinalEffect = false;
	float fHasPlayFinalEffectTime = 0.0f;

	int iFlashSetp = 0;
	float fTxtAlpha = 1.0f;
	void Update () 
	{	
		//闪烁显示升级属性..
		if (m_iFlashBz==1)
		{
			//白色显示老属性..
			if (iFlashSetp==0)
			{
				if(m_lbLevel)
					m_lbLevel.GetComponent<UILabel>().text = "Lv." + m_iFLevel1.ToString();
				if (m_expBar!=null)
					m_expBar.GetComponent<UISlider>().value = m_fFExp1;
				if(m_lbExp)
					m_lbExp.GetComponent<UILabel>().text = m_iFExp1.ToString() + "%";
				if(m_lbv3)
				{
					m_lbv3.GetComponent<UILabel>().text = m_iAttack1.ToString();
					m_lbv3.GetComponent<UILabel>().color = Color.white;
				}
				if(m_lbv4)
				{
					m_lbv4.GetComponent<UILabel>().text = m_iDef1.ToString();
					m_lbv4.GetComponent<UILabel>().color = Color.white;
				}
				if(m_lbv5)
				{
					m_lbv5.GetComponent<UILabel>().text = m_iHp1.ToString();
					m_lbv5.GetComponent<UILabel>().color = Color.white;
				}

				SetObjectAlpha(1.0f);
				iFlashSetp = 1;
				fFlashTime = 0.0f;
			}
			else if (iFlashSetp==1)
			{
				fFlashTime += Time.deltaTime;
				if (fFlashTime>0.0f&&fFlashTime<=0.5f)
				{
				}
				else if (fFlashTime>0.5f&&fFlashTime<=1.3f)
				{
					fTxtAlpha = 1.0f-(fFlashTime-0.5f)/0.8f;
					if (fTxtAlpha<0.0f)
						fTxtAlpha = 0.0f;
					else if (fTxtAlpha>1.0f)
						fTxtAlpha = 1.0f;
					
					SetObjectAlpha(fTxtAlpha);
				}
				else if (fFlashTime>1.3f)
				{
					iFlashSetp = 2;
					fFlashTime = 0.0f;
				}
			}
			else if (iFlashSetp==2)
			{
				if(m_lbLevel)
					m_lbLevel.GetComponent<UILabel>().text = "Lv." + m_iFLevel2.ToString();
				if (m_expBar!=null)
					m_expBar.GetComponent<UISlider>().value = m_fFExp2;
				if(m_lbExp)
					m_lbExp.GetComponent<UILabel>().text = m_iFExp2.ToString() + "%";
				if(m_lbv3)
				{
					m_lbv3.GetComponent<UILabel>().text = m_iAttack2.ToString();
					m_lbv3.GetComponent<UILabel>().color = Color.green;
				}
				if(m_lbv4)
				{
					m_lbv4.GetComponent<UILabel>().text = m_iDef2.ToString();
					m_lbv4.GetComponent<UILabel>().color = Color.green;
				}
				if(m_lbv5)
				{
					m_lbv5.GetComponent<UILabel>().text = m_iHp2.ToString();
					m_lbv5.GetComponent<UILabel>().color = Color.green;
				}

				SetObjectAlpha(0.0f);
				iFlashSetp = 3;
				fFlashTime = 0.0f;
			}
			else if (iFlashSetp==3)
			{
				fFlashTime += Time.deltaTime;
				if(fFlashTime>0.0f&&fFlashTime<=0.8f)
				{
					fTxtAlpha = fFlashTime*(1.0f/0.8f);
					if (fTxtAlpha<0.0f)
						fTxtAlpha = 0.0f;
					else if (fTxtAlpha>1.0f)
						fTxtAlpha = 1.0f;
					
					SetObjectAlpha(fTxtAlpha);
				}
				else if (fFlashTime>0.8f)
				{
					iFlashSetp = 4;
					fFlashTime = 0.0f;
				}
			}
			else if (iFlashSetp==4)
			{
				fFlashTime += Time.deltaTime;
				if (fFlashTime>0.0f&&fFlashTime<=0.5f)
				{
				}
				else if (fFlashTime>0.5f&&fFlashTime<=1.3f)
				{
					fTxtAlpha = 1.0f-(fFlashTime-0.5f)/0.8f;
					if (fTxtAlpha<0.0f)
						fTxtAlpha = 0.0f;
					else if (fTxtAlpha>1.0f)
						fTxtAlpha = 1.0f;
					
					SetObjectAlpha(fTxtAlpha);
				}
				else if (fFlashTime>1.3f)
				{
					iFlashSetp = 5;
					fFlashTime = 0.0f;
				}
			}
			else if (iFlashSetp==5)
			{
				if(m_lbLevel)
					m_lbLevel.GetComponent<UILabel>().text = "Lv." + m_iFLevel1.ToString();
				if (m_expBar!=null)
					m_expBar.GetComponent<UISlider>().value = m_fFExp1;
				if(m_lbExp)
					m_lbExp.GetComponent<UILabel>().text = m_iFExp1.ToString() + "%";
				if(m_lbv3)
				{
					m_lbv3.GetComponent<UILabel>().text = m_iAttack1.ToString();
					m_lbv3.GetComponent<UILabel>().color = Color.white;
				}
				if(m_lbv4)
				{
					m_lbv4.GetComponent<UILabel>().text = m_iDef1.ToString();
					m_lbv4.GetComponent<UILabel>().color = Color.white;
				}
				if(m_lbv5)
				{
					m_lbv5.GetComponent<UILabel>().text = m_iHp1.ToString();
					m_lbv5.GetComponent<UILabel>().color = Color.white;
				}
				
				SetObjectAlpha(0.0f);
				iFlashSetp = 6;
				fFlashTime = 0.0f;
			}
			else if (iFlashSetp==6)
			{
				fFlashTime += Time.deltaTime;
				if(fFlashTime>0.0f&&fFlashTime<=0.8f)
				{
					fTxtAlpha = fFlashTime*(1.0f/0.8f);
					if (fTxtAlpha<0.0f)
						fTxtAlpha = 0.0f;
					else if (fTxtAlpha>1.0f)
						fTxtAlpha = 1.0f;
					
					SetObjectAlpha(fTxtAlpha);
				}
				else if (fFlashTime>0.8f)
				{
					iFlashSetp = 0;
					fFlashTime = 0.0f;
				}
			}
		}
		else if (m_iFlashBz==2)
		{
			OnClickPetLevelUpOK();
		}



		if (m_iLevelUpingSetp>0)
		{
			if (m_panEnd)
				m_panEnd.SetActive(true);
		}
		else
		{
			if (m_panEnd)
				m_panEnd.SetActive(false);
		}
		
		//宠物升级特效加载..
		if (m_iLevelUpingSetp==1)
		{
			LoadLevelEffect();
			m_iLevelUpingSetp = 2;
		}
		
		//宠物升级特效播放..
		if (bHasLoadLevelUpEffect0)
		{
			fHasPlayLevelUpEffectTime0 += Time.deltaTime;
			if (fHasPlayLevelUpEffectTime0>=1.0f)
			{
				bHasLoadLevelUpEffect0 = false;
				fHasPlayLevelUpEffectTime0 = 0.0f;

				iPlayEffectNum = sdNewPetMgr.Instance.GetPetLevelupDBIDNum();
				sdNewPetMgr.Instance.ResetPetLevelUpDBID();
				ReflashPetLevelIcon(false);
				SetLastLevelUpValue();
				if (iPlayEffectNum==1)
				{
					iPlayEffectNum=0;
					m_iLevelUpingSetp = 3;
				}
			}
		}
		if (bHasLoadLevelUpEffect1)
		{
			fHasPlayLevelUpEffectTime1 += Time.deltaTime;
			if (fHasPlayLevelUpEffectTime1>=1.0f)
			{
				bHasLoadLevelUpEffect1 = false;
				fHasPlayLevelUpEffectTime1 = 0.0f;

				if (iPlayEffectNum==2)
				{
					iPlayEffectNum=0;
					m_iLevelUpingSetp = 3;
				}
			}
		}
		if (bHasLoadLevelUpEffect2)
		{
			fHasPlayLevelUpEffectTime2 += Time.deltaTime;
			if (fHasPlayLevelUpEffectTime2>=1.0f)
			{
				bHasLoadLevelUpEffect2 = false;
				fHasPlayLevelUpEffectTime2 = 0.0f;

				if (iPlayEffectNum==3)
				{
					iPlayEffectNum=0;
					m_iLevelUpingSetp = 3;
				}
			}
		}
		if (bHasLoadLevelUpEffect3)
		{
			fHasPlayLevelUpEffectTime3 += Time.deltaTime;
			if (fHasPlayLevelUpEffectTime3>=1.0f)
			{
				bHasLoadLevelUpEffect3 = false;
				fHasPlayLevelUpEffectTime3 = 0.0f;

				if (iPlayEffectNum==4)
				{
					iPlayEffectNum=0;
					m_iLevelUpingSetp = 3;
				}
			}
		}
		if (bHasLoadLevelUpEffect4)
		{
			fHasPlayLevelUpEffectTime4 += Time.deltaTime;
			if (fHasPlayLevelUpEffectTime4>=1.0f)
			{
				bHasLoadLevelUpEffect4 = false;
				fHasPlayLevelUpEffectTime4 = 0.0f;

				if (iPlayEffectNum==5)
				{
					iPlayEffectNum=0;
					m_iLevelUpingSetp = 3;
				}
			}
		}
		if (bHasLoadLevelUpEffect5)
		{
			fHasPlayLevelUpEffectTime5 += Time.deltaTime;
			if (fHasPlayLevelUpEffectTime5>=1.0f)
			{
				bHasLoadLevelUpEffect5 = false;
				fHasPlayLevelUpEffectTime5 = 0.0f;

				if (iPlayEffectNum==6)
				{
					iPlayEffectNum=0;
					m_iLevelUpingSetp = 3;
				}
			}
		}
		if (bHasLoadLevelUpEffect6)
		{
			fHasPlayLevelUpEffectTime6 += Time.deltaTime;
			if (fHasPlayLevelUpEffectTime6>=1.0f)
			{
				bHasLoadLevelUpEffect6 = false;
				fHasPlayLevelUpEffectTime6 = 0.0f;

				if (iPlayEffectNum==7)
				{
					iPlayEffectNum=0;
					m_iLevelUpingSetp = 3;
				}
			}
		}
		if (bHasLoadLevelUpEffect7)
		{
			fHasPlayLevelUpEffectTime7 += Time.deltaTime;
			if (fHasPlayLevelUpEffectTime7>=1.0f)
			{
				bHasLoadLevelUpEffect7 = false;
				fHasPlayLevelUpEffectTime7 = 0.0f;

				if (iPlayEffectNum==8)
				{
					iPlayEffectNum=0;
					m_iLevelUpingSetp = 3;
				}
			}
		}
		
		//播放升级hit动画..
		if (m_iLevelUpingSetp==3)
		{
			LoadLevelUpHitEffect();
			m_iLevelUpingSetp = 4;
		}
		
		if (bHasLoadHitEffect)
		{
			fHasPlayHitEffectTime += Time.deltaTime;
			if (fHasPlayHitEffectTime>=1.0f)
			{
				bHasLoadHitEffect = false;
				fHasPlayHitEffectTime = 0.0f;
				
				m_iLevelUpingSetp = 5;
			}
		}
		
		//经验条走动效果..
		if (m_iLevelUpingSetp == 5 && m_bBeginLineMove==true)
		{
			if (m_panEnd)
				m_panEnd.SetActive(true);
			
			float fTime = Time.deltaTime;
			float fMove = fTime*m_fPerMoveSpeed;
			
			if (m_iLastLevel>m_iFirstLevel)
			{
				if (m_fFirstLineValue<1.0f)
				{
					m_fFirstLineValue += fMove;
					if (m_fFirstLineValue>=1.0f)
						m_fFirstLineValue = 1.0f;
					
					if (m_expBar!=null)
						m_expBar.GetComponent<UISlider>().value = m_fFirstLineValue;

					if(m_lbExp)
					{
						float fValue = m_fFirstLineValue*100.0f;
						int iValue = (int)fValue;
						m_lbExp.GetComponent<UILabel>().text = iValue.ToString() + "%";
					}
				}
				else if (m_fFirstLineValue>=1.0f)
				{
					m_fFirstLineValue = 0.0f;
					m_iFirstLevel++;
					
					if(m_lbLevel)
						m_lbLevel.GetComponent<UILabel>().text = "Lv." + m_iFirstLevel.ToString();
					
					if (m_expBar!=null)
						m_expBar.GetComponent<UISlider>().value = m_fFirstLineValue;
					
					if(m_lbExp)
					{
						float fValue = m_fFirstLineValue*100.0f;
						int iValue = (int)fValue;
						m_lbExp.GetComponent<UILabel>().text = iValue.ToString() + "%";
					}

					int iAttack2 = (int)sdNewPetMgr.Instance.GetPetAttCoe(m_nPetTemplateID, m_iPetUp , m_iFirstLevel);
					int iDef2 = (int)sdNewPetMgr.Instance.GetPetDefCoe(m_nPetTemplateID, m_iPetUp , m_iFirstLevel);
					int iHp2 = (int)sdNewPetMgr.Instance.GetPetHPCoe(m_nPetTemplateID, m_iPetUp , m_iFirstLevel);
					//攻击..
					iAttack2 = iAttack2 + m_iAddAttack;
					//防御..
					iDef2 = iDef2 + m_iAddDef;
					//生命..
					iHp2 = iHp2 + m_iAddHp;
					if(m_lbv3)
						m_lbv3.GetComponent<UILabel>().text = iAttack2.ToString();
					if(m_lbv4)
						m_lbv4.GetComponent<UILabel>().text = iDef2.ToString();
					if(m_lbv5)
						m_lbv5.GetComponent<UILabel>().text = iHp2.ToString();
				}
			}
			else if (m_iLastLevel==m_iFirstLevel)
			{
				if (m_fFirstLineValue<m_fLastLineValue)
				{
					m_fFirstLineValue += fMove;
					if (m_fFirstLineValue>=m_fLastLineValue)
						m_fFirstLineValue = m_fLastLineValue;
					
					if (m_expBar!=null)
						m_expBar.GetComponent<UISlider>().value = m_fFirstLineValue;
					
					if(m_lbExp)
					{
						float fValue = m_fFirstLineValue*100.0f;
						int iValue = (int)fValue;
						m_lbExp.GetComponent<UILabel>().text = iValue.ToString() + "%";
					}
				}
				else
				{
					m_iLevelUpingSetp = 6;
				}
			}
			
			float fMoveLength = m_fLastLineValue + (m_iLastLevel-m_iFirstLevel-1)*1.0f + (1.0f - m_fFirstLineValue);
			float fCount = fMoveLength/fMove;
			int iCount = (int)fCount;
			if (iCount<=0)
				iCount = 1;
			
			int iLost = (int)(m_iFirstCost/iCount);
			if (iLost<1)
				iLost = 1;
			m_iFirstCost = m_iFirstCost -iLost;
			if (m_iFirstCost<m_iLastCost)
				m_iFirstCost = m_iLastCost;
			
			UInt64 uiLost = m_ulFirstGiveExp/(UInt64)iCount;
			if (uiLost<1)
				uiLost = 1;
			if (m_ulFirstGiveExp>=uiLost)
				m_ulFirstGiveExp = m_ulFirstGiveExp - uiLost;
			else
				m_ulFirstGiveExp = m_ulLastGiveExp;
			
			uiLost = m_ulFirstNeedExp/(UInt64)iCount;
			if (uiLost<1)
				uiLost = 1;
			if (m_ulFirstNeedExp>=uiLost)
			{
				m_ulFirstNeedExp = m_ulFirstNeedExp - uiLost;
				if (m_ulFirstNeedExp<m_ulLastNeedExp)
					m_ulFirstNeedExp = m_ulLastNeedExp;
			}
			else
			{
				m_ulFirstNeedExp = m_ulLastNeedExp;
			}
			
			if (m_lpCostVTxt)
				m_lpCostVTxt.GetComponent<UILabel>().text = m_iFirstCost.ToString();
		
			if (m_lpGetExpVTxt)
				m_lpGetExpVTxt.GetComponent<UILabel>().text = m_ulFirstGiveExp.ToString();
			
			if (m_lpMaxNeedExpVTxt)
				m_lpMaxNeedExpVTxt.GetComponent<UILabel>().text = m_ulFirstNeedExp.ToString();
		}
		
		if (m_iLevelUpingSetp == 6)
		{
			ReflashPetLevelUI();
			m_iLevelUpingSetp = 0;
			m_bBeginLineMove = false;
		}
	}

	void OnClick()
    {
	}

	public GameObject GetPetModel()
	{
		return m_PetModel;
	}

	public void SetObjectAlpha(float fAlpha)
	{
		if(m_lbLevel)
			m_lbLevel.GetComponent<UILabel>().alpha = fAlpha;

		if (m_expBar!=null)
			m_expBar.transform.FindChild("Foreground").gameObject.GetComponent<UISprite>().alpha = fAlpha;

		if(m_lbExp)
			m_lbExp.GetComponent<UILabel>().alpha = fAlpha;

		if(m_lbv3)
			m_lbv3.GetComponent<UILabel>().alpha = fAlpha;

		if(m_lbv4)
			m_lbv4.GetComponent<UILabel>().alpha = fAlpha;

		if(m_lbv5)
			m_lbv5.GetComponent<UILabel>().alpha = fAlpha;
	}
	
	public void ActivePetLevelPnl(GameObject PreWnd, UInt64 uPetID)
	{
		m_preWnd = PreWnd;
		m_uuDBID = uPetID;

		//DestroyPetModel();
		LoadPetModel();
		ReflashPetLevelUI();
		ResetPropParams();
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
			m_iPetUp = Info.m_iUp;
			DestroyPetModel();
			
			// 载入模型..
			ResLoadParams param = new ResLoadParams();
			param.pos = new Vector3(0.0f,-210.0f,-200.0f);
			param.rot.Set(0,180.0f,0,0);
			param.scale = new Vector3(180.0f,180.0f,180.0f);
			string strPath = Info.m_strRes.Replace(".prefab","_UI.prefab");
			sdResourceMgr.Instance.LoadResource(strPath, PetLoadInstantiate, param);
		}
	}

	public void ResetPropParams()
	{
		//升级特效参数初始化..
		if( m_LevelEffect0 )
			Destroy(m_LevelEffect0);
		m_LevelEffect0 = null;
		
		if( m_LevelEffect1 )
			Destroy(m_LevelEffect1);
		m_LevelEffect1 = null;
		
		if( m_LevelEffect2 )
			Destroy(m_LevelEffect2);
		m_LevelEffect2 = null;
		
		if( m_LevelEffect3 )
			Destroy(m_LevelEffect3);
		m_LevelEffect3 = null;
		
		if( m_LevelEffect4 )
			Destroy(m_LevelEffect4);
		m_LevelEffect4 = null;
		
		if( m_LevelEffect5 )
			Destroy(m_LevelEffect5);
		m_LevelEffect5 = null;
		
		if( m_LevelEffect6 )
			Destroy(m_LevelEffect6);
		m_LevelEffect6 = null;
		
		if( m_LevelEffect7 )
			Destroy(m_LevelEffect7);
		m_LevelEffect7 = null;
		
		bHasLoadLevelUpEffect0 = false;
		fHasPlayLevelUpEffectTime0 = 0.0f;
		
		bHasLoadLevelUpEffect1 = false;
		fHasPlayLevelUpEffectTime1 = 0.0f;
		
		bHasLoadLevelUpEffect2 = false;
		fHasPlayLevelUpEffectTime2 = 0.0f;
		
		bHasLoadLevelUpEffect3 = false;
		fHasPlayLevelUpEffectTime3 = 0.0f;
		
		bHasLoadLevelUpEffect4 = false;
		fHasPlayLevelUpEffectTime4 = 0.0f;
		
		bHasLoadLevelUpEffect5 = false;
		fHasPlayLevelUpEffectTime5 = 0.0f;
		
		bHasLoadLevelUpEffect6 = false;
		fHasPlayLevelUpEffectTime6 = 0.0f;
		
		bHasLoadLevelUpEffect7 = false;
		fHasPlayLevelUpEffectTime7 = 0.0f;
		
		m_iLevelUpingSetp = 0;
		
		if( m_LevelUpHitEffect )
			Destroy(m_LevelUpHitEffect);
		m_LevelUpHitEffect = null;
		
		bHasLoadHitEffect = false;
		fHasPlayHitEffectTime = 0.0f;
		
		if( m_LevelFinalEffect )
			Destroy(m_LevelFinalEffect);
		m_LevelFinalEffect = null;
		
		bHasLoadFinalEffect = false;
		fHasPlayFinalEffectTime = 0.0f;
		
		m_bBeginLineMove = false;
		if (m_panEnd)
			m_panEnd.SetActive(false);
	}
	
	public void PetLoadInstantiate(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) return;
		m_PetModel = GameObject.Instantiate(obj) as GameObject;
		m_PetModel.name = "PetLevelModel";
		m_PetModel.transform.parent = UnityEngine.GameObject.Find("levelModelLeft").transform;
		m_PetModel.transform.localPosition = param.pos;
		m_PetModel.SetActive(true);
		//播放一下idle01的动画..
		sdConfDataMgr.Instance().OnModelPlayIdleAnm(m_PetModel);
	}
	
	void OnDrag(Vector2 delta)
	{
		if( gameObject.name == "levelModelLeft" )
		{
			if( m_PetModel == null ) return;
			m_PetModel.transform.Rotate(0,-delta.x/2.0f,0);
		}
	}
	
	public void ReflashPetLevelUI()
	{
		SClientPetInfo Info = null;
		if (m_uuDBID!=UInt64.MaxValue)
		{
			Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;
		}
		//左侧UI显示..
		if (m_picType)
		{
			if (Info.m_iBaseJob==1)
			{
				m_picType.GetComponent<UISprite>().spriteName = "IPzs";
				m_picType.SetActive(true);
			}
			else if (Info.m_iBaseJob==2)
			{
				m_picType.GetComponent<UISprite>().spriteName = "IPfs";
				m_picType.SetActive(true);
			}
			else if (Info.m_iBaseJob==3)
			{
				m_picType.GetComponent<UISprite>().spriteName = "IPyx";
				m_picType.SetActive(true);
			}
			else if (Info.m_iBaseJob==4)
			{
				m_picType.GetComponent<UISprite>().spriteName = "IPms";
				m_picType.SetActive(true);
			}
			else
			{
				m_picType.SetActive(false);
			}
		}

		if(m_lbName)
		{
			sdNewPetMgr.SetLabelColorByAbility(Info.m_iAbility, m_lbName);
			m_lbName.GetComponent<UILabel>().text = Info.m_strName;
		}
		
		if(m_lbStrong)
		{
			if (Info.m_iUp>0)
			{
				m_lbStrong.GetComponent<UILabel>().text = "+" + Info.m_iUp.ToString();
				m_lbStrong.SetActive(true);
			}
			else
			{
				m_lbStrong.SetActive(false);
			}
		}
		
		if(m_lbValue)
		{
			if (m_uuDBID!=UInt64.MaxValue)
			{
				int iTemp = sdConfDataMgr.Instance().GetPetScore(m_uuDBID);
				m_lbValue.GetComponent<UILabel>().text = iTemp.ToString();
			}
		}
		//左侧经验条..
		ReflashExpLineUI();
		//基础属性..
		if(m_lbv3)
			m_lbv3.GetComponent<UILabel>().text = Info.m_CurProperty.m_iAtkDmgMax.ToString();
		if(m_lbv4)
			m_lbv4.GetComponent<UILabel>().text = Info.m_CurProperty.m_iDef.ToString();
		if(m_lbv5)
			m_lbv5.GetComponent<UILabel>().text = Info.m_CurProperty.m_iMaxHP.ToString();
		//刷新升级材料ICON..
		ReflashPetLevelIcon(true);
		ResetPetLevelCostAndExp();
	}
	
	public void ReflashExpLineUI()
	{
		SClientPetInfo Info = null;
		if (m_uuDBID!=UInt64.MaxValue)
		{
			Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;
		}

		if(m_lbLevel)
			m_lbLevel.GetComponent<UILabel>().text = "Lv." + Info.m_iLevel.ToString();
		
		UInt64 myExp = Info.m_uuExperience;
		int iAbility = Info.m_iAbility;
		int iLevel = Info.m_iLevel;
		UInt64 uuLevelUPExp = sdNewPetMgr.Instance.GetPetCurLevelNeedExp(Info.m_uiTemplateID, iAbility, iLevel);

		float fValue = 1.0f;
		if (uuLevelUPExp>0)
			fValue = (float)myExp/(float)uuLevelUPExp;
		if (fValue>1.0f)
			fValue = 1.0f;
		
		if (m_expBar!=null)
			m_expBar.GetComponent<UISlider>().value = fValue;
		
		if(m_lbExp)
		{
			fValue = fValue*100.0f;
			int iValue = (int)fValue;
			m_lbExp.GetComponent<UILabel>().text = iValue.ToString() + "%";
		}
	}
	
	public void ReflashPetLevelIcon(bool bNeedResetParam)
	{
		UInt64 uuID = UInt64.MaxValue;
		if (m_lppet0)
		{
			sdUIPetLevelUpIcon icon = m_lppet0.GetComponent<sdUIPetLevelUpIcon>();
			if (icon)
			{
				uuID = GetPetLevelPetID(1);
				icon.SetDBIDAndReflashUI(uuID);
			}
		}
		
		if (m_lppet1)
		{
			sdUIPetLevelUpIcon icon = m_lppet1.GetComponent<sdUIPetLevelUpIcon>();
			if (icon)
			{
				uuID = GetPetLevelPetID(2);
				icon.SetDBIDAndReflashUI(uuID);
			}
		}
		
		if (m_lppet2)
		{
			sdUIPetLevelUpIcon icon = m_lppet2.GetComponent<sdUIPetLevelUpIcon>();
			if (icon)
			{
				uuID = GetPetLevelPetID(3);
				icon.SetDBIDAndReflashUI(uuID);
			}
		}
		
		if (m_lppet3)
		{
			sdUIPetLevelUpIcon icon = m_lppet3.GetComponent<sdUIPetLevelUpIcon>();
			if (icon)
			{
				uuID = GetPetLevelPetID(4);
				icon.SetDBIDAndReflashUI(uuID);
			}
		}
		
		if (m_lppet4)
		{
			sdUIPetLevelUpIcon icon = m_lppet4.GetComponent<sdUIPetLevelUpIcon>();
			if (icon)
			{
				uuID = GetPetLevelPetID(5);
				icon.SetDBIDAndReflashUI(uuID);
			}
		}
		
		if (m_lppet5)
		{
			sdUIPetLevelUpIcon icon = m_lppet5.GetComponent<sdUIPetLevelUpIcon>();
			if (icon)
			{
				uuID = GetPetLevelPetID(6);
				icon.SetDBIDAndReflashUI(uuID);
			}
		}
		
		if (m_lppet6)
		{
			sdUIPetLevelUpIcon icon = m_lppet6.GetComponent<sdUIPetLevelUpIcon>();
			if (icon)
			{
				uuID = GetPetLevelPetID(7);
				icon.SetDBIDAndReflashUI(uuID);
			}
		}
		
		if (m_lppet7)
		{
			sdUIPetLevelUpIcon icon = m_lppet7.GetComponent<sdUIPetLevelUpIcon>();
			if (icon)
			{
				uuID = GetPetLevelPetID(8);
				icon.SetDBIDAndReflashUI(uuID);
			}
		}

		//这里刷新一下需不需要闪烁显示升级属性..
		if (bNeedResetParam)
			ReflashPetLevelUpParam();
	}
	
	public void ResetPetLevelCostAndExp()
	{
		//消耗金钱，获得经验UI显示..
		SClientPetInfo Info = null;
		if (m_uuDBID==UInt64.MaxValue)
			return;
		
		Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
		if (Info == null)
			return;
		
		UInt64 giveExp = GetPetLevelUpGiveExp();
		UInt64 maxExp = sdNewPetMgr.Instance.GetPetLevelMaxAllNeedExp(Info.m_uiTemplateID, Info.m_iAbility);
		UInt64 nowExp = GetNowPetNowLevelExp(Info.m_uiTemplateID, Info.m_iAbility, Info.m_iLevel);
		UInt64 iCost = GetPetLevelUpCost();
		nowExp = nowExp + Info.m_uuExperience;
		if (nowExp>=maxExp)
			maxExp = 0;
		else
			maxExp = maxExp - nowExp;
		
		if (m_lpCostVTxt)
			m_lpCostVTxt.GetComponent<UILabel>().text = iCost.ToString();
		
		if (m_lpGetExpVTxt)
			m_lpGetExpVTxt.GetComponent<UILabel>().text = giveExp.ToString();
		
		if (m_lpMaxNeedExpVTxt)
			m_lpMaxNeedExpVTxt.GetComponent<UILabel>().text = maxExp.ToString();
	}
	
	public void SetFirstLevelUpValue()
	{
		SClientPetInfo Info = null;
		if (m_uuDBID==UInt64.MaxValue)
			return;
		
		Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
		if (Info == null)
			return;
		
		UInt64 giveExp = GetPetLevelUpGiveExp();
		UInt64 maxExp = sdNewPetMgr.Instance.GetPetLevelMaxAllNeedExp(Info.m_uiTemplateID, Info.m_iAbility);
		UInt64 nowExp = GetNowPetNowLevelExp(Info.m_uiTemplateID, Info.m_iAbility, Info.m_iLevel);
		UInt64 iCost = GetPetLevelUpCost();
		nowExp = nowExp + Info.m_uuExperience;
		if (nowExp>=maxExp)
			maxExp = 0;
		else
			maxExp = maxExp - nowExp;
		
		m_iFirstCost = (int)iCost;
		m_ulFirstGiveExp = giveExp;
		m_ulFirstNeedExp = maxExp;
		m_iFirstLevel = Info.m_iLevel;
		
		UInt64 myExp = Info.m_uuExperience;
		int iAbility = Info.m_iAbility;
		int iLevel = Info.m_iLevel;
		int iIndex = iAbility*10000+iLevel;
		string strExp = sdConfDataMgr.Instance().GetPetLevelsValueByStringKey(iIndex, "Exp");
		UInt64 uuLevelUPExp = UInt64.Parse(strExp);
		float fValue = 1.0f;
		if (uuLevelUPExp>0)
			fValue = (float)myExp/(float)uuLevelUPExp;
		
		if (fValue>1.0f)
			fValue = 1.0f;
		
		m_fFirstLineValue = fValue;
		m_bBeginLineMove = false;
	}
	
	public void SetLastLevelUpValue()
	{
		SClientPetInfo Info = null;
		if (m_uuDBID==UInt64.MaxValue)
			return;
		
		Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
		if (Info == null)
			return;
		
		UInt64 giveExp = GetPetLevelUpGiveExp();
		UInt64 maxExp = sdNewPetMgr.Instance.GetPetLevelMaxAllNeedExp(Info.m_uiTemplateID, Info.m_iAbility);
		UInt64 nowExp = GetNowPetNowLevelExp(Info.m_uiTemplateID, Info.m_iAbility, Info.m_iLevel);
		UInt64 iCost = GetPetLevelUpCost();
		nowExp = nowExp + Info.m_uuExperience;
		if (nowExp>=maxExp)
			maxExp = 0;
		else
			maxExp = maxExp - nowExp;
		
		m_iLastCost = (int)iCost;
		m_ulLastGiveExp = giveExp;
		m_ulLastNeedExp = maxExp;
		m_iLastLevel = Info.m_iLevel;
		
		UInt64 myExp = Info.m_uuExperience;
		int iAbility = Info.m_iAbility;
		int iLevel = Info.m_iLevel;
		int iIndex = iAbility*10000+iLevel;
		string strExp = sdConfDataMgr.Instance().GetPetLevelsValueByStringKey(iIndex, "Exp");
		UInt64 uuLevelUPExp = UInt64.Parse(strExp);
		float fValue = 1.0f;
		if (uuLevelUPExp>0)
			fValue = (float)myExp/(float)uuLevelUPExp;
		
		if (fValue>1.0f)
			fValue = 1.0f;
		
		m_fLastLineValue = fValue;
		m_bBeginLineMove = true;
	}
	
	public UInt64 GetPetLevelPetID(int index)
	{
		int iNum = 0;
		for (int x = 0; x < 8; ++x)
		{
			if (sdNewPetMgr.Instance.m_uuPetLevelSelectID[x]!=UInt64.MaxValue)
			{
				iNum++;
				if (index==iNum)
				{
					return sdNewPetMgr.Instance.m_uuPetLevelSelectID[x];
				}
			}
		}
		
		return UInt64.MaxValue;
	}
	
	//当前材料提供的经验..
	public UInt64 GetPetLevelUpGiveExp()
	{
		UInt64 uuExp = 0;
		for (int x = 0; x < 8; ++x)
		{
			if (sdNewPetMgr.Instance.m_uuPetLevelSelectID[x]!=UInt64.MaxValue)
			{
				SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(sdNewPetMgr.Instance.m_uuPetLevelSelectID[x]);
				if (Info == null)
					continue;
				
				int iAbility = Info.m_iAbility;
				int iLevel = Info.m_iLevel;
				UInt64 uuTemp = sdNewPetMgr.Instance.GetPetCurLevelGiveExp(Info.m_uiTemplateID, iAbility, iLevel);
				uuExp += uuTemp;
			}
		}
		
		return uuExp;
	}
	
	//当前宠物当前等级已经有的经验..
	public UInt64 GetNowPetNowLevelExp(uint uiTemplateID, int iAbility, int iLevel)
	{
		UInt64 uuExp = 0;
		if (iLevel>1)
		{
			for (int i=1;i<iLevel; i++)
			{
				UInt64 uuTemp = sdNewPetMgr.Instance.GetPetCurLevelNeedExp(uiTemplateID, iAbility, i);
				uuExp += uuTemp;
			}
		}
		
		return uuExp;
	}

	//给定总经验值求宠物的等级..
	public int GetNowExpPetLevel(uint uiTemplateID, int iAbility, UInt64 uuExp)
	{
		int iLevel = 1;
		for (int i=1;i<=50; i++)
		{
			if (i<50)
			{
				UInt64 uuTemp1 = GetNowPetNowLevelExp(uiTemplateID, iAbility, i);
				UInt64 uuTemp2 = GetNowPetNowLevelExp(uiTemplateID, iAbility, i+1);

				if (uuExp>=uuTemp1&&uuExp<uuTemp2)
				{
					iLevel = i;
					break;
				}
			}
			else
			{
				UInt64 uuTemp1 = GetNowPetNowLevelExp(uiTemplateID, iAbility, i);
				if (uuExp>=uuTemp1)
				{
					iLevel = i;
					break;
				}
			}
		}

		return iLevel;
	}

	//给定总经验值求当前等级下的经验值..
	public UInt64 GetNowExpPetCurExp(uint uiTemplateID, int iAbility, UInt64 uuExp)
	{
		UInt64 uuCurExp = 0;
		for (int i=1;i<=50; i++)
		{
			if (i<50)
			{
				UInt64 uuTemp1 = GetNowPetNowLevelExp(uiTemplateID, iAbility, i);
				UInt64 uuTemp2 = GetNowPetNowLevelExp(uiTemplateID, iAbility, i+1);
				
				if (uuExp>=uuTemp1&&uuExp<uuTemp2)
				{
					uuCurExp = uuExp - uuTemp1;
					break;
				}
			}
			else
			{
				UInt64 uuTemp1 = GetNowPetNowLevelExp(uiTemplateID, iAbility, i);
				if (uuExp>=uuTemp1)
				{
					uuCurExp = 0;
					break;
				}
			}
		}
		
		return uuCurExp;
	}

	
	//当前材料需要的金钱..
	public UInt64 GetPetLevelUpCost()
	{
		UInt64 iCost = 0;

		SClientPetInfo InfoPet = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
		if (InfoPet != null)
		{
			for (int x = 0; x < 8; ++x)
			{
				if (sdNewPetMgr.Instance.m_uuPetLevelSelectID[x]!=UInt64.MaxValue)
				{
					SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(sdNewPetMgr.Instance.m_uuPetLevelSelectID[x]);
					if (Info == null)
						continue;
					
					int iAbility = Info.m_iAbility;
					int iLevel = Info.m_iLevel;
					UInt64 iTemp = sdNewPetMgr.Instance.GetPetAEatPetBNeedMoney(InfoPet.m_uiTemplateID, iAbility, iLevel);
					iCost += iTemp;
				}
			}
		}
		
		return iCost;
	}

	public void OnClickPetLevelUpOK()
	{
		m_iFlashBz = 0;
		fFlashTime = 0.0f;
		
		if(m_lbLevel)
			m_lbLevel.GetComponent<UILabel>().text = "Lv." + m_iFLevel1.ToString();
		if (m_expBar!=null)
			m_expBar.GetComponent<UISlider>().value = m_fFExp1;
		if(m_lbExp)
			m_lbExp.GetComponent<UILabel>().text = m_iFExp1.ToString() + "%";
		if(m_lbv3)
		{
			m_lbv3.GetComponent<UILabel>().text = m_iAttack1.ToString();
			m_lbv3.GetComponent<UILabel>().color = Color.white;
		}
		if(m_lbv4)
		{
			m_lbv4.GetComponent<UILabel>().text = m_iDef1.ToString();
			m_lbv4.GetComponent<UILabel>().color = Color.white;
		}
		if(m_lbv5)
		{
			m_lbv5.GetComponent<UILabel>().text = m_iHp1.ToString();
			m_lbv5.GetComponent<UILabel>().color = Color.white;
		}
		
		SetObjectAlpha(1.0f);
	}
	
	public void SendPetLevelUpMsg()
	{
		OnClickPetLevelUpOK();
		if (m_uuDBID!=UInt64.MaxValue)
		{
			sdPetMsg.Send_CS_PET_GET_EXP_RPT(m_uuDBID);
		}
	}
	
	public void SetPetModelVisible(bool bShow)
	{
		if (m_PetModel)
			m_PetModel.SetActive(bShow);
	}

	public void LoadLevelEffect()
	{
		//先销毁特效..
		if( m_LevelEffect0 )
			Destroy(m_LevelEffect0);
		m_LevelEffect0 = null;
		
		if( m_LevelEffect1 )
			Destroy(m_LevelEffect1);
		m_LevelEffect1 = null;
		
		if( m_LevelEffect2 )
			Destroy(m_LevelEffect2);
		m_LevelEffect2 = null;
		
		if( m_LevelEffect3 )
			Destroy(m_LevelEffect3);
		m_LevelEffect3 = null;
		
		if( m_LevelEffect4 )
			Destroy(m_LevelEffect4);
		m_LevelEffect4 = null;
		
		if( m_LevelEffect5 )
			Destroy(m_LevelEffect5);
		m_LevelEffect5 = null;
		
		if( m_LevelEffect6 )
			Destroy(m_LevelEffect6);
		m_LevelEffect6 = null;
		
		if( m_LevelEffect7 )
			Destroy(m_LevelEffect7);
		m_LevelEffect7 = null;
		
		bHasLoadLevelUpEffect0 = false;
		fHasPlayLevelUpEffectTime0 = 0.0f;
		
		bHasLoadLevelUpEffect1 = false;
		fHasPlayLevelUpEffectTime1 = 0.0f;
		
		bHasLoadLevelUpEffect2 = false;
		fHasPlayLevelUpEffectTime2 = 0.0f;
		
		bHasLoadLevelUpEffect3 = false;
		fHasPlayLevelUpEffectTime3 = 0.0f;
		
		bHasLoadLevelUpEffect4 = false;
		fHasPlayLevelUpEffectTime4 = 0.0f;
		
		bHasLoadLevelUpEffect5 = false;
		fHasPlayLevelUpEffectTime5 = 0.0f;
		
		bHasLoadLevelUpEffect6 = false;
		fHasPlayLevelUpEffectTime6 = 0.0f;
		
		bHasLoadLevelUpEffect7 = false;
		fHasPlayLevelUpEffectTime7 = 0.0f;
		//重新加载..
		UInt64 uuID = UInt64.MaxValue;
		uuID = GetPetLevelPetID(1);
		if (uuID!=UInt64.MaxValue)
		{
			string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup_Par01.prefab"; 
			ResLoadParams param = new ResLoadParams();
			param.petInt = 1;
			sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelPrefab,param);
		}
		
		uuID = GetPetLevelPetID(2);
		if (uuID!=UInt64.MaxValue)
		{
			string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup_Par02.prefab"; 
			ResLoadParams param = new ResLoadParams();
			param.petInt = 2;
			sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelPrefab,param);
		}
		
		uuID = GetPetLevelPetID(3);
		if (uuID!=UInt64.MaxValue)
		{
			string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup_Par03.prefab"; 
			ResLoadParams param = new ResLoadParams();
			param.petInt = 3;
			sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelPrefab,param);
		}
		
		uuID = GetPetLevelPetID(4);
		if (uuID!=UInt64.MaxValue)
		{
			string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup_Par04.prefab"; 
			ResLoadParams param = new ResLoadParams();
			param.petInt = 4;
			sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelPrefab,param);
		}
		
		uuID = GetPetLevelPetID(5);
		if (uuID!=UInt64.MaxValue)
		{
			string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup_Par05.prefab"; 
			ResLoadParams param = new ResLoadParams();
			param.petInt = 5;
			sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelPrefab,param);
		}
		
		uuID = GetPetLevelPetID(6);
		if (uuID!=UInt64.MaxValue)
		{
			string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup_Par06.prefab"; 
			ResLoadParams param = new ResLoadParams();
			param.petInt = 6;
			sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelPrefab,param);
		}
		
		uuID = GetPetLevelPetID(7);
		if (uuID!=UInt64.MaxValue)
		{
			string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup_Par07.prefab"; 
			ResLoadParams param = new ResLoadParams();
			param.petInt = 7;
			sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelPrefab,param);
		}
		
		uuID = GetPetLevelPetID(8);
		if (uuID!=UInt64.MaxValue)
		{
			string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup_Par08.prefab"; 
			ResLoadParams param = new ResLoadParams();
			param.petInt = 8;
			sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelPrefab,param);
		}
	}
	
	public void OnLoadLevelPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null)
			return;
		
		if (param.petInt==1)
		{
			m_LevelEffect0 = GameObject.Instantiate(obj) as GameObject;
			m_LevelEffect0.transform.parent = gameObject.transform;
			m_LevelEffect0.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
			m_LevelEffect0.transform.localRotation = Quaternion.identity;
			m_LevelEffect0.transform.localScale	=	Vector3.one;
			
			bHasLoadLevelUpEffect0 = true;
			fHasPlayLevelUpEffectTime0 = 0.0f;
		}
		else if (param.petInt==2)
		{
			m_LevelEffect1 = GameObject.Instantiate(obj) as GameObject;
			m_LevelEffect1.transform.parent = gameObject.transform;
			m_LevelEffect1.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
			m_LevelEffect1.transform.localRotation = Quaternion.identity;
			m_LevelEffect1.transform.localScale	=	Vector3.one;
			
			bHasLoadLevelUpEffect1 = true;
			fHasPlayLevelUpEffectTime1 = 0.0f;
		}
		else if (param.petInt==3)
		{
			m_LevelEffect2 = GameObject.Instantiate(obj) as GameObject;
			m_LevelEffect2.transform.parent = gameObject.transform;
			m_LevelEffect2.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
			m_LevelEffect2.transform.localRotation = Quaternion.identity;
			m_LevelEffect2.transform.localScale	=	Vector3.one;
			
			bHasLoadLevelUpEffect2 = true;
			fHasPlayLevelUpEffectTime2 = 0.0f;
		}
		else if (param.petInt==4)
		{
			m_LevelEffect3 = GameObject.Instantiate(obj) as GameObject;
			m_LevelEffect3.transform.parent = gameObject.transform;
			m_LevelEffect3.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
			m_LevelEffect3.transform.localRotation = Quaternion.identity;
			m_LevelEffect3.transform.localScale	=	Vector3.one;
			
			bHasLoadLevelUpEffect3 = true;
			fHasPlayLevelUpEffectTime3 = 0.0f;
		}
		else if (param.petInt==5)
		{
			m_LevelEffect4 = GameObject.Instantiate(obj) as GameObject;
			m_LevelEffect4.transform.parent = gameObject.transform;
			m_LevelEffect4.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
			m_LevelEffect4.transform.localRotation = Quaternion.identity;
			m_LevelEffect4.transform.localScale	=	Vector3.one;
			
			bHasLoadLevelUpEffect4 = true;
			fHasPlayLevelUpEffectTime4 = 0.0f;
		}
		else if (param.petInt==6)
		{
			m_LevelEffect5 = GameObject.Instantiate(obj) as GameObject;
			m_LevelEffect5.transform.parent = gameObject.transform;
			m_LevelEffect5.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
			m_LevelEffect5.transform.localRotation = Quaternion.identity;
			m_LevelEffect5.transform.localScale	=	Vector3.one;
			
			bHasLoadLevelUpEffect5 = true;
			fHasPlayLevelUpEffectTime5 = 0.0f;
		}
		else if (param.petInt==7)
		{
			m_LevelEffect6 = GameObject.Instantiate(obj) as GameObject;
			m_LevelEffect6.transform.parent = gameObject.transform;
			
			bHasLoadLevelUpEffect6 = true;
			fHasPlayLevelUpEffectTime6 = 0.0f;
		}
		else if (param.petInt==8)
		{
			m_LevelEffect7 = GameObject.Instantiate(obj) as GameObject;
			m_LevelEffect7.transform.parent = gameObject.transform;
			
			bHasLoadLevelUpEffect7 = true;
			fHasPlayLevelUpEffectTime7 = 0.0f;
		}
	}
	
	public void LoadLevelUpHitEffect()
	{
		if( m_LevelUpHitEffect )
			Destroy(m_LevelUpHitEffect);
		m_LevelUpHitEffect = null;
		
		bHasLoadHitEffect = false;
		fHasPlayHitEffectTime = 0.0f;
		
		string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup_hit.prefab";
		ResLoadParams param = new ResLoadParams();
		param.info = "HitEffect";
		sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelUpHitPrefab,param);
	}
	
	public void OnLoadLevelUpHitPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null)
		{
			bHasLoadHitEffect = false;
			fHasPlayHitEffectTime = 0.0f;
			return;
		}
		
		if (param.info == "HitEffect")
		{
			m_LevelUpHitEffect = GameObject.Instantiate(obj,gameObject.transform.position,gameObject.transform.transform.rotation) as GameObject;
			m_LevelUpHitEffect.transform.parent = gameObject.transform;
			m_LevelUpHitEffect.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
			m_LevelUpHitEffect.transform.localRotation = Quaternion.identity;
			m_LevelUpHitEffect.transform.localScale	=	Vector3.one;
			m_LevelUpHitEffect.SetActive(true);
			bHasLoadHitEffect = true;
			fHasPlayHitEffectTime = 0.0f;
		}
	}
	
	public void LoadLevelFinalEffect()
	{
		if( m_LevelFinalEffect )
			Destroy(m_LevelFinalEffect);
		m_LevelFinalEffect = null;
		
		bHasLoadFinalEffect = false;
		fHasPlayFinalEffectTime = 0.0f;
		
		string prefabname = "Effect/MainChar/FX_UI/$Fx_chongwushengji/Fx_Lveup.prefab";
		ResLoadParams param = new ResLoadParams();
		param.info = "FinalEffect";
		sdResourceMgr.Instance.LoadResource(prefabname,OnLoadLevelFinalPrefab,param);
	}
	
	public void OnLoadLevelFinalPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null)
		{
			bHasLoadFinalEffect = false;
			fHasPlayFinalEffectTime = 0.0f;
			return;
		}
		
		if (param.info == "FinalEffect")
		{
			m_LevelFinalEffect = GameObject.Instantiate(obj) as GameObject;
			m_LevelFinalEffect.transform.parent = gameObject.transform;
			m_LevelFinalEffect.transform.localPosition = new Vector3(249.0f,238.0f,0.0f);
			m_LevelFinalEffect.transform.localScale = new Vector3(179.0f,100.0f,100.0f);
			
			bHasLoadFinalEffect = true;
			fHasPlayFinalEffectTime = 0.0f;
		}
	}

	public void ReflashPetLevelUpParam()
	{
		fFlashTime = 0.0f;
		SClientPetInfo Info = null;
		if (m_uuDBID!=UInt64.MaxValue)
		{
			Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;
		}
		//1.计算当前等级的属性..
		UInt64 myExp = Info.m_uuExperience;
		int iAbility = Info.m_iAbility;
		int iLevel = Info.m_iLevel;
		UInt64 uuLevelUPExp = sdNewPetMgr.Instance.GetPetCurLevelNeedExp(Info.m_uiTemplateID, iAbility, iLevel);
		float fValue = 1.0f;
		if (uuLevelUPExp>0)
			fValue = (float)myExp/(float)uuLevelUPExp;
		if (fValue>1.0f)
			fValue = 1.0f;
		//经验条位置..
		m_fFExp1 = fValue;
		fValue = fValue*100.0f;
		//经验百分比..
		m_iFExp1 = (int)fValue;
		//等级..
		m_iFLevel1 = Info.m_iLevel;
		//攻击..
		m_iAttack1 = Info.m_CurProperty.m_iAtkDmgMax;
		//防御..
		m_iDef1 = Info.m_CurProperty.m_iDef;
		//生命..
		m_iHp1 = Info.m_CurProperty.m_iMaxHP;
		//算宠物额外的属性值..
		m_iAddAttack = m_iAttack1 - (int)sdNewPetMgr.Instance.GetPetAttCoe(Info.m_uiTemplateID, Info.m_iUp , Info.m_iLevel);
		m_iAddDef = m_iDef1 - (int)sdNewPetMgr.Instance.GetPetDefCoe(Info.m_uiTemplateID, Info.m_iUp , Info.m_iLevel);
		m_iAddHp = m_iHp1 - (int)sdNewPetMgr.Instance.GetPetHPCoe(Info.m_uiTemplateID, Info.m_iUp , Info.m_iLevel);

		//2.计算升级后新等级的属性..
		bool bNull = sdNewPetMgr.Instance.AllPetLevelUpDBIDIsNull();
		if (bNull)
		{
			m_iFLevel2 = 0;
			m_iFExp2 = 0;
			m_fFExp2 = 0.0f;
			m_iAttack2 = 0;
			m_iDef2 = 0;
			m_iHp2 = 0;

			if(m_lbLevel)
				m_lbLevel.GetComponent<UILabel>().text = "Lv." + m_iFLevel1.ToString();
			if (m_expBar!=null)
				m_expBar.GetComponent<UISlider>().value = m_fFExp1;
			if(m_lbExp)
				m_lbExp.GetComponent<UILabel>().text = m_iFExp1.ToString() + "%";
			if(m_lbv3)
			{
				m_lbv3.GetComponent<UILabel>().text = m_iAttack1.ToString();
				m_lbv3.GetComponent<UILabel>().color = Color.white;
			}
			if(m_lbv4)
			{
				m_lbv4.GetComponent<UILabel>().text = m_iDef1.ToString();
				m_lbv4.GetComponent<UILabel>().color = Color.white;
			}
			if(m_lbv5)
			{
				m_lbv5.GetComponent<UILabel>().text = m_iHp1.ToString();
				m_lbv5.GetComponent<UILabel>().color = Color.white;
			}

			m_iFlashBz = 0;
			iFlashSetp = 0;
			fFlashTime = 0.0f;
			SetObjectAlpha(1.0f);
		}
		else
		{
			m_iFlashBz = 1;

			UInt64 giveExp = GetPetLevelUpGiveExp();
			UInt64 maxExp = sdNewPetMgr.Instance.GetPetLevelMaxAllNeedExp(Info.m_uiTemplateID, Info.m_iAbility);
			UInt64 nowExp = GetNowPetNowLevelExp(Info.m_uiTemplateID, Info.m_iAbility, Info.m_iLevel);
			nowExp = nowExp + Info.m_uuExperience + giveExp;
			if (nowExp>=maxExp)
			{
				int iMyLevel = int.Parse(sdGameLevel.instance.mainChar.Property["Level"].ToString());
				iMyLevel = iMyLevel+20;
				if (iMyLevel>sdNewPetMgr.MAX_PET_LEVEL)
					iMyLevel = sdNewPetMgr.MAX_PET_LEVEL;

				m_iFLevel2 = iMyLevel;
				m_iFExp2 = 0;
				m_fFExp2 = 0.0f;
			}
			else
			{
				//等级..
				m_iFLevel2 = GetNowExpPetLevel(Info.m_uiTemplateID, Info.m_iAbility, nowExp);
				UInt64 uuCurExp = GetNowExpPetCurExp(Info.m_uiTemplateID, Info.m_iAbility, nowExp);
				UInt64 uuCurMaxExp = sdNewPetMgr.Instance.GetPetCurLevelNeedExp(Info.m_uiTemplateID, Info.m_iAbility, m_iFLevel2);
				m_iFExp2 = 0;
				m_fFExp2 = 0.0f;
				float fValue2 = 1.0f;
				if (uuCurMaxExp>0)
					fValue2 = (float)uuCurExp/(float)uuCurMaxExp;
				if (fValue2>1.0f)
					fValue2 = 1.0f;
				//经验条位置..
				m_fFExp2 = fValue2;
				fValue2 = fValue2*100.0f;
				//经验百分比..
				m_iFExp2 = (int)fValue2;
			}

			m_iAttack2 = (int)sdNewPetMgr.Instance.GetPetAttCoe(Info.m_uiTemplateID, Info.m_iUp , m_iFLevel2);
			m_iDef2 = (int)sdNewPetMgr.Instance.GetPetDefCoe(Info.m_uiTemplateID, Info.m_iUp , m_iFLevel2);
			m_iHp2 = (int)sdNewPetMgr.Instance.GetPetHPCoe(Info.m_uiTemplateID, Info.m_iUp , m_iFLevel2);
			//攻击..
			m_iAttack2 = m_iAttack2 + m_iAddAttack;
			//防御..
			m_iDef2 = m_iDef2 + m_iAddDef;
			//生命..
			m_iHp2 = m_iHp2 + m_iAddHp;
		}
	}
}

