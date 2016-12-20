using System;
using System.Collections.Generic;
using UnityEngine;

class sdEffectFinish : MonoBehaviour
{
    public List<EventDelegate> onFinish = new List<EventDelegate>();

    void Update()
    {
        if ((GetComponentsInChildren<Animator>() == null || GetComponentsInChildren<Animator>().Length == 0) &&
            (GetComponentsInChildren<Animation>() == null || GetComponentsInChildren<Animation>().Length == 0))
        {
            if (onFinish.Count > 0)
            {
                EventDelegate.Execute(onFinish);
                onFinish.Clear();
            }
        }
    }
}
