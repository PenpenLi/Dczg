using UnityEngine;
using System.Collections;

public class sdPVPRefreshWnd : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public void Refresh()
    {
        UILabel costmoney = GameObject.Find("Sprite_pvprefresh").transform.FindChild("Label_info").FindChild("Label_money").GetComponent<UILabel>();
        Hashtable configTable = sdConfDataMgr.Instance().GetTable("config");
        if (configTable.ContainsKey("refreshpvp"))
        {
            Hashtable table = configTable["refreshpvp"] as Hashtable;
            costmoney.text = table["value"] as string;
        }
    }

    void OnClick()
    {
        GameObject root = GameObject.Find("Sprite_pvprefresh");
        if (gameObject.name == "Button_no")
        {
            sdUICharacter.Instance.ShowPVPRefreshWnd(false);
            //sdUICharacter.Instance.ShowFullScreenUI(true);
            //GameObject.Destroy(root.transform.parent.gameObject);
        }
        else if (gameObject.name == "Button_yes")
        {
            sdPVPMsg.Send_CSID_GET_PVP_MATCH_REQ();
            sdUICharacter.Instance.ShowPVPRefreshWnd(false);
            //sdUICharacter.Instance.ShowFullScreenUI(true);
            //GameObject.Destroy(root.transform.parent.gameObject);
        }
    }
}
