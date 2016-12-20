using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicTowerPanel : MonoBehaviour
{
    public UILabel m_LblPetRemainCount;
    public UILabel m_LblPetRemainTimeCheap;
    public UILabel m_LblPetRemainTimeExpensive;

	// Use this for initialization
	void Start ()
	{
		RefreshLblPetRemainCount(sdMallManager.Instance.PetRemainCount);
	}

    public void RefreshLblPetRemainCount(uint count)
    {
        if (m_LblPetRemainCount != null)
        {
            if (count == 0)
            {
                string strInfo = "*本次购买必得4星战魂*";
                m_LblPetRemainCount.text = strInfo;
            }
            else
            {
                string strInfo = "*再购买";
                strInfo += count.ToString();
                strInfo += "次必获得4星战魂*";
                m_LblPetRemainCount.text = strInfo;
            }
        }
    }

	// Update is called once per frame
	void Update ()
	{
        //UpdateRemainTime((int)HeaderProto.EBuyPetType.EBuyPetType_Cheap);
        UpdateRemainTime((int)HeaderProto.EBuyPetType.EBuyPetType_Expensive);
	}

    void UpdateRemainTime(int nType)
    {
        bool bCheap = nType == (int)HeaderProto.EBuyPetType.EBuyPetType_Cheap;
        ulong nLastTime = bCheap ? sdMallManager.Instance.PetRemainTimeCheap : sdMallManager.Instance.PetRemainTimeExpensive;
        UILabel lblTime = bCheap ? m_LblPetRemainTimeCheap : m_LblPetRemainTimeExpensive;
        if (lblTime == null)
            return;

		if (nLastTime == 0)
        {
            lblTime.text = "本次购买免费";
        }
        else
        {

            //服务器时间
            ulong serverTime = sdGlobalDatabase.Instance.ServerBeginTime + 
				sdGlobalDatabase.Instance.TimeFromServerBeginTime;
           
           	// 实际上nRemainTime是上次免费购买时间
			int remainTime = 48 * 3600 - (int)(serverTime - nLastTime) / 1000;
			if (remainTime <= 0)
            {
				lblTime.text = "本次购买免费";
            }
			else
			{
				int hour = remainTime / 3600;
				remainTime -= hour * 3600;
				int minute = remainTime / 60;
				int second = remainTime % 60;
					
				string str1 = hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
				string str2 = "后免费";
				string str = str1 + str2;
				lblTime.text = str;				
			}
        }
    }
	
	private void OnEnable()
	{
		
	}
	
}
