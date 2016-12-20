using UnityEngine;

public class sdNpcHud : MonoBehaviour
{
    public float m_distance = 15.0f;
    public string m_strWndName = "1";
    public string m_strAtlas;
    public string m_strSpriteName;

    GameObject m_hudWnd = null;
    bool m_bActive = false;
    float m_fTime = 0.0f;
    Vector3 headWndPos = Vector3.zero;

    public void SetActive(bool bActive)
    {
        if (m_bActive == bActive)
            return;
        m_bActive = bActive;
        if (bActive)
        {
            if (m_hudWnd)
            {
                m_hudWnd.SetActive(true);
                UISprite sprite = m_hudWnd.GetComponent<UISprite>();
                sdUICharacter.Instance.LoadAtlas("UI/$common/" + m_strAtlas + ".prefab", sprite, m_strSpriteName);
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$NpcHud.prefab", OnLoadWnd,  param);
            }
        }
        else
        {
            m_hudWnd.SetActive(false);
        }
    }

    void OnLoadWnd(ResLoadParams param, UnityEngine.Object obj)
    {
        if (obj == null)
        {
            Debug.Log("npchud error");
            return;
        }
        m_hudWnd = GameObject.Instantiate(obj) as GameObject;
        m_hudWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
        m_hudWnd.transform.localPosition = GetScreenPos();
        m_hudWnd.transform.localRotation = Quaternion.identity;
        m_hudWnd.transform.localScale = Vector3.one;
        m_hudWnd.name = m_strWndName;
        SetActive(true);
    }

    void Update()
    {
        m_fTime += Time.deltaTime;
        if(m_fTime > 1.0f)
        {
            m_fTime = 0.0f;
            if(sdGameLevel.instance.mainChar)
            {
                Vector3 mainCharPos = sdGameLevel.instance.mainChar.transform.position;
                mainCharPos = mainCharPos - gameObject.transform.position;
                if (mainCharPos.sqrMagnitude > m_distance * m_distance)
                    SetActive(false);
                else
                    SetActive(true);
            }
        }
        if(m_bActive && m_hudWnd)
            m_hudWnd.transform.localPosition = GetScreenPos();
    }

    Vector3 GetScreenPos()
    {
        headWndPos = sdGameLevel.instance.mainCamera.GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position + new Vector3(0,2,0));
        float a = 1280f / Screen.width;
        headWndPos.x = (headWndPos.x - Screen.width / 2f) * a;
        headWndPos.y = (headWndPos.y - Screen.height / 2f) * a;
        return headWndPos;
    }
}