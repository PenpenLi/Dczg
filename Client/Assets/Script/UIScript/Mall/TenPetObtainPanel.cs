using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TenPetObtainPanel : MonoBehaviour
{
    public UILabel m_LblPetRemainCount;
	public static int MAX_PET_UI_SLOT = 10;

	enum EffectType
	{
		EFFECT_TYPE_QIDAO1,
		EFFECT_TYPE_QIDAO2,
	}

	class ShowAniPetInfo
	{
		public float beginAniTime;
		public bool isBeginAni;
		public string strName;
		public int ability;
		public string icon;
		public int idx;
		public uint templateId;
	}
	
	List<ShowAniPetInfo> _showAniPetInfos = new List<ShowAniPetInfo>();
	float _currentAlpha = 0;

	void ShowEffect(EffectType effectType)
	{ 
		if(effectType == EffectType.EFFECT_TYPE_QIDAO1)
		{
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = effectType;
			sdResourceMgr.Instance.LoadResource(
				"Effect/MainChar/FX_UI/$Fx_Qidao/Fx_Qidao_001.prefab", 
				OnFxLoaded, param);
			
		}
		else if(effectType == EffectType.EFFECT_TYPE_QIDAO2)
		{
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = effectType;
			sdResourceMgr.Instance.LoadResource(
				"Effect/MainChar/FX_UI/$Fx_Qidao/Fx_Qidao_002.prefab", 
				OnFxLoaded, param);
		}
	}
	
	void OnFxLoaded(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj != null)
		{
			GameObject gobj = sdGameObject.Instantiate(obj) as GameObject;
			sdAutoDestory auto  =   gobj.AddComponent<sdAutoDestory>();
			if((EffectType)param.userdata0 == EffectType.EFFECT_TYPE_QIDAO1)
			{
				gobj.transform.parent = this.transform;
				gobj.transform.localPosition = new Vector3(0,-190.0f, -200.0f);
				gobj.transform.localScale = new Vector3(200.0f,200.0f,200.0f);
				auto.Life = 1.5f;
			}
			if ( (EffectType)param.userdata0 == EffectType.EFFECT_TYPE_QIDAO2)
			{
				gobj.transform.parent = this.transform;
				gobj.transform.localPosition = new Vector3(0,-190.0f, -200.0f);
				gobj.transform.localScale = new Vector3(60.0f,60.0f,60.0f);
				auto.Life = 1.5f;
			}
		}
	}

	// Use this for initialization
	void Start () 
	{
        if (sdConfDataMgr.Instance().PetAtlas == null)
        {
            ResLoadParams kParam = new ResLoadParams();
            kParam.info = "pet";
            string kPath = "UI/Icon/$icon_pet_0/icon_pet_0.prefab";
            sdResourceMgr.Instance.LoadResource(kPath, OnLoadAtlas, kParam, typeof(UIAtlas));
        }
	
		RefreshLblPetRemainCount(sdMallManager.Instance.PetRemainCount);
	}

    protected void OnLoadAtlas(ResLoadParams kRes, UnityEngine.Object kObj)
    {
        if (kRes.info == "pet")
        {
            if (sdConfDataMgr.Instance().PetAtlas == null)
                sdConfDataMgr.Instance().PetAtlas = kObj as UIAtlas;

            RefreshPanel();
        }
    }

	void OnEnable()
	{
	
		if(sdMallManager.Instance.m_magicTowerPanel != null)
		{
			sdMallManager.Instance.m_magicTowerPanel.transform.
				FindChild("Fx_Qidao_003").gameObject.SetActive(false);
		}		
	}
	
	void OnDisable()
	{		
		sdMallManager.Instance.m_tenPetObtainPanelOpen = false;

		_showAniPetInfos.Clear();

		if(sdMallManager.Instance.m_magicTowerPanel != null)
		{
			sdMallManager.Instance.m_magicTowerPanel.transform.
				FindChild("Fx_Qidao_003").gameObject.SetActive(true);
		}		
	}

	// Update is called once per frame
	void Update () 
	{
		if(gameObject == null)
			return;

		if (_currentAlpha < 1.0f)
		{
			_currentAlpha += Time.deltaTime;
			if (_currentAlpha > 1.0f)
				_currentAlpha = 1.0f;
			
			this.transform.FindChild("PetView").GetComponent<UITexture>().alpha = _currentAlpha;
		}

		if(_showAniPetInfos.Count == 0)
			return;

		foreach(ShowAniPetInfo item in _showAniPetInfos)
		{
			if(item.isBeginAni == false)
			{
				item.isBeginAni = true;
				item.beginAniTime = Time.time;				
				return;
			}
			else
			{
				if(Time.time >= item.beginAniTime + 0.4f)
				{
					FillSinglePetUI(item.templateId, item.idx, item.strName, item.ability, item.icon);
					_showAniPetInfos.Remove(item);
				}
	
				return;
			}
		}
	}

	public void RefreshLblPetRemainCount(uint count)
	{
		if (m_LblPetRemainCount != null)
		{
            if (count == 0)
            {
                string strInfo = "本次购买必得4星战魂";
                m_LblPetRemainCount.text = strInfo;
            }
            else
            {
                string strInfo = "再购买";
                strInfo += count.ToString();
                strInfo += "次必获得4星战魂";
                m_LblPetRemainCount.text = strInfo;
            }
		}
	}

	public void RefreshPanel()
	{
		int nPetSize = sdMallManager.Instance.PetResultSize;
		
		_showAniPetInfos.Clear();
		for (int i=0; i < nPetSize && i < MAX_PET_UI_SLOT; i++)
		{
			uint uiTemplateId = sdMallManager.Instance.GetResultTemplates(i);
			Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(uiTemplateId.ToString());
			if (info != null)
			{
				ShowAniPetInfo showAniPetInfo = new ShowAniPetInfo();
				showAniPetInfo.strName = info["Name"].ToString();
				showAniPetInfo.ability = int.Parse(info["Ability"].ToString());
				showAniPetInfo.icon = info["Icon"].ToString();
				showAniPetInfo.beginAniTime = 0;
				showAniPetInfo.isBeginAni = false;
				showAniPetInfo.idx = i;
				showAniPetInfo.templateId = uiTemplateId;
				_showAniPetInfos.Add(showAniPetInfo);
			}
		}
	}

	private void ShowPetModel(uint m_TemplateID, uint ability, GameObject iconItem)
	{
		Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(m_TemplateID.ToString());
		if (info != null)
		{
			ResLoadParams param = new ResLoadParams();
			param.pos = new Vector3(0, -130.0f, -200.0f);
			param.scale = new Vector3(140.0f, 140.0f, 140.0f);
			param.userdata0 = iconItem;
			string strPath = (info["Res"].ToString()).Replace(".prefab", "_UI.prefab");
			sdResourceMgr.Instance.LoadResource(strPath, PetLoadInstantiate, param);

			if(ability>=1 && ability<=3)
				ShowEffect(EffectType.EFFECT_TYPE_QIDAO1);
			else
				ShowEffect(EffectType.EFFECT_TYPE_QIDAO2);
		}
	}
	
	private void PetLoadInstantiate(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) 
			return;
		
		GameObject petModel = GameObject.Instantiate(obj) as GameObject;
		petModel.name = "PetSmallTipModel";

		if(sdGameLevel.instance.petCamera.transform.FindChild("PetSmallTipModel") != null)
		{
			GameObject.DestroyImmediate(sdGameLevel.instance.petCamera.
				transform.FindChild("PetSmallTipModel").gameObject);
		}

		petModel.transform.parent = sdGameLevel.instance.petCamera.transform;
		
		petModel.transform.localPosition = new Vector3(0.0f, -1.00f, 5.0f);
		petModel.transform.localRotation = Quaternion.AngleAxis(180.0f, Vector3.up);
		petModel.transform.localScale *= 2.0f;
		
		int petLayer = LayerMask.NameToLayer("PetNode");
		Transform[] renderData = petModel.GetComponentsInChildren<Transform>();
		foreach(Transform t in renderData)
		{
			t.gameObject.layer = petLayer;
		}
		
		this.transform.FindChild("PetView").GetComponent<ScalePetView>().m_petIcon = param.userdata0 as GameObject;
		this.transform.FindChild("PetView").GetComponent<ScalePetView>().m_beginTime = Time.time;
		this.transform.FindChild("PetView").GetComponent<ScalePetView>().m_currScale = 1.0f;
		this.transform.FindChild("PetView").localScale = new Vector3(1.0f, 1.0f, 1.0f);
		this.transform.FindChild("pet_grey").gameObject.SetActive(true);

		this.transform.FindChild("PetView").GetComponent<UITexture>().mainTexture = 
			sdGameLevel.instance.petTexture;
		sdGameLevel.instance.petCamera.enabled = true;
		
		this.transform.FindChild("PetView").gameObject.SetActive(true);
		_currentAlpha = 0;
		this.transform.FindChild("PetView").GetComponent<UITexture>().alpha = 0;
	}

    private void FillSinglePetUI(uint uiTemplateId, int index, string name, int ability, string icon)
	{
		Transform item = gameObject.transform.FindChild ("Item" + index);
		item.gameObject.SetActive(true);
		//ShowPetModel(uiTemplateId, (uint)ability, item.gameObject);

		UILabel petName = item.FindChild("PetName").GetComponent<UILabel>();
		petName.text = name;
		petName.color = sdConfDataMgr.Instance().GetItemQuilityColor(ability);

		item.FindChild("PetPic/Background").GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetPetQuilityBorder(ability);

		UISprite star1 = item.FindChild ("StarRoot/Star1").GetComponent<UISprite> ();
		UISprite star2 = item.FindChild ("StarRoot/Star2").GetComponent<UISprite> ();
		UISprite star3 = item.FindChild ("StarRoot/Star3").GetComponent<UISprite> ();
		UISprite star4 = item.FindChild ("StarRoot/Star4").GetComponent<UISprite> ();
		UISprite star5 = item.FindChild ("StarRoot/Star5").GetComponent<UISprite> ();
		switch (ability)
		{
			case 1:
			{
				star1.gameObject.SetActive(true);
				star2.gameObject.SetActive(false);
				star3.gameObject.SetActive(false);
				star4.gameObject.SetActive(false);
				star5.gameObject.SetActive(false);
				break;
			}

			case 2:
			{
				star1.gameObject.SetActive(true);
				star2.gameObject.SetActive(true);
				star3.gameObject.SetActive(false);
				star4.gameObject.SetActive(false);
				star5.gameObject.SetActive(false);
				break;
			}

			case 3:
			{
				star1.gameObject.SetActive(true);
				star2.gameObject.SetActive(true);
				star3.gameObject.SetActive(true);
				star4.gameObject.SetActive(false);
				star5.gameObject.SetActive(false);
				break;
			}

			case 4:
			{
				star1.gameObject.SetActive(true);
				star2.gameObject.SetActive(true);
				star3.gameObject.SetActive(true);
				star4.gameObject.SetActive(true);
				star5.gameObject.SetActive(false);
				break;
			}
			
			case 5:
			{
				star1.gameObject.SetActive(true);
				star2.gameObject.SetActive(true);
				star3.gameObject.SetActive(true);
				star4.gameObject.SetActive(true);
				star5.gameObject.SetActive(true);
				break;
			}
			
			
		default:
			break;

		}

		UISprite petPic = item.FindChild ("PetPic").GetComponent<UISprite> ();
		petPic.spriteName = icon;
        petPic.atlas = sdConfDataMgr.Instance().PetAtlas;

        UISprite background = item.FindChild("PetPic").FindChild("Background").GetComponent<UISprite>();
        if (background != null)
        {
            TenPetObtainPicHandler handler = background.GetComponent<TenPetObtainPicHandler>();
            handler.m_iPetId = uiTemplateId;
        }
	}

}
