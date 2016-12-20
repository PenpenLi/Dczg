using System.Collections;
using UnityEngine;
using System.Collections.Generic;


class sdSkillWnd : MonoBehaviour
{
	public GameObject job1;
	public GameObject job2;
	public GameObject job3;

    public GameObject passivePanel = null;
	
	public void ShowSkillInfo(string id)
	{
		Hashtable skillinfo = sdConfDataMgr.Instance().GetSkill(id);
		if (skillinfo != null)
		{
			GameObject name = GameObject.Find("label_skillname");
			if (name != null)
			{
				name.GetComponent<UILabel>().text = skillinfo["strName"].ToString();	
			}
			
			GameObject des = GameObject.Find("label_des");
			if (des != null)
			{
				des.GetComponent<UILabel>().text = skillinfo["Description"].ToString();
			}
		}
		
		Hashtable table = sdConfDataMgr.Instance().GetPassiveSkill(id);
		if (table != null)
		{
			//GameObject passivePanel = GameObject.Find("passivepanel");
			if (passivePanel == null) return;
			foreach(DictionaryEntry temp in table)
			{
				Hashtable item = temp.Value as Hashtable;
				int index = int.Parse(item["Index"].ToString());
				string name = "passive"+(index+1).ToString();
				Transform icon = passivePanel.transform.FindChild(name);
				if (icon != null)
				{
					sdSlotIcon slot = icon.GetComponent<sdSlotIcon>();
					
					icon.FindChild("passivename").GetComponent<UILabel>().text = item["strName"].ToString();
					
					sdGameSkill skill = sdGameSkillMgr.Instance.GetSkillByClassId(item["dwClassID"].ToString());
					string level = sdConfDataMgr.Instance().GetShowStr("NoLearn");
					if (skill != null)
					{
						level =  string.Format("Lv.{0}", skill.level.ToString());
						Hashtable cur = sdConfDataMgr.Instance().GetSkill(skill.templateID.ToString());
						slot.SetInfo(skill.templateID.ToString(), cur);
					}
					else
					{
						slot.SetInfo(item["uid"].ToString(), item);	
					}

					slot.HideLock();
					slot.SetMax(false);
					if (skill != null && sdGameSkillMgr.Instance.GetSkill(int.Parse(id)) != null)
					{
						slot.SetHighLight(true);
						Hashtable cur = sdConfDataMgr.Instance().GetSkill(skill.templateID.ToString());
						if (cur != null)
						{
							if (cur["NextLevel"].ToString() == "0")
							{
                                level = "Lv.Max";
							}
							else
							{
							}
						}
					}
					else
					{
						int compareLevel = 1;
						sdMainChar mainChar = GameObject.Find("@GameLevel").GetComponent<sdGameLevel>().mainChar;
						if (mainChar != null)
						{
							compareLevel = (int)mainChar["Level"];
						}
						int needLevel = int.Parse(item["nLearnLevel"].ToString());
						int needPoint = int.Parse(item["dwTotalSkillTreePoint"].ToString());
						int requestPoint = int.Parse(item["dwCostSkillPoint"].ToString());
                        bool hasNeedSkill = false;
                        if (item.ContainsKey("NeedSkill"))
                        {
                            int needSkill = int.Parse(item["NeedSkill"].ToString());
                            Hashtable needInfo = sdConfDataMgr.Instance().GetSkill(needSkill.ToString());
                            if (needInfo != null)
                            {
                                if (int.Parse(needInfo["byIsPassive"].ToString()) == 0)
                                {
                                    hasNeedSkill = true;
                                }
                                else
                                {
                                    sdGameSkill skillneed = sdGameSkillMgr.Instance.GetSkillByClassId(needInfo["dwClassID"].ToString());
                                    if (skillneed != null && skillneed.level >= int.Parse(needInfo["byLevel"].ToString()))
                                    {
                                        hasNeedSkill = true;
                                    }
                                }
                            }
                            else
                            {
                                hasNeedSkill = true;
                            }
                        }
                        
						if (compareLevel < needLevel  ||
                            sdGameSkillMgr.Instance.GetSkill(int.Parse(id)) == null || !hasNeedSkill)
						{
							slot.SetHighLight(false);
							slot.ShowLock();
						}
						else
						{
// 							if (sdGameSkillMgr.Instance.GetSkillPoint() < requestPoint)
// 							{
// 								slot.SetHighLight(false);
// 								//slot.ShowLock();
// 							}
// 							else
// 							{
								slot.SetHighLight(true);
							//}
						}
					}
                    icon.FindChild("passivelv").GetComponent<UILabel>().text = level;
				}	
			}
		}
	}
	
