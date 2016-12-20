using UnityEngine;


public class sdRoleHud : MonoBehaviour
{
    GameObject m_hudWnd = null;
    string m_objName;

    public void Disappear()
    {
        GameObject obj = GameObject.Find(m_objName);
        if (this != null)
        {
            if (obj) GameObject.Destroy(obj);
        }
    }

    public void SetInfo(string name, string union, int viplevel)
    {
        if (m_hudWnd)
        {
            m_hudWnd.SetActive(true);
            UILabel lb_name = m_hudWnd.transform.FindChild("Label_name").GetComponent<UILabel>();
            lb_name.text = name;
            UILabel lb_union = m_hudWnd.transform.FindChild("Label_union").GetComponent<UILabel>();
            lb_union.text = union;
            UISprite sprite_vip = m_hudWnd.transform.FindChild("Sprite_vip").GetComponent<UISprite>();
            //sdUICharacter.Instance.LoadAtlas("UI/$common/" + m_strAtlas + ".prefab", sprite_vip, m_strSpriteName);
        }
        else
        {
            ResLoadParams param = new ResLoadParams();
            param.userdata0 = name;
            param.userdata1 = union;
            param.userdata2 = viplevel;
            sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$RoleHud.prefab", OnLoadWnd, param);
        }
    }

    void OnLoadWnd(ResLoadParams param, UnityEngine.Object obj)
    {
        if (obj == null || this == null)
        {
            Debug.Log("rolehud error");
            return;
        }
        m_hudWnd = GameObject.Instantiate(obj) as GameObject;
        m_hudWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
        m_hudWnd.transform.localPosition = GetScreenPos();
        m_hudWnd.transform.localRotation = Quaternion.identity;
        m_hudWnd.transform.localScale = Vector3.one;
        m_objName = "RoleHud_" + param.userdata0.ToString();
        m_hudWnd.name = m_objName;
        SetInfo(param.userdata0.ToString(), param.userdata1.ToString(), (int)param.userdata2);
    }

    void Update()
    {
        if (m_hudWnd)
            m_hudWnd.transform.localPosition = GetScreenPos();
    }

    Vector3 GetScreenPos()
    {
        Vector3 pos = sdGameLevel.instance.mainCamera.GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position + new Vector3(0,1.5f,0));
        float a = 1280f / Screen.width;
        pos.x = (pos.x - Screen.width / 2f) * a;
        pos.y = (pos.y - Screen.height / 2f) * a;
        return pos;
    }
}