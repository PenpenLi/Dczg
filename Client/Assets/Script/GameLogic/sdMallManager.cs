using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class sdMallManager : Singleton<sdMallManager> 
{
    [HideInInspector]
    public ArrayList m_aNormalProducts = new ArrayList();
    [HideInInspector]
    public ArrayList m_aGiftProducts = new ArrayList();

    [HideInInspector]
    public const string HUIZHANG_SPRITE = "icon_xz";
    [HideInInspector]
    public const string JINBI_SPRITE = "icon_jinb";

	public bool m_buyActionTips = false;
	public uint m_iEnergyCount = 0;
	private uint m_iRemainCount = 10;

	public GameObject m_VIPPanel = null;
	public GameObject m_MainPanel = null;
	public GameObject m_BatchPanel = null;
	public GameObject m_BuyPetExPanel = null;

	private GameObject m_OnePetObtainPanel = null;
	public bool m_onePetObtainPanelOpen = false;
	private int _currBuyPetType = 0;
  	private uint _currTemplateId = 0;
	
	private GameObject m_TenPetObtainPanel = null;
	public bool m_tenPetObtainPanelOpen = false;
    private GameObject m_GoodsTipPanel = null;
	public GameObject m_magicTowerPanel = null;

	
	public uint m_iCurrentVIPLevel = 1;
	public Dictionary<uint, uint> m_ProductBoughtCount;
	public int m_iCurrentBatchBoughtProductId;
    private uint[] m_PetResultTemplates;
    private byte m_PetResultSize = 0;
    private UInt64 m_PetRemainTimeCheap = 0;        /// 低级档上次免费购买时间(单位毫秒) .
    private byte m_PetRemainCount = 10;	        /// 一定得紫色战魂剩余次数 .
    private UInt64 m_PetRemainTimeExpensive = 0;	/// 高档上次免费购买时间(单位毫秒) .

	public	int m_RMBCount;	/// 充值次数         .
	public	int m_RMBSum;	/// 充值RMB总额   

	public enum GoodsClass
	{
		normalGoods = 0,
		giftGoods = 1,
	}

	public class ActionPointInfo
	{
		public int SpiritNum;
		public int CostNonCash;
		public int NeedVIPLevel;
	}
	public List<ActionPointInfo> m_actionPointInfos = new List<ActionPointInfo>();


	public class GHomeProduct
	{
		public string ProductCode;
		public string ItemName;
		public int price;
		public int type;

	}
	public Dictionary<string, GHomeProduct> m_gHomeProducts = new Dictionary<string, GHomeProduct>();

    void Awake()
    {
        m_PetResultTemplates = new uint[(int)HeaderProto.MAX_SHOP_BUY_PET_RESULT_COUNT];
        m_PetResultSize = 0;

        m_ProductBoughtCount = new Dictionary<uint, uint>();

        Hashtable shopTable = sdConfDataMgr.Instance().GetShopsTable();
        if(shopTable != null)
        {
            foreach (DictionaryEntry de in shopTable)
            {
                string key = de.Key.ToString();
                Hashtable product = de.Value as Hashtable;

                int goodsClass = int.Parse(product["GoodsClass"].ToString());
                if (goodsClass == (int)sdMallManager.GoodsClass.normalGoods)
                    m_aNormalProducts.Add(key);
                else if(goodsClass == (int)sdMallManager.GoodsClass.giftGoods)
                    m_aGiftProducts.Add(key);
            }
        }
    }

	// Use this for initialization
	void Start () 
	{
		m_actionPointInfos.Clear();
		Hashtable actionPointDB = sdConfDataMgr.Instance().m_actionPointDB;
		foreach(DictionaryEntry item in actionPointDB)
		{
			ActionPoint actionPoint = item.Value as ActionPoint;
			ActionPointInfo actionPointInfo = new ActionPointInfo();
			actionPointInfo.SpiritNum = actionPoint.SpiritNum;
			actionPointInfo.CostNonCash = actionPoint.CostNonCash;
			actionPointInfo.NeedVIPLevel = actionPoint.NeedVIPLevel;
				
			m_actionPointInfos.Add (actionPointInfo);
		}
	
		m_actionPointInfos.Sort(delegate(ActionPointInfo x, ActionPointInfo y) 			                 
		{
			return x.SpiritNum.CompareTo(y.SpiritNum);
		});

		/*
		if (Application.platform == RuntimePlatform.Android ||
		    Application.platform == RuntimePlatform.IPhonePlayer)
		{
			GHome.GetInstance().GetAreaConfig((code, msg, data) =>
			{
				if (code == 0)
				{
					string json = (string)data["data"];
					JsonNode NODE = new JsonNode();
					int iPOS = 0;
					NODE.Parse(json,ref iPOS);
					List<JsonNode> lstAREA = new List<JsonNode>();
					NODE.FindListHasAttibuteName("area_code",lstAREA);
					//foreach()
					//SDGlobal.phoneNumber = (string)data["userId"];
					//SDGlobal.ticket = (string)data["ticket"];
				}
				else
				{
					sdUICharacter.Instance.ShowMsgLine("GHome.GetInstance().GetAreaConfig() failure！", Color.red);
				}
			});
		}
		*/

	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	public GameObject VIPPanelOb
	{
		get { return m_VIPPanel; }
	}
	
	public GameObject MallPanelOb
	{
		get { return m_MainPanel; }
	}

	public GameObject BuyPetExPanelOb
	{
		get { return m_BuyPetExPanel; }
	}

    public UInt64 PetRemainTimeCheap
    {
        get { return m_PetRemainTimeCheap; }
        set { m_PetRemainTimeCheap = value; }
    }
    public byte PetRemainCount
    {
        get { return m_PetRemainCount; }
        set 
		{ 
			m_PetRemainCount = value; 
			
			if (m_magicTowerPanel != null)
			{
				m_magicTowerPanel.GetComponent<MagicTowerPanel>().RefreshLblPetRemainCount(m_PetRemainCount);
			}
			
			if (m_TenPetObtainPanel != null)
			{
				m_TenPetObtainPanel.GetComponent<TenPetObtainPanel>().RefreshLblPetRemainCount(m_PetRemainCount);
			}

		}
    }
    public byte PetResultSize
    {
        get { return m_PetResultSize; }
        set { m_PetResultSize = value; }
    }
    public UInt64 PetRemainTimeExpensive
    {
        get { return m_PetRemainTimeExpensive; }
        set 
        { 
            m_PetRemainTimeExpensive = value;

			if (m_magicTowerPanel != null)
            {
				m_magicTowerPanel.GetComponent<MagicTowerPanel>().RefreshLblPetRemainCount(m_PetRemainCount);
            }

            if (m_TenPetObtainPanel != null)
            {
                m_TenPetObtainPanel.GetComponent<TenPetObtainPanel>().RefreshLblPetRemainCount(m_PetRemainCount);
            }
        }
    }

    public void SetPetTemplates(uint[] aTemplates, byte count)
    {
        if (count > HeaderProto.MAX_SHOP_BUY_PET_RESULT_COUNT)
            return;

        byte byPetCount = 0;
        
        for (int i = 0; i < count; ++i)
        {
            uint itemId = aTemplates[i];
            Hashtable info = sdConfDataMgr.Instance().GetItemById(itemId.ToString());
            if (info != null)
            {
                int itemType = int.Parse(info["Class"].ToString());
                if (itemType == (int)HeaderProto.EItemClass.ItemClass_Pet_Item)
                {
                    m_PetResultTemplates[byPetCount] = uint.Parse(info["Expend"].ToString());
                    byPetCount++;
                }
            }
        }
        m_PetResultSize = byPetCount;
    }

    public uint GetResultTemplates(int idx)
    {
        if (idx >= m_PetResultSize)
            return 0;
        return m_PetResultTemplates[idx];
    }

	public GameObject OnePetObtainPanelOb
	{
		get { return m_OnePetObtainPanel; }
	}

	public GameObject TenPetObtainPanelOb
	{
		get { return m_TenPetObtainPanel; }
	}

    public GameObject GoodsTipPanelOb
    {
        get { return m_GoodsTipPanel; }
    }

	public void OpenBuyLiveWnd()
	{		
		m_iRemainCount = 10 - m_iEnergyCount;
		if (m_iRemainCount > 0)
		{
			string msg1 = "花费";
			string msg2 = GetBadgeCount(m_iEnergyCount);
			string msg3 = "徽章即可在现有的体力基础上再获得";
			string msg4 = GetEnergyCount();
			string msg5 = "体力。\n您今天还可以购买";
			string msg6 = m_iRemainCount.ToString();
			string msg7 = "次。";
			string msg = msg1 + msg2 + msg3 + msg4 + msg5 + msg6 +msg7;
		 	sdUICharacter.Instance.ShowOkCanelMsg(msg, MsgConfirm, null);
		}

		if (m_iRemainCount == 0)
		{
			sdUICharacter.Instance.ShowOkMsg("今日购买次数已用尽！", null);
		}
	}
	
	public void ShowBoughtResult (uint count)
	{
		m_iEnergyCount = count;
	}

	public void OnMsgGoodsList(CliProto.SC_SHOP_GET_GOODSLIST_ACK refMsg)
	{
		m_ProductBoughtCount.Clear();

		byte count = refMsg.m_Count;
        for (int i = 0; i < count && i < HeaderProto.MAX_SHOP_GOODS_COUNT; ++i)
		{
			CliProto.SGoodsInfo productInfo = refMsg.m_GOODSINFOS[i];
			m_ProductBoughtCount[productInfo.m_GoodsId] = productInfo.m_Num;
		}

		if (m_MainPanel!=null && m_MainPanel.GetComponent<MallPanel>().m_isPanelOpen)
		{
			m_MainPanel.GetComponent<MallPanel>().RefreshTabPanel((int)MallPanel.TabName.TAB_NAME_VIP, false);
			m_MainPanel.GetComponent<MallPanel>().RefreshTabPanel((int)MallPanel.TabName.TAB_NAME_MALL, false);
		}
	}

    public void OnMsgGoods(CliProto.SC_SHOP_BUY_GOODS_ACK refMsg)
    {
        CliProto.SGoodsInfo goodsInfo = refMsg.m_TemplateID[0];
        int goodsId = (int)goodsInfo.m_GoodsId;
        int goodsNum = (int)goodsInfo.m_Num;

        Hashtable productInfo = sdConfDataMgr.Instance().GetItemById(goodsId.ToString());
        int quality = int.Parse(productInfo["Quility"].ToString());
        string goodsName = productInfo["ShowName"].ToString();

        string colorStr = null;
        string msgStr = null;
        if (quality == 0)
            colorStr = "[cccccc]";
        else if (quality == 1)
            colorStr = "[ffffff]";
        else if (quality == 2)
            colorStr = "[00ff00]";
        else if (quality == 3)
            colorStr = "[0000ff]";
        else if (quality == 4)
            colorStr = "[9933cc]";
        else if (quality == 5)
            colorStr = "[ffff00]";

        msgStr = string.Format("购买成功,获得{0}{1}[-] x{2}!", colorStr, goodsName, goodsNum);
        sdUICharacter.Instance.ShowOkMsg(msgStr, null);
    }

	public void CurrentVIPLevel (uint vipLevel)
	{
		m_iCurrentVIPLevel = vipLevel;
	}

	public void UpdateRMBInfo(int rmbCount, int rmbSum)
	{
		m_RMBCount = rmbCount;
		m_RMBSum = rmbSum;

		if (m_MainPanel != null)
		{
			if(m_MainPanel.GetComponent<MallPanel>().m_isPanelOpen)
			{	
				m_MainPanel.GetComponent<MallPanel>().RefreshTabPanel((int)MallPanel.TabName.TAB_NAME_VIP, false);
			}
		}

	}

	public void RefreshAttribute ()
	{
		
	}

	protected void MsgConfirm ()
	{
		if (int.Parse(GetCurrentBadgeCount()) >= int.Parse(GetBadgeCount(m_iEnergyCount)))
			sdMallMsg.Send_CS_SHOP_BUY_ACTION_POINT_REQ ();
		else
		{
			ActiveVIPPanel(null);
		}
	}

	private string GetBadgeCount(uint energyCount)
	{
		Hashtable m_shopActionPoint = null;
		m_shopActionPoint = sdConfDataMgr.Instance ().GetShopActionPoints (energyCount.ToString ());

		if (m_shopActionPoint == null)
			return null;

		if (m_shopActionPoint ["SpiritNum"].ToString () != null) 
			return m_shopActionPoint["CostNonCash"].ToString();
		else 
			return null;
	}

	private string GetEnergyCount()
	{
		sdMainChar mc = sdGameLevel.instance.mainChar;
		if( mc != null ) 
			return mc.Property["MaxEP"].ToString();
		else
			return null;
	}

	private string GetCurrentBadgeCount()
	{
		sdMainChar mc = sdGameLevel.instance.mainChar;
		if (mc != null)
			return mc.GetBaseProperty () ["Cash"].ToString ();
		else
			return null;
	}

	public void OnMagicTowerPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if (m_magicTowerPanel != null)
			return;
		
		m_magicTowerPanel = GameObject.Instantiate(obj) as GameObject;
		if (m_magicTowerPanel)
		{
			m_magicTowerPanel.SetActive(true);
			m_magicTowerPanel.transform.parent = sdGameLevel.instance.UICamera.transform;
			m_magicTowerPanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			m_magicTowerPanel.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			ActiveMagicTowerPanel(null);
		}
	}
	
	public void ActiveMagicTowerPanel(GameObject PreWnd)
	{
		if(m_magicTowerPanel==null)
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource("UI/Shop/$MagicTowerPanel.prefab", OnMagicTowerPanelLoaded, param);
			return;
		}

		if(PreWnd)
			PreWnd.SetActive(false);

		WndAni.ShowWndAni(m_magicTowerPanel,true,"w_black");
		//m_magicTowerPanel.SetActive(true);
		//sdUICharacter.Instance.ShowFullScreenUI(true);
	}

    public void LoadBuyPetExPanel(ResLoadParams param, UnityEngine.Object obj)
    {
        sdUILoading.ActiveSmallLoadingUI(false);
        if (m_BuyPetExPanel != null)
            return;

        m_BuyPetExPanel = GameObject.Instantiate(obj) as GameObject;
        if (m_BuyPetExPanel)
        {
            m_BuyPetExPanel.SetActive(true);
            m_BuyPetExPanel.transform.parent = sdGameLevel.instance.UICamera.transform;
            m_BuyPetExPanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            m_BuyPetExPanel.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
    public void ActiveBuyPetExPanel()
    {
        if (m_BuyPetExPanel == null)
        {
            sdUILoading.ActiveSmallLoadingUI(true);
            ResLoadParams param = new ResLoadParams();
            sdResourceMgr.Instance.LoadResource("UI/Shop/$UniqueBuyPanel.prefab", LoadBuyPetExPanel, param);
            return;
        }

        m_BuyPetExPanel.SetActive(true);
    }

	public void LoadBatchPanel(ResLoadParams param, UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if (m_BatchPanel != null)
			return;

		m_BatchPanel = GameObject.Instantiate(obj) as GameObject;
		if (m_BatchPanel)
		{
			m_BatchPanel.SetActive(true);
			m_BatchPanel.transform.parent = sdGameLevel.instance.UICamera.transform;
			m_BatchPanel.transform.localScale = new Vector3(1f, 1f, 1f);
			m_BatchPanel.transform.localPosition = new Vector3(0f, 0f, 0f);
		}
	}

	public void ActiveBatchPanel()
	{
		if( m_BatchPanel == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource("UI/Shop/$BatchPanel.prefab", LoadBatchPanel, param);
			return;
		}
		
		m_BatchPanel.SetActive(true);
	}

	public void LoadVIPPanel(ResLoadParams param, UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_VIPPanel != null )
			return;
		
		m_VIPPanel = GameObject.Instantiate(obj) as GameObject;
		if( m_VIPPanel )
		{
			m_VIPPanel.SetActive(true);
			m_VIPPanel.transform.parent = sdGameLevel.instance.UICamera.transform;
			m_VIPPanel.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			m_VIPPanel.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		}
	}

	public void ActiveVIPPanel(GameObject PreWnd)
	{
		if( m_VIPPanel == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource("UI/Shop/$VIPPanel.prefab", LoadVIPPanel, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);

		m_VIPPanel.SetActive(true);
	}

	public void LoadMainPanel(ResLoadParams param, UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_MainPanel != null )
			return;
		
		m_MainPanel = GameObject.Instantiate(obj) as GameObject;
		if( m_MainPanel )
		{
			m_MainPanel.SetActive(true);
			m_MainPanel.transform.parent = sdGameLevel.instance.UICamera.transform;
			m_MainPanel.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			m_MainPanel.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);

			//sdUICharacter.Instance.ShowFullScreenUI(true);
			ActiveMainPanel(null, (bool)param.userdata0);
		}
	}
	
	public void ActiveMainPanel(GameObject PreWnd, bool activeChargeTab)
	{
		if( m_MainPanel == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = activeChargeTab;
			sdResourceMgr.Instance.LoadResource("UI/Shop/$MallPanel.prefab", LoadMainPanel, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		if(activeChargeTab)
			m_MainPanel.GetComponent<MallPanel>().ActiveTab(MallPanel.TabName.TAB_NAME_CHARGE);
		else
			m_MainPanel.GetComponent<MallPanel>().ActiveTab(MallPanel.TabName.TAB_NAME_MALL);
		
		WndAni.ShowWndAni(m_MainPanel,true,"w_black");
		//m_MainPanel.SetActive(true);
		//sdUICharacter.Instance.ShowFullScreenUI(true);
	}

	public void LoadOnePetObtainPanel(ResLoadParams param, UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_OnePetObtainPanel != null )
			return;

        int iBuyPetType = param.petInt;
        uint uiTemplateID = uint.Parse(param.petData0);

		m_OnePetObtainPanel = GameObject.Instantiate(obj) as GameObject;
		if( m_OnePetObtainPanel )
		{
			m_OnePetObtainPanel.transform.parent = sdGameLevel.instance.UICamera.transform;
			m_OnePetObtainPanel.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			m_OnePetObtainPanel.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);

			ActiveOnePetObtainPanel(iBuyPetType,uiTemplateID);
		}
	}

	public void OnOnePetObtainPanelShowAnimFinish()
	{
		m_OnePetObtainPanel.GetComponent<OnePetObtainPanel>().RefreshPanel(_currBuyPetType, _currTemplateId);
	}

    public void ActiveOnePetObtainPanel(int iBuyPetType, uint uiTemplateID)
	{
		if( m_OnePetObtainPanel == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
            param.petInt = iBuyPetType;
            param.petData0 = uiTemplateID.ToString();
			sdResourceMgr.Instance.LoadResource("UI/Shop/$Old_OnePetObtainPanel.prefab", LoadOnePetObtainPanel, param);
			return;
		}

		if(m_onePetObtainPanelOpen == false)
		{
			_currBuyPetType = iBuyPetType;
			_currTemplateId = uiTemplateID;

			m_OnePetObtainPanel.transform.FindChild("root/Background").gameObject.SetActive(false);
			m_OnePetObtainPanel.transform.FindChild("root/ShowAttribute").gameObject.SetActive(false);
			m_OnePetObtainPanel.transform.FindChild("root/BtnBuyAgain0").gameObject.SetActive(false);
			m_OnePetObtainPanel.transform.FindChild("root/BtnBuyAgain2").gameObject.SetActive(false);
			m_OnePetObtainPanel.transform.FindChild("root/BtnBuyAgain3").gameObject.SetActive(false);
			m_OnePetObtainPanel.transform.FindChild("root/PetView").gameObject.SetActive(false);

			WndAni.ShowWndAni(m_OnePetObtainPanel,false,"bg_grey");
		}
		else
		{
			m_OnePetObtainPanel.GetComponent<OnePetObtainPanel>().RefreshPanel(iBuyPetType, uiTemplateID);
		}

		m_OnePetObtainPanel.SetActive(true);
		m_onePetObtainPanelOpen = true;
	}
	
	public void OnTenPetObtainPanelShowAnimFinish()
	{
		m_TenPetObtainPanel.GetComponent<TenPetObtainPanel>().RefreshPanel();
	}

	public void LoadTenPetObtainPanel(ResLoadParams param, UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_TenPetObtainPanel != null )
			return;
		
		m_TenPetObtainPanel = GameObject.Instantiate(obj) as GameObject;
		if( m_TenPetObtainPanel )
		{
			m_TenPetObtainPanel.GetComponent<TenPetObtainPanel>().RefreshPanel();

			m_TenPetObtainPanel.SetActive(true);
			m_TenPetObtainPanel.transform.parent = sdGameLevel.instance.UICamera.transform;
			m_TenPetObtainPanel.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			m_TenPetObtainPanel.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);

			ActiveTenPetObtainPanel();
		}
	}
	
	public void ActiveTenPetObtainPanel()
	{
		if( m_TenPetObtainPanel == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource("UI/Shop/$TenPetObtainPanel.prefab", LoadTenPetObtainPanel, param);
			return;
		}

		m_TenPetObtainPanel.transform.FindChild("Item0").gameObject.SetActive(false);
		m_TenPetObtainPanel.transform.FindChild("Item1").gameObject.SetActive(false);
		m_TenPetObtainPanel.transform.FindChild("Item2").gameObject.SetActive(false);
		m_TenPetObtainPanel.transform.FindChild("Item3").gameObject.SetActive(false);
		m_TenPetObtainPanel.transform.FindChild("Item4").gameObject.SetActive(false);
		m_TenPetObtainPanel.transform.FindChild("Item5").gameObject.SetActive(false);
		m_TenPetObtainPanel.transform.FindChild("Item6").gameObject.SetActive(false);
		m_TenPetObtainPanel.transform.FindChild("Item7").gameObject.SetActive(false);
		m_TenPetObtainPanel.transform.FindChild("Item8").gameObject.SetActive(false);
		m_TenPetObtainPanel.transform.FindChild("Item9").gameObject.SetActive(false);

		if(m_tenPetObtainPanelOpen == false)
		{
			WndAni.ShowWndAni(m_TenPetObtainPanel,false,"bg_grey");
		}
		else
		{
			m_TenPetObtainPanel.GetComponent<TenPetObtainPanel>().RefreshPanel();
		}

		m_TenPetObtainPanel.SetActive(true);
		m_tenPetObtainPanelOpen = true;
	}

    public void LoadGoodsTipPanel(ResLoadParams param, UnityEngine.Object obj)
    {
        sdUILoading.ActiveSmallLoadingUI(false);
        if (m_GoodsTipPanel != null)
            return;

        int id = int.Parse(param.info);

        m_GoodsTipPanel = GameObject.Instantiate(obj) as GameObject;
        if (m_GoodsTipPanel)
        {
            m_GoodsTipPanel.GetComponent<GoodsTipPanel>().ShowGoodsConfirmInfo(id);

            m_GoodsTipPanel.SetActive(true);
            m_GoodsTipPanel.transform.parent = sdGameLevel.instance.UICamera.transform;
            m_GoodsTipPanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            m_GoodsTipPanel.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    public void ActiveGoodsTipPanel(int id)
    {
        if (m_GoodsTipPanel == null)
        {
            sdUILoading.ActiveSmallLoadingUI(true);
            ResLoadParams param = new ResLoadParams();
            param.info = id.ToString();
            sdResourceMgr.Instance.LoadResource("UI/Shop/$GoodsTipPanel.prefab", LoadGoodsTipPanel, param);
            return;
        }

        m_GoodsTipPanel.GetComponent<GoodsTipPanel>().ShowGoodsConfirmInfo(id);
        m_GoodsTipPanel.SetActive(true);
    }

	// 购买物品
	public bool BuyProduct(int productId, int count)
	{
		if (sdGameItemMgr.Instance.GetAllItem ((int)PanelType.Panel_Bag, -1).Count >= sdGameItemMgr.Instance.maxBagNum)
		{
			sdUICharacter.Instance.ShowOkMsg("背包已满，无法购买", null);
			return false;
		}
		
		sdMainChar mc = sdGameLevel.instance.mainChar;
		Hashtable productInfo = sdConfDataMgr.Instance ().GetShopProduct (productId.ToString ());
		if (mc != null && productInfo != null)
        {
            int ifSale = int.Parse(productInfo["IfSale"].ToString());
            int realPrice = 0;
            if (ifSale == 1)
                realPrice = int.Parse(productInfo["SalePrice"].ToString());
            else
                realPrice = int.Parse(productInfo["Price"].ToString());

            realPrice = realPrice * count;

            int costType = int.Parse(productInfo["CostType"].ToString());
            if (costType == 0)
            {
                // 使用徽章购买，目前策划决定此处的徽章是两种徽章之和

                int myCash = int.Parse(mc.GetBaseProperty()["Cash"].ToString()) +
                             int.Parse(mc.GetBaseProperty()["NonCash"].ToString());

                if (realPrice > myCash)
                {
                    // 徽章不足
                    //sdMallManager.Instance.ActiveVIPPanel(null);
					sdUICharacter.Instance.ShowOkMsg("徽章不足", null);
                    return false;
                }
            }
            else
            {
                // 使用金币购买

                int myNonMoney = int.Parse(mc.GetBaseProperty()["NonMoney"].ToString());
                if (realPrice > myNonMoney)
                {
                    sdUICharacter.Instance.ShowOkMsg("金币不足", null);
                    return false;
                }
            }

		}

		sdMallMsg.Send_CS_SHOP_BUY_GOODS_REQ((uint) productId, count);
		return true;
	}
}
