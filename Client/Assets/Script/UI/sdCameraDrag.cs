
using UnityEngine;
using System.Collections.Generic;

public class sdCameraDrag : MonoBehaviour
{
	GameObject posA = null;
	GameObject posB = null;
	GameObject posC = null;
	GameObject posD = null;
	
	Vector3 xVec = Vector3.zero;
	Vector3 yVec = Vector3.zero;
	Vector3 vecCenter = Vector3.zero;
	Vector2 cameraPos = new Vector2(-0.5f, 0.0f);
	public float change = 0.001f;

	float       fDampen  = 0.0f;
	Vector2     mMomentum = Vector2.zero;
	float       fLastDragTime = 0.0f;

    float m_fSurpass = 0.7f;
	
	struct momentumSample
	{
		public float fTime;
		public Vector2 distance;
	};
	List<momentumSample>  sampleList = new List<momentumSample>();
	enum eMoveStyle
	{
		eMS_Invalid = 0,
		eMS_NormalX = 1<<1,
		eMS_NormalY = 1<<2,
		eMS_MaxX    = 1<<3,
		eMS_MaxY    = 1<<4,
		eMS_MinX	= 1<<5,
		eMS_Miny	= 1<<6,
	};
	
	eMoveStyle  moveStyle = eMoveStyle.eMS_Invalid;	
	
	void Start()
	{
		if( posA == null )
		{
			posA = GameObject.Find("CameraPointA");
			posB = GameObject.Find("CameraPointB");
			posC = GameObject.Find("CameraPointC");
			posD = GameObject.Find("CameraPointD");
		}
		
		if (sdGameLevel.instance != null && sdGameLevel.instance.levelType != sdGameLevel.LevelType.WorldMap)
		{
			gameObject.SetActive(false);
			return;
		}
		else
		{
			gameObject.SetActive(true);	
		}
        if (posA != null && posC != null && posD != null)
        {
            if (SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r16_9)
            {
                posA.transform.position = new Vector3(posA.transform.position.x + 5.0f, posA.transform.position.y, posA.transform.position.z);
                posD.transform.position = new Vector3(posD.transform.position.x + 5.0f, posD.transform.position.y, posD.transform.position.z);
            }
            xVec = posC.transform.position - posD.transform.position;
            yVec = posA.transform.position - posD.transform.position;
            m_fSurpass = 15.0f / -xVec.x + 0.5f;
            vecCenter = (posC.transform.position + posA.transform.position) * 0.5f;
            if (sdGameLevel.instance != null) sdGameLevel.instance.isFollow = false;
            if (sdGlobalDatabase.Instance.globalData.ContainsKey("worldmapcamerapos"))
                cameraPos = (Vector2)sdGlobalDatabase.Instance.globalData["worldmapcamerapos"];
            SetCameraPos();
        }
	}
	
