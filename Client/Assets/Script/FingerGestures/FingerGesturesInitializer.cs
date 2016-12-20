using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// This class is used to create and initialise the proper FigureGesture implementation based on the current platform the application is being run on
/// </summary>
/// 
public enum enumAutoType
{
	enAT_Stop,
	enAT_Move,
	enAT_WaitMove,
}

/// <summary>
/// 涓昏?鎵嬪娍鎺у埗aa
/// </summary>
public class FingerGesturesInitializer : MonoBehaviour 
{
	public FingerGestures editorGestures;
	public FingerGestures desktopGestures;
	public FingerGestures iosGestures;
	public FingerGestures androidGestures;
	
	// whether to make the FingerGesture persist through scene loads
	public bool makePersistent = true;
	
	GUITexture	DirControlBG	=	null;
	GUITexture	DirControlCenter=	null;
	GUITexture	QuickSkillPanel	= 	null;	// 蹇?熸妧鑳介潰鏉剧璺熼殢鎵嬫寚瑙︽帶)aa
	//GameObject	MoveTarget		=	null;
    bool bHasRoll = false;
	// 涓昏?鑹插?璞?a
	protected sdGameActor mMainChar = null;
	public sdGameActor MainChar
	{
		set 
		{ 
			mMainChar = value;
			mFingerControl.SetActor(value);
		}
	}

	// 涓荤浉鏈哄?璞?a
	protected sdGameCamera mMainCamera = null;
	public sdGameCamera MainCamera
	{
		set 
		{ 
			mMainCamera = value;
			mFingerControl.MainCamera = value.GetComponent<Camera>();
		}
	}

	Vector2 	vecFingerDir	=	Vector2.zero;
	int 		iFingerIndex	=	-1;	
	int			iDirFingerIndex	=	-1;	
	
	float       fCurrentTime    = 	0.0f;

	bool			bUseQuickSkill  = 	false;	// 鏄?惁浣跨敤蹇?熸妧鑳介潰鏉縜a
//	bool			bRoll			=	false;
//	enumAutoType 	AutoMove		=	enumAutoType.enAT_Stop;
	Vector3			AutoMoveTarget	=	Vector3.zero;
	List<Vector3>	walkpath			=	new List<Vector3>();
//	int				lastState		=	0;

	// 鑷?姩鎴樻枟绯荤粺aa
	protected FingerControl	mFingerControl	= new FingerControl();
	public FingerControl CharFingerControl
	{
		get { return mFingerControl; }	
	}

	void Awake()                                  
	{
		transform.parent	=	sdGameLevel.instance.transform;
		
		if( !FingerGestures.Instance )
		{
			FingerGestures prefab;

			if( Application.isEditor )
			{
				prefab = editorGestures;
			}
			else
			{
#if UNITY_IPHONE
				prefab = iosGestures;
#elif UNITY_ANDROID
				prefab = androidGestures;
#else
				prefab = desktopGestures;
#endif
			}

			Debug.Log( "Creating FingerGestures using " + prefab.name );
			FingerGestures instance = Instantiate( prefab ) as FingerGestures;
			instance.name = prefab.name;
			instance.transform.parent	=	transform;
			//if( makePersistent )
			//	DontDestroyOnLoad( instance.gameObject );
		}
		name	=	"@FingerGestures";
		
		DirControlBG		=	transform.FindChild("DirControl_BG").GetComponent<GUITexture>();
		DirControlCenter	=	transform.FindChild("DirControl_Center").GetComponent<GUITexture>();
		QuickSkillPanel		=	transform.FindChild("MoveFingerSkillUI").GetComponent<GUITexture>();
		//MoveTarget			=	transform.FindChild("MoveTarget").gameObject;
		DirControlBG.enabled		=	false;
		DirControlCenter.enabled	=	false;
		QuickSkillPanel.enabled		=	false;
		//MoveTarget.SetActive(false);
		
		SetDirControl_Pos(0,0);
		
		// 璁剧疆鑷?姩鎴樻枟妯″紡aa
		bool bFullAutoMode = sdGameLevel.instance.FullAutoMode;
		mFingerControl.SetFullAutoMode(bFullAutoMode);
	}

