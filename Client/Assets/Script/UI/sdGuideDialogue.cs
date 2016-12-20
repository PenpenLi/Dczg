using UnityEngine;
using System.Collections;

public class sdGuideDialogue : MonoBehaviour
{
	GameObject  m_dialogue = null;
	stStageMovie stageMovie;
	public void SetMovieInfo(int stageID, Vector3 scale, Vector3 pos)
	{
		Hashtable table = sdConfDataMgr.Instance().m_Movie;
		if(table.ContainsKey(stageID))
			stageMovie = (stStageMovie)table[stageID];

		if(m_dialogue == null)
		{
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = pos;
			param.userdata1 = scale;
			sdResourceMgr.Instance.LoadResource("UI/$Guide/guidedialogue.prefab", LoadDialogue, param);
		}
		else
		{
			m_dialogue.transform.localPosition = pos;
			ShowText();
		}
	}

	void LoadDialogue(ResLoadParams param, Object obj)
	{
		m_dialogue = GameObject.Instantiate(obj) as GameObject;
		m_dialogue.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_dialogue.transform.localPosition = (Vector3)param.userdata0;
		m_dialogue.transform.localScale = (Vector3)param.userdata1;
		ShowText();
	}

	void ShowText()
	{
		Transform obj = m_dialogue.transform.FindChild("Sprite_background");
		if(obj != null)
		{
			UILabel label = obj.FindChild("Label_dialogue").GetComponent<UILabel>();
			label.color = new Color(39.0f/255.0f, 24.0f/255.0f, 1.0f/255.0f,1.0f);
			label.text = stageMovie.movieList[0].content;
		}
	}

	public void Hide()
	{
		m_dialogue.transform.localPosition = new Vector3(90000.0f, 0.0f, 0.0f);
	}
}
