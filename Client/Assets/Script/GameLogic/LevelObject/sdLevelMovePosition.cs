using UnityEngine;
using System.Collections;

public class sdLevelMovePosition : sdPhysicsTrigger
{
	public	GameObject	target			=	null;

	void	Start()
	{
		GetComponent<Collider>().isTrigger	=	true;
	}

    protected override void WhenEnterTrigger(GameObject obj, int[] param)
    {
        base.WhenEnterTrigger(obj, param);
        if (OnceLife)
        {
            live = false;
        }

        DoMove(obj);
    }
    void DoMove(GameObject obj)
	{
        if (OnceLife)
        {
            live = false;
        }
		if(target!=null)
		{
			obj.transform.position	=	target.transform.position;
		}
	}
}
