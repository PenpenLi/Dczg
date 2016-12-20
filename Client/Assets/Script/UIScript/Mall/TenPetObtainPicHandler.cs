using UnityEngine;
using System.Collections;

public class TenPetObtainPicHandler : MonoBehaviour 
{
    public uint m_iPetId;

    void OnClick()
    {
        UISprite background = GetComponent<UISprite>();
        if (background != null)
        {
            sdUIPetControl.Instance.ActivePetSmallTip(null, (int)m_iPetId, 0, 1, new Vector3(0f, 0f, 0f));
        }
    }
}
