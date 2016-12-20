using UnityEngine;
using System.Collections;

public class sdPVPTimeBuyWnd : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }
    public void Refresh(byte byType)
    {
        sdPVPManager.Instance.m_buyWndType = byType;
        switch (byType)
        {
            case 0:
                {
                    int cost = 0;
                    Hashtable configTable = sdConfDataMgr.Instance().GetTable("config");
                    if (configTable.ContainsKey("buypvptimecost"))
                    {
                        Hashtable table = configTable["buypvptimecost"] as Hashtable;
                        cost = int.Parse(table["value"] as string);
                    }
                    int total = sdPVPManager.Instance.m_ChallengeBuyLeft * cost;
                    GameObject root = GameObject.Find("Sprite_pvpbuytimewnd_bg");
                    UILabel lb_title = root.transform.FindChild("Label_title").GetComponent<UILabel>();
                    lb_title.text = "竞技场次数";
                    UILabel label_times = root.transform.FindChild("Label_info").GetComponent<UILabel>();
                    label_times.text = sdPVPManager.Instance.m_ChallengeBuyLeft.ToString();
                    UILabel label_one = root.transform.FindChild("Label_onecost").GetComponent<UILabel>();
                    label_one.text = cost.ToString();
                    UILabel label_total = root.transform.FindChild("Label_totalcost").GetComponent<UILabel>();
                    label_total.text = total.ToString();
                }
                break;
            case 1:
                {
                    int cost = 100;
                    int total = sdPTManager.Instance.m_BuyTimes * cost;
                    GameObject root = GameObject.Find("Sprite_pvpbuytimewnd_bg");
                    UILabel lb_title = root.transform.FindChild("Label_title").GetComponent<UILabel>();
                    lb_title.text = "战魂试炼次数";
                    UILabel label_times = root.transform.FindChild("Label_info").GetComponent<UILabel>();
                    label_times.text = sdPTManager.Instance.m_BuyTimes.ToString();
                    UILabel label_one = root.transform.FindChild("Label_onecost").GetComponent<UILabel>();
                    label_one.text = cost.ToString();
                    UILabel label_total = root.transform.FindChild("Label_totalcost").GetComponent<UILabel>();
                    label_total.text = total.ToString();
                }
                break;
        }
    }
}
