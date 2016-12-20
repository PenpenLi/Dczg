using UnityEngine;
using System.Collections;

[AddComponentMenu("DsMobile/sdGameCamera")]
public class sdGameCamera : MonoBehaviour
{
	// Use this for initialization
	//public 	GameObject 	cameraObject 		= 	null;
	public 	bool 		enableCameraShake 	= 	false;
	//bool		enableZoomIn		=	false;
	float		_OrginZoomIn			=	1.0f;
    public float OrginZommIn
    {
        get { return _OrginZoomIn; }
        set { _OrginZoomIn = value; }
    }
	float		ZoomIn				=	1.0f;
	float		ZoomInTime			=	0.3f;
	float		CurrentZoomIn		=	1.0f;
	float  		CurrentZoomInTime	=	0.0f;
	
	
	
	//< by runtianlong
	private Vector3 	shakeDirection;			//相机振动方向
	private float 		shakeDuration;			//振动时间
	private float 		shakeFrequency;			//震动频率
	private float 		shakeAmplitude;			//震动衰减幅度
	
	sdGameMonster		boss;
	float				VectoryTime	=	0.0f;
	Quaternion			oldrot		=	Quaternion.identity;
	bool main						=	true;
	float				EndTime		=	5.0f;
    bool                bVectory    = false;
  
    //相机移动aaa
	bool                bCameraMove = false;
	Quaternion          fromtrotate;
	Quaternion          torotate;
	Vector3             frompos;
	Vector3             topos;
	float 				fZoomTime;
	float               fTime;

    int iVectoryPhase = 0;

	//相机是否是跟随状态
	protected bool   mMainCharFollow = true;
	public bool MainCharFollow
	{
		set {mMainCharFollow = value;}
		get {return mMainCharFollow;}
	}
	
	public  void   SetZoomData(Quaternion fromRotate, Quaternion toRotate, Vector3 fromPos, Vector3 toPos, float ftime)
	{
		fromtrotate = fromRotate;
		torotate = toRotate;
		frompos = fromPos;
		topos = toPos;
		fZoomTime = ftime;
		fTime = 0.0f;
		bCameraMove = true;
	}
	

	public Camera getUnityComponent()
	{
		return GetComponent<Camera>();	
	}
		
