using UnityEngine;
using System.Collections;

public class sdDropItem : MonoBehaviour {

	int itemId = -1;
	int money = 0;

	Vector3 dest;
	Vector3 moveDir;
	bool moving = true;
	bool movingToChar = false;

	float waitTime 	= 0.5f;
	float moveTime	=	0.667f;
	float moveSpeed = 0.0f;
	float tmpTime = 0.0f;

	GameObject mainChar;

	public

	void Awake()
	{
		mainChar = sdGameLevel.instance.mainChar.gameObject;
	}

	// Use this for initialization
	void Start () {
		movingToChar = true;
	}

	// Update is called once per frame
	void Update () {
		if(moving)
		{
			tmpTime += Time.deltaTime;
			if(tmpTime > waitTime+moveTime)
			{
				moving = false;
				transform.position = dest;
			}
			else
			{
				if(tmpTime<moveTime)
				{
					transform.position = transform.position + moveDir * moveSpeed * Time.deltaTime;
				}
			}
		}
		else if(movingToChar)
		{
			Vector3 charDest = mainChar.transform.position;
			charDest.y += 1.0f;

			float distance = Vector3.Distance(charDest,transform.position);
			Vector3 tmpMoveDir = charDest - transform.position;
			tmpMoveDir/= distance;

			if(distance < 10.0f * Time.deltaTime)
			{
				SDGlobal.tmpBag.money += money;
				if (money != 0) sdUICharacter.Instance.AddLootMoney(money);
				if(itemId >= 0)
					SDGlobal.tmpBag.AddItem(itemId);

				GameObject.Destroy(gameObject);
			}
			else
				transform.position = transform.position + tmpMoveDir * 20.0f * Time.deltaTime;
		}
	}

	public void SetDest(Vector3 dt)
	{
		dest = dt;
		float dis = Vector3.Distance(transform.position,dest);
		moveSpeed = dis / moveTime;
		moveDir = dest - transform.position;
		moveDir.Normalize();
	}

	public void SetItemId(int id)
	{
		itemId = id;
	}

	public void SetMoney(int m)
	{
		money = m;
	}

}
