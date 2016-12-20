using UnityEngine;
using System.Collections;
using System;

public class sdBuffIcon : MonoBehaviour
{
	public bool isBuff = true;
	public int index = 0;
	private int buffid = -1;
	public GameObject icon = null;
	private float lastTime = -1;
	public GameObject mask = null;
	public GameObject buffTip = null;
	public GameObject debuffTip = null;
	
	float delttime = -1;
	bool isPressed = false;
	bool isLoadAtlas = false;
	
	void OnPress(bool isPress)
	{
		if (isPress)
		{
			isPressed = true;
			delttime = 0;
		}
		else
		{
			isPressed = false;	
		}
	}
	
	void ShowTip(GameObject obj)
	{
		obj.SetActive(true);
		obj.transform.localPosition = new Vector3(transform.localPosition.x-(obj.GetComponent<UISprite>().localSize.x/2),
			transform.localPosition.y + (obj.GetComponent<UISprite>().localSize.y/2), obj.transform.localPosition.z);
		Hashtable buffTable	=	sdConfDataMgr.Instance().GetTable("buff");
		Hashtable item = buffTable[buffid.ToString()] as Hashtable;
		Transform name = obj.transform.FindChild("bufftip_name");
		if (name != null)
		{
			name.GetComponent<UILabel>().text = item["szName[ROLE_NAME_LEN]"].ToString();	
		}
		Transform des = obj.transform.FindChild("bufftip_des");
		if (des != null)
		{
			des.GetComponent<UILabel>().text = item["szDescription[DESCRIPTION_LEN]"].ToString();	
		}
		Transform time = obj.transform.FindChild("bufftip_time");
		if (time != null)
		{
			if (item["nTotalTime"].ToString() == "0")
			{
				time.GetComponent<UILabel>().text = "";
				return;
			}
			int min = (int)(lastTime/60);
			int sec = (int)(lastTime%60);
			string txt = string.Format("{0}:{1}", min.ToString(), sec.ToString());
			time.GetComponent<UILabel>().text = txt;
		}
	}
	
	public void HideTip(GameObject obj)
	{
		obj.SetActive(false);
	}
	
	public void SetBuffId(sdBuff buff)
	{
		if (buff == null)
		{
			gameObject.GetComponent<UISprite>().spriteName = "";
			if (icon != null)
			{
				icon.GetComponent<UISprite>().spriteName = "";
			}

            lastTime = -1;
			if(mask != null)
			{
				mask.GetComponent<UISprite>().fillAmount = 0;	
			}
			
			return;
		}
		
		gameObject.GetComponent<UISprite>().spriteName = "FightingSystem_Img_Buff";
		buffid = buff.GetTemplateID();
		Hashtable buffTable		=	sdConfDataMgr.Instance().GetTable("buff");
		Hashtable item = buffTable[buffid.ToString()] as Hashtable;
		if (item != null)
		{
			if (icon != null)
			{
				icon.GetComponent<UISprite>().spriteName = item["icon"].ToString();
				if( sdConfDataMgr.Instance().BuffAtlas != null )
				{
					icon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().BuffAtlas;
				}
				else if( !isLoadAtlas )
				{
					LoadBuffIcon();
					isLoadAtlas = true;
				}
			}
			lastTime = float.Parse(item["nTotalTime"].ToString())/1000 - buff.GetLifeTime();
			if(mask != null)
			{
				mask.GetComponent<UISprite>().fillAmount = lastTime/(float.Parse(item["nTotalTime"].ToString())/1000);	
			}
		}
	}
	
	void Update()
	{	
		if (lastTime > 0)
		{
			lastTime -= Time.deltaTime;
			if(mask != null)
			{
				Hashtable buffTable	=	sdConfDataMgr.Instance().GetTable("buff");
				Hashtable item = buffTable[buffid.ToString()] as Hashtable;
					mask.GetComponent<UISprite>().fillAmount = lastTime/(float.Parse(item["nTotalTime"].ToString())/1000);	
			}
		}
		
		if (isPressed && delttime >= 0)
		{
			delttime += Time.deltaTime;
			if (delttime >= 1)
			{
				if (isBuff && buffTip != null)
				{
					ShowTip(buffTip);
				}
				else if(!isBuff && debuffTip != null)
				{
					ShowTip(debuffTip);
				}
			}
		}
		else if (!isPressed && delttime >= 0)
		{
			delttime = -1;
			if (isBuff && buffTip != null)
			{
				HideTip(buffTip);
			}
			else if(!isBuff && debuffTip != null)
			{
				HideTip(debuffTip);
			}
		}
	}

	// 加载Buff图标aa
	protected void LoadBuffIcon()
	{
		ResLoadParams para = new ResLoadParams();
		para.info = "buff";
		string namePreb = "UI/Icon/$icon_buff_0/buff.prefab";
		sdResourceMgr.Instance.LoadResource(namePreb, LoadAtlas, para, typeof(UIAtlas));
	}

	// 加载图集回调aa
	protected void LoadAtlas(ResLoadParams kRes, UnityEngine.Object kObj)
	{
		if (kRes.info == "buff")
		{
			if (sdConfDataMgr.Instance().BuffAtlas == null)
				sdConfDataMgr.Instance().BuffAtlas = kObj as UIAtlas;

			if (icon != null)
				icon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().BuffAtlas;
		}
	}
}