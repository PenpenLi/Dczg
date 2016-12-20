using System;
using UnityEngine;
using System.Collections;

public class sdUIPetSkillButtonClick : MonoBehaviour 
{
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
		if ( gameObject.name=="skill0"||gameObject.name=="skill1"||gameObject.name=="skill2"
			||gameObject.name=="skill3"||gameObject.name=="skill4" )
		{
			sdUIPetSkillIcon petIcon = gameObject.GetComponent<sdUIPetSkillIcon>();
			if (petIcon)
			{
				if( petIcon.m_iSkillID>0 )
					sdUIPetControl.Instance.ActivePetSkillTip(null, petIcon.m_iSkillID);
			}
		}
	}
}

