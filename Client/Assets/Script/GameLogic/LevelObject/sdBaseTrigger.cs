using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public	class TriggerParam
{
	public	int[] v=	new int[4];
	public	int this[int idx]
	{
		get
		{
			return v[idx];
		}
		set
		{
			v[idx]	=	value;
		}
	}
}
public class sdBaseTrigger : sdTriggerReceiver {
	
	public bool OnceLife = false;//为真则在leave之后该trigger不再会发生任何响应...
	protected bool live = true;
	
	public sdTriggerReceiver[] enterReceivers;
	public string[] enterParams;
	protected List<TriggerParam> iEnterParams= new List<TriggerParam>();

	
	
	public sdTriggerReceiver[] leaveReceivers;
	public string[] leaveParams;
	protected List<TriggerParam> iLeaveParams= new List<TriggerParam>();
	
	public List<EventDelegate> onEnter = new List<EventDelegate>();
	public List<EventDelegate> onLeave = new List<EventDelegate>();
	
	protected virtual void Awake()
	{
		if((enterParams != null) && enterParams.Length > 0)
		{
	
			
			for(int i = 0; i < enterParams.Length; i++)
			{
				TriggerParam param = new TriggerParam();
				
				if(enterParams[i].Length > 0)
				{
					string[] splited = enterParams[i].Split(new char[]{','});
					
					if(splited.Length > 0)
						param[0] = int.Parse(splited[0]);
					
					if(splited.Length > 1)
						param[1]= int.Parse(splited[1]);
					
					if(splited.Length > 2)
						param[2] = int.Parse(splited[2]);
					
					if(splited.Length > 3)
						param[3] = int.Parse(splited[3]);
				}
				iEnterParams.Add(param);
			}
		}
		
		if((leaveParams != null) && leaveParams.Length > 0)
		{
		
			
			for(int i = 0; i < leaveParams.Length; i++)
			{
				TriggerParam param = new TriggerParam();
				
				if(leaveParams[i].Length > 0)
				{
					string[] splited = leaveParams[i].Split(new char[]{','});
					
					if(splited.Length > 0)
						param[0] = int.Parse(splited[0]);
					
					if(splited.Length > 1)
						param[1] = int.Parse(splited[1]);
					
					if(splited.Length > 2)
						param[2] = int.Parse(splited[2]);
					
					if(splited.Length > 3)
						param[3] = int.Parse(splited[3]);
				}
				iLeaveParams.Add(param);
			}
		}
	}
	
	// Use this for initialization
	protected virtual	void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
	
	protected virtual void WhenEnterTrigger(GameObject obj,int[] param)
	{
		EventDelegate.Execute(onEnter);
		onEnter.Clear();
	}
	
	protected virtual void WhenLeaveTrigger(GameObject obj,int[] param)
	{
		EventDelegate.Execute(onLeave);
		onLeave.Clear();
		if(OnceLife)
		{
			live = false;
		}
	}
}
