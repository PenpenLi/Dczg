using UnityEngine;
using System.Collections;

public class sdBBSWnd : MonoBehaviour
{
    UILabel lb_info = null;

    void Awake()
    {
        lb_info = GameObject.Find("bbsWnd(Clone)").transform.FindChild("Sprite_bg").FindChild("dragpanel").FindChild("Label_info").GetComponent<UILabel>();
    }

    // Use this for initialization
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RefreshSize(bool bMini)
    {
        GameObject root = GameObject.Find("bbsWnd(Clone)");
        GameObject bg = root.transform.FindChild("Sprite_bg").gameObject;
        GameObject dragpanle = bg.transform.FindChild("dragpanel").gameObject;
        GameObject label_info = dragpanle.transform.FindChild("Label_info").gameObject;
        GameObject btn_close = root.transform.FindChild("Btn_bbswnd_close").gameObject;
        GameObject downArrow = root.transform.FindChild("ArrowBar").FindChild("sp_arrow2").gameObject;

        if (bMini)
        {
            bg.transform.localPosition = new Vector3(0, 50, 0);
            UISprite sprite = bg.GetComponent<UISprite>();
            sprite.height = 500;
            dragpanle.GetComponent<UIPanel>().clipRange = new Vector4(0, 0, 620, 350);
            label_info.transform.localPosition = new Vector3(-310,175,0);
            btn_close.transform.localPosition = new Vector3(0,-190,0);
            downArrow.transform.localPosition = new Vector3(1, -95, 0);
        }
        else
        {
            bg.transform.localPosition = new Vector3(0, 0, 0);
            UISprite sprite = bg.GetComponent<UISprite>();
            sprite.height = 600;
            dragpanle.GetComponent<UIPanel>().clipRange = new Vector4(0, 0, 620, 450);
            label_info.transform.localPosition = new Vector3(-310, 225, 0);
            btn_close.transform.localPosition = new Vector3(0, -290, 0);
            downArrow.transform.localPosition = new Vector3(1, -195, 0);

        }
    }

    public  void ShowInfo(string strMsg, bool bMini)
    {
        RefreshSize(bMini);
        string str = strMsg.Replace("\\n", "\n");
        lb_info.text = str;
        BoxCollider collider = lb_info.GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.size = new Vector3(collider.size.x, (float)lb_info.height, collider.size.z);
            collider.center = new Vector3(collider.center.x, lb_info.height * -0.5f, collider.center.z);

            GameObject parent = lb_info.gameObject.transform.parent.gameObject;
            if (parent != null)
            {
                UIPanel panel = parent.GetComponent<UIPanel>();
                if (panel != null)
                {
                    UIDraggablePanel dragPanel = parent.GetComponent<UIDraggablePanel>();
                    GameObject ArrowBar = null;
                    if (dragPanel != null)
                    {
                        dragPanel.transform.localPosition = Vector3.zero;
                        UIScrollBar bar = dragPanel.verticalScrollBar;
                        if (bar != null)
                            ArrowBar = bar.gameObject;
                    }
                    if (panel.clipRange.w < lb_info.height)
                    {
                        collider.enabled = true;
                        if (ArrowBar != null) ArrowBar.SetActive(true);
                    }
                    else
                    {
                        collider.enabled = false;
                        if (ArrowBar != null) ArrowBar.SetActive(false);
                    }
                    
                }
            }
        }
    }
}
