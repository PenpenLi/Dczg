using UnityEngine;
using System.Collections;

public class DoUpdatePet : MonoBehaviour 
{
	public ReadyButton readyBtn;
	public ChoosePetButton pet1;
	public ChoosePetButton pet2;
	public ChoosePetButton pet3;
	public ChoosePetButton pet4;
	public ChoosePetButton pet5;
	public ChoosePetButton pet6;
	public ChoosePetButton pet7;
	public ChoosePetButton pet8;
	public ChoosePetButton pet9;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	// 从宠物窗口返回，更新宠物状态.
	public void UpdatePet()
	{
		pet1.UpdatePet();
		pet2.UpdatePet();
		pet3.UpdatePet();
		pet4.UpdatePet();
		pet5.UpdatePet();
		pet6.UpdatePet();
		pet7.UpdatePet();
		pet8.UpdatePet();
		pet9.UpdatePet();
		readyBtn.UpdatePetTeam();
	}
}