	void Update()
	{
        cameraPos.x = Mathf.Clamp(cameraPos.x, -m_fSurpass, m_fSurpass);
        cameraPos.y = Mathf.Clamp(cameraPos.y, -m_fSurpass, m_fSurpass);

		if(fDampen > 0.0f)
		{
			float fStep = Time.deltaTime;
			SetCameraPos();
			if (fStep > 1f) fStep = 1f;
			float dampeningFactor = 1f - 9.0f * 0.001f;
			int ms = Mathf.RoundToInt(fStep * 1000f);
			float totalDampening = Mathf.Pow(dampeningFactor, ms);
			if(!Mathf.Equals(Mathf.Abs(mMomentum.x), 0.0f))
				mMomentum.x = mMomentum.x * totalDampening;	
			if(!Mathf.Equals(Mathf.Abs(mMomentum.y), 0.0f))
				mMomentum.y = mMomentum.y * totalDampening;	
			cameraPos += mMomentum;
			if((int)(moveStyle&eMoveStyle.eMS_MaxX) != 0)
			{
				if(cameraPos.x < 0.5f)
					cameraPos.x = 0.5f;
			}
			else if((int)(moveStyle&eMoveStyle.eMS_MinX) != 0)
			{
				if(cameraPos.x > -0.5f)
					cameraPos.x = -0.5f;
			}
			else
				cameraPos.x=Mathf.Clamp(cameraPos.x, -0.5f, 0.5f);
				
			if((int)(moveStyle&eMoveStyle.eMS_MaxY) != 0)
			{
				if(cameraPos.y < 0.5f)
					cameraPos.y = 0.5f;
			}
			else if((int)(moveStyle&eMoveStyle.eMS_Miny) != 0)
			{
				if(cameraPos.y > -0.5f)
					cameraPos.y = -0.5f;
			}
			else
				cameraPos.y = Mathf.Clamp(cameraPos.y, -0.5f, 0.5f);
			fDampen -= fStep;
		}
	}	
	void OnPress(bool bFlag)
	{
		Vector3 worldmapcamera = sdGameLevel.instance.WordMapCameraPos;
		if(worldmapcamera.magnitude > 0.001f)
		{
			cameraPos.x = (worldmapcamera.x - vecCenter.x)/(float)xVec.x;
			cameraPos.y = (worldmapcamera.y - vecCenter.y)/(float)yVec.y;
			sdGameLevel.instance.WordMapCameraPos = Vector3.zero;
		}
		if(!bFlag)
		{
			///停住aaa
			if(Time.time - fLastDragTime > 0.05f && Mathf.Abs(cameraPos.x) < 0.5f && Mathf.Abs(cameraPos.y) < 0.5f)
				fDampen = 0.0f;
			else
			{
				moveStyle = eMoveStyle.eMS_Invalid;
				fDampen = 0.8f;
				if(cameraPos.x > 0.5f)
				{
					mMomentum.x = (0.5f - cameraPos.x)*0.2f;
					moveStyle |= eMoveStyle.eMS_MaxX;
				}
				else if(cameraPos.x < -0.5f)
				{
					mMomentum.x = (-0.5f - cameraPos.x)*0.2f;
					moveStyle |= eMoveStyle.eMS_MinX;
				}
				else
				{
					mMomentum.x = GetMomentum().x;
					moveStyle |= eMoveStyle.eMS_NormalX;
				}
				
				if(cameraPos.y > 0.5f)
				{
					mMomentum.y = (0.5f - cameraPos.y)*0.2f;
					moveStyle |= eMoveStyle.eMS_MaxY;
				}
				else if(cameraPos.y < -0.5f)
				{
					mMomentum.y = (-0.5f - cameraPos.y)*0.2f;
					moveStyle |= eMoveStyle.eMS_Miny;
				}
				else
				{
					mMomentum.y = GetMomentum().y;
					moveStyle |= eMoveStyle.eMS_NormalY;
				}
			}
		}
		else
		{
			mMomentum = Vector2.zero;
			fDampen = 0.0f;
		}
	}
	
	void OnDrag(Vector2 delta)
	{
		Vector3 worldmapcamera = sdGameLevel.instance.WordMapCameraPos;
		if(worldmapcamera.magnitude > 0.001f)
		{
			cameraPos.x = (worldmapcamera.x - vecCenter.x)/(float)xVec.x;
			cameraPos.y = (worldmapcamera.y - vecCenter.y)/(float)yVec.y;
			sdGameLevel.instance.WordMapCameraPos = Vector3.zero;
		}
		sdUICharacter.Instance.SetNeedShowLevel(null);
		delta *= 0.40f;
		delta=-delta;
		momentumSample sample = new momentumSample();
		sample.fTime = Time.time;
		sample.distance = delta;
		sampleList.Add(sample);
		UpdateMomentum();
		delta*=(change*(-xVec.x/65.0f));
		cameraPos += delta*0.5f;
        cameraPos.x = Mathf.Clamp(cameraPos.x, -m_fSurpass, m_fSurpass);
        cameraPos.y = Mathf.Clamp(cameraPos.y, -m_fSurpass, m_fSurpass);		
		SetCameraPos();		
		fLastDragTime = Time.time;
	}
	
	void SetCameraPos()
	{
		GameObject cam = sdGameLevel.instance.mainCamera.gameObject;
		if(cam != null)
		{
			Vector3 pos = (cameraPos.x*xVec + cameraPos.y*yVec) + vecCenter;
			cam.transform.localPosition = pos;
            sdGlobalDatabase.Instance.globalData["worldmapcamerapos"] = cameraPos;
		}
	}
	
	Vector2 GetMomentum()
	{
		Vector2 nRet = Vector2.zero;
		UpdateMomentum();
		Vector2 vDistance = Vector2.zero;
		for(int index = 0;index < sampleList.Count; ++index)
		{
			vDistance += sampleList[index].distance;
		}
		if(sampleList.Count > 0)
		{
			float fElapse = Time.time - sampleList[0].fTime;
			if(!fElapse.Equals(0.0f))
			{
				nRet = vDistance/fElapse;
				nRet *= 0.0001f;
			}
		}
		return nRet;
	}
	
	void UpdateMomentum()
	{
		int max = -1;
		for(int index = 0; index < sampleList.Count; ++index)
		{
			if(Time.time - sampleList[index].fTime > 0.3f)
				max = index;
			else
				break;
		}
		if(max > 0)
			sampleList.RemoveRange(0, max + 1);	
	}
}
