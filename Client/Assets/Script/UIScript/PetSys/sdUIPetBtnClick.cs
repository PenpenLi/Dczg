using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetBtnClick : MonoBehaviour
{
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	void OnClick()
    {
		//宠物列表界面..
		if (gameObject.name=="RbZh")
		{
			sdUIPetControl.Instance.ActivePetListPnl(null, false);
		}
		else if (gameObject.name=="RbSp")
		{
			sdUIPetControl.Instance.ActivePetPaperPnl(null);
		}
		else if (gameObject.name=="RbRh")
		{
			sdUIPetControl.Instance.ActivePetRonghePnl(null);
		}
		else if (gameObject.name=="RbTj")
		{
			sdUIPetControl.Instance.ActivePetTujianPnl(null);
		}
		else if (gameObject.name=="petListClose")
		{
			if (sdUIPetControl.m_UIPetListPnl!=null)
			{
				WndAni.HideWndAni(sdUIPetControl.m_UIPetListPnl,true,"w_black");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetListPnl obj = sdUIPetControl.m_UIPetListPnl.GetComponent<sdUIPetListPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}

			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}
		}
		else if (gameObject.name=="btnPetSale")
		{
			sdUIPetControl.Instance.ActivePetSaleSelectPnl(null);
		}
		else if (gameObject.name=="listSortColor")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetListPnl listPnl = wnd.GetComponentInChildren<sdUIPetListPnl>();
				if (listPnl)
				{
					if (listPnl.m_iSortType != (int)PetSortType.Pet_SortBy_Color)
					{
						listPnl.m_iSortType = (int)PetSortType.Pet_SortBy_Color;
						listPnl.RefreshPetListPage();
					}
				}
			}
		}
		else if (gameObject.name=="listSortLevel")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetListPnl listPnl = wnd.GetComponentInChildren<sdUIPetListPnl>();
				if (listPnl)
				{
					if (listPnl.m_iSortType != (int)PetSortType.Pet_SortBy_Level)
					{
						listPnl.m_iSortType = (int)PetSortType.Pet_SortBy_Level;
						listPnl.RefreshPetListPage();
					}
				}
			}
		}
		//宠物出售界面..
		else if (gameObject.name=="saleSelectClose")
		{
			if (sdUIPetControl.m_UIPetSaleSelectPnl!=null)
			{
				WndAni.HideWndAni(sdUIPetControl.m_UIPetSaleSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetSaleSelectPnl obj = sdUIPetControl.m_UIPetSaleSelectPnl.GetComponent<sdUIPetSaleSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}
		else if (gameObject.name=="btnSaleOk")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetSaleSelectPnl listPnl = wnd.GetComponentInChildren<sdUIPetSaleSelectPnl>();
				if (listPnl)
				{
					listPnl.PetBeginSale();
				}
			}
		}
		else if (gameObject.name=="SaleOne")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetSaleSelectPnl listPnl = wnd.GetComponentInChildren<sdUIPetSaleSelectPnl>();
				if (listPnl)
				{
					listPnl.OnClickSaleSelectBtn(1);
				}
			}
		}
		else if (gameObject.name=="SaleTwo")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetSaleSelectPnl listPnl = wnd.GetComponentInChildren<sdUIPetSaleSelectPnl>();
				if (listPnl)
				{
					listPnl.OnClickSaleSelectBtn(2);
				}
			}
		}
		else if (gameObject.name=="SaleThree")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetSaleSelectPnl listPnl = wnd.GetComponentInChildren<sdUIPetSaleSelectPnl>();
				if (listPnl)
				{
					listPnl.OnClickSaleSelectBtn(3);
				}
			}
		}
		else if (gameObject.name=="SaleFour")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetSaleSelectPnl listPnl = wnd.GetComponentInChildren<sdUIPetSaleSelectPnl>();
				if (listPnl)
				{
					listPnl.OnClickSaleSelectBtn(4);
				}
			}
		}
		//宠物碎片合成界面..
		else if (gameObject.name=="petPaperClose")
		{
			if (sdUIPetControl.m_UIPetPaperPnl!=null)
			{
				WndAni.HideWndAni(sdUIPetControl.m_UIPetPaperPnl,true,"w_black");
				if (sdUIPetControl.m_UIPetListPnl)
					WndAni.HideWndAni(sdUIPetControl.m_UIPetListPnl,true,"w_black");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetPaperPnl obj = sdUIPetControl.m_UIPetPaperPnl.GetComponent<sdUIPetPaperPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
			
			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}
		}
		else if (gameObject.name=="btnPetPaper")
		{
			//数量够，允许合成..
			if (gameObject.transform.FindChild("Background").gameObject.GetComponent<UISprite>().spriteName=="btn_2")
			{
				int iPetId = gameObject.transform.parent.parent.gameObject.GetComponent<sdUIPetPaperCard>().m_iPetID;
				if (iPetId>0)
				{
					sdUIPetPaperPnl.m_iNowSelectID = iPetId;
					int iCurNum = gameObject.transform.parent.parent.gameObject.GetComponent<sdUIPetPaperCard>().m_iCurNum;
					int iMaxNum = gameObject.transform.parent.parent.gameObject.GetComponent<sdUIPetPaperCard>().m_iMaxNum;
					if (iCurNum>=iMaxNum && iMaxNum>0)
					{
						Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(iPetId.ToString());
						if (info != null)
						{
							sdMsgBox.OnConfirm btn_ok = new sdMsgBox.OnConfirm(OnClickPetGatherOk);
							sdUICharacter.Instance.ShowOkCanelMsg("确定要将已有碎片合成么？", btn_ok, null);
						}
						else
						{
							sdUICharacter.Instance.ShowOkMsg("无效的宠物ID", null);
						}
					}
					else
					{
						sdUICharacter.Instance.ShowOkMsg("没有足够的碎片", null);
					}
				}
			}
			else
			{
				sdUICharacter.Instance.ShowMsgLine("碎片数量不足", Color.yellow);
			}
		}
		//宠物融合界面..
		else if (gameObject.name=="rongheClose")
		{
			if (sdUIPetControl.m_UIPetRonghePnl!=null)
			{
				WndAni.HideWndAni(sdUIPetControl.m_UIPetRonghePnl,true,"w_black");
				if (sdUIPetControl.m_UIPetListPnl)
					WndAni.HideWndAni(sdUIPetControl.m_UIPetListPnl,true,"w_black");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetRonghePnl obj = sdUIPetControl.m_UIPetRonghePnl.GetComponent<sdUIPetRonghePnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}

			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}
		}
		else if ( gameObject.name=="btnRhSel0" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetRonghePnl petPnl = wnd.GetComponentInChildren<sdUIPetRonghePnl>();
				if (petPnl)
				{
					sdUIPetControl.Instance.ActivePetRongheSelectPnl(null, petPnl.m_uuPetID1, 998);
					petPnl.ShowHideModel(false);
				}
			}
		}
		else if ( gameObject.name=="btnRhSel1" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetRonghePnl petPnl = wnd.GetComponentInChildren<sdUIPetRonghePnl>();
				if (petPnl)
				{
					sdUIPetControl.Instance.ActivePetRongheSelectPnl(null, petPnl.m_uuPetID0, 999);
					petPnl.ShowHideModel(false);
				}
			}
		}
		else if ( gameObject.name=="btnRonghe" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetRonghePnl petPnl = wnd.GetComponentInChildren<sdUIPetRonghePnl>();
				if (petPnl)
				{
					if (petPnl.m_uuPetID0!=UInt64.MaxValue && petPnl.m_uuPetID1!=UInt64.MaxValue)
					{
						sdPetMsg.Send_CS_PET_MERGE_REQ(petPnl.m_uuPetID0, petPnl.m_uuPetID1);
						petPnl.OnClickRongheBtn();
					}
					else
					{
						sdUICharacter.Instance.ShowOkMsg("请选择两个战魂作为融合材料", null);
					}
					//petPnl.OnClickRongheBtn();
					//petPnl.m_bBeginRonghe = true;
					//petPnl.m_iPetIDNew = 102;
					//petPnl.LoadRongheEffect();
				}
			}
		}
		else if ( gameObject.name=="btnRongheShow" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetRonghePnl petPnl = wnd.GetComponentInChildren<sdUIPetRonghePnl>();
				if (petPnl)
				{
					if( petPnl.m_iPetIDNew>0 )
					{
						sdUIPetControl.Instance.ActivePetSmallTip(null, petPnl.m_iPetIDNew, 0, 1);
						petPnl.ShowHideModel(false);
					}
				}
			}
		}
		else if ( gameObject.name=="btnRongheBack" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetRonghePnl petPnl = wnd.GetComponentInChildren<sdUIPetRonghePnl>();
				if (petPnl)
				{
					petPnl.ResetPetRongheUI();
					petPnl.ShowRonghePetSelectLeftModel();
					petPnl.ShowRonghePetSelectRightModel();
				}
			}
		}
		//宠物融合选择界面..
		else if (gameObject.name=="rongheSelectClose")
		{
			if (sdUIPetControl.m_UIPetRongheSelectPnl!=null)
			{
				WndAni.HideWndAni(sdUIPetControl.m_UIPetRongheSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetRongheSelectPnl obj = sdUIPetControl.m_UIPetRongheSelectPnl.GetComponent<sdUIPetRongheSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}

				GameObject wnd = sdGameLevel.instance.NGUIRoot;
				if (wnd)
				{
					sdUIPetRonghePnl petPnl = wnd.GetComponentInChildren<sdUIPetRonghePnl>();
					if (petPnl)
					{
						petPnl.ShowHideModel(true);
					}
				}
			}
		}
		//宠物图鉴界面..
		else if (gameObject.name=="petTujianClose")
		{
			if (sdUIPetControl.m_UIPetTujianPnl!=null)
			{
				WndAni.HideWndAni(sdUIPetControl.m_UIPetTujianPnl,true,"w_black");
				if (sdUIPetControl.m_UIPetListPnl)
					WndAni.HideWndAni(sdUIPetControl.m_UIPetListPnl,true,"w_black");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetTujianPnl obj = sdUIPetControl.m_UIPetTujianPnl.GetComponent<sdUIPetTujianPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}

			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}
		}
		//宠物战队界面..
		else if (gameObject.name=="petWarClose")
		{
			if (sdUIPetControl.m_UIPetWarPnl!=null)
			{
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetWarPnl obj = sdUIPetControl.m_UIPetWarPnl.GetComponent<sdUIPetWarPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
				WndAni.HideWndAni(sdUIPetControl.m_UIPetWarPnl,true,"w_black");
			}

			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}
		}
		else if (gameObject.name=="rbPetWar1")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetWarPnl petWarPnl = wnd.GetComponentInChildren<sdUIPetWarPnl>();
				if (petWarPnl)
				{
					petWarPnl.mCurTeamIndex = 0;
					petWarPnl.ReflashPetBattleTeam();
					petWarPnl.RefreshPetZuhePage();
				}
			}
		}
		else if (gameObject.name=="rbPetWar2")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetWarPnl petWarPnl = wnd.GetComponentInChildren<sdUIPetWarPnl>();
				if (petWarPnl)
				{
					petWarPnl.mCurTeamIndex = 1;
					petWarPnl.ReflashPetBattleTeam();
					petWarPnl.RefreshPetZuhePage();
				}
			}
		}
		else if (gameObject.name=="rbPetWar3")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetWarPnl petWarPnl = wnd.GetComponentInChildren<sdUIPetWarPnl>();
				if (petWarPnl)
				{
					petWarPnl.mCurTeamIndex = 2;
					petWarPnl.ReflashPetBattleTeam();
					petWarPnl.RefreshPetZuhePage();
				}
			}
		}
		else if (gameObject.name=="btnAccept")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetWarPnl petWarPnl = wnd.GetComponentInChildren<sdUIPetWarPnl>();
				if (petWarPnl)
				{
					if (petWarPnl.mCurTeamIndex!=sdNewPetMgr.Instance.mPetCurTeam)
					{
						sdPetMsg.Send_CS_PET_TEAM_RPT(petWarPnl.mCurTeamIndex);
					}
				}
			}
		}
		//战队选择面板..
		else if (gameObject.name=="warSelectClose")
		{
			if (sdUIPetControl.m_UIPetWarSelectPnl!=null)
			{
				WndAni.HideWndAni(sdUIPetControl.m_UIPetWarSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetWarSelectPnl obj = sdUIPetControl.m_UIPetWarSelectPnl.GetComponent<sdUIPetWarSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}
		//宠物属性界面..
		else if (gameObject.name=="petPropClose")
		{
			if (sdUIPetControl.m_UIPetPropPnl!=null)
			{
				WndAni.HideWndAni(sdUIPetControl.m_UIPetPropPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetPropPnl obj = sdUIPetControl.m_UIPetPropPnl.GetComponent<sdUIPetPropPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}
		else if (gameObject.name=="btnPetlock")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
				{
					SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(petPnl.m_uuDBID);
					if (Info == null)
						return;

					sdPetMsg.Send_CS_LOCK_RPT((int)HeaderProto.ELockType.PET_LOCK, petPnl.m_uuDBID, 1-Info.m_Lock);

					if (Info.m_Lock==1)
						sdUICharacter.Instance.ShowMsgLine("解锁成功", Color.yellow);
					else
						sdUICharacter.Instance.ShowMsgLine("加锁成功", Color.yellow);
				}
			}
		}
		else if (gameObject.name=="btnPetshow")
		{
			
		}
		else if (gameObject.name=="propModelLeft")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
					sdConfDataMgr.Instance().OnModelClickRandomPlayAnm(petPnl.GetPetModel());
			}
		}
		else if (gameObject.name=="RbPropDesc")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
				{
					petPnl.PropUIShow(0);
				}
			}
		}
		else if (gameObject.name=="RbPropJn")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
				{
					petPnl.PropUIShow(2);
				}
			}
		}
		else if (gameObject.name=="RbPropZh")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
				{
					petPnl.PropUIShow(3);
				}
			}
		}
		else if (gameObject.name=="btnPetLevelup")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
				{
					SClientPetInfo Info = null;
					if (petPnl.m_uuDBID==UInt64.MaxValue)
						return;
					Info = sdNewPetMgr.Instance.GetPetInfo(petPnl.m_uuDBID);
					if (Info == null)
						return;

					if (Info.m_CanLevelUp!=1)
					{
						sdUICharacter.Instance.ShowOkMsg("该战魂不能升级", null);
						return;
					}

					int iLevel = Info.m_iLevel;
					int iMyLevel = int.Parse(sdGameLevel.instance.mainChar.Property["Level"].ToString());
					iMyLevel = iMyLevel+20;
					if (iLevel>=sdNewPetMgr.MAX_PET_LEVEL)
					{
						sdUICharacter.Instance.ShowOkMsg("该战魂已经满级", null);
					}
					else if (iLevel<sdNewPetMgr.MAX_PET_LEVEL && iLevel>=iMyLevel)
					{
						sdUICharacter.Instance.ShowOkMsg("战魂等级不可高于主角等级20级以上", null);
					}
					else
					{
						sdNewPetMgr.Instance.ResetPetLevelUpDBID();
						if (sdUIPetControl.m_UIPetPropPnl!=null)
						{
							UInt64 uuDBID = petPnl.m_uuDBID;
							//隐藏属性界面模型..
							petPnl.SetPetModelVisible(false);
							//这里弹出升级界面..
							sdUIPetControl.Instance.ActivePetLevelPnl(null, uuDBID);
						}
					}
				}
			}
		}
		else if (gameObject.name=="btnPetStrong")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
				{
					SClientPetInfo Info = null;
					if (petPnl.m_uuDBID==UInt64.MaxValue)
						return;
					Info = sdNewPetMgr.Instance.GetPetInfo(petPnl.m_uuDBID);
					if (Info == null)
						return;

					int iLevel = Info.m_iLevel;
					int iUp = Info.m_iUp;
					if (iUp>=sdNewPetMgr.MAX_PET_STRONG_LEVEL)
					{
						sdUICharacter.Instance.ShowOkMsg("该战魂已经强化到最高级别", null);
					}
					else
					{
						if (iLevel<sdNewPetMgr.MAX_PET_CAN_STRONG_LEVEL)
						{
							sdUICharacter.Instance.ShowOkMsg("战魂等级达到20级后可开启战魂强化", null);
						}
						else
						{
							sdNewPetMgr.Instance.m_uuPetStrongSelectID = UInt64.MaxValue;

							if (sdUIPetControl.m_UIPetPropPnl!=null)
							{
								UInt64 uuDBID = petPnl.m_uuDBID;
								//关闭属性界面..
								petPnl.SetPetModelVisible(false);
								//这里弹出强化界面..
								sdUIPetControl.Instance.ActivePetStrongPnl(null, uuDBID);
							}
						}
					}
				}
			}
		}
		else if (gameObject.name=="btnPetGather")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
				{
					int iPetId = petPnl.m_iPetTemplateID;
					if (iPetId>0)
					{
						int iCurNum = sdNewPetMgr.Instance.getPetGatherCurNumByPetId(iPetId);
						int iMaxNum = sdNewPetMgr.Instance.getPetGatherMaxNumByPetId(iPetId);
						if (iCurNum>=iMaxNum && iMaxNum>0)
						{
							Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(iPetId.ToString());
							if (info != null)
							{
								sdMsgBox.OnConfirm btn_ok = new sdMsgBox.OnConfirm(OnClickPetGatherOk);
								sdUICharacter.Instance.ShowOkCanelMsg("确定要将已有碎片合成么？", btn_ok, null);
							}
							else
							{
								sdUICharacter.Instance.ShowOkMsg("无效的宠物ID", null);
							}
						}
						else
						{
							sdUICharacter.Instance.ShowOkMsg("没有足够的碎片", null);
						}
					}
				}
			}
		}
		//升级界面..
		else if (gameObject.name == "petLevelClose")
		{
			if (sdUIPetControl.m_UIPetLevelPnl!=null)
			{
				UInt64 uuDBID = UInt64.MaxValue;
				sdUIPetLevelPnl obj = sdUIPetControl.m_UIPetLevelPnl.GetComponent<sdUIPetLevelPnl>();
				if( obj!=null )
					uuDBID = obj.m_uuDBID;
				
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetLevelPnl);
				//WndAni.HideWndAni(sdUIPetControl.m_UIPetLevelPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}

				GameObject wnd = sdGameLevel.instance.NGUIRoot;
				if (wnd)
				{
					sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
					if (petPnl)
						petPnl.SetPetModelVisible(true);
				}
			}
		}
		else if (gameObject.name=="levelModelLeft")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetLevelPnl petPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
				if (petPnl)
					sdConfDataMgr.Instance().OnModelClickRandomPlayAnm(petPnl.GetPetModel());
			}
		}
		else if (gameObject.name=="btnPetLevelupOk")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetLevelPnl petPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
				if (petPnl)
				{
					if (petPnl.m_iLevelUpingSetp!=0||sdPetMsg.bPetLeveling==true)
						return;
					
					if (sdNewPetMgr.Instance.AllPetLevelUpDBIDIsNull()==true)
					{
						sdUICharacter.Instance.ShowOkMsg("请选择战魂升级材料", null);
					}
					else
					{
						uint uiCost = (uint)petPnl.GetPetLevelUpCost();
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
						
						if (uiCost>uiMyMoney)
						{
							sdUICharacter.Instance.ShowOkMsg("没有足够的金币", null);
						}
						else
						{
							petPnl.SendPetLevelUpMsg();
						}
					}
				}
			}
		}
		else if (gameObject.name=="btnPetLevelAutoSelect")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetLevelPnl petPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
				if (petPnl)
				{
					if (petPnl.m_iLevelUpingSetp!=0||sdPetMsg.bPetLeveling==true)
						return;
				}
				
				if (petPnl)
				{
					SClientPetInfo Info = null;
					if (petPnl.m_uuDBID==UInt64.MaxValue)
						return;
					Info = sdNewPetMgr.Instance.GetPetInfo(petPnl.m_uuDBID);
					if (Info == null)
						return;

					int iLevel = Info.m_iLevel;
					int iMyLevel = int.Parse(sdGameLevel.instance.mainChar.Property["Level"].ToString());
					iMyLevel = iMyLevel + 20;
					if (iLevel>=sdNewPetMgr.MAX_PET_LEVEL)
					{
						sdUICharacter.Instance.ShowOkMsg("该战魂已经满级", null);
						return;
					}
					else if (iLevel<sdNewPetMgr.MAX_PET_LEVEL && iLevel>=iMyLevel)
					{
						sdUICharacter.Instance.ShowOkMsg("战魂等级不可高于主角等级20级以上", null);
						return;
					}
				}
			}
			
			sdNewPetMgr.Instance.AutoSelectPetLevelMaterial();
		}
		else if (gameObject.name=="panEnd")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetLevelPnl petPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
				if (petPnl && petPnl.m_iLevelUpingSetp == 5 && petPnl.m_bBeginLineMove==true)
				{
					petPnl.m_iLevelUpingSetp = 6;
				}
			}
		}
		//升级材料选择..
		else if (gameObject.name == "levelupSelectClose")
		{
			if (sdUIPetControl.m_UIPetLevelSelectPnl!=null)
			{
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetLevelSelectPnl);
				//WndAni.HideWndAni(sdUIPetControl.m_UIPetLevelSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetLevelSelectPnl obj = sdUIPetControl.m_UIPetLevelSelectPnl.GetComponent<sdUIPetLevelSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}

				GameObject wnd = sdGameLevel.instance.NGUIRoot;
				if (wnd)
				{
					sdUIPetLevelPnl petPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
					if (petPnl)
						petPnl.SetPetModelVisible(true);
				}
			}
		}
		else if (gameObject.name == "btnLevelSelectGo")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetLevelSelectPnl petPnl = wnd.GetComponentInChildren<sdUIPetLevelSelectPnl>();
				if (petPnl)
				{
					sdNewPetMgr.Instance.m_uuPetLevelSelectID[0] = petPnl.m_uuSelectDBID0;
					sdNewPetMgr.Instance.m_uuPetLevelSelectID[1] = petPnl.m_uuSelectDBID1;
					sdNewPetMgr.Instance.m_uuPetLevelSelectID[2] = petPnl.m_uuSelectDBID2;
					sdNewPetMgr.Instance.m_uuPetLevelSelectID[3] = petPnl.m_uuSelectDBID3;
					sdNewPetMgr.Instance.m_uuPetLevelSelectID[4] = petPnl.m_uuSelectDBID4;
					sdNewPetMgr.Instance.m_uuPetLevelSelectID[5] = petPnl.m_uuSelectDBID5;
					sdNewPetMgr.Instance.m_uuPetLevelSelectID[6] = petPnl.m_uuSelectDBID6;
					sdNewPetMgr.Instance.m_uuPetLevelSelectID[7] = petPnl.m_uuSelectDBID7;
					
					if( sdUIPetControl.m_UIPetLevelSelectPnl != null )
					{
						sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetLevelSelectPnl);
					}
					
					sdUIPetLevelPnl petLevelPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
					if (petLevelPnl)
					{
						petLevelPnl.ReflashPetLevelIcon(true);
						petLevelPnl.ResetPetLevelCostAndExp();
						petLevelPnl.SetPetModelVisible(true);
					}
				}
			}
		}
		//强化界面..
		else if (gameObject.name == "petStrongClose")
		{
			if (sdUIPetControl.m_UIPetStrongPnl!=null)
			{
				UInt64 uuDBID = UInt64.MaxValue;
				sdUIPetStrongPnl obj = sdUIPetControl.m_UIPetStrongPnl.GetComponent<sdUIPetStrongPnl>();
				if( obj!=null )
					uuDBID = obj.m_uuDBID;

				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetStrongPnl);
				//WndAni.HideWndAni(sdUIPetControl.m_UIPetStrongPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}

				GameObject wnd = sdGameLevel.instance.NGUIRoot;
				if (wnd)
				{
					sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
					if (petPnl)
						petPnl.SetPetModelVisible(true);
				}
			}
		}
		else if (gameObject.name == "btnPetStrongOk")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetStrongPnl petPnl = wnd.GetComponentInChildren<sdUIPetStrongPnl>();
				if (petPnl)
				{
					petPnl.OnClickStrongOk();
				}
			}
		}
		//强化选择界面..
		else if (gameObject.name == "strongSelectClose")
		{
			if (sdUIPetControl.m_UIPetStrongSelectPnl!=null)
			{
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetStrongSelectPnl);
				//WndAni.HideWndAni(sdUIPetControl.m_UIPetStrongSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetStrongSelectPnl obj = sdUIPetControl.m_UIPetStrongSelectPnl.GetComponent<sdUIPetStrongSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}
	}

	public static void DoCloseClick(string strName)
	{
		//宠物列表界面..
		if (strName=="petListClose")
		{
			if (sdUIPetControl.m_UIPetListPnl!=null)
			{
				//sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetListPnl);
				WndAni.HideWndAni(sdUIPetControl.m_UIPetListPnl,true,"w_black");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetListPnl obj = sdUIPetControl.m_UIPetListPnl.GetComponent<sdUIPetListPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
			
			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}
		}
		//宠物出售界面..
		else if (strName=="saleSelectClose")
		{
			if (sdUIPetControl.m_UIPetSaleSelectPnl!=null)
			{
				//sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetSaleSelectPnl);
				WndAni.HideWndAni(sdUIPetControl.m_UIPetSaleSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetSaleSelectPnl obj = sdUIPetControl.m_UIPetSaleSelectPnl.GetComponent<sdUIPetSaleSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}
		//宠物融合界面..
		else if (strName=="rongheClose")
		{
			if (sdUIPetControl.m_UIPetRonghePnl!=null)
			{
				//sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetRonghePnl);
				WndAni.HideWndAni(sdUIPetControl.m_UIPetRonghePnl,true,"w_black");
				if (sdUIPetControl.m_UIPetListPnl)
					WndAni.HideWndAni(sdUIPetControl.m_UIPetListPnl,true,"w_black");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetRonghePnl obj = sdUIPetControl.m_UIPetRonghePnl.GetComponent<sdUIPetRonghePnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
			
			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}
		}
		//宠物融合选择界面..
		else if (strName=="rongheSelectClose")
		{
			if (sdUIPetControl.m_UIPetRongheSelectPnl!=null)
			{
				//sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetRongheSelectPnl);
				WndAni.HideWndAni(sdUIPetControl.m_UIPetRongheSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetRongheSelectPnl obj = sdUIPetControl.m_UIPetRongheSelectPnl.GetComponent<sdUIPetRongheSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
				
				GameObject wnd = sdGameLevel.instance.NGUIRoot;
				if (wnd)
				{
					sdUIPetRonghePnl petPnl = wnd.GetComponentInChildren<sdUIPetRonghePnl>();
					if (petPnl)
					{
						petPnl.ShowHideModel(true);
					}
				}
			}
		}
		//宠物图鉴界面..
		else if (strName=="petTujianClose")
		{
			if (sdUIPetControl.m_UIPetTujianPnl!=null)
			{
				//sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetTujianPnl);
				WndAni.HideWndAni(sdUIPetControl.m_UIPetTujianPnl,true,"w_black");
				if (sdUIPetControl.m_UIPetListPnl)
					WndAni.HideWndAni(sdUIPetControl.m_UIPetListPnl,true,"w_black");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetTujianPnl obj = sdUIPetControl.m_UIPetTujianPnl.GetComponent<sdUIPetTujianPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
			
			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}
		}
		//宠物战队界面..
		else if (strName=="petWarClose")
		{
			if (sdUIPetControl.m_UIPetWarPnl!=null)
			{
				//sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetWarPnl);
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetWarPnl obj = sdUIPetControl.m_UIPetWarPnl.GetComponent<sdUIPetWarPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
				WndAni.HideWndAni(sdUIPetControl.m_UIPetWarPnl,true,"w_black");
			}
			
			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}
		}
		//战队选择面板..
		else if (strName=="warSelectClose")
		{
			if (sdUIPetControl.m_UIPetWarSelectPnl!=null)
			{
				//sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetWarSelectPnl);
				WndAni.HideWndAni(sdUIPetControl.m_UIPetWarSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetWarSelectPnl obj = sdUIPetControl.m_UIPetWarSelectPnl.GetComponent<sdUIPetWarSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}
		//宠物属性界面..
		else if (strName=="petPropClose")
		{
			if (sdUIPetControl.m_UIPetPropPnl!=null)
			{
				//sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetPropPnl);
				WndAni.HideWndAni(sdUIPetControl.m_UIPetPropPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetPropPnl obj = sdUIPetControl.m_UIPetPropPnl.GetComponent<sdUIPetPropPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}
		//升级界面..
		else if (strName == "petLevelClose")
		{
			if (sdUIPetControl.m_UIPetLevelPnl!=null)
			{
				UInt64 uuDBID = UInt64.MaxValue;
				sdUIPetLevelPnl obj = sdUIPetControl.m_UIPetLevelPnl.GetComponent<sdUIPetLevelPnl>();
				if( obj!=null )
					uuDBID = obj.m_uuDBID;
				
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetLevelPnl);
				//WndAni.HideWndAni(sdUIPetControl.m_UIPetLevelPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}

		//升级材料选择..
		else if (strName == "levelupSelectClose")
		{
			if (sdUIPetControl.m_UIPetLevelSelectPnl!=null)
			{
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetLevelSelectPnl);
				//WndAni.HideWndAni(sdUIPetControl.m_UIPetLevelSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetLevelSelectPnl obj = sdUIPetControl.m_UIPetLevelSelectPnl.GetComponent<sdUIPetLevelSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}

		//强化界面..
		else if (strName == "petStrongClose")
		{
			if (sdUIPetControl.m_UIPetStrongPnl!=null)
			{
				UInt64 uuDBID = UInt64.MaxValue;
				sdUIPetStrongPnl obj = sdUIPetControl.m_UIPetStrongPnl.GetComponent<sdUIPetStrongPnl>();
				if( obj!=null )
					uuDBID = obj.m_uuDBID;
				
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetStrongPnl);
				//WndAni.HideWndAni(sdUIPetControl.m_UIPetStrongPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}

		//强化选择界面..
		else if (strName == "strongSelectClose")
		{
			if (sdUIPetControl.m_UIPetStrongSelectPnl!=null)
			{
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetStrongSelectPnl);
				//WndAni.HideWndAni(sdUIPetControl.m_UIPetStrongSelectPnl,false,"bg_grey");
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetStrongSelectPnl obj = sdUIPetControl.m_UIPetStrongSelectPnl.GetComponent<sdUIPetStrongSelectPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
			}
		}
	}

	public void OnClickPetGatherOk()
	{
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			int iPetId = sdUIPetPaperPnl.m_iNowSelectID;
			if (iPetId>0)
			{
				Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(iPetId.ToString());
				if (info != null)
				{
					int iGatherId = int.Parse(info["GID"].ToString());
					sdPetMsg.Send_CS_GATHER_ITEM_MERGE_REQ(iGatherId);
				}
			}
		}
	}
}