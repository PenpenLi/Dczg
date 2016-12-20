using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnePetObtainPanel : MonoBehaviour
{

    enum EffectType
	{
		EFFECT_TYPE_QIDAO1,
		EFFECT_TYPE_QIDAO2,
	}

    public GameObject m_BuyType0 = null;
    public GameObject m_BuyType1 = null;
    public GameObject m_BuyType2 = null;
    public GameObject m_BuyType3 = null;

    public UILabel m_LblPetName = null;
	public UISprite m_Star1 = null;
	public UISprite m_Star2 = null;
	public UISprite m_Star3 = null;
	public UISprite m_Star4 = null;
	public UISprite m_Star5 = null;
	public GameObject m_starRoot = null;


	public GameObject fx_qidao_001 = null;
   	public GameObject fx_qidao_002 = null;

    private int m_BuyPetType = -1;
    private uint m_TemplateID = 0;

	float _beginShowTime = 0;
	float _delayShowTime = 0;
	bool _beginShow = false;


	protected List<Shader> mOriginShaders = new List<Shader>();
	protected float mCurrentAlpha = 0.0f;
	public Hashtable meshTable = new Hashtable();

	public Shader alphaBlendShader = null;
	Shader AlphaBlendShader
	{
		get{
			if(alphaBlendShader==null)
			{
				GameObject obj = Resources.Load("BlendShader") as GameObject;
				ShaderList shaderlst = obj.GetComponent<ShaderList>();
				alphaBlendShader = shaderlst.shaderList[1];
			}
			return alphaBlendShader;
		}
	}

	void Awake()
	{

	}

	// Use this for initialization
	void Start ()
    {

	}
	
	void OnEnable()
	{

		if(sdMallManager.Instance == null)
			return;

		if(sdMallManager.Instance.m_magicTowerPanel==null)
			return;
			
		sdMallManager.Instance.m_magicTowerPanel.transform.
			FindChild("Fx_Qidao_003").gameObject.SetActive(false);

		
	}

	void OnDisable()
	{		
		sdMallManager.Instance.m_onePetObtainPanelOpen = false;

		if(sdMallManager.Instance == null)
			return;

		if(sdMallManager.Instance.m_magicTowerPanel==null)
			return;

		sdMallManager.Instance.m_magicTowerPanel.transform.
			FindChild("Fx_Qidao_003").gameObject.SetActive(true);

		sdGameLevel.instance.petCamera.enabled = false;

		

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mCurrentAlpha < 1.0f)
		{
			mCurrentAlpha += Time.deltaTime;
			if (mCurrentAlpha > 1.0f)
				mCurrentAlpha = 1.0f;
			
			this.transform.FindChild("root/PetView").GetComponent<UITexture>().alpha = mCurrentAlpha;
		}

	}

	// 收集渲染对象信息aa
	protected int GatherMeshes(GameObject rootRenerNode)
	{
		if (rootRenerNode == null)
			return 0;
		
		meshTable.Clear();
	
		SkinnedMeshRenderer[] skinmesh = rootRenerNode.GetComponentsInChildren<SkinnedMeshRenderer>();
		int i = skinmesh.Length;
		foreach (SkinnedMeshRenderer child in skinmesh)
		{
			meshTable[child.name] = child.gameObject;
		}
		MeshRenderer[] staticmesh = rootRenerNode.GetComponentsInChildren<MeshRenderer>();
		i += staticmesh.Length;
		foreach (MeshRenderer child in staticmesh)
		{
			meshTable[child.name] = child.gameObject;
		}
		
		return i;
	}
	
	// 收集Mesh的Shaderaa
	protected void GatherMeshShaders()
	{
		if (meshTable == null)
			return;

		mOriginShaders.Clear();
		foreach(DictionaryEntry kEntry in meshTable)
		{
			GameObject kObject = kEntry.Value as GameObject;
			Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
			for (int k = 0; k < kObjectRenderer.materials.Length; k++)
			{
				if (kObjectRenderer.materials[k] != null)
					mOriginShaders.Add(kObjectRenderer.materials[k].shader);
			}
		}
	}

	public void TurnToBlendShader()
	{
		foreach(DictionaryEntry kEntry in meshTable)
		{
			GameObject kObject = kEntry.Value as GameObject;
			Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
			for(int k = 0; k < kObjectRenderer.materials.Length; k++)
			{
				if (kObjectRenderer.materials[k] != null)
					kObjectRenderer.materials[k].shader = AlphaBlendShader;
			}
		}
	}
	
	// 替换回原有着色器,并设置为不透明aa
	protected void ReturnToOriginShader()
	{
		int iCount = 0;
		foreach(DictionaryEntry kEntry in meshTable)
		{
			GameObject kObject = kEntry.Value as GameObject;
			Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
			for(int k = 0; k < kObjectRenderer.materials.Length; k++)
			{
				if (kObjectRenderer.materials[k] != null)
				{
					kObjectRenderer.materials[k].shader = mOriginShaders[iCount++];
					
					Color kSourceColor = kObjectRenderer.materials[k].GetColor("_Color");
					kSourceColor.a = 1.0f;	
					kObjectRenderer.materials[k].SetColor("_Color", kSourceColor);
				}
			}
		}
	}

	protected void SetShadowEnable(bool bEnable)
	{
		foreach(DictionaryEntry kEntry in meshTable)
		{
			GameObject kObject = kEntry.Value as GameObject;
			Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
			kObjectRenderer.castShadows = bEnable;
			kObjectRenderer.receiveShadows = false;
		}
	}
	
	Vector2 lastDelta;
	void OnDrag(Vector2 delta)
	{
		GameObject	petCam	=	sdGameLevel.instance.petCamera.gameObject;
		if(petCam!=null)
		{
			Quaternion	q	=	petCam.transform.GetChild(0).localRotation;
			q*=	Quaternion.AngleAxis(-delta.x*0.001f*360.0f,new Vector3(0,1,0));
			petCam.transform.GetChild(0).localRotation = q;
		}
	}

	/*
	void OnDrag(Vector2 delta)
	{
		if( m_PetModel == null )
			 return;
			
		m_PetModel.transform.Rotate(0,-delta.x/2.0f,0);
	}
	*/

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
				gobj.transform.localPosition = new Vector3(0,-140.0f, -200.0f);
				gobj.transform.localScale = new Vector3(200.0f,200.0f,200.0f);
				auto.Life = 1.5f;
			}
			if ( (EffectType)param.userdata0 == EffectType.EFFECT_TYPE_QIDAO2)
			{
				gobj.transform.parent = this.transform;
				gobj.transform.localPosition = new Vector3(0,-140.0f, -200.0f);
				gobj.transform.localScale = new Vector3(60.0f,60.0f,60.0f);
				auto.Life = 1.5f;
			}
		}
	}

    public void RefreshPanel(int iBuyPetType, uint uiTemplateID)
    {
        m_BuyPetType = iBuyPetType;
        m_TemplateID = uiTemplateID;
		
        switch (m_BuyPetType)
        {

            case ((int)HeaderProto.EBuyPetType.EBuyPetType_Exchange):
                {
					transform.FindChild("root/Background").gameObject.SetActive(true);
					transform.FindChild("root/Background").GetComponent<UISprite>().spriteName = "petshop_bg1";
					SetBuyTypeBtnActive(true, false, false, false);
                }
                break;
            case ((int)HeaderProto.EBuyPetType.EBuyPetType_Cheap):
                {
                    //SetBuyTypeBtnActive(false, true, false, false);
                }
                break;
            case ((int)HeaderProto.EBuyPetType.EBuyPetType_Expensive):
            case ((int)HeaderProto.EBuyPetType.EBuyPetType_Expensive10):
                {
					transform.FindChild("root/Background").gameObject.SetActive(true);
					transform.FindChild("root/Background").GetComponent<UISprite>().spriteName = "petshop_bg2";
                    SetBuyTypeBtnActive(false, false, true, true);
                }
                break;
        }

        Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(m_TemplateID.ToString());
        if (info != null)
        {
            ShowPetModel(m_TemplateID);

            string strName = info["Name"].ToString();
            if (m_LblPetName != null)
            {
				m_LblPetName.color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(info["Ability"].ToString()));
                m_LblPetName.text = strName;
            }

            string strPath = info["Res"].ToString();

			int ability = int.Parse(info["Ability"].ToString());
			if(m_Star1 != null || m_Star2 != null || m_Star3 != null || m_Star4 != null)
			{
				switch (ability)
				{
					case 1: 
					{
					m_starRoot.transform.localPosition = new Vector3(70, 200, 0);
					m_Star1.gameObject.SetActive(true);
					m_Star2.gameObject.SetActive(false);
					m_Star3.gameObject.SetActive(false);
					m_Star4.gameObject.SetActive(false);
					m_Star5.gameObject.SetActive(false);
					ShowEffect(EffectType.EFFECT_TYPE_QIDAO1);
					break;
					}

					case 2:
					{
					m_starRoot.transform.localPosition = new Vector3(52, 200, 0);
					m_Star1.gameObject.SetActive(true);
					m_Star2.gameObject.SetActive(true);
					m_Star3.gameObject.SetActive(false);
					m_Star4.gameObject.SetActive(false);
					m_Star5.gameObject.SetActive(false);
					ShowEffect(EffectType.EFFECT_TYPE_QIDAO1);
					break;
					}

					case 3:
					{
					m_starRoot.transform.localPosition = new Vector3(35, 200, 0);
					m_Star1.gameObject.SetActive(true);
					m_Star2.gameObject.SetActive(true);
					m_Star3.gameObject.SetActive(true);
					m_Star4.gameObject.SetActive(false);
					m_Star5.gameObject.SetActive(false);
					ShowEffect(EffectType.EFFECT_TYPE_QIDAO1);
					break;
					}

					case 4:
					{
					m_starRoot.transform.localPosition = new Vector3(17, 200, 0);
					m_Star1.gameObject.SetActive(true);
					m_Star2.gameObject.SetActive(true);
					m_Star3.gameObject.SetActive(true);
					m_Star4.gameObject.SetActive(true);
					m_Star5.gameObject.SetActive(false);
					ShowEffect(EffectType.EFFECT_TYPE_QIDAO2);
					break;
					}
				
					case 5:
					{
					m_starRoot.transform.localPosition = new Vector3(0, 200, 0);
					m_Star1.gameObject.SetActive(true);
					m_Star2.gameObject.SetActive(true);
					m_Star3.gameObject.SetActive(true);
					m_Star4.gameObject.SetActive(true);
					m_Star5.gameObject.SetActive(true);
					ShowEffect(EffectType.EFFECT_TYPE_QIDAO2);
					break;

					}
				}
			}
            //"Icon"
        }

		Transform showAttributeBtnTransform = transform.FindChild("root/ShowAttribute");
		if (showAttributeBtnTransform != null)
		{
			OnePetObtainBtnHander handler = showAttributeBtnTransform.GetComponent<OnePetObtainBtnHander> ();
			handler.m_iPetId = m_TemplateID;
		}
    }

    private void SetBuyTypeBtnActive(bool b1, bool b2, bool b3, bool b4)
    {
		
		this.transform.FindChild("root/ShowAttribute").gameObject.SetActive(true);

        if (m_BuyType0 != null)
        {
            m_BuyType0.SetActive(b1);
        }
        if (m_BuyType1 != null)
        {
            m_BuyType1.SetActive(b2);
        }
        if (m_BuyType2 != null)
        {
            m_BuyType2.SetActive(b3);
        }
        if (m_BuyType3 != null)
        {
            m_BuyType3.SetActive(b4);
        }
    }

    private void ShowPetModel(uint m_TemplateID)
    {
        Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(m_TemplateID.ToString());
        if (info != null)
        {
            ResLoadParams param = new ResLoadParams();
            param.pos = new Vector3(0, -130.0f, -200.0f);
            param.scale = new Vector3(140.0f, 140.0f, 140.0f);
            string strPath = (info["Res"].ToString()).Replace(".prefab", "_UI.prefab");
            sdResourceMgr.Instance.LoadResource(strPath, PetLoadInstantiate, param);
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

		this.transform.FindChild("root/PetView").GetComponent<UITexture>().mainTexture = 
			sdGameLevel.instance.petTexture;
		sdGameLevel.instance.petCamera.enabled = true;
		
		this.transform.FindChild("root/PetView").gameObject.SetActive(true);
		mCurrentAlpha = 0;
		this.transform.FindChild("root/PetView").GetComponent<UITexture>().alpha = 0;
	}

}
