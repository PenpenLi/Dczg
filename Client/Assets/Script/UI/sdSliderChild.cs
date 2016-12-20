using System.Collections;
using UnityEngine;
using System.Collections.Generic;

class sdSliderChild : sdUICharacterChild
{
	void Update()
	{
		if (change)
		{
			time += Time.deltaTime;
			if (time > 0.05)
			{
				if (rightDir)
				{
					Color color = gameObject.transform.FindChild("Foreground").GetComponent<UISprite>().color;
					gameObject.transform.FindChild("Foreground").GetComponent<UISprite>().color = new Color(color.r,color.g,color.b,(float)200/(float)255);	
				}
				time = 0;
				change = false;
			}
		}
	}
	
	float time = 0;
	bool change = false;
	public bool rightDir = true;
	string preValue = "";
	public override void Notify(string strValue)
	{
		if (preValue == strValue) return;
		preValue = strValue;
		change = true;
		if (gameObject.GetComponent<sdUICharacterChild>() != null)
		{
			string maxValue = "";
			foreach(sdUICharacterChild item in gameObject.GetComponents<sdUICharacterChild>())
			{
				if (item.GetValue() != "")
				{
					maxValue = item.GetValue();
				}
			}

			if(maxValue != "")
			{
				float max = float.Parse(maxValue);
				if (max > 0)
				{
					float now = float.Parse(strValue);
					bool isMonster = strKey == "MonsterHp";
					if (isMonster)
					{
						int line = sdUICharacter.Instance.MonsterHpNum;
						int curLine = line;
						if (line > 0)
						{
							max = max/line; 
						}
						
						if (now <= max)
						{
							gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "FightingSystem_HP_Boss_Background";
							curLine = 1;
						}
						else
						{
                            int delt = int.Parse(maxValue) % line;
							gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "FightingSystem_HP_Boss_Bar02";
                            if ((int)now % (int)max <= delt)
							{
								curLine = (int)now/(int)max;
							}
							else
							{
								curLine = (int)now/(int)max + 1	;
							}
							now = now%max;

							if (now <= delt)
							{
								now = max;	
							}
						}
						
						GameObject item = GameObject.Find("FightUi");
						if (item != null)
						{
							if (curLine > sdUICharacter.Instance.MonsterHpNum)
							{
								curLine = sdUICharacter.Instance.MonsterHpNum;
							}
							sdUICharacter.Instance.CurrentMonsterHpNum = curLine;
							item.GetComponent<sdFightUi>().SetHpNum();
						}
					}
					
					Color color = gameObject.transform.FindChild("Foreground").GetComponent<UISprite>().color;
					gameObject.transform.FindChild("Foreground").GetComponent<UISprite>().color = new Color(color.r,color.g,color.b,1);
		
					if (rightDir)
					{
						if (max > 0)
						{
							gameObject.transform.FindChild("Foreground").GetComponent<UISprite>().fillAmount = now/max;
						}
						else
						{
							gameObject.transform.FindChild("Foreground").GetComponent<UISprite>().fillAmount = 0;
						}
					}
					else if (!rightDir)
					{
						if (now > 0)
						{
							gameObject.transform.FindChild("Foreground").GetComponent<UISprite>().fillAmount = 1-now/max;
						}
						else
						{
							gameObject.transform.FindChild("Foreground").GetComponent<UISprite>().fillAmount = 1;
						}
					}
					
				}
			}
		}
	}
}