public class sdMovieMgr :object
{
	private static sdMovieMgr instance = null;
	
	public static sdMovieMgr Instance()
	{
		if(instance == null)
			instance = new sdMovieMgr();
		return instance;
	}
	
	public void Update()
	{
	}
	
	public void Start(eMovieType type)
	{
	}
}