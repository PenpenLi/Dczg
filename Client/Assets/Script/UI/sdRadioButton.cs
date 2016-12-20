using UnityEngine;
using System.Collections.Generic;

public class sdRadioButton : MonoBehaviour
{
	public int index;
	public bool isActive = false;
	Color color = Color.white;
	
	public GameObject bg = null;
	public GameObject word = null;
	
	public bool isChangeDepth = false;
	public int baseDepth = 0;
	
	public string normalBg = "";
	public string activeBg = "";
	public string normalWordBg = "";
	public string activeWordBg = "";
    public GameObject redTip = null;

	void Start()
	{
		//ChangeRadioBtnPicDepth();
        Active(isActive);
	}

    public void ShowRedTip()
    {
        if (redTip == null) return;
        redTip.SetActive(true);
    }

    public void HideRedTip()
    {
        if (redTip == null) return;
        redTip.SetActive(false);
    }

	public List<EventDelegate> onClick = new List<EventDelegate>();
    public List<EventDelegate> onUnActive = new List<EventDelegate>();
	
	void OnClick()
	{
		if (isActive) return;
		
		sdUICharacter.Instance.ActiceRadioBtn(this);
		Active(true);

		EventDelegate.Execute(onClick);
	}


	
	public void Active(bool active)
	{
        if (isActive && !active)
        {
            if (onUnActive.Count > 0)
            {
                EventDelegate.Execute(onUnActive);
            }
        }

		isActive = active;
		if (active)
		{
			if (bg != null)
			{
				bg.GetComponent<UISprite>().spriteName = activeBg;
			}
			
			if (word != null)
			{
				word.GetComponent<UISprite>().spriteName = activeWordBg;
			}
		}
		else
		{
			if (bg != null)
			{
				bg.GetComponent<UISprite>().spriteName = normalBg;	
			}
			
			if (word != null)
			{
				word.GetComponent<UISprite>().spriteName = normalWordBg;
			}
		}
		
		ChangeRadioBtnPicDepth();
	}
	
	public void ChangeRadioBtnPicDepth()
	{
		if (isChangeDepth)
		{
			if (isActive)
			{
				if (bg != null)
				{
					bg.GetComponent<UISprite>().depth = baseDepth+1;
				}
				
				if (word != null)
				{
					word.GetComponent<UISprite>().depth = baseDepth+2;
				}
			}
			else
			{
				if (bg != null)
				{
					bg.GetComponent<UISprite>().depth = baseDepth-2;
				}
				if (word != null)
				{
					word.GetComponent<UISprite>().depth = baseDepth-1;
				}
			}
		}
	}
}