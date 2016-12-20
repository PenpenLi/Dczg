using UnityEngine;
using System.Collections;

public class sdScrollTextWnd: MonoBehaviour
{
    UILabel lb_info = null;
    float m_fSpeed = 30.0f;
    Vector3 m_pos = Vector3.zero;
    bool m_bStart = false;
    int m_scrollLength = 330;

    void Awake()
    {
        lb_info = GameObject.Find("scrolltextWnd(Clone)").transform.FindChild("Sprite_bg").FindChild("Panel").FindChild("Label_scrolltext").GetComponent<UILabel>();
        lb_info.color = Color.yellow;
    }

    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (sdUICharacter.Instance.m_lstScrollText.Count > 0)
        {
            if (!m_bStart)
            {
                lb_info.text = sdUICharacter.Instance.m_lstScrollText[0];
                m_pos.x = m_scrollLength;
                lb_info.gameObject.transform.localPosition = m_pos;
                m_bStart = true;
            }
            else
            {
                m_pos.x = m_pos.x - m_fSpeed * Time.deltaTime;
                if (m_pos.x < -(lb_info.width + m_scrollLength))
                {
                    sdUICharacter.Instance.m_lstScrollText.RemoveAt(0);
                    m_bStart = false;
                }
                else
                    lb_info.gameObject.transform.localPosition = m_pos;
            }
        }
        else
            sdUICharacter.Instance.ShowScrollTextWnd(false);
    }
}