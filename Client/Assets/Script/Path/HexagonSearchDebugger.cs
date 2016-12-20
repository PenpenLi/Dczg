using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 显示寻路路径上的Hexagon以及提供相关调试参数aa
/// </summary>
public class HexagonSearchDebugger : MonoBehaviour 
{
    public bool UseMiddlePointWeight = true;
    public bool SearchTest = false;
    public float StaticWeightScale = 1.0f;
	public float DynamicWeightScale = 1.0f;
	public float StraightWeightScale = 0.5f;
	public float HeuristicWeightScale = 1.8f;
    List<GameObject> lstObject = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		StaticWeightScale = Hexagon.PathFinder.StaticWeightScale;
		DynamicWeightScale = Hexagon.PathFinder.DynamicWeightScale;
		StraightWeightScale = Hexagon.PathFinder.StraightWeightScale;
		HeuristicWeightScale = Hexagon.PathFinder.HeuristicWeightScale;
        UseMiddlePointWeight = Hexagon.PathFinder.MiddlePointWeight;
	}

    void Add(GameObject obj)
    {
        lstObject.Add(obj);
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (SearchTest)
        {
            SearchTest = false;
            
            //if(actor.actorType == ActorType.AT_Monster)
            {
				Hexagon.PathFinder.StaticWeightScale = StaticWeightScale;
				Hexagon.PathFinder.DynamicWeightScale = DynamicWeightScale;
				Hexagon.PathFinder.StraightWeightScale = StraightWeightScale;
				Hexagon.PathFinder.HeuristicWeightScale = HeuristicWeightScale;
				Hexagon.PathFinder.MiddlePointWeight = UseMiddlePointWeight;

                foreach(GameObject o in lstObject)
                {
                    GameObject.Destroy(o);
                }
                lstObject.Clear();

                sdMainChar mc = sdGameLevel.instance.mainChar;
                sdGameActor actor = GetComponent<sdGameActor>();
                if (actor != null)
                {
                    actor.UnInject(false);
                }
                mc.UnInject(false);
                List<Hexagon.SearchNode> lstSearch = new List<Hexagon.SearchNode>();
                BT.BinaryTree lstUnSearch = new BT.BinaryTree();
                Hexagon.Manager.GetSingleton().DebugFindPath(transform.position, mc.transform.position, ref lstSearch, ref lstUnSearch);
                mc.Inject(false);
                if (actor != null)
                {
                    actor.Inject(false);
                }
                Hexagon.SearchNode end = lstSearch[lstSearch.Count-1];
                for (int i = lstSearch.Count - 1; i >=0; i--)
                {
                    Hexagon.SearchNode n = lstSearch[i];
                    int type = 0;
                    if (n == end)
                    {
                        type = 2;
                        end = n.parent;
                    }
                    ushort tempHeight   =   Hexagon.Manager.GetSingleton().GetHeight(n.v);
                    Add(HexagonElement.NewElement(n.v, tempHeight, n.weight(), type));
                }
                Hexagon.SearchNode head = (Hexagon.SearchNode)lstUnSearch.Pop();
                while (true)
                {
                    if (head == null)
                    {
                        break;
                    }
                    ushort tempHeight   =   Hexagon.Manager.GetSingleton().GetHeight(head.v);
                    Add(HexagonElement.NewElement(head.v, tempHeight, head.weight(), 1));
                    head = (Hexagon.SearchNode)lstUnSearch.Pop();
                }
            }
        }
	}
}
