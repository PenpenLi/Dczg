using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class sdHideItem : MonoBehaviour
{
    public bool needLevel = false;
    public int endId = 0;
    public int startId = 0;

    void Awake()
    {
        if (needLevel)
        {
            if (sdLevelInfo.GetLevelValid(startId) && !sdLevelInfo.GetLevelValid(endId))
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (sdGuideMgr.Instance.hideList.Contains(name))
            {
                sdGuideMgr.Instance.hideList[name] = gameObject;
            }
            else
            {
                sdGuideMgr.Instance.hideList.Add(name, gameObject);
            }
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
       
    }
}