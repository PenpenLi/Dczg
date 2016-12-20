using UnityEngine;
using System.Collections;

public class sdLevelAddBuff : sdPhysicsTrigger
{
    public int EnterBuffID = 0;
    public int ExitBuffID = 0;
	// Update is called once per frame
    protected override void WhenEnterTrigger(GameObject obj, int[] param)
    {
        if (EnterBuffID == 0)
        {
            return;
        }
        sdGameActor actor = obj.GetComponent<sdGameActor>();
        if (actor == null)
        {
            return;
        }
        actor.AddBuff(EnterBuffID, 0, actor);
        if (OnceLife)
        {
            live = false;
        }
    }

    protected override void WhenLeaveTrigger(GameObject obj, int[] param)
    {
        if (ExitBuffID == 0)
        {
            return;
        }
        sdGameActor actor = obj.GetComponent<sdGameActor>();
        if (actor == null)
        {
            return;
        }
        actor.AddBuff(ExitBuffID, 0, actor);
        if (OnceLife)
        {
            live = false;
        }
    }
}