	//
	public void  ClearMainCharacterMove()
	{
		mFingerControl.ClearMainCharacterMove();
	}

	// 鑴氭湰琚?惎鐢х缁ф壙鑷狹onoBehaviour)aa
	protected bool mEventHandler = false;
	void OnEnable()
	{
		if (!mEventHandler)
		{
			mEventHandler = true;

			Debug.Log("FingerGesturesInitializer.Enable.Handler");

			FingerGestures.OnFingerDown += OnFingerDown;
			FingerGestures.OnFingerUp += OnFingerUp;
			FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
			FingerGestures.OnFingerDragMove += OnFingerDragMove;
			FingerGestures.OnFingerDragEnd += OnFingerDragEnd;
//			FingerGestures.OnFingerSwipe += OnFingerSwipe;
//			FingerGestures.OnFingerTap += OnFingerTap;
//			FingerGestures.OnFingerStationaryBegin += OnFingerStationaryBegin;
//			FingerGestures.OnFingerStationary += OnFingerStationary;
//			FingerGestures.OnFingerStationaryEnd += OnFingerStationaryEnd;
//			FingerGestures.OnFingerLongPress += OnFingerLongPress;
		}
	}

	// 鑴氭湰琚??鐢х缁ф壙鑷狹onoBehaviour)aa
	void OnDisable()
	{
		if (mEventHandler)
		{
			mEventHandler = false;

			Debug.Log("FingerGesturesInitializer.Disable.Handler");

			FingerGestures.OnFingerDown -= OnFingerDown;
			FingerGestures.OnFingerUp -= OnFingerUp;
			FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
			FingerGestures.OnFingerDragMove -= OnFingerDragMove;
			FingerGestures.OnFingerDragEnd -= OnFingerDragEnd;
//			FingerGestures.OnFingerSwipe -= OnFingerSwipe;
//			FingerGestures.OnFingerTap -= OnFingerTap;
//			FingerGestures.OnFingerStationaryBegin -= OnFingerStationaryBegin;
//			FingerGestures.OnFingerStationary -= OnFingerStationary;
//			FingerGestures.OnFingerStationaryEnd -= OnFingerStationaryEnd;
//			FingerGestures.OnFingerLongPress -= OnFingerLongPress;
		}
	}

