using System;
using UnityEngine;
using System.Collections;

public class sdUIPetEquipButtonClick : MonoBehaviour 
{
	public int m_iEquipID = 0;
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	void OnClick()
    {
		if ( gameObject.name=="equipK0"||gameObject.name=="equipK1"||gameObject.name=="equipK2"
			||gameObject.name=="iconbg")
		{
			if (m_iEquipID>0)
			{
				sdUIPetControl.Instance.ActivePetEquipTip(null, m_iEquipID);
			}
			else
			{
				if (gameObject.name=="equipK0")
				{
					GameObject wnd = sdGameLevel.instance.NGUIRoot;
					if (wnd)
					{
						sdUIPetPropPnl pnlWnd = wnd.GetComponentInChildren<sdUIPetPropPnl>();
						if (pnlWnd&&pnlWnd.m_uuDBID!=UInt64.MaxValue)
						{
							sdUIPetControl.Instance.ActivePetChangeEquipPnl(null, pnlWnd.m_uuDBID, (int)PetEquipType.Pet_EquipType_fj);
							pnlWnd.SetPetModelVisible(false);
						}
					}
				}
				else if (gameObject.name=="equipK1")
				{
					GameObject wnd = sdGameLevel.instance.NGUIRoot;
					if (wnd)
					{
						sdUIPetPropPnl pnlWnd = wnd.GetComponentInChildren<sdUIPetPropPnl>();
						if (pnlWnd&&pnlWnd.m_uuDBID!=UInt64.MaxValue)
						{
							sdUIPetControl.Instance.ActivePetChangeEquipPnl(null, pnlWnd.m_uuDBID, (int)PetEquipType.Pet_EquipType_wq);
							pnlWnd.SetPetModelVisible(false);
						}
					}
				}
				else if (gameObject.name=="equipK2")
				{
					GameObject wnd = sdGameLevel.instance.NGUIRoot;
					if (wnd)
					{
						sdUIPetPropPnl pnlWnd = wnd.GetComponentInChildren<sdUIPetPropPnl>();
						if (pnlWnd&&pnlWnd.m_uuDBID!=UInt64.MaxValue)
						{
							sdUIPetControl.Instance.ActivePetChangeEquipPnl(null, pnlWnd.m_uuDBID, (int)PetEquipType.Pet_EquipType_sp);
							pnlWnd.SetPetModelVisible(false);
						}
					}
				}
			}
		}
	}
}

