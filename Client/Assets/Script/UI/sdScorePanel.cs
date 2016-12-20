using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdScorePanel : MonoBehaviour
{
    int dataFrom = 0;
    int dataTo = 0;
    int delt = 0;
    public void Init(int from, int to)
    {
        if (to <= from) return;
        dataFrom = from;
        dataTo = to;
        delt = (dataTo - dataFrom)/20;
        if (delt == 0)
        {
            delt = 1;
        }
        GetComponent<TweenAlpha>().enabled = false;
        GetComponent<TweenAlpha>().Reset();
        GetComponent<UIPanel>().alpha = 1f;
    }

    public void Finish()
    {
        gameObject.SetActive(false);
        sdUICharacter.Instance.playerScore = 0;
    }

    float time = 0f;
    float lifeTime = 0f;

    void Update()
    {
        if (gameObject.active && delt != 0)
        {
            time += Time.unscaledDeltaTime;
            if (time < 0.1f) return;
            dataFrom += delt;
            if (dataFrom > dataTo)
            {
                dataFrom = dataTo;
            }

            GetComponentInChildren<UILabel>().text = string.Format(sdConfDataMgr.Instance().GetShowStr("ScorePanel"), dataFrom);
            if (dataFrom == dataTo)
            {
                if (lifeTime < 1f)
                {
                    lifeTime += Time.unscaledDeltaTime;
                }
                else
                {
                    GetComponent<TweenAlpha>().enabled = true;
                    delt = 0;
                    time = 0;
                    lifeTime = 0;
                }
            }
        }
    }
}
