using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class SelectSrvWnd : MonoBehaviour
{
    UILabel lb_SrvName = null;
    GameObject item_template = null;
    GameObject panel_srvList = null;

    int xposStart = -156;
    int yposStart = 125;
    int xSpace = 300;
    int ySpace = -90;

    void Awake()
    {
        GameObject root = GameObject.Find("$SelectServerWnd(Clone)");
        lb_SrvName = root.transform.FindChild("Sprite_latestSrv").FindChild("Sprite_item1").FindChild("Label_item1").GetComponent<UILabel>();

        panel_srvList = root.transform.FindChild("Panel_srvlist").gameObject;
        item_template = panel_srvList.transform.FindChild("item0").gameObject;
    }

    void Start()
    {
        
    }

    public void UpdateSrvList()
    {
        lb_SrvName.text = SDNetGlobal.serverName;
        int index = 0;
        for (; index < SDNetGlobal.m_lstSrvInfo.Count; ++index)
        {
            GameObject item = null;
            Transform trans = panel_srvList.transform.FindChild("item" + index.ToString());
            if (trans == null)
                item = GameObject.Instantiate(item_template) as GameObject;
            else
                item = trans.gameObject;
            item.transform.parent = panel_srvList.transform;
            item.name = "item" + index.ToString();
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3(xposStart + index % 2 * xSpace, yposStart - index / 2 * ySpace, 0);
            item.SetActive(true);
            UILabel lb_name = item.transform.FindChild("name").GetComponent<UILabel>();
            lb_name.text = SDNetGlobal.m_lstSrvInfo[index].Attribute("ServerName");
            Transform status = item.transform.FindChild("Sprite_status");
            if (status != null)
            {
                UISprite sprite_staus = status.GetComponent<UISprite>();
                string strStatus = SDNetGlobal.m_lstSrvInfo[index].Attribute("ServerStatus");
                if (strStatus == "火爆")
                    sprite_staus.spriteName = "r";
                else if (strStatus == "正常")
                    sprite_staus.spriteName = "x";
                else
                    sprite_staus.spriteName = "wh";
            }
        }
        for (; index < panel_srvList.transform.childCount; ++index)
        {
            GameObject child = panel_srvList.transform.FindChild("item" + index.ToString()).gameObject;
            child.SetActive(false);
        }
        UIDraggablePanel panel = panel_srvList.GetComponent<UIDraggablePanel>();
        panel.ResetPosition();
    }

}