using UnityEngine;
using System.Collections;

/// <summary>
/// 宠物技能图标(直接使用底层的CD时间)aa
/// </summary>
public class sdPetSkillShortCutIcon : sdShortCutIcon
{
	// 技能IDaa
	protected int mSkillID = 0;
	public int SkillID
	{
		set { mSkillID = value;}
		get { return mSkillID;}
	}
	
	// 上次更新时间aa
	protected float mLastUpdateTime = 0.0f;	
	
	// 按钮是否允许
	protected bool mIsButtonEnable = false;	
	
	// 处理更新(继承自MonoBehaviour)
	protected override void Update()
	{
		if (Time.time - mLastUpdateTime > 0.1f)
		{
			mLastUpdateTime = Time.time;
			
			GameObject kGameLevelObject = sdGameLevel.instance.gameObject;
			sdTuiTuLogic kTuituLogic = kGameLevelObject.GetComponent<sdTuiTuLogic>();	
			sdGameMonster kPet = kTuituLogic.ActivePet;
			if (kPet == null)
				return;
			
			if (!kPet.Initialized)
				return;
				
			if (kPet.skillTree == null)
				return;
			
			if (kPet.Property == null)
				return;
				
			int iSpSkillID = (int)(kPet.Property["SpSkill"]);
			if (iSpSkillID == 0)
				return;
			
			sdSkill kSkill = kPet.skillTree.getSkill(iSpSkillID);
			if (kSkill == null)
				return;
			
			if (kSkill.skillState == (int)sdSkill.State.eSS_OK)
			{
				if (!mIsButtonEnable)
				{
					mIsButtonEnable = true;
					
					gameObject.GetComponent<UIButton>().enabled = true;	
						
					UISprite bg = gameObject.transform.GetComponentInChildren<UISprite>();
					if (bg != null)
						bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 1);
						
					UILabel lable = gameObject.transform.GetComponentInChildren<UILabel>();
					if (lable != null)
						lable.text = "";
				}
			}
			else
			{
				if (mIsButtonEnable)
				{
					mIsButtonEnable = false;
					
					gameObject.GetComponent<UIButton>().enabled = false;	
					
					UISprite bg = gameObject.transform.GetComponentInChildren<UISprite>();
					if (bg != null)
						bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 100.0f/255.0f);	
				}
				
				UILabel lable = gameObject.transform.GetComponentInChildren<UILabel>();
				if (lable != null)
				{
					int iSkillCD = kSkill.GetCD();
					int iShowCd = (int)(iSkillCD * 0.001f) + 1;
					lable.text = iShowCd.ToString();
				}
			}
		}
	}
	
	// 处理按钮按下(继承自MonoBehaviour)
	protected override void OnPress(bool isDown)
	{
		if (type == ShortCutType.Type_PetSkill)
		{	
			sdTuiTuLogic tuitu = GameObject.Find("@GameLevel").GetComponent<sdTuiTuLogic>();
			if (tuitu != null)
				tuitu.UsePetSkill();
		}
	}
}

