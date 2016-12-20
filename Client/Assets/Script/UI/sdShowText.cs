using UnityEngine;

public class sdShowText : MonoBehaviour
{
	public string strId = "";
	public string format = "";
	void Start()
	{
		string text = "";
		if (format != "")
		{
			text = string.Format(format, sdConfDataMgr.Instance().GetShowStr(strId));
		}
		else
		{
			text = sdConfDataMgr.Instance().GetShowStr(strId);
		}
		gameObject.GetComponent<UILabel>().text = text;
	}
}