	// 鏇存柊(缁ф壙鑷狹onoBehaviour)aa
	void Update()
	{
		if (mMainChar == null)
			return;

		if (mMainCamera == null)
			return;

		if(iDirFingerIndex!=-1)
		{
			Rect r	=	DirControlCenter.pixelInset;
			Vector2 vCenter	=	new Vector2(r.x,r.y);
			r		=	DirControlBG.pixelInset;
			vCenter	-=	new Vector2(r.x+32,r.y+32);
			vCenter/=32.0f;
			if(Vector2.Distance(vCenter,Vector2.zero)	>	0.0001f)
			{
				Vector3 v = mMainCamera.transform.InverseTransformDirection(new Vector3(vCenter.x,0,vCenter.y));
				v.y=0.0f;
				v.Normalize();
				mMainChar._moveVector	=	v;
//				mMainChar.spinToTargetDirection(v,true);
//				mMainChar.moveInterval(mc.faceDirection*mc.moveSpeed*Time.deltaTime,false);
			}
		}

		mFingerControl.Update();

		return ;

//		if(bRoll){
//			if(mMainChar!=null)
//			{
//				sdMainChar mc	=	mMainChar.GetComponent<sdMainChar>();
//					if(mc!=null)
//					{
//						sdTSM graph = mc.logicTSM;
//						if(	(graph.statePointer.id == sdBaseState.warriorBaseID + 1 ||
//							graph.statePointer.id == sdBaseState.warriorBaseID + 2) &&
//							lastState	==	sdBaseState.warriorBaseID + 7)
//						{
//							bRoll	=	false;
//							lastState	=	0;
//							if(AutoMove!=enumAutoType.enAT_WaitMove)
//							{
//								mc._moveVector	=	Vector3.zero;
//							}
//						}else{
//							lastState	=	graph.statePointer.id;
//							//mc._moveVector	=	Vector3.zero;
//						}
//					}
//			}
//		}
//		else if(AutoMove==enumAutoType.enAT_WaitMove)
//		{
//			AutoMove	=	enumAutoType.enAT_Stop;
//			NavMeshAgent	agent	=	mMainChar.GetComponent<NavMeshAgent>();
//			if(agent!=null){
//				if(!agent.enabled){
//					agent.enabled	=	true;
//				}
//				
//				
//				NavMeshPath path = new NavMeshPath();
//				if(agent.CalculatePath(AutoMoveTarget,path))
//				{
//					StartAutoMove(path.corners);
//					AutoMove	=	enumAutoType.enAT_Move;
//				};
//					
//				
//				agent.enabled	=	false;
//			}
//			
//		}
//		else if(AutoMove==enumAutoType.enAT_Move)
//		{
//			//mc._moveVector	=	v;
//			if(walkpath.Count>0)
//			{
//				Vector3 v	=	walkpath[0]-mc.transform.position;
//				v.y=0;
//				if(Vector2.Distance(v,Vector2.zero)<0.2f)
//				{
//					walkpath.RemoveAt(0);
//				}
//
//				if(walkpath.Count>0)
//				{
//					Vector3 vDir	=	walkpath[0]-mc.transform.position;
//					vDir.y=0;
//					vDir.Normalize();
//					mc._moveVector	=	vDir;
//				}
//				else
//				{
//					StopAutoMove();
//					mc._moveVector	=	Vector3.zero;
//				}
//			}
//		}
	}

//	void StopAutoMove()
//	{
//		AutoMove = enumAutoType.enAT_Stop;
//		walkpath.Clear();
//	}
//
//	void StartAutoMove(Vector3[] navpath)
//	{
//		AutoMove =	enumAutoType.enAT_Move;
//		walkpath.Clear();
//		for(int i=1;i<navpath.Length;i++)
//		{
//			walkpath.Add(navpath[i]);
//		}
//	}

	void	SetDirControl_Pos(float x,float y){
		
		
		Rect r = DirControlBG.pixelInset;
		
		float xx	=	r.width/2 	- 	(float)Screen.width/2 + x;
		float yy	=	r.height/2	-	(float)Screen.height/2+ y;
		
		DirControlBG.pixelInset	=	new Rect(xx,yy,r.width,r.height);
		r	=	DirControlCenter.pixelInset;
		DirControlCenter.pixelInset	=	new Rect(xx+32,yy+32,r.width,r.height);
	}
	void	SetDir(Vector2 vDir){
		Rect rBg 		= DirControlBG.pixelInset;
		Rect rCenter 	= DirControlCenter.pixelInset;
		
		DirControlCenter.pixelInset	=	new Rect(rBg.x+vDir.x+32,rBg.y+vDir.y+32,rCenter.width,rCenter.height);
	}
	public	bool	DirCheck(Vector2 fingerPos,bool bCheck)
	{
		fingerPos	-=new Vector2(Screen.width,Screen.height)*0.5f;
		Rect rBg 		= DirControlBG.pixelInset;
		
		Vector2 center	=	new Vector2(rBg.x+rBg.width/2,rBg.y+rBg.height/2);
		float dis	=	Vector2.Distance(center,fingerPos);
		if(bCheck){
			if(dis>rBg.width/2)
			{
				return false;
			}
		}
		Vector2	offset	=	(fingerPos-center);
		if(dis > 32.0f){
			offset.Normalize();
			offset*=32.0f;
		}
		SetDir(offset);
		return true;

	}	

