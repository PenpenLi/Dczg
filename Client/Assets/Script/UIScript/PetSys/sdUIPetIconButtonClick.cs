using System;
using UnityEngine;
using System.Collections;

public class sdUIPetIconButtonClick : MonoBehaviour 
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
		if ( gameObject.name=="zhpet0"||gameObject.name=="zhpet1"||gameObject.name=="zhpet2"
			||gameObject.name=="zhpet3"||gameObject.name=="peticon")
		{
			sdUIPetZuheSmallIcon petIcon = gameObject.GetComponent<sdUIPetZuheSmallIcon>();
			if (petIcon)
			{
				if( petIcon.GetId()>0 )
					sdUIPetControl.Instance.ActivePetZuheSmallTip(null, petIcon.GetId(), 3, 50);
			}
		}
	}
}

