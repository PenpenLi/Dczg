using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SDLevel : MonoBehaviour {
	
	public Color MajorLight = new Color(1.0f,1.0f,1.0f,1.0f);
	public float MajorLightIntensity = 1.0f;
	public float MajorLightRadius = 5.0f;
	
	protected Camera mainCamera;
	protected Vector3 cameraVec;
	protected float cameraDis;
	protected Vector3 frontDir;//移动时用的前方世界方	
	protected Vector3 rightDir;
	protected int uiLayer;
	protected GameObject mainPlayer;
	protected SDPlayer playerctrl;
	
	protected GameObject joystk = null;
	protected GUITexture joyTexture= null;
	protected Joystick joyCtrl= null;
	protected GameObject joyBack = null;
	protected GUITexture joyBackTexture = null;
	
	protected Camera uiCamera = null;
	
	protected List<GameObject> monsters = new List<GameObject>();//怪物列表
	protected List<GameObject> dropItems = new List<GameObject>();//掉落道具
	
	protected virtual void Awake() 
	{
		mainPlayer = /*SDScriptInterface.getMainCharacter().gameObject;*/GameObject.Find("@MainCharacter");
		playerctrl = (SDPlayer)mainPlayer.GetComponent("SDPlayer");
		mainCamera = /*SDScriptInterface.getMainCamera().gameObject.GetComponent<Camera>();*/GameObject.Find("@MainCamera").GetComponent<Camera>();
		
		cameraVec = mainCamera.transform.position - mainPlayer.transform.position;
		cameraDis = Vector3.Distance(Vector3.zero,cameraVec);
		cameraVec.Normalize();
		
		Shader.SetGlobalVector("cameraDir",new Vector4(cameraVec.x,cameraVec.y,cameraVec.z,0));
		Shader.SetGlobalVector("MajorLightColor",new Vector4(MajorLight.r*MajorLightIntensity,
			MajorLight.g*MajorLightIntensity,
			MajorLight.b*MajorLightIntensity,MajorLightRadius));
		
		frontDir = new Vector3(-cameraVec.x,0.0f,-cameraVec.z);
		frontDir.Normalize();
		rightDir = Vector3.Cross(Vector3.up,frontDir);
		rightDir.Normalize();
		
		/*joystk = GameObject.Find("@JoyStick");
		joyTexture = joystk.GetComponent<GUITexture>();
		joyCtrl = joystk.GetComponent<Joystick>();
		
		joyBack = GameObject.Find("@JoyBack").gameObject;
		if(joyBack != null)
		{
			joyBackTexture = joyBack.GetComponent<GUITexture>();
			joyBack.SetActive(false);
		}*/
		
		uiLayer = LayerMask.NameToLayer("NGUI");
		//InitTriggers();
	}
	// Use this for initialization
	void Start () 
	{
		//BundleGlobal.Instance.UnloadNormalBundle();
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		for(int i = 0; i < monsters.Count; i++)
		{
			if(monsters[i] == null)
				continue;
			
			SDPlayer monCtrl = monsters[i].GetComponent<SDPlayer>();
			if(monCtrl.isDisapeared())
			{
				monCtrl.DestroyHPUI();
				GameObject.Destroy(monsters[i]);
				monsters[i] = null;
			}
		}
		
		
		//手机和PC模式用不同的方式给摇杆赋
		Vector2 joyPos = new Vector2(0.0f,0.0f);
		if((Application.platform == RuntimePlatform.Android) ||
			(Application.platform) == RuntimePlatform.IPhonePlayer)
		{
			int tCount = Input.touchCount;
			if(tCount > 0)
			{
				for(int i = 0; i < tCount; i++)
				{
					Touch tch = Input.GetTouch(i);
					if(tch.phase == TouchPhase.Began && (!joyCtrl.IsFingerDown()))
					{
						if(!CheckHitUI(tch.position))
						{
							Rect pixelInset = new Rect(tch.position.x-joyTexture.pixelInset.width*0.5f,
								tch.position.y-joyTexture.pixelInset.height*0.5f,
							joyTexture.pixelInset.width,joyTexture.pixelInset.height);
							joyTexture.pixelInset = pixelInset;
							joyCtrl.ReSetDefaultRect();
							joyCtrl.ResetJoystick();
							joystk.SetActive(true);
							
							if(joyBackTexture != null)
							{
								pixelInset = new Rect(tch.position.x-joyBackTexture.pixelInset.width*0.5f,
								tch.position.y-joyBackTexture.pixelInset.height*0.5f,
								joyBackTexture.pixelInset.width,joyBackTexture.pixelInset.height);
								joyBackTexture.pixelInset = pixelInset;
							}
							//<							
						}
					}
				}
			}
			
			if(joyCtrl.IsFingerDown())
			{
				joyPos = joyCtrl.position;
				if(joyBack != null)
				{
					joyBack.SetActive(true);
				}
			}
			else
			{
				if(joyBack != null)
				{
					joyBack.SetActive(false);
				}
			}
		}
		else
		{
			joyPos.x = Input.GetAxis("Horizontal");
			joyPos.y = Input.GetAxis("Vertical");
		}
		
		if(Mathf.Abs(joyPos.x) > 0.01f || Mathf.Abs(joyPos.y) > 0.01f)
		{
			Vector3 worldDir = rightDir * joyPos.x + frontDir * joyPos.y;
			worldDir.Normalize();
			playerctrl.runToTarget(mainPlayer.transform.position + worldDir*1.0f);
		}
		else
		{
			if(playerctrl.isRunning())
			{
				playerctrl.runToTarget(mainPlayer.transform.position);
			}
		}
		
		
		
		mainCamera.transform.position = playerctrl.transform.position + cameraVec*cameraDis;
		mainCamera.transform.LookAt(playerctrl.transform);
			
		Shader.SetGlobalVector("MajorPos",new Vector4(playerctrl.transform.position.x,
			playerctrl.transform.position.y,playerctrl.transform.position.z,0));
		
		/*
		if( Input.GetKeyUp(KeyCode.Escape) )
		{
			GameObject townui = GameObject.Find("TownUi");
			if( townui != null )
			{
				townui.GetComponent<sdTown>().ShowSetting();	
			}
			else
			{
				GameObject fightui = GameObject.Find("NGUIRoot");
				if (fightui != null)
					fightui.GetComponentInChildren<sdFightUi>().ShowSetting();
			}
				
			//Application.Quit();
		}
		*/
	}
	
	/*void InitTriggers()
	{
		GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");
		
		for(int i = 0; i < triggers.Length; i++)
		{
			MonsterTrigger mTrigger = triggers[i].GetComponent<MonsterTrigger>();
			
			if(mTrigger != null)
			{
				mTrigger.CreateMonsterList();
			}
		}
		
		GameObject[] monsters2 = GameObject.FindGameObjectsWithTag("Monster");
		
		for(int i = 0; i < monsters2.Length; i++)
		{
			if(monsters2[i].activeSelf)
			{
				SDPlayer monCtrl = monsters2[i].GetComponent<SDPlayer>();
				monCtrl.DestroyHPUI();
				GameObject.Destroy(monsters2[i]);
				//monsters2[i].SetActive(false);
			}
		}
	}*/
	
	protected bool CheckHitUI(Vector2 pos)
	{
		if(uiCamera == null)
			return false;
		Ray ray = uiCamera.ScreenPointToRay(pos);
		RaycastHit hit;
		LayerMask layerMask = 1 << uiLayer;
		return Physics.Raycast(ray,out hit,100.0f,layerMask);
	}
	
	public int AddMonster(GameObject mon)
	{
		for(int i = 0; i < monsters.Count; i++)
		{
			if(monsters[i] == null)
			{
				monsters[i] = mon;
				return i;
			}
		}
		monsters.Add(mon);
		return monsters.Count-1;
	}
	
	public int GetMonsterNum()
	{
		return monsters.Count;
	}
	
	public GameObject GetMonster(int id)
	{
		if(id < 0 || id >= monsters.Count)
			return null;
		
		return monsters[id];
	}
	
	public void AddDropItem(GameObject item)
	{
		for(int i = 0; i < dropItems.Count; i++)
		{
			if(dropItems[i] == null)
			{
				dropItems[i] = item;
				return;
			}
		}
		dropItems.Add(item);
	}
	
	public int GetDropItemNum()
	{
		return dropItems.Count;
	}
	
	public GameObject GetDropItem(int id)
	{
		if(id < 0 || id >= dropItems.Count)
			return null;
		
		return dropItems[id];
	}
}
