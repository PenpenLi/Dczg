using UnityEngine;
using System.Collections;

public class sdMallButton : MonoBehaviour {

	private int m_iProductId;
	private Hashtable m_productInfo;
	private int m_iVIPLimit;
	private int m_iIsBatch;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetInfo(int id, Hashtable info)
	{
		m_iProductId = id;
		m_productInfo = info;

		m_iIsBatch = int.Parse(m_productInfo["IfBatch"].ToString());
	}

	void OnClick()
	{
		if (m_iIsBatch == 1)
		{
			sdMallManager.Instance.m_iCurrentBatchBoughtProductId = m_iProductId;
			sdMallManager.Instance.ActiveBatchPanel();
		}
		else
		{
            sdMallManager.Instance.ActiveGoodsTipPanel(m_iProductId);
		}
	}
}
