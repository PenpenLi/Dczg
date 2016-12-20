
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdCopyItem : MonoBehaviour
{	
	void Awake()
	{
        
	}

    public GameObject Show()
    {
        GameObject obj = sdGameObject.Instantiate(gameObject) as GameObject;
        obj.transform.parent = transform.parent;
        obj.transform.localPosition = transform.localPosition;
        obj.transform.localScale = transform.localScale;
        obj.name = "Fx_Shengjichenggong";
        gameObject.SetActive(true);
        return obj;
    }

    void Update()
    {
        if (gameObject.active && GetComponentsInChildren<NcCurveAnimation>() == null || GetComponentsInChildren<NcCurveAnimation>().Length == 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public List<EventDelegate> onFinish = new List<EventDelegate>();

    void OnDestroy()
    {
        if (onFinish.Count > 0)
        {
            EventDelegate.Execute(onFinish);
        }
    }

    public void Hide()
    {
        
    }

    public static void HideSuccess()
    {
        if (successObj != null)
        {
            successObj.SetActive(false);
        }
        else
        {
            needHide = true;
        }
    }

    public static void ShowSuccess(EventDelegate evt)
    {
        needHide = false;
        ResLoadParams param = new ResLoadParams();
        param.userdata0 = evt;
        sdResourceMgr.Instance.LoadResource("Effect/MainChar/FX_UI/$Fx_Chenggong/Fx_Shengjichenggong.prefab", OnLoadSuccess, param);
    }

    static bool needHide = false;
    static GameObject successObj = null;

    public static void OnLoadSuccess(ResLoadParams param,UnityEngine.Object obj)
    {
        if (needHide)
        {
            return;
        }

        if (obj != null)
        {
            successObj = sdGameObject.Instantiate(obj) as GameObject;
            sdAutoDestory auto = successObj.AddComponent<sdAutoDestory>();
            if (sdGameLevel.instance.UICamera != null)
            {
                Vector3 v = sdGameLevel.instance.UICamera.transform.position;
                v.z -= 0.1f;
                successObj.transform.position = v;
            }
            if (param.userdata0 != null)
            {
                auto.onFinish.Add((EventDelegate)param.userdata0);
            }
            auto.Life = 1.5f;
        }
    }
}
