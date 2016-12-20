using UnityEngine;
using System.Collections;

public class sdLevelArrow : sdTriggerReceiver {
	
	//sdTuiTuLogic logic;
	
	public Vector3[] arrowTarget;
	
	private GameObject mainChar = null;
	
	private Camera mainCamera = null;
	
	bool isActive = false;
	
	int currentID = 0;
	
	//箭头分怪物指向和位置指向.....
	
	bool monsterMode = false;
	
	void Awake()
	{
		
		//logic = sdGameLevel.instance.tuiTuLogic;
		
	//	mainCamera = sdGameLevel.instance.mainCamera.camera;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(mainChar == null)
		{
			mainChar = sdGameLevel.instance.mainChar.gameObject;
		}
		if(mainCamera==null)
		{
			mainCamera = sdGameLevel.instance.mainCamera.GetComponent<Camera>();
		}
		if(isActive)
		{
			if(mainChar != null)
			{
				sdTuiTuLogic logic	=	sdGameLevel.instance.tuiTuLogic;
				Vector3 mainPos = mainChar.transform.position;
				Vector3 targetPos = arrowTarget[currentID-1];
				
				Vector3 dir3D = targetPos-mainPos;
				dir3D.Normalize();
				
				//Vector3 vDir	=	sdGameLevel.instance.cameraRelativeDistance;
				//vDir.Normalize();
				//Vector3 vRight	=	
				
				//targetPos = mainPos+dir3D;
				
				//mainPos = mainCamera.WorldToScreenPoint(mainPos);
				targetPos = mainCamera.WorldToViewportPoint(targetPos);
				targetPos.z = 0.5f;
				targetPos*=2.0f;
				targetPos-=Vector3.one;
				targetPos.Normalize();
				//targetPos.y = -targetPos.y;

				GameObject arrowUI = logic.GetArrowUI();
				if(arrowUI != null)
				{
					if(!arrowUI.active)
					{
						arrowUI.SetActive(true);
					}
					GameObject sonArrowUI = logic.GetSonArrowUI();
					//UIAnchor anchor = arrowUI.GetComponent<UIAnchor>();
					//anchor.pixelOffset = uiPos;
					arrowUI.transform.position = new Vector3(targetPos.x,targetPos.y,0.0f);//*0.6f;

					sonArrowUI.transform.localRotation = Quaternion.FromToRotation(new Vector3(1,0,0),targetPos);

				}
			}
		}
		else
		{
			if(monsterMode == false)
			{
				
			}
		}
	}
	
	public override void OnTriggerHitted (GameObject obj,int[] param)
	{
		base.OnTriggerHitted (obj,param);
		sdTuiTuLogic logic	=	sdGameLevel.instance.tuiTuLogic;
		if(param[3] == 0)//不显示方向箭头
		{
			/*
			if(logic.GetArrowUI() != null)
			{
				logic.GetArrowUI().SetActive(false);
				isActive = false;
			}
			*/
		}
		else
		{
			/*
			if(logic.GetArrowUI() != null)
			{
				logic.GetArrowUI().SetActive(true);
				
			}
			*/
			isActive = true;
			currentID = param[3];
		}
	}
}