	public void Refresh()
	{
		ShowSkillInfo(sdUICharacter.Instance.curSkillId);
		
		GameObject point = GameObject.Find("label_point");
		if (point != null)
		{
			point.GetComponent<UILabel>().text = sdGameSkillMgr.Instance.GetSkillPoint().ToString();
		}
		
		GameObject jindu = GameObject.Find("jindu");
		
		if (jindu != null)
		{
			Transform pointfill = jindu.transform.FindChild("pointfill");
			if (pointfill != null)
			{
				int totalPoint = sdGameSkillMgr.Instance.GetTotalPoint();
				Transform total = pointfill.FindChild("totalpoint");
				total.GetComponent<UILabel>().text = totalPoint.ToString();
				float fill = 0;
				if (totalPoint >= 24)
				{
					fill = 1;
					Transform skillfill2 = jindu.transform.FindChild("skillfill2");
					if (skillfill2 != null)
					{
						skillfill2.GetComponent<UISprite>().fillAmount = 1;
					}
					Transform skillfill3 = jindu.transform.FindChild("skillfill3");
					if (skillfill3 != null)
					{
						skillfill3.GetComponent<UISprite>().fillAmount = 1;
					}
					Transform skillfill4 = jindu.transform.FindChild("skillfill4");
					if (skillfill4 != null)
					{
						skillfill4.GetComponent<UISprite>().fillAmount = 1;
					}
				}
				else if (totalPoint >= 16)
				{
					float delt = (float)(totalPoint - 16);
					fill = (float)((float)1/(float)1.53+ delt/8/2.82);
					Transform skillfill2 = jindu.transform.FindChild("skillfill2");
					if (skillfill2 != null)
					{
						skillfill2.GetComponent<UISprite>().fillAmount = 1;
					}
					Transform skillfill3 = jindu.transform.FindChild("skillfill3");
					if (skillfill3 != null)
					{
						skillfill3.GetComponent<UISprite>().fillAmount = 1;
					}
					Transform skillfill4 = jindu.transform.FindChild("skillfill4");
					if (skillfill4 != null)
					{
						skillfill4.GetComponent<UISprite>().fillAmount = 0;
					}
				}
				else if (totalPoint >= 8)
				{
					float delt = (float)(totalPoint - 8);
					fill = (float)((float)1/(float)2.82+ delt/8/2.82);
					Transform skillfill2 = jindu.transform.FindChild("skillfill2");
					if (skillfill2 != null)
					{
						skillfill2.GetComponent<UISprite>().fillAmount = 1;
					}
					Transform skillfill3 = jindu.transform.FindChild("skillfill3");
					if (skillfill3 != null)
					{
						skillfill3.GetComponent<UISprite>().fillAmount = 0;
					}
					Transform skillfill4 = jindu.transform.FindChild("skillfill4");
					if (skillfill4 != null)
					{
						skillfill4.GetComponent<UISprite>().fillAmount = 0;
					}
				}
				else
				{
					fill = (float)(totalPoint)/(float)8/(float)2.82;
					Transform skillfill2 = jindu.transform.FindChild("skillfill2");
					if (skillfill2 != null)
					{
						skillfill2.GetComponent<UISprite>().fillAmount = 0;
					}
					Transform skillfill3 = jindu.transform.FindChild("skillfill3");
					if (skillfill3 != null)
					{
						skillfill3.GetComponent<UISprite>().fillAmount = 0;
					}
					Transform skillfill4 = jindu.transform.FindChild("skillfill4");
					if (skillfill4 != null)
					{
						skillfill4.GetComponent<UISprite>().fillAmount = 0;
					}
				}
				
				if (fill >= 0.5)
				{
					float len = (float)(pointfill.GetComponent<UISprite>().localSize.x * (fill-0.5));
					total.localPosition = new Vector3(len, -2, 0);
				}
				else
				{
					float len = (float)(pointfill.GetComponent<UISprite>().localSize.x * (0.5 - fill));
					total.localPosition = new Vector3(-len, -2, 0);
				}
				
				pointfill.GetComponent<UISprite>().fillAmount = fill;
			}
		}
	}
	
