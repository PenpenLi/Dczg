using UnityEngine;
using System.Collections;


public class sdTimeCountDownWnd : MonoBehaviour
{
    UILabel label_countdown = null;
    void Awake()
    {
        label_countdown = GameObject.Find("Label_timecountdown").GetComponent<UILabel>();
    }
    // Update is called once per frame
    void Update()
    {
        if (sdPVPManager.Instance.m_eCountDownType == eCountDownType.eCDT_None || sdPVPManager.Instance.m_eCountDownType == eCountDownType.eCDT_pvpReady)
            return;
        if (sdPVPManager.Instance.m_fCountDown < 0.0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            int nTime = (int)sdPVPManager.Instance.m_fCountDown;
            Color textColor = Color.white;
            if (nTime <= 5 && nTime%2 == 1)
            {
               textColor = Color.red; 
            }
            label_countdown.color = textColor;
            label_countdown.text = "挑战剩余时间：" + nTime + "秒";
        }
    }
}
