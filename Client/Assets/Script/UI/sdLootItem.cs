using UnityEngine;
using System.Collections;
using System;

public class sdLootItem : MonoBehaviour
{
	public GameObject icon = null;
	public GameObject name = null;
	public int index = -1;
	private int id = 0;
	public float lifetiem = 0;
	public bool isOut = false;
	public LootType lootType; 

	public void Start()
	{
		GetComponent<TweenAlpha>().enabled = false;
        GetComponent<TweenAlpha>().onFinished.Add(new EventDelegate(OnFinish));
	}

    public bool isFinish = false;
    void OnFinish()
    {
        isFinish = true;
    }
	
	bool hasAtlas = false;
	int iconId = -1;
    int addMoney = 0;
    int maxMoney = 0;
    float time = 0;
	void Update()
	{
        if (addMoney > 0)
        {
            time += Time.deltaTime;
            if (time > 0.1)
            {
                int delt = addMoney/5;
                if (delt == 0) delt = 1;
                id += delt;
                if (id >= maxMoney)
                {
                    id = maxMoney;
                    addMoney = 0;
                }
                name.GetComponent<UILabel>().text = id.ToString();
            }
        }
	}

	void OnSetAtlas(ResLoadParams param, UnityEngine.Object obj)
	{
		gameObject.transform.FindChild("icon").GetComponent<UISprite>().atlas = obj as UIAtlas;;
	}

    void OnSetPetAtlas()
    {
        icon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;	
    }

    public void RefreshMoney(int money)
    {
        addMoney = money;
        maxMoney = id + money;
        name.GetComponent<UILabel>().text = id.ToString();
        GetComponent<TweenAlpha>().Reset();
        lifetiem = Time.time;
        isOut = false;

    }
	
	public void SetInfo(LootType type, int itemId)
	{
        lootType = type;
		if (type == LootType.Item)
		{
			id = itemId;
			Hashtable info = sdConfDataMgr.Instance().GetItemById(id.ToString());
			if (info == null) return;
			
			if (icon != null)
			{
				int itemType = int.Parse(info["Class"].ToString());
				
				if (itemType == (int)HeaderProto.EItemClass.ItemClass_Pet_Item)
				{
					Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(info["Expend"].ToString());
					icon.GetComponent<UISprite>().spriteName = petInfo["Icon"].ToString();
                    if (sdConfDataMgr.Instance().PetAtlas != null)
                        icon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;	
                    else
                        sdConfDataMgr.Instance().LoadPetAtlas(new EventDelegate(OnSetPetAtlas));
					
				}
				else
				{
					icon.GetComponent<UISprite>().spriteName = info["IconPath"].ToString();
					iconId = int.Parse(info["IconID"].ToString());
                    sdConfDataMgr.Instance().LoadItemAtlas(iconId.ToString(), OnSetAtlas);
				}
			}
		
			if (name != null)
			{
				name.GetComponent<UILabel>().text = info["ShowName"].ToString();
				name.GetComponent<UILabel>().color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(info["Quility"].ToString()));
			}
		}
		else
		{
			if (icon != null)
			{
                icon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().commonAtlas;
				icon.GetComponent<UISprite>().spriteName = "icon_jinb";
			}
			if (name != null)
			{
                id = itemId;
				name.GetComponent<UILabel>().text = itemId.ToString();
			}
		}
		lifetiem = Time.time;
		
		if (isOut)
		{
			GetComponent<TweenAlpha>().enabled = false;
			GetComponent<TweenAlpha>().Reset();
            isFinish = false;
			isOut = false;
		}
	}
}
