using UnityEngine;
using System.Collections;

public class globalInterface : MonoBehaviour {

	public static sdGameLevel currentGameLevel = null;
	public static sdMainChar mainChar = null;

	
	public static sdGameLevel getCurrentLevel()
	{
		if(globalInterface.currentGameLevel != null)
			return globalInterface.currentGameLevel;
		
		currentGameLevel = sdGameLevel.instance;
		return currentGameLevel;
	}
	
	public static sdMainChar getMainCharacter()
	{
		return sdGameLevel.instance.mainChar;
	}
}
