using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdSweepWnd : MonoBehaviour
{
    public GameObject preparePanel = null;
    public GameObject resultPanel = null;
    public GameObject copyItem = null;

    sdSweepResultItem[] itemList = new sdSweepResultItem[10];

    int showNum = 0;
    bool update = false;

    public UIDraggablePanel dragPanel = null;

    public UILabel sweepNum = null;

    public void SetInfo(List<CliProto.SC_TREASURE_CHEST_NTF> list)
    {
        int num = 0;
        foreach (CliProto.SC_TREASURE_CHEST_NTF msg in list)
        {
            if (itemList.Length <= num) break;
            sdSweepResultItem item = itemList[num];
            if (item == null)
            {
                GameObject obj = GameObject.Instantiate(copyItem) as GameObject;
                obj.transform.parent = copyItem.transform.parent;
                obj.transform.localScale = copyItem.transform.localScale;
                Vector3 pos = copyItem.transform.localPosition;
                pos.y -= 210;
                obj.transform.localPosition = pos;
                item = obj.GetComponent<sdSweepResultItem>();
                item.index = num;
                itemList[num] = item;
            }

            item.SetInfo(msg);
            item.gameObject.SetActive(false);
            ++num;
        }

        showNum = 0;

        update = true;
    }

    public void ShowResult()
    {
        preparePanel.SetActive(false);
        resultPanel.SetActive(true);
    }

    public void ShowPrepare()
    {
        preparePanel.SetActive(true);
        resultPanel.SetActive(false);
        sweepNum.text = "10";
    }

    void Start()
    {
        itemList[0] = copyItem.GetComponent<sdSweepResultItem>();
    }

    public void OnClose()
    {
        if (update) return;
        gameObject.SetActive(false);
    }

    float time = 0;

    void Update()
    {
        if (!update) return;

        time += Time.unscaledDeltaTime;
        if (time > 0.5)
        {
            sdSweepResultItem item = itemList[showNum];
            if (item == null)
            {
                update = false;

                foreach (sdSweepResultItem box in itemList)
                {
                    box.GetComponent<BoxCollider>().isTrigger = true;
                }

                return;
            }

            item.gameObject.SetActive(true);
            ++showNum;
            time = 0;
        }
    }
}
