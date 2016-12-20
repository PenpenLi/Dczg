using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShortCutType
{
	Type_Item = 0,
	Type_Skill,
	Type_Pet,
	Type_PetSkill,
}

/// <summary>
/// 技能图标aa
/// </summary>
public class sdShortCutIcon : MonoBehaviour
{
	// 图标类型aa
	public ShortCutType type = ShortCutType.Type_Item;
	
	// 图标IDaa
	public UInt64 id = 0;

    public sdCopyItem cdEffect = null;
	
	// 图标冷却信息aa
	protected float cooldown = 0;		//< 剩余冷却时间aa
	protected float maxCd = 0;			//< 总共冷却时间aa
	protected bool bIsShowCd = true;	//< 是否显示CD信息aa
	
	// 设置冷却时间,并初始化冷却效果aa
	public void SetCoolDown(float fCooldown, bool bShow)
	{
		SetCoolDown(fCooldown, fCooldown, bShow);
	}
	
	// 设置冷却时间,并初始化冷却效果aa
	public void SetCoolDown(float fMaxCooldown, float fCooldown, bool bShow)
	{
		bIsShowCd = bShow;	
		if (bShow)
		{
			gameObject.GetComponent<UIButton>().enabled = false;
		}
			
		maxCd = fMaxCooldown;
		cooldown = fCooldown;
		if (cooldown <=0)
		{
			maxCd = 0;	
			gameObject.GetComponent<UIButton>().enabled = true;
			UISprite bg = gameObject.transform.GetComponentInChildren<UISprite>();
			if (bg != null)
			{
				bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 1);
			}
			UILabel lable = gameObject.transform.GetComponentInChildren<UILabel>();
			if (lable != null)
			{
				lable.text = "";
			}
		}	
	}
	
	// 显示CDaa
	public void ShowCd()
	{
		bIsShowCd = true;	
		if (cooldown > 0)
		{
			gameObject.GetComponent<UIButton>().enabled = false;
		}
		else
		{
			gameObject.GetComponent<UIButton>().enabled = true;
		}
	}
	
	bool hasAtlas = false;	
	protected virtual void Update()
	{
		// 宠物技能获取图标(用于宠物技能)aa
		if ((type == ShortCutType.Type_Skill) && !hasAtlas)	
		{
			if (sdConfDataMgr.Instance().skilliconAtlas != null)
			{
				if (gameObject.GetComponentInChildren<UISprite>().spriteName == "suodi")
				{
					hasAtlas = true;
					return;
				}
				Transform icon = transform.FindChild("Background");
				icon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().skilliconAtlas;
				hasAtlas = true;
			}
		}
		
		// 更新CDaa
		if (maxCd > 0)
		{
			cooldown -= Time.deltaTime;
			if (type == ShortCutType.Type_Pet)
			{
				gameObject.transform.FindChild("cd").GetComponent<UISprite>().fillAmount = cooldown/maxCd;
			}
			else
			{
				if (bIsShowCd)
				{
					UILabel lable = gameObject.transform.GetComponentInChildren<UILabel>();
					if (lable != null)
					{
						int showCd = (int)cooldown + 1;
						lable.text = showCd.ToString();
					}
	
					UISprite bg = gameObject.transform.GetComponentInChildren<UISprite>();
					if (bg != null)
					{
						bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, (float)100/(float)255);
					}

                    if (cooldown <= 0)
                    {
                        if (cdEffect != null)
                        {
                            cdEffect = cdEffect.Show().GetComponent<sdCopyItem>();
                        }
                    }
				}
			}
			
			if (cooldown <=0)
			{
				maxCd = 0;	//< CD结束aa
			}

			if (maxCd == 0)
			{
				gameObject.GetComponent<UIButton>().enabled = true;	
					
				UISprite bg = gameObject.transform.GetComponentInChildren<UISprite>();
				if (bg != null)
					bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 1);
					
				UILabel lable = gameObject.transform.GetComponentInChildren<UILabel>();
				if (lable != null)
					lable.text = "";		
			}
		}
		
		//
		if (needspell)
		{
			sdMainChar mainChar = sdGameLevel.instance.mainChar;
			if (mainChar != null)
			{
				mainChar.CastSkill((int)id);
				//sdSkill skill = mainChar.skillTree.getSkill((int)id);
				//if (skill != null)
				//{
				//	skill.spell(mainChar);	
				//}
			}
		}
	}
	
	bool needspell = false;
	bool isBig = false;
	protected virtual void OnPress(bool isDown)
	{
		if (type == ShortCutType.Type_Skill && id == 1001 && !sdGameLevel.instance.AutoMode)
		{
			if (isDown)	
			{
				needspell = true;
			}
			else
			{
				needspell = false;	
			}
		}
		
		if (!isDown) 
		{
			if (type == ShortCutType.Type_Skill && isBig)
			{
				gameObject.transform.localScale /= 1.2f;
				isBig = false;
			}
			return;
		}
		
		if (type == ShortCutType.Type_Skill)
		{
            if (isDown)
            {
                GPUSH_API.Vibrate(25);
            }
			if (!isBig)
			{
				gameObject.transform.localScale *= 1.2f;
				isBig = true;
			}
			
			// CD尚未结束,则直接返回aa
			if (maxCd > 0 && bIsShowCd) 
				return;

			// 检查场景和主角战斗控制器aa
			if (sdGameLevel.instance == null)
				return;

			FingerControl kFingerControl = sdGameLevel.instance.GetFingerControl(); ;
			if (kFingerControl == null)
				return;

			// 自动打怪模式下:自动寻路攻击最近的目标aa
			int iSkillID = (int)id / 100;
			if (sdGameLevel.instance.AutoMode && iSkillID != 1002)
			{
				if (!kFingerControl.AttackNearest(iSkillID))
					return;	
			}

			// 自动打怪模式下:调整方向aa
			if(sdGameLevel.instance.AutoMode)
			{
				kFingerControl.AdjustDirection(iSkillID);
			}

			// 释放技能(自动模式下普通攻击不再强制施放)aa
			if (!sdGameLevel.instance.AutoMode || iSkillID != 1001)
			{
				sdMainChar mainChar = sdGameLevel.instance.mainChar;
				if (mainChar != null)
				{
					sdGameSkill skill = sdGameSkillMgr.Instance.GetSkill((int)id);
					if (skill != null)
					{
						int error = -1;
						if (!mainChar.CastSkill(skill.classId, ref error))
						{
							string msg = string.Format("Error_{0}", error);
							sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr(msg), Color.yellow);
						}
					}
					else
					{
						int error = -1;
						if (!mainChar.CastSkill((int)id, ref error))
						{
							string msg = string.Format("Error_{0}", error);
							sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr(msg), Color.yellow);
						}

					}
					//sdSkill skill = mainChar.skillTree.getSkill((int)id);
					//if (skill != null)
					//{
					//	skill.spell(mainChar);
					//}
				}
			}
		}
		else if (type == ShortCutType.Type_Pet)
		{

		}
		else if (type == ShortCutType.Type_PetSkill)
		{

		}
	}
	
	public void SetSkillCurStage(int stage)
	{
		Hashtable info = sdConfDataMgr.Instance().GetSkill(id.ToString());
		if (info != null)
		{
			UISprite bg = gameObject.transform.FindChild("Background").GetComponent<UISprite>();
			if (bg != null)
			{
				string key = "stageicon" + stage.ToString();
				object	obj	=	info[key];
				if(obj!=null){
					string name = obj.ToString();
					bg.spriteName = name;
				}
				else
				{
					//Debug.Log(key + " Cant Find In Skill Hashtable.");
				}
			}
		}
	}
}