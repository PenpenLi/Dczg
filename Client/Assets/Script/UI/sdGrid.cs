using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sdGrid : MonoBehaviour
{
    public enum Arrangement
    {
        Horizontal,
        Vertical,
    }

    public Arrangement sortType = Arrangement.Horizontal;
    public int maxInOneLine = 0;

    public void Reposstion()
    {
        sdGridChild[] list = GetComponentsInChildren<sdGridChild>();
        if (list.Length <= 0) return;
        List<sdGridChild> sortList = new List<sdGridChild>();
        foreach (sdGridChild child in list)
        {
            sortList.Add(child);
        }
        sortList.Sort();
        bool isFirst = true;
        int num = 0;
        Vector3 localPos = Vector3.zero;
        Vector3 initPos = Vector3.zero;
        foreach (sdGridChild info in sortList)
        {
            if (maxInOneLine != 0)
            {
                if (num % maxInOneLine == 0)
                {
                    if (isFirst)
                    {
                        initPos = info.firstPos;
                        Vector3 pos = info.firstPos;
                        info.transform.localPosition = pos;
                        if (sortType == Arrangement.Horizontal)
                        {
                            pos.x += info.width;
                        }
                        else
                        {
                            pos.y -= info.height;
                        }

                        localPos = pos;
                        isFirst = false;
                    }
                    else
                    {
                        localPos = initPos;
                        info.transform.localPosition = localPos + info.magrinPos;
                        
                        if (sortType == Arrangement.Horizontal)
                        {
                            localPos.x += info.magrinPos.x;
                            localPos.x += info.width;
                        }
                        else
                        {
                            localPos.y += info.magrinPos.y;
                            localPos.y -= info.height;
                        }
                    }
                    
                    if (sortType == Arrangement.Horizontal)
                    {
                        initPos.y += info.height;
                    }
                    else
                    {
                        initPos.x -= info.width;
                    }
                }
                else
                {

                    info.transform.localPosition = localPos + info.magrinPos;
                    if (sortType == Arrangement.Horizontal)
                    {
                        localPos.x += info.magrinPos.x;
                        localPos.x += info.width;
                    }
                    else
                    {
                        localPos.y += info.magrinPos.y;
                        localPos.y -= info.height;
                    }
                }

                return;
            }

            if (isFirst)
            {
                Vector3 pos = info.firstPos;
                info.transform.localPosition = pos;
                if (sortType == Arrangement.Horizontal)
                {
                    pos.x += info.width;
                }
                else
                {
                    pos.y -= info.height;
                }

                localPos = pos;
                isFirst = false;
            }
            else
            {
                
                info.transform.localPosition = localPos + info.magrinPos;
                if (sortType == Arrangement.Horizontal)
                {
                    localPos.x += info.magrinPos.x;
                    localPos.x += info.width;
                }
                else
                {
                    localPos.y += info.magrinPos.y;
                    localPos.y -= info.height;
                }
            }
        }
    }

    public bool needReposition = false;
    void Update()
    {
        if (needReposition)
        {
            Reposstion();
            needReposition = false;
        }
    }
}

