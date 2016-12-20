using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
//
//
//public class LoadTest : MonoBehaviour {
//	
//	//bool jumped = false;
//	bool bundleUpdated = false;
//	
//	int testLoadId = -1;
//	GameObject testLoadObj = null;
//	string macAddress;
//	bool macLoaded = false;
//	
//	int testLoginStep = 0;
//	int roleCount = 0;
//	int lastSelectRole = 0;
//	
//	int testTuiTuStep = 0;
//	
//	sdPlayerInfo[] playerList = new sdPlayerInfo[3];
//	void Awake()
//	{
//		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_LOGIN,
//			OnMessage_GCID_LOGIN);
//		
//		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_ROLELIST,
//			OnMessage_GCID_ROLELIST);
//		
//		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_CREATEROLE,
//			OnMessage_GCID_CREATEROLE);
//		
//		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_LEVEL_ACK,
//			OnMessage_SCID_LEVEL_ACK);
//		
//		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_RELIVE_ACK,
//			OnMessage_SCID_RELIVE_ACK);
//		
//		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_TREASURE_CHEST_NTF,
//			OnMessage_SCID_TREASURE_CHEST_NIF);
//		
//		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_OPEN_TREASURE_CHEST_ACK,
//			OnMessage_SCID_OPEN_TREASURE_CHEST_ACK);
//	}
//	// Use this for initialization
//	void Start () {
//		BundleGlobal.Instance.thePath = "192.168.123.1/D%3A/bundles/";
//		//if(Application.platform == RuntimePlatform.Android)
//			BundleGlobal.Instance.thePath = "127.0.0.1/D%3A/androidBundles/";
//		if((Application.platform != RuntimePlatform.WindowsEditor) || BundleGlobal.bundleTest)
//			BundleGlobal.Instance.StartInitBundle();
//		
//		//macAddress = GetMacAddress();
//		SDGlobal.StartGetMacAddress();
//		
//		SDNetGlobal.init();
//		
//		//SDCSV csvTest = new SDCSV();
//		//csvTest.LoadCSV("Assets/Prefab/property.txt",null);
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		/*if(!jumped && (BundleGlobal.Instance.created))
//		{
//			jumped = true;
//			BundleGlobal.Instance.StartLoadBundleLevel("scene2.unity3d","testScene2");
//		}*/
//		if(!bundleUpdated && (BundleGlobal.Instance.created))
//		{
//			//if((Application.platform != RuntimePlatform.WindowsEditor) || BundleGlobal.bundleTest)
//			//	BundleGlobal.Instance.StartUpdateAllBundles();
//			bundleUpdated = true;
//		}
//		
//		if(!macLoaded)
//		{
//			macLoaded = SDGlobal.CheckMacCompelete();
//			if(macLoaded)
//			{
//				macAddress = SDGlobal.GetMacAddress();
//				//SDNetGlobal.doConnectLogin(2);
//				testLoginStep = 1;
//			}
//		}
//		
//		if(testLoadId < 0 && 
//			(((BundleGlobal.Instance.created) &&
//			(BundleGlobal.Instance.downloadedNum == BundleGlobal.Instance.needDownLoadNum)) ||
//			(Application.platform == RuntimePlatform.WindowsEditor)))
//		{
//			ResLoadParams param = new ResLoadParams();
//			param.pos = Vector3.zero;
//			param.rot = Quaternion.identity;
//			param.scale = new Vector3(1.0f,1.0f,1.0f);
//			BundleGlobal.Instance.LoadResource(ref testLoadId,"Assets/Monster/$testMonster/Am_Zhizhu.fbx",typeof(GameObject),InstantiateObj,param);
//		}
//		
//	}
//	
//	public void InstantiateObj(ResLoadParams param,Object obj)
//	{
//		testLoadObj = GameObject.Instantiate(obj) as GameObject;
//		//BundleGlobal.Instance.CleanLoadingObj(testLoadId);
//	}
//	
//	private void OnMessage_GCID_LOGIN(int iMsgID, ref CMessage msg)
//	{
//		SDGlobal.Log("gate received");
//		if(testLoginStep < 2)
//			testLoginStep = 2;
//	}
//	
//	private void OnMessage_GCID_CREATEROLE(int iMsgID, ref CMessage msg)
//	{
//		SDGlobal.Log("create role received");
//	}
//	
//	private void OnMessage_GCID_ROLELIST(int iMsgID, ref CMessage msg)
//	{
//		testLoginStep = 3;
//		
//		CliProto.GC_ROLELIST refMSG = (CliProto.GC_ROLELIST)msg;
//		
//		roleCount = refMSG.m_Count;
//		lastSelectRole = refMSG.m_LastSelect;
//		
//		for(int i = 0; i < roleCount; i++)
//		{
//			if(playerList[i] == null)
//				playerList[i] = new sdPlayerInfo();
//			
//			sdPlayerInfo kPlayerInfo = playerList[i];		
//			kPlayerInfo.mRoleName	= System.Text.Encoding.UTF8.GetString(refMSG.m_RoleInfoList[i].m_RoleInfo.m_RoleName);
//			kPlayerInfo.mDBID		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_DBRoleId;
//			kPlayerInfo.mGender		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_Gender;
//			kPlayerInfo.mSkinColor	= refMSG.m_RoleInfoList[i].m_RoleInfo.m_SkinColor;
//			kPlayerInfo.mHairStyle	= refMSG.m_RoleInfoList[i].m_RoleInfo.m_HairStyle;
//			kPlayerInfo.mBaseJob	= refMSG.m_RoleInfoList[i].m_RoleInfo.m_BaseJob;
//			kPlayerInfo.mJob		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_Job;
//			kPlayerInfo.mLevel		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_Level;
//			kPlayerInfo.mEquipCount	= refMSG.m_RoleInfoList[i].m_EquipCount;		
//
//			if (kPlayerInfo.mEquipCount > 0)
//			{
//				kPlayerInfo.mEquipID = new uint[kPlayerInfo.mEquipCount];
//				for (int j = 0; j < kPlayerInfo.mEquipCount; ++j)
//					kPlayerInfo.mEquipID[i] = refMSG.m_RoleInfoList[i].m_EquipID[i];
//			}
//		}
//	}
//	
//	private void OnMessage_SCID_LEVEL_ACK(int iMsgID, ref CMessage msg)
//	{
//		SDGlobal.Log("tuitu ack");
//	}
//	
//	private void OnMessage_SCID_RELIVE_ACK(int iMsgID, ref CMessage msg)
//	{
//		SDGlobal.Log("relive ack");
//	}
//	
//	private void OnMessage_SCID_TREASURE_CHEST_NIF(int iMsgID, ref CMessage msg)
//	{
//		SDGlobal.Log("treasure chest");
//	}
//	
//	private void OnMessage_SCID_OPEN_TREASURE_CHEST_ACK(int iMsgID, ref CMessage msg)
//	{
//		SDGlobal.Log("open treasure chest");
//	}
//	
//	void OnGUI()
//	{
//		GUI.BeginGroup(new Rect(Screen.width/2 - 200,Screen.height/2 - 200,400,400));
//		GUI.Box(new Rect(0,0,400,400),"");
//		if(!BundleGlobal.Instance.created)
//		{
//			GUI.Label(new Rect(0,20,400,180),"检查版本..");
//		}
//		else
//		{
//			GUI.Label(new Rect(0,20,400,180),"更新资源..  " + BundleGlobal.Instance.downloadedNum + "/" + BundleGlobal.Instance.needDownLoadNum);
//		}
//		
//		GUI.Label(new Rect(0,40,400,180),"MAC地址: " + macAddress);
//		
//		if(testLoginStep == 0)
//		{
//			GUI.Label(new Rect(0,60,400,180),"初始化");
//		}
//		else if(testLoginStep == 1)
//		{
//			GUI.Label(new Rect(0,60,400,180),"等待登陆");
//			if(GUI.Button(new Rect(0,100,100,60),"登陆"))
//			{
//				SDNetGlobal.doConnectLogin(2);
//			}
//		}
//		else if(testLoginStep == 2)
//		{
//			GUI.Label(new Rect(0,60,400,180),"已连接gate");
//		}
//		else if(testLoginStep == 3)
//		{
//			GUI.Label(new Rect(0,60,400,180),"已获得角色列表");
//			GUI.Label(new Rect(0,80,400,180),"已创建角色数："+roleCount);
//			for(int i = 0; i < roleCount; i++)
//			{
//				GUI.Label(new Rect(0,100+i*20,400,180),"姓名：" + playerList[i].mRoleName + 
//					" 等级：" + playerList[i].mLevel +
//					" 性别：" + ((playerList[i].mGender == 0) ? "男" : "女"));
//			}
//			if(GUI.Button(new Rect(0,160,100,60),"创建角色"))
//			{
//				CliProto.CG_CREATEROLE refMSG = new CliProto.CG_CREATEROLE();
//				
//				refMSG.m_RoleInfo.m_RoleName = System.Text.Encoding.Default.GetBytes("测试角色");
//				refMSG.m_RoleInfo.m_Gender = 0;
//				refMSG.m_RoleInfo.m_Level = 1;
//				refMSG.m_RoleInfo.m_SkinColor = 0;
//				refMSG.m_RoleInfo.m_HairStyle = 0;
//				refMSG.m_RoleInfo.m_Job = 0;
//				
//				SDNetGlobal.SendMessage(refMSG);
//			}
//			
//			if(GUI.Button(new Rect(0,220,100,60),"进入游戏"))
//			{
//				CliProto.CG_SELECTROLE refMSG = new CliProto.CG_SELECTROLE();
//				refMSG.m_RoleName = System.Text.Encoding.Default.GetBytes((playerList[0].mRoleName));
//				SDNetGlobal.SendMessage(refMSG);
//			}
//		}
//		
//		if(testTuiTuStep == 0)
//		{
//			if(GUI.Button(new Rect(0,280,100,60),"推图测试"))
//			{
//				testTuiTuStep = 1;
//			}
//		}
//		else if(testTuiTuStep == 1)
//		{
//			if(GUI.Button(new Rect(0,280,100,60),"章节难度"))
//			{
//				testTuiTuStep = 2;
//			}
//		}
//		else if(testTuiTuStep == 2)
//		{
//			if(GUI.Button(new Rect(0,280,100,60),"选择战役"))
//			{
//				testTuiTuStep = 3;
//			}
//		}
//		else if(testTuiTuStep == 3)
//		{
//			if(GUI.Button(new Rect(0,280,100,60),"选择关卡"))
//			{
//				testTuiTuStep = 4;
//			}
//		}
//		else if(testTuiTuStep == 4)
//		{
//			if(GUI.Button(new Rect(0,280,100,60),"好友宠物"))
//			{
//				testTuiTuStep = 5;
//			}
//		}
//		else if(testTuiTuStep == 5)
//		{
//			if(GUI.Button(new Rect(0,280,100,60),"进入推图"))
//			{
//				CliProto.CS_LEVEL_REQ refMSG = new CliProto.CS_LEVEL_REQ();
//				SDNetGlobal.SendMessage(refMSG);
//				testTuiTuStep = 6;
//			}
//		}
//		else if(testTuiTuStep == 6)
//		{
//			if(GUI.Button(new Rect(0,280,100,60),"测试复活"))
//			{
//				CliProto.CS_RELIVE_REQ refMSG = new CliProto.CS_RELIVE_REQ();
//				SDNetGlobal.SendMessage(refMSG);
//			}
//			if(GUI.Button(new Rect(100,280,100,60),"结束推图"))
//			{
//				CliProto.CS_LEVEL_RESULT_NTF refMSG = new CliProto.CS_LEVEL_RESULT_NTF();
//				SDNetGlobal.SendMessage(refMSG);
//				testTuiTuStep = 7;
//			}
//		}
//		else if(testTuiTuStep == 7)
//		{
//			if(GUI.Button(new Rect(0,280,100,60),"付费开箱"))
//			{
//				CliProto.CS_OPEN_TREASURE_CHEST_REQ refMSG = new CliProto.CS_OPEN_TREASURE_CHEST_REQ();
//				SDNetGlobal.SendMessage(refMSG);
//			}
//		}
//		GUI.EndGroup();
//	}
//}