	// 妫娴嬫槸鍚﹁Е鍙慤Iaa
	bool IsHitUI(Vector2 pos)
	{
		return UICamera.hoveredObject != null;
	}
    
	//
    void OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
		
		if(IsHitUI(fingerPos)){
			return;
		}
		
		// 鏄剧ず蹇?熸妧鑳介潰鏉剧fingerPos浠ュ乏涓嬭?涓哄師鐐鸽UI浠ュ睆骞曚腑蹇冧负鍘熺偣)aa
		if (bUseQuickSkill)
		{
			Rect rectPanel = QuickSkillPanel.pixelInset;
			float newPanelPosX = -rectPanel.width/2 -  (float)Screen.width/2 + fingerPos.x;
			float newPanelPosY = -rectPanel.height/2 - (float)Screen.height/2 + fingerPos.y;
			QuickSkillPanel.pixelInset	= new Rect(newPanelPosX, newPanelPosY, rectPanel.width, rectPanel.height);;	
			QuickSkillPanel.enabled = true;
		}
		
		//if(iDirFingerIndex==-1){
		//	if(DirCheck(fingerPos,true)){
		//		iDirFingerIndex	=	fingerIndex;
		//		StopAutoMove();
		//		return;
		//	}
		//}
		
    }

	// 灏勭嚎涓庝笁瑙掑舰姹備氦aa
	public static bool Ray_Triangle(Ray ray, Vector3 v0, Vector3 v1, Vector3 v2, ref float distance)
	{
		Vector3 edge1 = v1 - v0;
		Vector3 edge2 = v2 - v0;
		Vector3 pvec = Vector3.Cross(ray.direction, edge2);
		float det = Vector3.Dot(edge1, pvec);
		float u, v, t;

		Vector3 tvec;
		if (det > 0)
		{
			tvec = ray.origin - v0;
		}
		else
		{
			tvec = v0 - ray.origin;
			det = -det;
		}

		if (det < 0.0001f)
			return false;

		u = Vector3.Dot(tvec, pvec);
		if (u < 0.0f || u > det)
		{
			return false;
		}
		Vector3 qvec = Vector3.Cross(tvec, edge1);
		v = Vector3.Dot(ray.direction, qvec);
		if (v < 0.0f || u + v > det)
		{
			return false;
		}
		t = Vector3.Dot(edge2, qvec) / det;
		if (t < 0.0f || t > distance)
		{
			return false;
		}
		distance = t;
		return true;
	}

	// 瀵艰埅闈㈢墖灏勭嚎妫娴婄鎷垮埌鎵鏈夌殑涓夎?鍖栫殑瀵艰埅闈㈢墖,閫愪笁瑙掑舰杩涜?灏勭嚎妫娴婅aa
	public static bool NavMesh_RayCast(Ray kRay, ref Vector3 kPoint, float fMax)
	{
		kRay.direction.Normalize();

		Vector3[] akPosition = null;
		int[] aiIndex = null;
		NavMesh.Triangulate(out akPosition, out aiIndex);
		if (akPosition == null || aiIndex == null)
		{
			Debug.Log("NavMesh_RayCast error");
			return false;
		}

		float fMaxDistance = fMax;
		bool bHit = false;
		for (int i = 0; i < aiIndex.Length; i += 3)
		{
			if (Ray_Triangle(kRay, akPosition[aiIndex[i]], akPosition[aiIndex[i + 1]], akPosition[aiIndex[i + 2]], ref fMaxDistance))
			{
				bHit = true;
			}
		}

		if (bHit)
		{
			kPoint = kRay.GetPoint(fMaxDistance);
		}

		return bHit;
	}
	
	//
	void OnFingerUp( int fingerIndex, Vector2 fingerPos, float timeHeldDown )
	{
		// 澶勭悊蹇?熸妧鑳介潰鏉縜a
		if (QuickSkillPanel.enabled == true)
		{
			QuickSkillPanel.enabled = false;
				
			// 鎵嬫寚鏉惧紑浣嶇疆鍧愭爣杞?崲鍒癠I鍧愭爣绯籥a
			float fingerUIPosX = fingerPos.x - (float)Screen.width/2;
			float fingerUIPosY = fingerPos.y - (float)Screen.height/2;
			
			// 鍒ゆ柇鎵嬫寚鏉惧紑浣嶇疆鐨勬妧鑳肩杩欓噷娌℃湁涓ユ牸鍒ゅ畾杈圭晫)aa
			Rect rectPanel = QuickSkillPanel.pixelInset;
			Vector2 centerPanel = rectPanel.center;
			Vector2 fingerUIPos = new Vector2(fingerUIPosX, fingerUIPosY);
			Vector2 moveDelta = fingerUIPos - centerPanel;
			
			float moveDeltaLength = moveDelta.magnitude;	
			Vector2 normalizedMoveDelta = moveDelta.normalized;
			
			int skillID = -1;
			if (moveDeltaLength > 16.0f && moveDeltaLength < 64.0f)
			{
				float dotXValue = Vector2.Dot(Vector2.right, normalizedMoveDelta);
				float dotYValue = Vector2.Dot(Vector2.up, normalizedMoveDelta);
				float threshold  = Mathf.Cos(Mathf.PI * 0.25f);
				if (dotXValue >= threshold)
				{
					skillID = 1002;
				}
				else if (dotXValue <= -threshold)
				{
					skillID = 1004;
				}
				else
				{
					if (dotYValue >= threshold)
					{
						skillID = 1003;
					}
					else 
					{
						skillID = 1005;
					}
				}
			}
			
			// 閲婃斁鎶鑳絘a
			if (skillID != -1)
			{
				mFingerControl.QuickSkill(fingerPos, skillID);
				iFingerIndex = -1;
				return;
			}
		}
		
		if(fingerIndex==iDirFingerIndex)
		{
			if (mMainChar != null)
				mMainChar._moveVector	=	Vector3.zero;
		
			SetDir(Vector2.zero);
			iDirFingerIndex		=-1;
			return;
		}
		
		if(iDirFingerIndex!=-1)
		{
			return;
		}
			
		
		if(fingerIndex==iFingerIndex)
		{
			return;
		}
		
		if(IsHitUI(fingerPos)){
			return;
		}

		mFingerControl.OnFingerUp(fingerPos);

		return;

//		if (mMainChar != null && mMainCamera != null)
//		{
//			Ray ray = mMainCamera.camera.ScreenPointToRay(fingerPos);
//
//			Vector3 point = new Vector3();
//			if(NavMesh_RayCast(ray, ref point, 100000.0f))
//			{
//				
//				NavMeshAgent	agent	=	mMainChar.GetComponent<NavMeshAgent>();
//				if(agent!=null){
//					if(!agent.enabled){
//						agent.enabled	=	true;
//					}
//					/*
//					NavMeshPath path = new NavMeshPath();
//					if(agent.CalculatePath(point,path))
//					{
//						StartAutoMove(path.corners);
//					};
//					*/
//					AutoMoveTarget	=	point;
//					if(!bRoll)
//					{
//						NavMeshPath path = new NavMeshPath();
//						if(agent.CalculatePath(point,path))
//						{
//							StartAutoMove(path.corners);
//						};
//						
//					}
//					else
//					{
//						AutoMove	=	enumAutoType.enAT_WaitMove;
//					}
//					agent.enabled	=	false;
//				}
//				
//			}
//		}
//		
//		//Debug.Log(" OnFingerUp ="  +fingerPos); 
	}
	
	//
	void OnFingerDragBegin( int fingerIndex, Vector2 fingerPos, Vector2 startPos )
    {
        bHasRoll = false;
		if(IsHitUI(fingerPos)){
			return;
		}
		
		if(iDirFingerIndex	==	fingerIndex){
			return;
		}
		
		
		if(iDirFingerIndex!=-1)
		{
			return;
		}
		
		
		if(iFingerIndex==-1)
		{
			vecFingerDir		=	fingerPos;
			iFingerIndex		=	fingerIndex;
			fCurrentTime        = 	0.0f;
		}
   	 	 //Debug.Log("OnFingerDragBegin fingerIndex =" + fingerIndex  + " fingerPos ="+fingerPos +"startPos =" +startPos); 
    }
	
	//
	void OnFingerDragEnd( int fingerIndex, Vector2 fingerPos )
	{
		if(fingerIndex==iFingerIndex)
		{
			iFingerIndex	=	-1;
		}
        if (!bHasRoll)
        {
            if (!IsHitUI(fingerPos))
            {
                mFingerControl.OnFingerUp(fingerPos);
            }
        }
	}
	
	//
	bool DoRoll(int fingerIndex, Vector2 fingerPos)
	{
		if(fingerIndex == iFingerIndex)
		{
			vecFingerDir	-=	fingerPos;
			vecFingerDir.x/=Screen.width;
			vecFingerDir.y/=Screen.height;
			if(Vector2.Distance(vecFingerDir,Vector2.zero)>0.03f){
				
				if(mMainCamera!=null){
					vecFingerDir.Normalize();
					Vector3 vDir 	= 	sdGameLevel.instance.cameraRelativeDistance;
					Vector3 vRight	=	Vector3.Cross(new Vector3(0,1,0),vDir);
					vDir			=	Vector3.Cross(vRight,new Vector3(0,1,0));
					Vector3 v	=	vDir*vecFingerDir.y + vRight*vecFingerDir.x;
					v.y=0.0f;
					v.Normalize();
					mFingerControl.Roll(v);
					return true;
				}
			}	
		}
		return false;
	}

	//
    void OnFingerDragMove( int fingerIndex, Vector2 fingerPos, Vector2 delta )
    {
        
        if(fingerIndex==iDirFingerIndex)
		{
			DirCheck(fingerPos,false);
		}
			
		fCurrentTime += Time.deltaTime;
		if(fCurrentTime > 0.05f)
		{

            if (DoRoll(fingerIndex, fingerPos))
            {
                bHasRoll = true;
                vecFingerDir = fingerPos;
                fCurrentTime = 0.0f;
            }

		}
    }
	
