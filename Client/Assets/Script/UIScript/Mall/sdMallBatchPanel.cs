using UnityEngine;
using System.Collections;

public class sdMallBatchPanel : MonoBehaviour {

	private Transform m_ProductName = null;
	private Transform m_CloseBtn = null;
	private Transform m_MinusTenBtn = null;
	private Transform m_MinusOneBtn = null;
	private Transform m_PlusOneBtn = null;
	private Transform m_PlusTenBtn = null;
	private Transform m_CountInput = null;
	private UIInput m_CountUIInput = null;
	private Transform m_CancelBtn = null;
	private Transform m_ConfirmBtn = null;
	private UILabel m_ProductNameLabel = null;
	private UILabel m_TotalPriceLabel = null;

	private const int MAX_COUNT = 999;
	private int m_iMaxCount;
	private int m_iProductId;
	private int m_iPrice;
	private int m_iCountLastFrame = 1;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_CountUIInput != null)
		{
			int count = 0;
			if (m_CountUIInput.value != "")
				count = int.Parse(m_CountUIInput.value);

			if (count != m_iCountLastFrame)
			{
				m_iCountLastFrame = count;
				drawTotalPrice();
			}
		}
	}

	void OnEnable()
	{
		if (m_ProductName == null)
			m_ProductName = transform.FindChild("ProductName");

		if (m_CloseBtn == null)
		{
			m_CloseBtn = transform.FindChild("Close");
			if (m_CloseBtn != null)
			{
				UIButton btn = m_CloseBtn.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate(){ OnCloseBtnClicked(); });
			}
		}
		if (m_MinusTenBtn == null)
		{
			m_MinusTenBtn = transform.FindChild("MinusTen");
			if (m_MinusTenBtn != null)
			{
				UIButton btn = m_MinusTenBtn.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate(){ OnMinusTenBtnClicked(); });
			}
		}
		if (m_MinusOneBtn == null)
		{
			m_MinusOneBtn = transform.FindChild("MinusOne");
			if (m_MinusOneBtn != null)
			{
				UIButton btn = m_MinusOneBtn.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate(){ OnMinusOneBtnClicked(); });
			}
		}
		if (m_PlusOneBtn == null)
		{
			m_PlusOneBtn = transform.FindChild("PlusOne");
			if (m_PlusOneBtn != null)
			{
				UIButton btn = m_PlusOneBtn.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate(){ OnPlusOneBtnClicked(); });
			}
		}
		if (m_PlusTenBtn == null)
		{
			m_PlusTenBtn = transform.FindChild("PlusTen");
			if (m_PlusTenBtn != null)
			{
				UIButton btn = m_PlusTenBtn.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate(){ OnPlusTenBtnClicked(); });
			}
		}

		if (m_CountInput == null)
		{
			m_CountInput = transform.FindChild("CountInput");
			if (m_CountInput != null)
			{
				m_CountUIInput = m_CountInput.GetComponent<UIInput>();
				m_CountUIInput.onValidate = OnCountInputValidate;
			}
		}
		
		if (m_CancelBtn == null)
		{
			m_CancelBtn = transform.FindChild("CancelBtn");
			if (m_CancelBtn != null)
			{
				UIButton btn = m_CancelBtn.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate(){ OnCloseBtnClicked(); });
			}
		}
		
		if (m_ConfirmBtn == null)
		{
			m_ConfirmBtn = transform.FindChild("ConfirmBtn");
			if (m_ConfirmBtn != null)
			{
				UIButton btn = m_ConfirmBtn.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate(){ OnConfirmBtnClicked(); });
			}
		}

		if (m_ProductNameLabel == null)
		{
			Transform nameLabelTransform = transform.FindChild("ProductName");
			m_ProductNameLabel = nameLabelTransform.GetComponent<UILabel>();
		}

		if (m_TotalPriceLabel == null)
		{
			Transform totalPriceTransform = transform.FindChild("TotalPrice");
			m_TotalPriceLabel = totalPriceTransform.GetComponent<UILabel>();
		}

		// 得到可以购买的数量、单价 和 物品名称

		m_iMaxCount = MAX_COUNT;
		m_iProductId = sdMallManager.Instance.m_iCurrentBatchBoughtProductId;
		Hashtable productInfo = sdConfDataMgr.Instance ().GetShopProduct (m_iProductId.ToString ());
		if (productInfo != null)
		{
			int limitationType = int.Parse(productInfo["LimitationType"].ToString());
			if (limitationType != 0)
			{
				int limitationNum = int.Parse(productInfo["LimitationNum"].ToString());

				// 已经购买数量
				int boughtCount = 0;
				if (sdMallManager.Instance.m_ProductBoughtCount.ContainsKey((uint) m_iProductId))
					boughtCount = (int) sdMallManager.Instance.m_ProductBoughtCount[(uint) m_iProductId];

				m_iMaxCount = limitationNum - boughtCount;
				if (m_iMaxCount <= 0)
					Debug.LogError(string.Format("已购买数量{0}，限购数量{1}，此时不能再购买", boughtCount, limitationNum));
			}
			
			// 物品名称
			if (m_ProductNameLabel != null)
				m_ProductNameLabel.text = productInfo["GoodsName"].ToString();

			// 单价
			bool ifSale = int.Parse(productInfo["IfSale"].ToString()) == 1;
			if (ifSale)
				m_iPrice = int.Parse(productInfo["SalePrice"].ToString());
			else
				m_iPrice = int.Parse(productInfo["Price"].ToString());

			// 初始数量
			if (m_CountUIInput != null)
				m_CountUIInput.value = "1".ToString();

			// 总价
			drawTotalPrice();

            // 货币类型
            Transform coinTransform = transform.FindChild("Coin");
            if (coinTransform != null)
            {
                UISprite coinSprite = coinTransform.GetComponent<UISprite>();
                int costType = int.Parse(productInfo["CostType"].ToString());
                if (costType == 0)
                    coinSprite.spriteName = sdMallManager.HUIZHANG_SPRITE;
                else
                    coinSprite.spriteName = sdMallManager.JINBI_SPRITE;
            }
		}

	}

	private void OnCloseBtnClicked()
	{
		gameObject.SetActive(false);
	}
	
	private void OnMinusTenBtnClicked()
	{
		if (m_CountUIInput != null)
		{
			string countStr = m_CountUIInput.value;
			int count = 0;
			if (countStr != "")
				count = int.Parse(countStr);

			count = Mathf.Max(1, count - 10);
			m_CountUIInput.value = count.ToString();
		}
	}
	
	private void OnMinusOneBtnClicked()
	{
		if (m_CountUIInput != null)
		{
			string countStr = m_CountUIInput.value;
			int count = 0;
			if (countStr != "")
				count = int.Parse(countStr);
			
			count = Mathf.Max(1, count - 1);
			m_CountUIInput.value = count.ToString();
		}
	}
	
	private void OnPlusOneBtnClicked()
	{
		if (m_CountUIInput != null)
		{
			string countStr = m_CountUIInput.value;
			int count = 0;
			if (countStr != "")
				count = int.Parse(countStr);
			
			count = Mathf.Min(m_iMaxCount, count + 1);
			m_CountUIInput.value = count.ToString();
		}
	}
	
	private void OnPlusTenBtnClicked()
	{
		if (m_CountUIInput != null)
		{
			string countStr = m_CountUIInput.value;
			int count = 0;
			if (countStr != "")
				count = int.Parse(countStr);
			
			count = Mathf.Min(m_iMaxCount, count + 10);
			m_CountUIInput.value = count.ToString();
		}
	}

	private char OnCountInputValidate(string text, int pos, char ch)
	{
		if (ch < '0' || ch > '9')
			return (char)0;

		string tempText = text.Insert(pos, ch.ToString());
		int textToInt = int.Parse(tempText);
		if (textToInt > m_iMaxCount)
			return (char)0;

		return ch;
	}

	private void drawTotalPrice()
	{
		if (m_TotalPriceLabel != null && m_CountUIInput != null)
		{
			int count = 0;
			if (m_CountUIInput.value != "")
				count = int.Parse(m_CountUIInput.value);
			m_TotalPriceLabel.text = (count * m_iPrice).ToString();
		}
	}

	private void OnConfirmBtnClicked()
	{
		if (m_CountUIInput != null)
		{
			int count = 0;
			if (m_CountUIInput.value != "")
				count = int.Parse(m_CountUIInput.value);

			if (count != 0)
			{
				if (sdMallManager.Instance.BuyProduct(m_iProductId, count))
					gameObject.SetActive(false);
			}
		}
	}
}
