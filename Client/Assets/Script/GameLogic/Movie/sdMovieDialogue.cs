using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class sdMovieDialogue : sdMovieBase
{ 	
	bool m_bGuide = false;
	int m_nDialogueIndex; 
	Camera m_mainCamera;
	Camera m_uiCamera;
	GameObject m_dialogueUI = null;
	bool m_bEnd = false;	
	UILabel m_labelName = null;
	UILabel m_labelDialogue = null;
	GameObject m_nameImage = null;
	GameObject m_texImage = null;
	GameObject m_headImage = null;
    GameObject m_btnBlcok = null;
	float enterTime = 1.0f;
    float beforeLeaveTime = 1.0f;
	float leaveTime = 1.0f;
	float m_fTime = 0.0f;
	float m_fNormal = -1.0f;
	int m_nStage = 0; //1 榛戝睆 2 澶村儚杩涘叆  3 瀵硅瘽妗嗚繘鍏み 4 鏄剧ず鏂囧瓧  5 鍙?互鐐瑰嚮  6.榛戝睆娣″叆 7 榛戝睆娣″嚭aaa
	Quaternion  m_cameraSaveRotate;  //淇濆瓨鍘熷?鐨勭浉鏈轰俊鎭痑a
	Vector3 m_cameraSavePosition;
	stStageMovie stageMovie;
	UISprite  m_mask = null;  //閬?僵aaa

	GameObject m_btnContinue; 

	//鍥剧墖鏄剧ず鎺у埗aaa
	float imageMove = 1280.0f; //鍥剧墖绉诲姩鐨勯熷害aaa
	float imagePosX = 320.0f;
	float imageCurPosX = -320.0f;


	//瀵硅瘽妗嗚儗鏅?帶鍒禷a
	float textDialogueMove = 2100.0f;
	float textDialoguePosX = -500.0f;
	float textDialogueCurPosX = 550.0f;

	//鏂囧瓧鏄剧ず鎺у埗aa	
	float textNormal = 0.1f;//鏂囧瓧鏄剧ず姝ｅ父閫熷害 澶氬皯绉掑嚭鐜颁竴涓?瓧aaa 
	float textSpeedup = 0.02f;//鏂囧瓧鍔犲揩閫熷害aaa
	string  textContent = null;
	int nTextCount = 0;
	int nTextIndex = 0;

    bool bCameraposChange = false;

    public bool IsWndActive()
    {
        if (m_dialogueUI != null && m_dialogueUI.active)
        {
            return true;
        }

        return false;
    }

	void SetCameraActive(bool bActive)
	{
		m_mainCamera.gameObject.SetActive(bActive);
		m_uiCamera.gameObject.SetActive(bActive);
	}

	void SetTime(float fTime)
	{
		m_fTime = fTime;
	}

	public void SetMovieInfo(int stageID, bool bHideFightUI, bool bGuide, Vector3 scale, Vector3 pos)
	{
        sdGlobalDatabase.Instance.globalData["moviedialogue"] = true;
		m_nStage = 0;
		m_bGuide = bGuide;
		Hashtable table = sdConfDataMgr.Instance().m_Movie;
		if(table.ContainsKey(stageID))
			stageMovie = (stStageMovie)table[stageID];
		m_nDialogueIndex = 0;
		m_mainCamera = sdGameLevel.instance.mainCamera.GetComponent<Camera>();
		m_uiCamera = sdGameLevel.instance.UICamera;
		m_cameraSaveRotate = m_mainCamera.transform.rotation;
		m_cameraSavePosition = m_mainCamera.transform.position;
		BuffChange(true);
		if(m_dialogueUI == null)
		{
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = scale;
			param.userdata1 = pos;
			sdResourceMgr.Instance.LoadResourceImmediately("UI/$Movie/moviedialogue.prefab", LoadDialogue, param);
            if (sdUICharacter.Instance.GetTownUI() != null)
            {
                sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(true);
            }
		}
		else
		{
			m_labelName.text = "";
			m_labelDialogue.text = "";
			ResetHeadImagePos();
			Init();
		}
		sdGameLevel.instance.SetFingerObjectActive(false);
		if(bHideFightUI)
			sdUICharacter.Instance.HideFightUi();
		else
			sdUICharacter.Instance.ShowFightUi();
        if (sdGameLevel.instance.tuiTuLogic)
		    sdGameLevel.instance.tuiTuLogic.bFightInitVisible = !bHideFightUI;
		m_nDialogueIndex = 0;
		if(bGuide)
		{
			enterTime = 0.0f;
			leaveTime = 0.1f;
		}
		m_bEnd = false;
		SetTime(1.25f*enterTime);
		m_nStage = 1;
        bCameraposChange = true;
	}
		

	void ResetHeadImagePos()
	{
		UIAnchor anchor = m_headImage.GetComponent<UIAnchor>();
		if(anchor != null)
			anchor.pixelOffset.x = imageCurPosX;
	}

	protected override void Update()
	{
		//1 榛戝睆 2 澶村儚杩涘叆  3 瀵硅瘽妗嗚繘鍏み 4 鏄剧ず鏂囧瓧  5 鍙?互鐐瑰嚮  6 榛戝睆aa
		if(m_nStage == 0)
			return;
		if(m_nStage == 1)//1 榛戝睆aa
		{
			if(m_mask == null)
				return;
			if(m_fTime < float.Epsilon)
			{
				m_mask.gameObject.SetActive(false);
				UIAnchor anchor = m_headImage.GetComponent<UIAnchor>();
				if(anchor != null)
                    anchor.pixelOffset.x = imageCurPosX;
				m_dialogueUI.SetActive(true);
				m_headImage.SetActive(true);
				SetHeadImage();
				m_nStage = 2;
			}
			else
			{
				if(m_fTime > enterTime)
					m_mask.color = new Color(0.0f, 0.0f,0.0f, 1.0f);
				else
					m_mask.color = new Color(0.0f, 0.0f,0.0f, m_fTime*m_fTime/enterTime/enterTime);
                m_fTime -= Time.deltaTime;
                if (bCameraposChange)
                {
                    SetCameraInfo(m_nDialogueIndex);
                    bCameraposChange = false;
                }
			}
		}
		else if(m_nStage == 2)//2 澶村儚杩涘叆aa
		{
			UIAnchor anchor = m_headImage.GetComponent<UIAnchor>();
			if(anchor != null)
			{
				anchor.pixelOffset.x += Time.deltaTime*imageMove;
                if (anchor.pixelOffset.x > imagePosX)
				{
                    anchor.pixelOffset.x = imagePosX;
					anchor = m_texImage.GetComponent<UIAnchor>();
					if(anchor != null)
                        anchor.pixelOffset.x = textDialogueCurPosX;
					m_texImage.gameObject.SetActive(true);
					m_btnContinue.SetActive(false);
					m_nStage = 3;
				}
			}
		}
		else if(m_nStage == 3)//3 瀵硅瘽妗嗚繘鍏みaa
		{
			if(textDialogueCurPosX > textDialoguePosX)
			{
                UIAnchor anchor = m_texImage.GetComponent<UIAnchor>();
				if(anchor != null)
				{
                    anchor.pixelOffset.x -= Time.deltaTime * textDialogueMove;
                    if (anchor.pixelOffset.x < textDialoguePosX)
					{
                        anchor.pixelOffset.x = textDialoguePosX;
						m_nDialogueIndex = 0;
						ShowTextBegin();
					}
				}
			}
		}
		else if(m_nStage == 4)//4 鏄剧ず鏂囧瓧aa
		{
			bool bFinished = false;
			m_fTime += Time.deltaTime;
			nTextIndex = 0;
			if(m_fNormal > float.Epsilon)
				nTextIndex = (int)(m_fNormal/textNormal + (m_fTime - m_fNormal)/textSpeedup);
			else
				nTextIndex = (int)(m_fTime/textNormal);
			if(nTextIndex > nTextCount)
			{
				nTextIndex = nTextCount;
				bFinished = true;
			}
			m_labelDialogue.text = textContent.Substring(0, nTextIndex);
			if(bFinished)
			{
				m_btnContinue.SetActive(true);
				m_nStage = 5;
			}
			if(Input.anyKeyDown)
			{
				if(m_fNormal < float.Epsilon)
					m_fNormal = m_fTime;
			}
		}
		else if(m_nStage == 5)//5 鍙?互鐐瑰嚮 aa
		{
			if(Input.anyKeyDown)
				Continue();	
		}
        else if (m_nStage == 6) //榛戝睆娣″叆aaa
        {
            if (m_bGuide)
            {
                BeforeLeave();
                return;
            }
            if (m_fTime > beforeLeaveTime)
            {
                BeforeLeave();
            }
            else
            {
                m_mask.color = new Color(0.0f, 0.0f, 0.0f, m_fTime * m_fTime / beforeLeaveTime / beforeLeaveTime);
                m_labelDialogue.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - m_fTime * m_fTime / beforeLeaveTime / beforeLeaveTime);
                if(m_mask.gameObject.activeSelf == false)
                    m_mask.gameObject.SetActive(true);
                m_fTime += Time.deltaTime;
            }
        }
        else if (m_nStage == 7)//榛戝睆娣″嚭aaa
        {
            if (m_fTime < float.Epsilon)
            {
                if (m_bGuide)
                {
                    m_btnBlcok.transform.localPosition = new Vector3(99999.0f, 0.0f, 0.0f);
                    m_dialogueUI.SetActive(false);
                    sdGameLevel.instance.GuideDialogueNotify();
                    m_nStage = 0;
                }
                else
                {
                    m_nStage = 8;
                    m_fTime = 0.5f;
                }
            }
            else
            {
                if (m_bGuide == false)
                {
                    m_mask.color = new Color(0.0f, 0.0f, 0.0f, m_fTime * m_fTime / leaveTime / leaveTime);
                }
                m_fTime -= Time.deltaTime;
            }
        }
        else if (m_nStage == 8) //鍓ф儏鍔ㄧ敾缁撴潫鍚嶐绉掔偣鍑绘棤鏁囘 鏂版墜寮曞?闄ゅ?aaaa
        {
            if (m_fTime < float.Epsilon)
            {
                m_btnBlcok.transform.localPosition = new Vector3(99999.0f, 0.0f, 0.0f);
                sdGameLevel.instance.SetFingerObjectActive(true);
                m_dialogueUI.SetActive(false);
                sdGameLevel.instance.GuideDialogueNotify();
                m_nStage = 0;
            }
            m_fTime -= Time.deltaTime;
        }
	}


    void BeforeLeave()
    {
        sdGlobalDatabase.Instance.globalData["moviedialogue"] = false;
        BuffChange(false);
        sdGameCamera gameCamera = sdGameLevel.instance.mainCamera;
        List<stMovie> movieList = stageMovie.movieList;
        if (m_nDialogueIndex < movieList.Count)
        {
            stMovie movieData = movieList[m_nDialogueIndex];
            if (movieData.npcNoShow == 1)
            {
                GameObject obj = GameObject.Find(movieData.npcModel);
                if (obj != null)
                    obj.SetActive(false);
            }
        }
        sdUICharacter.Instance.DialogueCharacterList.Clear();
        sdGameLevel.instance.mainCamera.MainCharFollow = true;

        m_mainCamera.transform.position = m_cameraSavePosition;
        m_mainCamera.transform.rotation = m_cameraSaveRotate;

        HideUI(m_headImage);
        HideUI(m_texImage);

        if(m_bGuide)
            sdGameLevel.instance.SetFingerObjectActive(true);
        sdUICharacter.Instance.ShowFightUi();
        SetTime(leaveTime);
        m_nStage = 7;
    }
	void ShowTextBegin()
	{
		List<stMovie> movielist = stageMovie.movieList;
		if(m_nDialogueIndex < movielist.Count)
		{
			stMovie movieData = movielist[m_nDialogueIndex];
			m_labelName.text = movieData.npcName;
			m_bEnd = (movieData.EndFlag == 1);
			nTextCount = movieData.content.Length;
			textContent = movieData.content;
			m_btnContinue.SetActive(false);
			nTextIndex = 0;
			m_fTime = 0.0f;
			m_fNormal = -1.0f;
			m_nStage = 4;
		}
		else
		{
			m_bEnd = true;
			nTextCount = 0;
			nTextIndex = 0;
		}
	}

	void SetHeadImage()
	{
		stMovie movieData = stageMovie.movieList[m_nDialogueIndex];
		string index = movieData.portrait;
		index = index.Substring(0, 1);
		string newName = "NpcPortrait_" + index;
		UISprite sprite = m_headImage.GetComponent<UISprite>();
		UIAtlas atlas =  sprite.atlas;
		if(newName != atlas.name)
		{
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = movieData.portrait;
			sdResourceMgr.Instance.LoadResourceImmediately("UI/NpcPortrait/$NpcPortrait" + index + "/NpcPortraitPic_" + index +".prefab" , loadAtlas, param, typeof(UIAtlas));
		}
		else
		{
			if(sprite.spriteName != movieData.portrait)
				sprite.spriteName = movieData.portrait;
		}
	}
	
	void LoadDialogue(ResLoadParams kParam, Object kObj)
	{
		if(kObj == null)
		{
            if (sdUICharacter.Instance.GetTownUI() != null)
            {
                sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
            }
            
			Debug.Log("movie dialogue == null");
			return;
		}
		m_dialogueUI = GameObject.Instantiate(kObj) as GameObject;
		m_texImage = m_dialogueUI.transform.FindChild("bigground").gameObject;
		m_nameImage = m_texImage.transform.FindChild("smalllground").gameObject;
		m_btnContinue = m_texImage.transform.FindChild("clickcontinue").gameObject;
		m_labelName = GameObject.Find("LabelName").GetComponent<UILabel>();
		m_labelName.color = new Color(255,255,128,255)/255.0f;			
		m_labelDialogue = GameObject.Find("LabelDialogue").GetComponent<UILabel>();
		m_labelDialogue.color = Color.white;
		m_headImage = GameObject.Find("characterImage");
		m_mask = m_dialogueUI.transform.FindChild("Sprite_mask").gameObject.GetComponent<UISprite>();
        m_btnBlcok = m_dialogueUI.transform.FindChild("Button_blockclick").gameObject;
		m_texImage.SetActive(false);
		m_headImage.SetActive(false);
		m_btnContinue.SetActive(false);

		m_dialogueUI.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_dialogueUI.transform.localPosition = (Vector3)kParam.userdata1;
		m_dialogueUI.transform.localRotation = Quaternion.identity;
		m_dialogueUI.transform.localScale = (Vector3)kParam.userdata0;
		Init();
        if (sdUICharacter.Instance.GetTownUI() != null)
        {
            sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
        }
	}

	void Init()
	{
        m_labelDialogue.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        m_btnBlcok.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		m_texImage.SetActive(false);
		m_headImage.SetActive(false);
		m_btnContinue.SetActive(false);
        //if(m_bGuide)
        //    m_mask.gameObject.SetActive(false);
	}
		
	public void loadAtlas(ResLoadParams param,Object obj)
	{
		UIAtlas  atlas = obj as UIAtlas;
		string spriteName = (string)param.userdata0;
		UISprite sprite = m_headImage.GetComponent<UISprite>();
		sprite.atlas = atlas;
		sprite.spriteName = spriteName;
	}

	public void Continue()
	{
		if(m_bEnd)
		{
            if (m_bGuide)
                m_mask.gameObject.SetActive(false);
			SetTime(0.0f);
			m_nStage = 6;
			return;
		}	
		//鏄?惁闇瑕侀殣钘忓綋鍓峮pc
		List<stMovie> movieList2 = stageMovie.movieList;
		if(m_nDialogueIndex < movieList2.Count)
		{
			stMovie data = movieList2[m_nDialogueIndex];
			if(data.npcNoShow == 1)
			{
				GameObject obj = GameObject.Find(data.npcModel);
				if(obj != null)
					obj.SetActive(false);
				//璁＄畻涓嬩竴涓?浉鏈轰綅缃產aa
				SetCameraInfo(m_nDialogueIndex + 1);
			}
		}
		m_nDialogueIndex++;
		ShowTextBegin();
	}


	public void Hide()
	{
		HeaderProto.ECreatureActionState[]  state = new HeaderProto.ECreatureActionState[2];
        state[0] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_IDLE;
		state[1] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL;
		for(int i = 0; i < state.Length; i++)
		{
			sdGameLevel.instance.mainChar.RemoveDebuffState(state[i]);
		}	

		sdGameLevel.instance.SetFingerObjectActive(true);
		sdUICharacter.Instance.DialogueCharacterList.Clear();
		sdGameLevel.instance.mainCamera.MainCharFollow = true;	
		m_dialogueUI.SetActive(false);
	}


	void SetCameraInfo(int dialogueIndex)
	{
		List<stMovie> movieList = stageMovie.movieList;
		if(dialogueIndex >= movieList.Count)
			return;
		stMovie movieData = movieList[dialogueIndex];

		for(int i=0; i < sdUICharacter.Instance.DialogueCharacterList.Count; ++i)
		{
			stDialogueCharacter data2 = sdUICharacter.Instance.DialogueCharacterList[i];
			if(data2.name == movieData.npcModel)
			{
				data2.character.SetActive(true);						
				Quaternion rotate = data2.rotate;
				Vector3 offset = rotate*new Vector3(0,0,data2.cameraDis);
				m_mainCamera.transform.position = data2.pos + offset + new Vector3(0,1,0);
				m_mainCamera.transform.rotation = rotate*Quaternion.AngleAxis(180.0f, new Vector3(0,1,0));
				return;
			}
		}
	}

	void HideUI(GameObject obj)
	{
		UIAnchor anchor = obj.GetComponent<UIAnchor>();
		if(anchor != null)
		{
            anchor.pixelOffset.x = -90000.0f;
		}
	}

	public static void BuffChange(bool bAdd)
	{
		HeaderProto.ECreatureActionState[]  state = new HeaderProto.ECreatureActionState[2];
        state[0] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_IDLE;
		state[1] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL;		
		Dictionary<int, List<sdActorInterface>> allActor = sdGameLevel.instance.actorMgr.GetAllActor();
		foreach(KeyValuePair<int, List<sdActorInterface>> pair in allActor)
		{
			List<sdActorInterface> lst = (List<sdActorInterface>)pair.Value;
			if(lst != null)
			{
				for(int index = 0; index < lst.Count; ++index)
				{
					for(int i = 0; i < state.Length; i++)
					{
						if(bAdd)
							lst[index].AddDebuffState(state[i]);
						else
							lst[index].RemoveDebuffState(state[i]);
					}
				}
			}
		}
	}

    void OnClick()
    {
        if (gameObject.name == "Button_blockclick")
            return;
    }
}
