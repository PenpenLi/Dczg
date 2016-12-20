using UnityEngine;

public class sdMovieScene : sdMovieBase
{
	int     mnStage = 0;         		//褰撳墠鐩告満闃舵?aa 0 缁撴潫  1 鎷夎繎 2 鍋滅暀 3.鎷夎繙aa
	Camera  mMainCamera = null;   		//鐩告満aa
	Camera  mUICamera = null;           //ui鐩告満aa
	sdLevelArea levelArea = null;
	GameObject  mBoss = null;
	GameObject  bossDialogue = null;
	UISprite  mask = null;
	public string     mbornAnimNext = null;
    public string     mbornAudio = null;
	public string  mStrDialogue = null;
	public float enterTime = 1.0f;
    float bornAnimTime = 0.0f;   //鍑虹敓鍔ㄤ綔杩囬暱锛屾病鏈夌粰寮鏀剧瓥鍒掗厤缃產aaa
	public float stayTime = 3.0f;
    public float afterStayTime = 0.5f;  //榛戝箷娣″叆鏃堕棿aa
	public float leaveTime = 1.0f;      //榛戝箷娣″嚭鏃堕棿aa
	public float fCameraDis = 5.0f;
	Vector3 cameraSavePos = Vector3.zero;              //淇濆瓨鐨勭浉鏈轰綅缃產a
	Quaternion cameraSaveRotate = Quaternion.identity; //淇濆瓨鐨勭浉鏈烘柟鍚慳a
	Quaternion bossSaveRotate = Quaternion.identity;   //淇濆瓨鐨刡oss鏂瑰悜aa
	bool bPlayShowAnimation = false;
    bool bReSetCamera = true;  //
	sdGameMonster  gameMonster = null;
	float m_fTime = 0.0f;
    long m_StartTime = 0;
	public void SetTarget(GameObject obj, sdLevelArea area)
	{
        sdUICharacter.Instance.bBossMovie = true;
        m_StartTime = System.DateTime.Now.Ticks / 10000000L;
		mBoss = obj;
		bossSaveRotate = mBoss.transform.rotation;
		levelArea = area;
		mMainCamera = sdGameLevel.instance.mainCamera.GetComponent<Camera>();
		cameraSavePos = mMainCamera.gameObject.transform.position;
		cameraSaveRotate = mMainCamera.gameObject.transform.rotation;
		mUICamera = sdGameLevel.instance.UICamera;
        SetTargetEffectVisible(false);
        sdGameLevel.instance.SetFingerObjectActive(false);        
        
		HeaderProto.ECreatureActionState[]  state = new HeaderProto.ECreatureActionState[2];
        state[0] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_IDLE;
		state[1] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL;
		levelArea.SetMonsterStatus(state, true);
        sdActorInterface activePet = sdGameLevel.instance.mainChar.Retainer;
		for(int i = 0; i < state.Length; i++)
		{            
            if (activePet != null)
                activePet.AddDebuffState(state[i]);
            sdGameLevel.instance.mainChar.AddDebuffState(state[i]);
		}
        if (activePet != null &&  activePet.actorType == ActorType.AT_Pet)
        {
            ((sdGameMonster)activePet).DeactiveHPUI();
        }

		ResLoadParams param = new ResLoadParams();
		sdResourceMgr.Instance.LoadResource("UI/$Movie/bossdialogue.prefab", LoadBossDialogue, param);
        //榛戝睆杩涘叆aa
		Quaternion cameraRotate = mMainCamera.gameObject.transform.rotation;
		cameraRotate = cameraRotate*Quaternion.AngleAxis(180.0f, new Vector3(0,1,0));
		Vector3 direction = cameraRotate*Vector3.forward;
		direction.y = 0.0f;
        direction.Normalize();
		Quaternion bossRotate = Quaternion.identity;		
		if(direction.z<-0.9999f)
			bossRotate	=	Quaternion.AngleAxis(180.0f,new Vector3(0,1,0));
		else
			bossRotate = Quaternion.FromToRotation(Vector3.forward,direction);
		mBoss.transform.rotation = bossRotate;
		sdGameLevel.instance.mainCamera.MainCharFollow = false;
		if(mbornAnimNext != null && mbornAnimNext.Length > 0)
		{
			bPlayShowAnimation = true;
			gameMonster = mBoss.GetComponent<sdGameMonster>();
		}
	}

