using UnityEngine;

public enum eMovieType
{
	eMT_Video,  //视频动画aa
	eMT_Scene,  //
	eMT_Dialogue,
}


public class sdMovieBase : MonoBehaviour
{
	protected eMovieType m_type;
	protected virtual void Update(){}
	protected virtual void End(){}
}