	HeaderProto.ERoleJob job = HeaderProto.ERoleJob.ROLE_JOB_NONE;
	
	void Update()
	{
		if (sdConfDataMgr.Instance().jobAtlas != null && job != sdGameLevel.instance.mainChar.GetJob())
		{
			job = sdGameLevel.instance.mainChar.GetJob();
			job2.GetComponent<UIButton>().tweenTarget.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().jobAtlas;
			job2.GetComponent<UIButton>().tweenTarget.GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetJobIcon(((int)job).ToString());
			job2.transform.FindChild("word").GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetJobWordPic(((int)job).ToString());
			List<string> list = sdConfDataMgr.Instance().GetLinkJob(((int)job).ToString());
			if (list[0] != null)
			{
				job1.GetComponent<UIButton>().tweenTarget.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().jobAtlas;
				job1.GetComponent<UIButton>().tweenTarget.GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetJobIcon(list[0]);
				job1.transform.FindChild("word").GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetJobWordPic(list[0])+"2";
			}
			
			if (list[1] != null)
			{
				job3.GetComponent<UIButton>().tweenTarget.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().jobAtlas;
				job3.GetComponent<UIButton>().tweenTarget.GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetJobIcon(list[1]);
				job3.transform.FindChild("word").GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetJobWordPic(list[1])+"2";
			}
		}
	}
	
	void Start()
	{
		GameObject panel = GameObject.Find("skillpanel");
		if (panel == null) return;
		
		Hashtable table = sdConfDataMgr.Instance().GetSkillList();
		if (table == null) return;

		Hashtable alineCount = new Hashtable();
		Hashtable plineCount = new Hashtable();
		
		string jobClass = "1";
		sdGameLevel gameLevel = sdGameLevel.instance;
		if (gameLevel && gameLevel.mainChar != null && gameLevel.mainChar.Property != null)
		{
			jobClass = ((int)gameLevel.mainChar.GetJob()).ToString();
		}
		
		foreach(DictionaryEntry item in table)
		{
			Hashtable skill = item.Value as Hashtable;
			string job = skill["byJob"].ToString();
			if (job != jobClass) continue;

			int active = int.Parse(skill["byIsPassive"].ToString());
			string x = skill["strName"].ToString();
			int index = int.Parse(skill["Index"].ToString());
			if (active == 1)
			{
//				int line = int.Parse(skill["ParentID"].ToString());
//				if (line != 1001) continue;
//				string name = "passive"+index.ToString();
//				Transform icon = panel.transform.FindChild(name);
//				if (icon != null)
//				{
//					icon.FindChild("icon").GetComponent<UISprite>().spriteName = skill["icon"].ToString();
//				}
			}
			else
			{
				string name = "skill"+(index+1).ToString();
				Transform icon = panel.transform.FindChild(name);
				if (icon != null)
				{
					if (index == 2)
					{
						sdUICharacter.Instance.ShowSkillInfo(skill["uid"].ToString());
					}
					
					string iconname = skill["icon"].ToString();
					icon.GetComponent<UISprite>().spriteName = skill["icon"].ToString();
					icon.GetComponent<sdSkillIcon>().skillId = skill["uid"].ToString();
				
					if (sdGameSkillMgr.Instance.GetSkill(int.Parse(skill["uid"].ToString())) == null)
					{
						icon.FindChild("lock").GetComponent<UISprite>().spriteName = "lock";
                        icon.GetComponent<UISprite>().alpha = 0.5f;
					}
					else
					{
						icon.FindChild("lock").GetComponent<UISprite>().spriteName = "";
                        icon.GetComponent<UISprite>().alpha = 1f;
					}
				}
			}
		}
		
		Refresh();
	}
}