    void SetTargetEffectVisible(bool bVisible)
    {
        float fScale = (bVisible ? 1.5f : 0.0f);
        if (sdGameLevel.instance.mainChar != null)
        {
            if(sdGameLevel.instance.mainChar.TargetEffectTarget != null)
                sdGameLevel.instance.mainChar.TargetEffectTarget.Movie = !bVisible;
        }
        sdActorInterface activePet = sdGameLevel.instance.mainChar.Retainer;
        if (activePet != null && activePet.actorType == ActorType.AT_Pet)
        {
            if (((sdGameMonster)activePet).TargetEffectTarget != null)
                ((sdGameMonster)activePet).TargetEffectTarget.Movie = !bVisible;
        }
     }
	void SetBossText()
	{
		GameObject dialogue = GameObject.Find("LabelBossDialogue");
		if(dialogue != null)
		{
			UILabel m_labelDialogue = dialogue.GetComponent<UILabel>();
			m_labelDialogue.text = mStrDialogue;
		}
	}
	void LoadBossDialogue(ResLoadParams kParam, Object kObj)
	{
		bossDialogue = GameObject.Instantiate(kObj) as GameObject;
		bossDialogue.transform.localPosition = Vector3.zero;
		bossDialogue.transform.localRotation = Quaternion.identity;
		bossDialogue.transform.localScale = Vector3.one;
		if(mStrDialogue == null || mStrDialogue.Length == 0)
		{
			bossDialogue.transform.FindChild("LabelBossDialogue").gameObject.SetActive(false);
			bossDialogue.transform.FindChild("Sprite_bossimage").gameObject.SetActive(false);
		}
		mask = bossDialogue.transform.FindChild("Sprite_bossmask").gameObject.GetComponent<UISprite>();
		SetBossText();
		bossDialogue.transform.parent = sdGameLevel.instance.UICamera.transform;

		Quaternion bossRotate = mBoss.transform.rotation;
		Vector3 cameraPos = mBoss.transform.position + bossRotate*new Vector3(0,0,fCameraDis);
		cameraPos += new Vector3(0.0f,1.0f,0.0f);
		
		bossRotate = bossRotate*Quaternion.AngleAxis(180.0f, new Vector3(0,1,0));
		mMainCamera.transform.position = cameraPos;
		mMainCamera.transform.rotation = bossRotate;

		m_fTime = 1.25f*enterTime;
        sdUICharacter.Instance.HideFightUi();
		mnStage = 1;
	}
	protected override void Update()
	{
		if(mnStage == 0)
			return;
		if(mnStage == 1) //榛戝睆aa
		{
			if(m_fTime <= float.Epsilon)
			{
				mnStage = 2;
                m_fTime = bornAnimTime;
			}
			else
			{
				if(m_fTime > enterTime)
					mask.color = new Color(0.0f,0.0f,0.0f,1.0f);
				else
					mask.color = new Color(0.0f,0.0f,0.0f,1.0f*m_fTime*m_fTime/enterTime/enterTime);
				m_fTime -= Time.deltaTime;
			}
		}
        else if (mnStage == 2) //鎾?斁鍑虹敓鍔ㄤ綔aa
        {
            if (m_fTime < float.Epsilon)
            {
                if (bPlayShowAnimation && gameMonster != null)//boss鎾?斁灞曠ず鍔ㄧ敾aa
                {
                    AnimationState aniState = gameMonster.AnimController[mbornAnimNext];
                    if (aniState != null)
                    {
                        aniState.wrapMode = WrapMode.Once;
                        aniState.layer = 10;
                        gameMonster.AnimController.CrossFade(mbornAnimNext, 0.15f);
                    }
                    if (mbornAudio != null && mbornAudio.Length > 0)
                        gameMonster.PlayAudio(mbornAudio);
                    bPlayShowAnimation = false;
                }
                m_fTime = stayTime;
                mnStage = 3;
            }
            else
                m_fTime -= Time.deltaTime;
        }
        else if (mnStage == 3)
        {
            if (m_fTime <= float.Epsilon)
            {
                //mBoss.transform.rotation = bossSaveRotate;
                //m_fTime = 1.25f*leaveTime;
                m_fTime = 0.0f;
                mnStage = 4;
            }
            else
            {
                m_fTime -= Time.deltaTime;
            }
        }
        else if (mnStage == 4)//榛戝睆娣″叆aa
        {
            if (m_fTime > afterStayTime)
            {
                mBoss.transform.rotation = bossSaveRotate;
                m_fTime = leaveTime;
                mnStage = 5;
            }
            else
            {
                m_fTime += Time.deltaTime;
                mask.color = new Color(0.0f, 0.0f, 0.0f, 1.0f * m_fTime * m_fTime / afterStayTime / afterStayTime);
            }
        }
        else if (mnStage == 5)//榛戝睆娣″嚭aa
        {
            if (m_fTime <= float.Epsilon)
            {
                if (bossDialogue != null)
                    bossDialogue.SetActive(false);
                SetTargetEffectVisible(true);
                sdUICharacter.Instance.bBossMovie = false;
                GameObject fightUI = sdUICharacter.Instance.GetFightUi();
                if (fightUI != null)
                {
                    sdFightUi fight = fightUI.GetComponent<sdFightUi>();
                    if (fight != null)
                        fight.AddComboTime(System.DateTime.Now.Ticks / 10000000L - m_StartTime);
                }
                mnStage = 0;
            }
            else
            {
                mask.color = new Color(0.0f, 0.0f, 0.0f, 1.0f * m_fTime * m_fTime / leaveTime / leaveTime);
                if (bReSetCamera)
                {
                    bReSetCamera = false;
                    mMainCamera.transform.position = cameraSavePos;
                    mMainCamera.transform.rotation = cameraSaveRotate;
                    HeaderProto.ECreatureActionState[] state = new HeaderProto.ECreatureActionState[2];
                    state[0] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_IDLE;
                    state[1] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL;
                    levelArea.SetMonsterStatus(state, false);
                    sdActorInterface activePet = sdGameLevel.instance.mainChar.Retainer;
                    for (int i = 0; i < state.Length; i++)
                    {
                        if (activePet != null)
                            activePet.RemoveDebuffState(state[i]);
                        sdGameLevel.instance.mainChar.RemoveDebuffState(state[i]);
                    }
                    sdGameLevel.instance.mainCamera.MainCharFollow = true;
                    sdGameLevel.instance.SetFingerObjectActive(true);
                    if (activePet != null && activePet.actorType == ActorType.AT_Pet)
                    {
                        ((sdGameMonster)activePet).ActiveHPUI();
                    }
                    sdUICharacter.Instance.ShowFightUi();
                }

                m_fTime -= Time.deltaTime;
            }
        }
	}
}