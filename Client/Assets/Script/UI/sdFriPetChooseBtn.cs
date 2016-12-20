using UnityEngine;
using System.Collections;
using System;

public class sdFriPetChooseBtn : MonoBehaviour
{
	sdFriend friInfo = null;

	public GameObject petFrame;
	public GameObject charName;
	public GameObject petName;
	public GameObject petLevel;
	public GameObject friImg;
	public GameObject petIcon;
	
	public void SetInfo(sdFriend friend)
	{
		friInfo = friend;
		if (friInfo == null) 
		{
			gameObject.SetActive(false);
			return;
		}
		
		charName.GetComponent<UILabel>().text = friend.name;
		petName.GetComponent<UILabel>().text = friend.petInfo.m_strName + (friend.petInfo.m_iUp==0?"":"+"+friend.petInfo.m_iUp);
		petFrame.GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetPetQuilityBorder(friend.petInfo.m_iAbility);
		petName.GetComponent<UILabel>().color = sdConfDataMgr.Instance().GetItemQuilityColor(friend.petInfo.m_iAbility);
		string lvl = string.Format("Lv.{0}", friend.petInfo.m_iLevel);
		petLevel.GetComponent<UILabel>().text = lvl;
		petIcon.GetComponent<UISprite>().spriteName = friend.petInfo.m_strIcon.ToString();
		if (sdFriendMgr.Instance.GetFriend(friend.id) == null)
		{
			friImg.SetActive(false);	
		}
		else
		{
			friImg.SetActive(true);
		}
	}
	
	void OnClick()
	{
		sdUICharacter.Instance.SelectFriPet(friInfo);
	}
}