//	void OnFingerSwipe( int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity )
//	{
//		//Debug.Log("OnFingerSwipe " + direction + " with finger " + fingerIndex);
//	}
//		
//	void OnFingerTap( int fingerIndex, Vector2 fingerPos, int tapCount )
//  {	
//		//Debug.Log("OnFingerTap " + tapCount + " times with finger " + fingerIndex);
//	}
//		
//	void OnFingerStationaryBegin( int fingerIndex, Vector2 fingerPos )
//	{
//		 //Debug.Log("OnFingerStationaryBegin " + fingerPos + " times with finger " + fingerIndex);
//	}	
//	
//	void OnFingerStationary( int fingerIndex, Vector2 fingerPos, float elapsedTime )
//	{
//		 //Debug.Log("OnFingerStationary " + fingerPos + " times with finger " + fingerIndex);	
//	}
//	
//	void OnFingerStationaryEnd( int fingerIndex, Vector2 fingerPos, float elapsedTime )
//	{	
//		 //Debug.Log("OnFingerStationaryEnd " + fingerPos + " times with finger " + fingerIndex);
//	}
//	
//	void OnFingerLongPress( int fingerIndex, Vector2 fingerPos )
//	{
//		//Debug.Log("OnFingerLongPress " + fingerPos );
//	}
//
//  Vector3 GetWorldPos(Vector2 screenPos)
//  {
//      Camera mainCamera = Camera.main;
//      return mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Vector3.Distance(transform.position,mainCamera.transform.position))); 
//  }
}
