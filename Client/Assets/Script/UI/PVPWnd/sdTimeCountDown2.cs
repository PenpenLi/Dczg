using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class sdTimeCountDown2 : MonoBehaviour
{
    GameObject[] sprite_obj = new GameObject[2];
    float fScaleTime = 1.0f;
    float fTime = 0.0f;
    float fPeakScale = 10.0f;
    int nWidth = 30;
    GameObject root = null;

    void Awake()
    {
        root = GameObject.Find("timecountdown2(Clone)");
        sprite_obj[0] = root.transform.FindChild("num0").gameObject;
        sprite_obj[1] = root.transform.FindChild("num1").gameObject;
        nWidth = sprite_obj[0].GetComponent<UISprite>().width;
    }
    public void SetTime(int time)
    {
        List<string> list = new List<string>();

        while (time > 0)
        {
            int number = time % 10;
            list.Add(number.ToString());
            time = time / 10;
        }
        int nTotal = list.Count;
        int index = 0;
        int startX = (nTotal - 1) * nWidth / 2;
        for (; index < nTotal; ++index)
        {
            sprite_obj[index].SetActive(true);
            sprite_obj[index].transform.localPosition = new Vector3(startX - nWidth * index, sprite_obj[index].transform.localPosition.y, 0);
            UISprite sprite = sprite_obj[index].GetComponent<UISprite>();
            sprite.spriteName = list[index];
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.0f);
        }
        for (; index < sprite_obj.Length; ++index)
            sprite_obj[index].SetActive(false);
        root.transform.localScale = Vector3.one;
        fTime = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        fTime += Time.deltaTime;
        if (fTime < fScaleTime * 0.5f)
        {
            float scale = 1.0f + fTime * (fPeakScale - 1.0f) / fScaleTime / 2.0f;
            root.transform.localScale = new Vector3(scale, scale, scale);
        }
        else if (fTime < fScaleTime)
        {
            float scale = fPeakScale - (float)((fTime - fScaleTime * 0.5f) * (fPeakScale - 1.0f) / fScaleTime / 2.0f);
            float alpha = 1.0f - (fTime - fScaleTime * 0.5f) * 2.0f;
            SetAlpha(alpha);
        }
        else
            root.transform.localScale = Vector3.one;
        if (fTime >= 1.0f)
            SetTime((int)sdPVPManager.Instance.m_fCountDown);
    }

    void SetAlpha(float fAlpha)
    {
        for (int index = 0; index < sprite_obj.Length; ++index)
        {
            UISprite sprite = sprite_obj[index].GetComponent<UISprite>();
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, fAlpha);
        }
    }
}