	public void tick()
	{
		if(mMainCharFollow == false)
			return;
        UpDateZoom();
		sdMainChar	mc	=	sdGameLevel.instance.mainChar;
        if (bVectory)
		{
            UpdateVectory();
			return;
		}
        if (CurrentZoomInTime > -0.00001f)
        {
            CurrentZoomInTime += Time.deltaTime;
        }
		if(CurrentZoomInTime	>	ZoomInTime)
		{
			EndZoomIn();
		}
		CurrentZoomIn	+=	(ZoomIn	-	CurrentZoomIn)*Time.deltaTime/0.25f;		
		transform.localPosition = sdGameLevel.instance.cameraRelativeDistance*CurrentZoomIn*_OrginZoomIn + mc.transform.localPosition;
		if(enableCameraShake)
		{
			float w = 1.0f - shakeAmplitude*shakeDuration;
			if(w <= 0)
			{
				enableCameraShake 		= false;
			}
			else
			{
				Vector3 cmove 			= shakeDirection * Mathf.Sin(shakeDuration*shakeFrequency)*w;
				shakeDuration 			+= Time.deltaTime;
				transform.position 		+= cmove;
			}
		}
	}	
	
	
	public void addRandomCameraShake(float minPower, float maxPower, float w, float attenuation)
	{
		Vector3 dir = new Vector3(Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f));
		float w1 = Random.Range(minPower,maxPower);
		dir.Normalize();
		dir *= w1;
		startCameraShake(dir, w, attenuation);
	}
	
	//<
	protected void startCameraShake(Vector3 dir, float w, float attanuation)
	{
		enableCameraShake 	= true;
		shakeDirection		= dir;
		shakeFrequency 		= w;
		shakeAmplitude 		= attanuation;
		shakeDuration 		= 0.0f;
	}
	public	void	BeginZoomIn(float fZoomIn,float fTime)
	{
		ZoomIn			=	fZoomIn;
		ZoomInTime		=	fTime;
		CurrentZoomInTime	=	0.0f;
	}
	void	EndZoomIn()
	{
		ZoomIn			=	1.0f;
		ZoomInTime		=	0.0f;
		CurrentZoomInTime	=	-1.0f;
		
	}

	// 初始化胜利动画aa
	public void InitVictory(sdGameMonster obj)
	{
		oldrot			=	GetComponent<Camera>().transform.localRotation;
		Time.timeScale	=	0.2f;
		boss	=	obj;
		VectoryTime	=	0.0f;
        bVectory = true;
	}

	// 展示胜利动画aa
	public void ShowVictory()
	{
		Debug.Log("ShowVictory!");
		StartCoroutine(VectoryPhase());
	}

    void SetZoomEnable(bool bEnable)
    {
        GameObject obj = GameObject.Find("btn_anglemode");
        if (obj)
        {
            BoxCollider collider = obj.GetComponent<BoxCollider>();
            if (collider)
                collider.enabled = bEnable;
        }
    }
	public	void	ChangeCameraDistance()
	{
		main=!main;
        SetZoomEnable(false);
		if(main)
		{
            Quaternion rotate = transform.rotation * Quaternion.AngleAxis(11, new Vector3(1, 0, 0));
            SetZoomData(transform.rotation, rotate, sdGameLevel.instance.cameraRelativeDistance, sdGameLevel.instance.cameraRelativeDistance - rotate * new Vector3(0, -1, 4f), 0.5f);
			//transform.rotation	*=	Quaternion.AngleAxis(11,new Vector3(1,0,0));
			//sdGameLevel.instance.cameraRelativeDistance	-=	transform.rotation*new Vector3(0,-1,4f);			
		}
		else
		{
            Quaternion rotate = transform.rotation * Quaternion.AngleAxis(-11, new Vector3(1, 0, 0));
            SetZoomData(transform.rotation, rotate, sdGameLevel.instance.cameraRelativeDistance, sdGameLevel.instance.cameraRelativeDistance + transform.rotation * new Vector3(0, -1, 4f), 0.5f);
			//sdGameLevel.instance.cameraRelativeDistance	+=	transform.rotation*new Vector3(0,-1,4f);
			//transform.rotation	*=	Quaternion.AngleAxis(-11,new Vector3(1,0,0));
		}
	}

    void UpDateZoom()
    {
        if (bCameraMove)
        {
            if (fTime > fZoomTime)
            {
                transform.rotation = torotate;
                sdGameLevel.instance.cameraRelativeDistance = topos;
                SetZoomEnable(true);
                bCameraMove = false;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(fromtrotate, torotate, fTime / fZoomTime);
                sdGameLevel.instance.cameraRelativeDistance = frompos + (topos - frompos) * fTime / fZoomTime;
                fTime += Time.deltaTime;
            }
        }
    }

    protected IEnumerator VectoryPhase()
    {
        iVectoryPhase = 0;

        sdMainChar mc = sdGameLevel.instance.mainChar;

        float BossDieTime = 1.0f;
        if (boss)
        {
            AnimationState die = boss.AnimController["death01"];
            if (die != null)
            {
                BossDieTime = die.length * 0.8f;
                sdUICharacter.Instance.LoadJiesuanWnd();
            }
            yield return new WaitForSeconds(BossDieTime);
        }
        Time.timeScale = 1.0f;
        iVectoryPhase = 1;

        Vector3 vRelVec = sdGameLevel.instance.cameraRelativeDistance;
        vRelVec.y = 0.0f;
        GameObject winArea = GameObject.Find("@WinArea");
        if (winArea != null)
            mc.transform.position = winArea.transform.position;
        mc.spinToTargetDirection(vRelVec.normalized, true);

        //Stop Pet
        sdActorInterface pet = sdGameLevel.instance.mainChar.Retainer;
        if (pet != null)
        {
            if (pet.IsActive() && pet.GetCurrentHP() > 0)
            {
                sdGameMonster m = (sdGameMonster)pet;
                m.useAI = false;

                Vector3 vmc = mc.transform.position;
                Vector3 dir = mc.GetDirection();
                Vector3 right = Vector3.Cross(new Vector3(0, 1, 0), dir);
                Vector3 pos = vmc + right * 1.25f - dir * 0.5f;
                m.transform.position = pos;
                m.spinToTargetDirection(dir, true);
                m.DeactiveHPUI();
                AnimationState ani = m.AnimController["cheer01"];
                if (ani != null)
                {
                    ani.layer = 11;
                    //ani.wrapMode = WrapMode.;
                    m.AnimController.Play("cheer01", PlayMode.StopAll);
                }
            }
        }

		// preload effect prefab
		ResLoadParams param = new ResLoadParams();
		if ((int)mc.GetJob() == 1)
		{
			sdResourceMgr.Instance.LoadResource("Effect/MainChar/$Warrior/Fx_Warrior_Begin_Show_001.prefab",OnLoadEffect,param);
		}
		else if ((int)mc.GetJob() == 4)
		{
			sdResourceMgr.Instance.LoadResource("Effect/MainChar/$Mage/Fx_Mage_Begin_Show_001.prefab",OnLoadEffect,param);
			sdResourceMgr.Instance.LoadResource("Effect/MainChar/$Mage/Fx_Mage_Begin_Show_002.prefab",OnLoadEffect,param);
		}
		else if ((int)mc.GetJob() == 7)
		{
			sdResourceMgr.Instance.LoadResource("Effect/MainChar/$Ranger/ranger_begin_show.prefab",OnLoadEffect,param);
		}
		else if ((int)mc.GetJob() == 10)
		{
			sdResourceMgr.Instance.LoadResource("Effect/MainChar/$Priest/Fx_Priest_Begin_Show_001.prefab",OnLoadEffect,param);
			sdResourceMgr.Instance.LoadResource("Effect/MainChar/$Priest/Fx_Priest_Begin_Show_002.prefab",OnLoadEffect,param);
		}

		yield return new WaitForSeconds(0.5f);	// load waiting time
		
		Animation anim = mc.AnimController;
		AnimationState state = anim["begin_show"];
        state.layer = 11;
        state.wrapMode = WrapMode.Once;
        anim.Play("begin_show");
        //anim.CrossFadeQueued("idle01");
		float FinishWaitTime = 0.15f;
        if ((int)mc.GetJob() == 1)
        {
            sdActorInterface.AddHitEffect("Effect/MainChar/$Warrior/Fx_Warrior_Begin_Show_001.prefab", mc.transform, 3.0f, Vector3.zero, true);
        }
        else if ((int)mc.GetJob() == 4)
        {
            sdActorInterface.AddHitEffect("Effect/MainChar/$Mage/Fx_Mage_Begin_Show_001.prefab", mc.transform, 3.0f, Vector3.zero, true);
            mc.attachEffect("Effect/MainChar/$Mage/Fx_Mage_Begin_Show_002.prefab", "dummy_right_weapon_at", Vector3.zero, Quaternion.identity, 1.0f, 3.0f);
			FinishWaitTime = 0.3f;

        }
        else if ((int)mc.GetJob() == 7)
        {
            sdActorInterface.AddHitEffect("Effect/MainChar/$Ranger/ranger_begin_show.prefab", mc.transform, 3.0f, Vector3.zero, true);
        }
        else if ((int)mc.GetJob() == 10)
        {
            sdActorInterface.AddHitEffect("Effect/MainChar/$Priest/Fx_Priest_Begin_Show_001.prefab", mc.transform, 3.0f, Vector3.zero, true);
            mc.attachEffect("Effect/MainChar/$Priest/Fx_Priest_Begin_Show_002.prefab", "dummy_right_weapon_at", Vector3.zero, Quaternion.identity, 1.0f, 3.0f);
        }

        yield return new WaitForSeconds(state.length);
		
        yield return new WaitForSeconds(FinishWaitTime);	// finish waiting time

		if (sdUICharacter.Instance.GetBattleType()==(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_WORLD_BOSS)
		{

		}
		else
		{
			Debug.Log("ShowJiesuanWnd!");
			sdUICharacter.Instance.ShowJiesuanWnd();	//< 显示结算窗口aa
		}
    }
    void UpdateVectory()
    {
        sdMainChar mc = sdGameLevel.instance.mainChar;

        if (iVectoryPhase == 0)
        {
            if (boss)
            {
                Vector3 vRelVec = sdGameLevel.instance.cameraRelativeDistance;
                Vector3 v = boss.transform.position + vRelVec;
                transform.position = v;
                mc._moveVector = Vector3.zero;
            }
        }
        else if (iVectoryPhase == 1)
        {
            Quaternion qRot = sdGameLevel.instance.mainChar.transform.rotation ;
            Vector3 relative = qRot * new Vector3(0, 0, 3.0f);
            transform.localRotation = qRot * Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
            Vector3 v = mc.transform.position + new Vector3(0, 0.7f, 0) + relative;
            transform.localPosition = v;
        }
    }
	void OnLoadEffect(ResLoadParams param,Object obj)
	{
	}

    
}
