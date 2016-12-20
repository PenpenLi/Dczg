using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public partial class sdTransitGraph : object
{
	public sdTransitGraph()
	{
		states 			= new Hashtable();
		transitNodes 	= new Hashtable();
	}
	
	public sdBaseState defaultState = null;

	
	public Hashtable states;
	public Hashtable transitNodes;
	
	public void tick()
	{
	}

	public	sdTransitNode	AddNode(string	str)
	{
		sdTransitNode node = (sdTransitNode)CreateObject(str);
		if(node!=null)
		{
			transitNodes[node.id] = node;
		}
		return node;
	}
	public	sdTransitNode	AddNode(string str,sdBaseState src,sdBaseState dest)
	{
		sdTransitNode	node	=	AddNode(str);
		node.fromState	=	src;
		node.destState	=	dest;
		src.transitPath.Add(node);
		return node;
	}
	static	Assembly 	mainAssembly = null;
	public	static	Assembly	GetMainAssembly()
	{
		if(mainAssembly==null){
			Assembly[] as1 = AppDomain.CurrentDomain.GetAssemblies();
			
			foreach(Assembly a in as1)
			{
				AssemblyName na = a.GetName();
				if(na.Name == "Assembly-CSharp")
				{
					mainAssembly = a;
					break;
				}
			}
		}
		return mainAssembly;
	}
	public static MethodInfo findStaticFuncByName(string funcName)
	{
		string[] ff = funcName.Split('.');
		Assembly ass	=	GetMainAssembly();
			
		if(ass != null)
		{
			Type t = ass.GetType(ff[0]);						
			if(t != null)
			{
				MethodInfo method = t.GetMethod(ff[1]);
				if(method != null)
				{
					return method;
					//method.Invoke(null, new object[1]);
				}
			}			
		}
		
		return null;
	}
	public	static	object	CreateObject(string str)
	{
		Assembly ass	=	GetMainAssembly();
		if(ass != null)
		{
			Type t = ass.GetType(str);						
			if(t != null)
			{
				return System.Activator.CreateInstance(t);
			}			
		}
		return null;
	}
}
