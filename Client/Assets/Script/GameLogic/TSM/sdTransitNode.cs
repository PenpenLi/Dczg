using UnityEngine;
using System.Collections;
using System.Reflection;

public class sdTransitNode : object 
{
	public static 	int TEMP_NODE_BASE_ID 	= 	100000;
	public static	int	DefaultID			=	0;
	public sdTransitNode(string info_, int priority_)
	{
		id = DefaultID++; info = info_; priority = priority_;
	}
	public string info = "";
	public int id = -1;
	public int priority = 0;
	public sdBaseState fromState = null;
	public sdBaseState destState = null;
	
	public virtual bool cond(sdGameActor _gameActor)
	{
		return false;
	}
	
	public virtual bool exe(sdGameActor _gameActor)
	{
		return false;
	}
}
