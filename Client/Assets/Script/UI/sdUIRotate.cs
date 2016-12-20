using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIRotate : MonoBehaviour {
    Quaternion[] oldRot = null;
    Vector3[] oldPos = null;
    public  float fSpeed = 1f;
	// Use this for initialization
    public bool isStop = true;

    public GameObject upEffect1 = null;
    public GameObject upEffect2 = null;
    public GameObject upEffect3 = null;
    public GameObject upEffectCenter = null;

    public List<EventDelegate> onFinish = new List<EventDelegate>();

	void Start () {

        upEffect1.SetActive(false);
        upEffect2.SetActive(false);
        upEffect3.SetActive(false);
        upEffectCenter.SetActive(false);

        oldRot = new Quaternion[transform.childCount];
        oldPos = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            oldRot[i] = transform.GetChild(i).rotation;
            Quaternion q = Quaternion.AngleAxis(60.0f*i,new Vector3(0,0,1f));
            transform.GetChild(i).localPosition = Matrix4x4.TRS(Vector3.zero, q, Vector3.one).MultiplyPoint(new Vector3(0, 158.0f, 0));
            oldPos[i] = transform.GetChild(i).localPosition;
        }
	}

    bool isAdd = true;
    bool hasShowEffect1 = false;
    bool hasShowEffect2 = false;
    float fTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (isStop) return;

        fTime += Time.deltaTime;
        if (fTime >= 0.01)
        {
            fTime = 0f;
            if (isAdd)
            {
                fSpeed += 0.1f;
            }
            else
            {
                fSpeed -= 0.03f;
            }

            if (fSpeed >= 5f && !hasShowEffect1)
            {
                upEffect1.SetActive(true);
                upEffect2.SetActive(true);
                upEffect3.SetActive(true);
                hasShowEffect1 = true;
            }

            if (fSpeed >= 10f && !hasShowEffect2)
            {
                upEffectCenter.SetActive(true);
                hasShowEffect2 = true;
                isAdd = false;
            }

            if (fSpeed <= 4f && !isAdd)
            {
                Stop();
                return;
            }
        }


        Quaternion rot = transform.localRotation;
        rot *= Quaternion.AngleAxis(Time.deltaTime * 90.0f * fSpeed, new Vector3(0, 0, -1));
        transform.localRotation = rot;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation= oldRot[i];
        }
	}

    public void Stop()
    {
        isStop = true;
        upEffect1.SetActive(false);
        upEffect2.SetActive(false);
        upEffect3.SetActive(false);
        upEffectCenter.SetActive(false);
        transform.localRotation = Quaternion.identity;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation = oldRot[i];
            transform.GetChild(i).localPosition = oldPos[i];
        }
        EventDelegate.Execute(onFinish);
        onFinish.Clear();
    }

    public void Play()
    {
        fSpeed = 2f;
        isAdd = true;
        hasShowEffect1 = false;
        hasShowEffect2 = false;
        isStop = false;
    }
}
