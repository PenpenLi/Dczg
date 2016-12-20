
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdFriAvatar : sdLoginChar
{
	bool isChange = false;
	
	void Start()
	{

	}
	
	override public void tickFrame()
	{
		base.tickFrame();
		if (!isChange)
		{
			ChangeLayer(gameObject);
			isChange = true;
		}
	}
	
	protected override void NotifyFinishLoadAni(string name)
	{
		if (name == kIdleAnim)
		{
			ChangeLayer(gameObject);
		}
	}
	
	protected override void NotifySkinnedMeshChanged(SkinnedMeshRenderer kSkinnedMeshRenderer)
	{
		base.NotifySkinnedMeshChanged(kSkinnedMeshRenderer);
		kSkinnedMeshRenderer.gameObject.layer = LayerMask.NameToLayer("NGUI");
	}
    protected override void NotifyStaticMeshChanged(MeshRenderer kSkinnedMeshRenderer)
    {
        base.NotifyStaticMeshChanged(kSkinnedMeshRenderer);
        kSkinnedMeshRenderer.gameObject.layer = LayerMask.NameToLayer("NGUI");
    }
	
	void ChangeLayer(GameObject obj)
	{
		int num = obj.transform.childCount;
		for(int i = 0; i < num; ++i)
		{
			Transform child = obj.transform.GetChild(i);
			child.gameObject.layer = LayerMask.NameToLayer("NGUI");
			ChangeLayer(child.gameObject);	
		}

//		MeshRenderer[] list1 = obj.GetComponentsInChildren<MeshRenderer>();
////		obj.layer = LayerMask.NameToLayer("UI");
////		int cout = obj.transform.childCount;
//		for(int i = 0; i < cout; ++i)
//		{
//			ChangeLayer(obj.transform.GetChild(i).gameObject);	
//		}
	}
}