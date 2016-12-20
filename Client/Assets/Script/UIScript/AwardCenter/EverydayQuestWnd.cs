using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EverydayQuestWnd : Singleton<EverydayQuestWnd>
{

	public bool m_dirt = false;

	bool _awardBoxDirt = false;
	bool _questListDirt = false;

	public GameObject 	m_goWndRoot	= null;
	bool _bWndOpen = false;
	
	UIButton _closeBtn = null;

	GameObject _goQuestBlockTemplate = null;
	GameObject _goDraggableArea = null;
	//GameObject _scoreBar = null;
	//GameObject _currScore = null;
	//GameObject _fx = null;

	List<int> _scorePoints = new List<int>();
	List<GameObject> _progressBar = new List<GameObject>();

	public class QuestInfo
	{
		public uint questType;
		public int questIdx;
		public uint questFinishedTimes;
		public int questTotalTimes;
		public int questScroe;
		
		public string questTitile;
		public string questDescript;
		
		public bool finished;
		public GameObject goQuestBtn;
		public GameObject goFinished;
	}
	
	public class AwardBoxInfo
	{
		public uint awardBoxId;
		public bool finished;
		public int needSocre;
		
		public int itemId1;
		public int itemNum1;
		public int itemId2;
		public int itemNum2;
		public int itemId3;
		public int itemNum3;
		
		public GameObject goAwardBoxBtn;
		public GameObject goAwardLabel;
		public GameObject goIconBtn1;
		public GameObject goIconBtn2;
		public GameObject goIconBtn3;
		
	}
	
	public int m_curScore = 0;
	public int m_maxScore = 100;

	public List<QuestInfo> m_questInfos= new List<QuestInfo>();
	public List<AwardBoxInfo> m_awardBoxInfos = new List<AwardBoxInfo>();

	
	void InitScorePoint()
	{
		
		List<AwardBoxInfo> awardBoxInfos = new List<AwardBoxInfo>();
		
		Hashtable awardBoxDB = sdConfDataMgr.Instance().m_awardBoxDB;
		foreach (DictionaryEntry item in awardBoxDB)
		{
			AwardBox awardBox = item.Value as AwardBox;
			
			AwardBoxInfo awardBoxInfo = new AwardBoxInfo();			
			awardBoxInfo.awardBoxId = awardBox.BoxID;
			awardBoxInfo.needSocre = awardBox.NeedScore;
			awardBoxInfo.finished = false;
			
			awardBoxInfo.itemId1 = awardBox.ItemId1;
			awardBoxInfo.itemNum1 = awardBox.ItemNum1;
			awardBoxInfo.itemId2 = awardBox.ItemId2;
			awardBoxInfo.itemNum2 = awardBox.ItemNum2;
			awardBoxInfo.itemId3 = awardBox.ItemId3;
			awardBoxInfo.itemNum3 = awardBox.ItemNum3;
			
			awardBoxInfo.goAwardBoxBtn = null;
			awardBoxInfo.goAwardLabel = null;
			
			//m_awardBoxInfos.Insert((int)awardBoxInfo.awardBoxId-1, awardBoxInfo);
			awardBoxInfos.Add (awardBoxInfo);
			
		}
		
		awardBoxInfos.Sort(delegate(AwardBoxInfo x, AwardBoxInfo y) 	        	
		                     {
			return x.awardBoxId.CompareTo(y.awardBoxId);
		});

		_scorePoints.Add(awardBoxInfos[0].needSocre);
		_scorePoints.Add(awardBoxInfos[1].needSocre-((m_awardBoxInfos[1].needSocre-m_awardBoxInfos[0].needSocre)/2));
		
		_scorePoints.Add(awardBoxInfos[1].needSocre);
		_scorePoints.Add(awardBoxInfos[2].needSocre-((m_awardBoxInfos[2].needSocre-m_awardBoxInfos[1].needSocre)/2));
		
		_scorePoints.Add(awardBoxInfos[2].needSocre);
		_scorePoints.Add(awardBoxInfos[3].needSocre-((m_awardBoxInfos[3].needSocre-m_awardBoxInfos[2].needSocre)/2));
		
		_scorePoints.Add(awardBoxInfos[3].needSocre);
		_scorePoints.Add(awardBoxInfos[4].needSocre-((m_awardBoxInfos[4].needSocre-m_awardBoxInfos[3].needSocre)/2));
		
		_scorePoints.Add(awardBoxInfos[4].needSocre);
		_scorePoints.Add(awardBoxInfos[4].needSocre+(m_awardBoxInfos[4].needSocre/5));
		
	}

    void RefreshDirt()
	{
		_questListDirt = false;
		foreach(QuestInfo item in m_questInfos)
		{
			if(item.finished == false)
			{
				if(item.questFinishedTimes == item.questTotalTimes)
					_questListDirt = true;
			}
		}
		
		_awardBoxDirt = false;
		foreach(AwardBoxInfo item in m_awardBoxInfos)
		{
			if(item.finished == false)
			{
				if(m_curScore >=item.needSocre)
					_awardBoxDirt = true;
			}
		}
		
		if(_awardBoxDirt || _questListDirt)
			m_dirt = true;
		else
			m_dirt = false;

	}

	public void UpdateAwardBox(CliProto.SC_GIFT_DAY_BOX_NTF netMsg)
	{
		m_awardBoxInfos.Clear();
		
		Hashtable awardBoxDB = sdConfDataMgr.Instance().m_awardBoxDB;
		foreach (DictionaryEntry item in awardBoxDB)
		{
			AwardBox awardBox = item.Value as AwardBox;
			
			AwardBoxInfo awardBoxInfo = new AwardBoxInfo();			
			awardBoxInfo.awardBoxId = awardBox.BoxID;
			awardBoxInfo.needSocre = awardBox.NeedScore;
			awardBoxInfo.finished = false;
			
			awardBoxInfo.itemId1 = awardBox.ItemId1;
			awardBoxInfo.itemNum1 = awardBox.ItemNum1;
			awardBoxInfo.itemId2 = awardBox.ItemId2;
			awardBoxInfo.itemNum2 = awardBox.ItemNum2;
			awardBoxInfo.itemId3 = awardBox.ItemId3;
			awardBoxInfo.itemNum3 = awardBox.ItemNum3;
			
			awardBoxInfo.goAwardBoxBtn = null;
			awardBoxInfo.goAwardLabel = null;
			
			//m_awardBoxInfos.Insert((int)awardBoxInfo.awardBoxId-1, awardBoxInfo);
			m_awardBoxInfos.Add (awardBoxInfo);
			
		}
		
		m_awardBoxInfos.Sort(delegate(AwardBoxInfo x, AwardBoxInfo y) 	        	
		                     {
			return x.awardBoxId.CompareTo(y.awardBoxId);
		});
		
		for(int i=0; i<netMsg.m_Count; i++)
		{
			foreach(AwardBoxInfo item in m_awardBoxInfos)
			{
				if(netMsg.m_BoxID[i] == item.awardBoxId)
				{
					item.finished = true;
					break;
				}
			}
		}

		RefreshDirt();
		
		if(m_goWndRoot!=null && _bWndOpen)
		{
			RefreshAwardBox();
			RefreshQuestList(false);
		}
		
	}
	
	public void UpdateQuestList(CliProto.SC_GIFT_DAY_UPD netMsg)
	{
		foreach(QuestInfo item in m_questInfos)
		{
			if(item.questType == netMsg.m_Info.m_QuestId)
			{
				item.questFinishedTimes = netMsg.m_Info.m_FinishProgress;

				
				if(netMsg.m_Info.m_ScoreReceived == 0)
				{
					item.finished = false;
				}
				else
				{
					m_curScore += item.questScroe;
					item.finished = true;
				}
				
				break;
			}
		}
		
		RefreshDirt();
		
		if(m_goWndRoot!=null && _bWndOpen)
		{
			RefreshAwardBox();
			RefreshQuestList(false);
		}
		
	}
	
	public void UpdateQuestList(CliProto.SC_GIFT_DAY_NTF netMsg)
	{
		m_questInfos.Clear();
		
		Hashtable dailyQuestDB = sdConfDataMgr.Instance().m_dailyQuestDB;
		foreach (DictionaryEntry item in dailyQuestDB)
		{
			DailyQuest dailyQuest = item.Value as DailyQuest;
			
			QuestInfo questInfo = new QuestInfo();
			questInfo.questType = (uint)dailyQuest.DayQuestType;
			questInfo.questFinishedTimes = 0;
			questInfo.questIdx = dailyQuest.DayQuestId;
			questInfo.questTotalTimes = dailyQuest.DayQuestCount;
			questInfo.questScroe = dailyQuest.DayQuestScore;
			questInfo.questTitile = dailyQuest.QuestTitle;
			questInfo.questDescript = dailyQuest.QuestDescription;
			questInfo.finished = false;
			
			//m_questInfos.Insert(questInfo.questIdx-1, questInfo);
			m_questInfos.Add (questInfo);
			
			//if(questInfo.questScroe > m_maxScore)
			//	m_maxScore = questInfo.questScroe;
		}
		
		
		m_questInfos.Sort(delegate(QuestInfo x, QuestInfo y) 	        		                     
		                  {
			return x.questIdx.CompareTo(y.questIdx);
		});
		
		for(int i=0; i<netMsg.m_Count; i++)
		{
			foreach(QuestInfo item in m_questInfos)
			{
				if(item.questType == netMsg.m_Info[i].m_QuestId)
				{
					item.questFinishedTimes = netMsg.m_Info[i].m_FinishProgress;
					
					if(netMsg.m_Info[i].m_ScoreReceived == 0)
					{	
						item.finished = false;
					}
					else
					{
						m_curScore += item.questScroe;
						item.finished = true;
					}
				}
			}
		}
		
		if(m_goWndRoot!=null && _bWndOpen)
		{
			RefreshAwardBox();
			RefreshQuestList(false);
		}
	}

	void RefreshAwardBoxIcon(int itemId, int num, GameObject goRoot)
	{
		if(itemId == 100)
		{
			goRoot.transform.FindChild("icon1").gameObject.SetActive(true);
			goRoot.transform.FindChild("icon2").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon3").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon4").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon5").gameObject.SetActive(false);
		}
		else if(itemId == 101)
		{
			goRoot.transform.FindChild("icon1").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon2").gameObject.SetActive(true);
			goRoot.transform.FindChild("icon3").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon4").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon5").gameObject.SetActive(false);
		}
		else if(itemId == 200)
		{
			goRoot.transform.FindChild("icon1").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon2").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon3").gameObject.SetActive(true);
			goRoot.transform.FindChild("icon4").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon5").gameObject.SetActive(false);
		}
		else if(itemId == 400501)
		{
			goRoot.transform.FindChild("icon1").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon2").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon3").gameObject.SetActive(false);
			goRoot.transform.FindChild("icon4").gameObject.SetActive(true);
			goRoot.transform.FindChild("icon5").gameObject.SetActive(false);
		}
		
		goRoot.transform.FindChild("Num").gameObject.GetComponent<UILabel>().text = num.ToString();
		
	}
	
	int ScoreToPoint(int score)
	{
		int point = 0;
		foreach(int item in _scorePoints)
		{
			if(score >= item)
				point++;					
		}

		return point;
	}


	void RefreshProgressBar(int point)
	{	
		int counter = 1;
		foreach(GameObject item in _progressBar)
		{
			if(point >= counter)
				item.SetActive(true);
			else
				item.SetActive(false);

			counter++;
		}
	}

	void RefreshAwardBox()
	{
		m_awardBoxInfos[0].goAwardBoxBtn = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/btn_award1").gameObject;
		m_awardBoxInfos[1].goAwardBoxBtn = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/btn_award2").gameObject;
		m_awardBoxInfos[2].goAwardBoxBtn = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/btn_award3").gameObject;
		m_awardBoxInfos[3].goAwardBoxBtn = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/btn_award4").gameObject;
		m_awardBoxInfos[4].goAwardBoxBtn = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/btn_award5").gameObject;
		
		
		m_awardBoxInfos[0].goAwardLabel = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/award_Label1").gameObject;
		m_awardBoxInfos[1].goAwardLabel = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/award_Label2").gameObject;
		m_awardBoxInfos[2].goAwardLabel = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/award_Label3").gameObject;
		m_awardBoxInfos[3].goAwardLabel = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/award_Label4").gameObject;
		m_awardBoxInfos[4].goAwardLabel = m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/award_Label5").gameObject;
		
		RefreshProgressBar(ScoreToPoint(m_curScore));

		foreach(AwardBoxInfo item in m_awardBoxInfos)
		{
			//if(item.finished || item.needSocre>AwardCenterWnd.Instance.m_curScore )
			if(item.finished)
			{				
				item.goAwardBoxBtn.GetComponent<UIButton>().enabled = false;
				item.goAwardBoxBtn.GetComponent<UIButton>().UpdateColor(false, true);
				item.goAwardBoxBtn.transform.FindChild("Fx_Tishi1Prefab").gameObject.SetActive(false);
				item.goAwardBoxBtn.transform.FindChild("Finish").gameObject.SetActive(true);
			}
			else
			{
				item.goAwardBoxBtn.transform.FindChild("Finish").gameObject.SetActive(false);
				item.goAwardBoxBtn.GetComponent<UIButton>().enabled = true;
				item.goAwardBoxBtn.GetComponent<UIButton>().UpdateColor(true, true);
				
				if(m_curScore >=item.needSocre)
					item.goAwardBoxBtn.transform.FindChild("Fx_Tishi1Prefab").gameObject.SetActive(true);
				else
					item.goAwardBoxBtn.transform.FindChild("Fx_Tishi1Prefab").gameObject.SetActive(false);
			}
			
			item.goAwardBoxBtn.transform.FindChild("iconbg").gameObject.SetActive(true);
			RefreshAwardBoxIcon(item.itemId1, item.itemNum1, 
			                    item.goAwardBoxBtn.transform.FindChild
			                    ("iconbg").gameObject);
			
			item.goAwardLabel.GetComponent<UILabel>().text = item.needSocre.ToString();
			UIEventListener.Get(item.goAwardBoxBtn).onClick = OnAwardBoxBtn;
		}
	}
	
	void OnAwardBoxBtn(GameObject go)
	{
		foreach(AwardBoxInfo item in m_awardBoxInfos)
		{
			if(item.goAwardBoxBtn==go)
			{
				if( item.goAwardBoxBtn.GetComponent<UIButton>().isEnabled && item.needSocre<=m_curScore)
				{
					CliProto.CS_GIFT_DAY_BOX_REQ netMsg = new CliProto.CS_GIFT_DAY_BOX_REQ();
					netMsg.m_BoxID = item.awardBoxId;
					SDNetGlobal.SendMessage(netMsg);

					
					sdUICharacter.Instance.ShowSuccessPanel();
					//sdUICharacter.Instance.ShowMsgLine("恭喜你获得奖励！", Color.white);
				}
				else if(item.finished)
				{
					sdUICharacter.Instance.ShowMsgLine("你已经领过了！", Color.white);
				}
				else
				{
					sdUICharacter.Instance.ShowMsgLine("达到" + item.needSocre.ToString() + 
					                                   "积分才能领取哟！", Color.white);
				}
			}
		}
	}
	
	void RefreshQuestList(bool resetPos)
	{
		_goQuestBlockTemplate = m_goWndRoot.transform.FindChild
			("EverydayQuest/QuestListView/DraggableArea/QuestBlock").gameObject;
		
		_goQuestBlockTemplate.SetActive(false);
		
		_goDraggableArea = m_goWndRoot.transform.FindChild
			("EverydayQuest/QuestListView/DraggableArea").gameObject;
		
		GameObject questList = null;
		if(m_goWndRoot.transform.FindChild("EverydayQuest/QuestListView/DraggableArea/QuestList"))
		{
			questList = m_goWndRoot.transform.FindChild
				("EverydayQuest/QuestListView/DraggableArea/QuestList").gameObject;
			GameObject.DestroyImmediate(questList);	
		}
		
		questList = new GameObject("QuestList");
		questList.transform.parent = _goDraggableArea.transform;
		questList.transform.localPosition = Vector3.zero;
		questList.transform.localScale = new Vector3(1,1,1);
		
		int col = 0;
		int row = 0;
		int counter = 0;
		
		foreach(QuestInfo item in m_questInfos)
		{
			GameObject goQuestBlock = GameObject.Instantiate(_goQuestBlockTemplate) as GameObject;
			goQuestBlock.transform.parent = questList.transform;
			
			goQuestBlock.transform.localPosition = new Vector3(-295.0f + (col * 600), 
			                                                   330.0f - (row * 140), 
			                                                   0.0f);
			goQuestBlock.transform.localScale = new Vector3(1,1,1);

			goQuestBlock.transform.FindChild("Point").GetComponent<UISprite>().spriteName = "s" + item.questIdx.ToString();
			goQuestBlock.SetActive(true);
			
			item.goQuestBtn = goQuestBlock.transform.FindChild("Button").gameObject;
			
			//goQuestBlock.transform.FindChild("Point/Label").
			//	GetComponent<UILabel>().text = item.questScroe.ToString();
			
			goQuestBlock.transform.FindChild("Title1").
				GetComponent<UILabel>().text = item.questTitile.ToString();

			goQuestBlock.transform.FindChild("Title2").
				GetComponent<UILabel>().text = "获得" + item.questScroe.ToString() + "点积分";
			
			goQuestBlock.transform.FindChild("Descript").
				GetComponent<UILabel>().text = item.questDescript + "[008888]" + 
					"(" + item.questFinishedTimes.ToString() + "/" + item.questTotalTimes + ")";
			
			
			if(item.finished)
			{
				item.goQuestBtn.SetActive(false);
				goQuestBlock.transform.FindChild("Finished").gameObject.SetActive(true);
			}
			else 
			{
				if(item.questFinishedTimes == item.questTotalTimes)
				{
					item.goQuestBtn.transform.FindChild("Background/Label").
						GetComponent<UILabel>().text = "完成";
					
					item.goQuestBtn.SetActive(true);
					UIEventListener.Get(item.goQuestBtn).onClick = OnQuestBlockBtn;
				}
				else
				{
					item.goQuestBtn.transform.FindChild("Background/Label").
						GetComponent<UILabel>().text = "前往";
					
					item.goQuestBtn.SetActive(false);
					UIEventListener.Get(item.goQuestBtn).onClick = OnQuestBlockBtn;
				}
				
				goQuestBlock.transform.FindChild("Finished").gameObject.SetActive(false);
			}
			
			col++;
			if(col>1)
			{
				row++;
				col=0;
			}
		}
		
		_goDraggableArea.GetComponent<UIWidget>().height = row * 80;


		m_goWndRoot.transform.FindChild("EverydayQuest/QuestListView").gameObject.SetActive(true);

		if(resetPos)
		{
			m_goWndRoot.transform.FindChild("EverydayQuest/QuestListView").gameObject.GetComponent
				<UIDraggablePanel>().ResetPosition();
		}	
		
	}
	
	void OnQuestBlockBtn(GameObject go)
	{
		foreach(QuestInfo item in m_questInfos)
		{
			if(item.goQuestBtn == go)
			{
				if(item.questFinishedTimes == item.questTotalTimes)
				{
					CliProto.CS_GIFT_DAY_SCORE_REQ netMsg = new CliProto.CS_GIFT_DAY_SCORE_REQ();
					netMsg.m_QuestId = item.questType;
					SDNetGlobal.SendMessage(netMsg);
					
					sdUICharacter.Instance.ShowMsgLine("获得" + item.questScroe.ToString() + 
					                                   "积分", Color.white);
					
					
					//_fx.SetActive(false);
					//_fx.SetActive(true);
					//_fx.GetComponentInChildren<ParticleSystem>().Stop();
					//_fx.GetComponentInChildren<ParticleSystem>().Play();
					
				}
				else
				{
					
				}
			}
		}
		
	}
	
	void Start () 
	{

	}

	void Update () 
	{

	}
	
	void OnDestory()
	{
	
	}
	
	public void Init()
	{

	}

	void OnCloseBtn(GameObject go)
	{
		ClosePanel();
	}

	public void OnShowWndAniFinish()
	{
		//m_goWndRoot.transform.FindChild("EverydayQuest/QuestListView").gameObject.SetActive(true);
		
	}

	public void OpenPanel()
	{
		_bWndOpen = true;
		if( m_goWndRoot != null )
		{
			AwardCenterWnd.Instance.m_goWndRoot.SetActive(false);

			m_goWndRoot.SetActive(true);
			WndAni.ShowWndAni(m_goWndRoot,true,"bg_grey");

			RefreshAwardBox();
			RefreshQuestList(true);
			
			//m_goWndRoot.transform.FindChild("EverydayQuest/QuestListView").gameObject.SetActive(false);
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.info = "EverydayQuestPanel";
			sdResourceMgr.Instance.LoadResource("UI/AwardCenter/$EverydayQuestWnd.prefab", 
		    	                                OnPanelLoaded, 
		        	                            param, 
		            	                        typeof(GameObject));

			
		}
	}
	
	public void OnPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info != "EverydayQuestPanel")
			return;

		m_goWndRoot = GameObject.Instantiate(obj) as GameObject;
		m_goWndRoot.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_goWndRoot.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_goWndRoot.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		m_goWndRoot.SetActive(true);

		_closeBtn =  m_goWndRoot.transform.FindChild("AwardFrame/topbar/btn_close").GetComponent<UIButton>();
		UIEventListener.Get(_closeBtn.gameObject).onClick = OnCloseBtn;

		_progressBar.Clear();
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot1/progressFull").gameObject);
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot2/progressHalfFull").gameObject);
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot2/progressFull").gameObject);
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot3/progressHalfFull").gameObject);
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot3/progressFull").gameObject);
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot4/progressHalfFull").gameObject);
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot4/progressFull").gameObject);
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot5/progressHalfFull").gameObject);
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot5/progressFull").gameObject);
		_progressBar.Add(m_goWndRoot.transform.FindChild("EverydayQuest/sp_awardline/progressSlot6/progressFull").gameObject);

		InitScorePoint();

		sdUILoading.ActiveSmallLoadingUI(false);
		OpenPanel();	
	}

	public bool IsOpen() { return _bWndOpen; }

	public void ClosePanel()
	{
		if( m_goWndRoot == null )
			return;

		_bWndOpen = false;

		AwardCenterWnd.Instance.m_goWndRoot.SetActive(true);

		//m_goWndRoot.SetActive(false);
		m_goWndRoot.transform.FindChild("EverydayQuest/QuestListView").gameObject.SetActive(false);
		WndAni.HideWndAni(m_goWndRoot,true,"bg_grey");
	}
}

