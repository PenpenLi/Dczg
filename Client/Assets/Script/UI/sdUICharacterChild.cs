using System.Collections;
using UnityEngine;

public class sdUICharacterChild : MonoBehaviour
{
	void Awake()	
	{	
		sdUICharacter.Instance.RegisterChild(this);
	}
	
	void OnDestroy()
	{
		if (sdUICharacter.Instance != null)
		{
			sdUICharacter.Instance.RemoveChild(this);	
		}
	}
	
	public string strKey = ""; 
	public string strFormat = "";
	public ChildUIType type = ChildUIType.Type_Value;
	private string textValue = "";
	
	public string GetValue()
	{
		return textValue;	
	}
	
	public virtual void Notify(string strValue)
	{
        if (!enabled) return;

		textValue = strValue;
		Component item = gameObject.GetComponent("UILabel");
		if (item != null)	
		{
			string strShow = "";
			if (strFormat != "")
			{
				strShow = string.Format(strFormat, strValue);
			}
			else
			{
				strShow = strValue;	
			}
			((UILabel)item).text = strShow;
		}
	}
}
