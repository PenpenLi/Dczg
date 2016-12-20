using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class sdDialogueCharacter : MonoBehaviour
{
	public float cameraDis = 4.0f;
	void Awake()
	{
		stDialogueCharacter character = new stDialogueCharacter();
		character.pos = gameObject.transform.position;
		character.rotate = gameObject.transform.rotation;
		character.character = gameObject;
		character.name = gameObject.name;
		character.cameraDis = cameraDis;
		sdUICharacter.Instance.DialogueCharacterList.Add(character);
		Hashtable movietable = sdConfDataMgr.Instance().m_Movie;
		if(movietable.ContainsKey(sdUICharacter.Instance.iCurrentLevelID))
		{
			sdPlayerInfo kPlayerInfo = SDNetGlobal.playerList[SDNetGlobal.lastSelectRole];
            string strLevel = sdConfDataMgr.Instance().GetRoleSetting("MovieDialogue_" + kPlayerInfo.mRoleName);
            if (strLevel.Length != 0 && int.Parse(strLevel) >= sdUICharacter.Instance.iCurrentLevelID)
				gameObject.SetActive(false);
			stStageMovie stagemovie = (stStageMovie)movietable[sdUICharacter.Instance.iCurrentLevelID];
			for(int index = 0; index <stagemovie.movieList.Count; ++index)
			{
				stMovie movieData = stagemovie.movieList[index];
				if(movieData.npcModel == gameObject.name)
				{
					if(movieData.npcShow == 1)
					{
						gameObject.SetActive(false);
						break;
					}
				}
			}
		}
	}
}


