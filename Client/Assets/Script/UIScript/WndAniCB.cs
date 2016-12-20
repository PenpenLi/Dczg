using UnityEngine;
using System.Collections;

public class WndAniCB : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void OnFinished()
	{
		if( gameObject.name == "pvpreward(Clone)" )
		{
			transform.FindChild("Sprite/panel_pvpreward").gameObject.SetActive(true);
		}
		else if( gameObject.name == "$LevelPrepareWnd(Clone)" ) 
		{
			sdUICharacter.Instance.HideTuitu(true);
			sdUICharacter.Instance.ShowFullScreenUI(true);
		}
		else if ( gameObject.name == "$PetPropPnl(Clone)" )
		{
			gameObject.GetComponent<sdUIPetPropPnl>().LoadPetModel();
		}
		else if ( gameObject.name == "$PetSmallTip(Clone)" )
		{
			gameObject.GetComponent<sdUIPetSmallTip>().LoadPetModel();
		}
		else if ( gameObject.name == "$PetLevelPnl(Clone)" )
		{
			gameObject.GetComponent<sdUIPetLevelPnl>().LoadPetModel();
		}
		else if ( gameObject.name == "$PetStrongPnl(Clone)" )
		{
			gameObject.GetComponent<sdUIPetStrongPnl>().LoadPetModel();
		}
		else if( gameObject.name == "$AwardCenterWnd(Clone)" )
		{
			AwardCenterWnd.Instance.OnShowWndAniFinish();
		}		
		else if( gameObject.name == "$EverydayAwardWnd(Clone)")
		{
			EverydayAwardWnd.Instance.OnShowWndAniFinish();
		}
		else if( gameObject.name == "$EverydayQuestWnd(Clone)")
		{
			EverydayQuestWnd.Instance.OnShowWndAniFinish();
		}
		else if(gameObject.name == "$LevelAwardWnd(Clone)")
		{
			LevelAwardWnd.Instance.OnShowWndAniFinish();
		}
		else if( gameObject.name == "$MallPanel(Clone)")
		{
			gameObject.GetComponent<MallPanel>().OnShowWndAniFinish();
		}
		else if(gameObject.name=="$Old_OnePetObtainPanel(Clone)")
		{
			sdMallManager.Instance.OnOnePetObtainPanelShowAnimFinish();
		}
		else if(gameObject.name=="$TenPetObtainPanel(Clone)")
		{
			sdMallManager.Instance.OnTenPetObtainPanelShowAnimFinish();
		}
        else if (gameObject.name == "$RoleTipWnd(Clone)")
        {
            gameObject.GetComponent<sdRoleTipWnd>().ShowAvatar();
        }
	}

	public void OnFinishedHide()
	{
		if ( gameObject.name == "$PetLevelPnl(Clone)" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
					petPnl.SetPetModelVisible(true);
			}
		}
		else if ( gameObject.name == "$PetLevelSelectPnl(Clone)" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetLevelPnl petPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
				if (petPnl)
					petPnl.SetPetModelVisible(true);
			}
		}
		else if ( gameObject.name == "$PetStrongPnl(Clone)" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
					petPnl.SetPetModelVisible(true);
			}
		}
		else if ( gameObject.name == "$PetPropPnl(Clone)" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetRonghePnl petPnl = wnd.GetComponentInChildren<sdUIPetRonghePnl>();
				if (petPnl)
				{
					petPnl.ShowHideModel(true);
				}
			}
		}
        else if (gameObject.name == "$RoleTipWnd(Clone)")
        {
            gameObject.GetComponent<sdRoleTipWnd>().HideAvatar();
        }
	}
}
