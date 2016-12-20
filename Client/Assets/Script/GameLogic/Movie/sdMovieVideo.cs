using UnityEngine;
using System.Collections;

public class sdMovieVideo : sdMovieBase
{	
	void Start ()
	{
	}
	
	void OnGUI()
	{
	}
	
	protected override void Update()
	{
	}
		
	public bool PlayMovie(string strFileName)
	{
		return Handheld.PlayFullScreenMovie(Application.persistentDataPath + "/Movie__" + strFileName, Color.black, FullScreenMovieControlMode.CancelOnInput);
	}
}
