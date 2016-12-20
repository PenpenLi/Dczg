using UnityEngine;
using System.Collections;

public class sdSkillSeries : object
{
	public string seriesName = "unamed";
	public string seriesEnName = "unamed";	
	public int enablePoints = 0;
	public ArrayList skills = new ArrayList();
	
	public int getTotalPoints()
	{
		int total = 0;
		for(int i = 0; i < skills.Count; ++i)
		{
			total += (skills[i] as sdSkill).learnPoints;
		}
		return total;
	}
	
	public sdSkill getSkill(int id)
	{
		sdSkill sk = null;
		for(int i = 0; i < skills.Count; ++i)
		{
			sdSkill temp = skills[i] as sdSkill;
			if(temp.id == id)
			{
				sk = temp;
				break;
			}
		}
		
		return sk;
	}
}

public class sdSkillTree : object
{
	public int skillPoint = 0;
	public	Hashtable	AllSkill		=	new Hashtable();
	//public	Hashtable	DrivingSkill	=	new Hashtable(); //主动技能
	public sdSkillTree()
	{
	}
	
	public	void	Add(sdSkill skill)
	{
		if(skill==null)
			return;
		AllSkill[skill.id]	=	skill;
//		if(!skill.isPassive)
//		{
//			DrivingSkill[skill.id]	=	skill;
//		}
	}
	public sdSkill getSkill(int id)
	{
		sdSkill skill = (sdSkill)AllSkill[id];
		return skill;
	}
	
	public void tick()
	{
		foreach(DictionaryEntry de in AllSkill)
		{
			sdSkill skill = de.Value as sdSkill;
			if(skill != null)
			{
				skill.tick();
			}
		}
	}
	public	bool	IsSkillLearned(int id)
	{
		sdSkill skill = getSkill(id);
		if(skill==null)
			return false;
		return skill.skillInfo!=null;
	}
	public	void	InitAllSkill(sdGameActor _gameActor)
	{
		foreach(DictionaryEntry de in AllSkill)
		{
			sdSkill skill = de.Value as sdSkill;
			if(skill != null)
			{
				skill.assembleActionStates(_gameActor);
			}
		}
	}
	public	void	PostInitAllSkill(sdGameActor _gameActor)
	{
		int job	=	_gameActor.Job;
		Hashtable table	=	sdConfDataMgr.Instance().m_vecJobSkillInfo[job] as Hashtable;
		_gameActor.SkillProperty	=	sdConfDataMgr.CloneHashTable(table);
		
		
		foreach(DictionaryEntry de in AllSkill)
		{
			sdSkill skill = de.Value as sdSkill;
			if(skill != null)
			{
				skill.PostInit(_gameActor);
				if(table!=null)
				{
					Hashtable ta		=	_gameActor.SkillProperty[skill.id]	as Hashtable;
					skill.skillProperty	=	ta;
				}
			}
		}	
	}
	
	static public sdSkillTree createSkillTree(sdGameActor _gameActor, int job)
	{
		if (_gameActor == null || _gameActor.logicTSM == null)
			return null;		
		sdSkillTree tree = new sdSkillTree();
		Hashtable	allskill	=	sdConfDataMgr.Instance().m_vecJobSkillInfo[job];
		foreach(DictionaryEntry de	in allskill)
		{
			Hashtable	skill	=	de.Value	as Hashtable;
			int id			=	(int)de.Key;
			int iPassive	=	(int)skill["byIsPassive"];
				
			string	name	=	skill["strName"] as string;
			tree.Add(new sdSkill(name, iPassive==1, id));
		}		
		tree.InitAllSkill(_gameActor);
		tree.PostInitAllSkill(_gameActor);
		
		return tree;	
	}
	
	
}
