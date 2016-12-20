using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This is the PocketRPG weapon trail. For best results itterate it and your animation several times a frame.
// It is based on the Tron trails Unity example
// PocketRPG trails run faster than the framerate... (default is 300 frames a second)... that is how they are so smooth (30fps trails are rather jerky)
// This code was written by Evan Greenwood (of Free Lives) and used in the game PocketRPG by Tasty Poison Games.
// But you can use this how you please... Make great games... that's what this is about.
// This code is provided as is. Please discuss this on the Unity Forums if you need help.
//
class TronTrailSection
{
	public Vector3 point;
    public Vector3 upDir;
    public float time;
	public float length;
    public TronTrailSection() {
		length = 0.0f;
    }
    public TronTrailSection(Vector3 p, float t) {
        point = p;
        time = t;
		length = 0.0f;
    }
}
[RequireComponent(typeof(MeshFilter))]
[AddComponentMenu("PocketRPG/Weapon Trail")]
public class sdWeaponTrail : MonoBehaviour {


    /*
     Generates a trail that is always facing upwards using the scriptable mesh interface. (This is from the Tron Trails in Unity)
     vertex colors and uv's are generated similar to the builtin Trail Renderer. But it DOES NOT RUN ITSELF LIKE THE TRAIL RENDERER. (there is no update method)
     To use it
     1. Create an empty game object (your weapon), with it's y axis pointing along the weapons blade.
     2. Attach this script and a MeshRenderer
     3. Then assign a particle material to the mesh renderer
     4. Call it's Itterate method everytime you sample the animation (if you want a smooth trail this should be more than once a frame)
     5. Call it's UpdateTrail method once a frame to rebuild the mesh
    */
    #region Public
    public float height = 3.0f;						//刀光宽度
    public float time = 2.0f;						//刀光尾巴长短 值越大尾巴越长
    public bool alwaysUp = false;
    public float minDistance = 0.02f;  				//采样点最小距离
	public float timeTransitionSpeed = 1f;
    public float life = 2.0f;
    public Color startColor = Color.white;			//刀光初始颜色
    public Color endColor = new Color(1, 1, 1, 0);	//刀光结束颜色
	
	public float maxDistance = 0.1f;				//刀光面片生成的最小距离
    #endregion
    //
    #region Temporary
    Vector3 position;
    float now = 0;
    TronTrailSection currentSection;
    Matrix4x4 localSpaceTransform;
	float		currentlife=2.0f;
    #endregion

    #region Internal
    private Mesh mesh;
    private Vector3[] vertices;
	private Vector3[] vertices2;
    private Color[] colors;
    private Vector2[] uv;
    #endregion

    #region Customisers 
    private MeshRenderer meshRenderer;
    private Material trailMaterial;
    #endregion
	
    private List<TronTrailSection> sections = new List<TronTrailSection>();
	
	//Test B-spline
	
	void deBoor(ref Vector3[] p,ref float[] T,int k,float t,int j,ref Vector3 V)
	{
		int i,r,temp,temp1;
		Vector3[] Q = new Vector3[10];
		float lamta;
		temp = j-k+1;
		for(i = 0; i < k; i++)
		{
			Q[i] =p[temp+i];
		}
		
		for(r = 1; r < k; r++)
			for(i = j; i >= temp+r; i--)
			{
				lamta = (t - T[i])/(T[i+k-r]-T[i]);
				temp1=i-temp;
				Q[temp1] = lamta*Q[temp1]+(1.0f-lamta)*Q[temp1-1];
			}
		V = Q[k-1];
	}
	
	void bspLine(ref Vector3[] p,ref float[] T,int n,int k,int count,ref Vector3[] poses)
	{
		int i,j;
		float deltat,t;
		Vector3 V = Vector3.zero;
		deltat = (T[n+1] - T[k-1])/count;
		t = T[k-1];
		j = k-1;
		
		deBoor(ref p,ref T,k,t,j,ref V);
		
		poses[0] = V;
		
		for(i = 1; i <= count; i++)
		{
			t = t + deltat;
			while((t > T[j+1]+0.001f) && (j < n))
				j++;
			deBoor(ref p,ref T,k,t,j,ref V);
			poses[i] = V;
		}
	}
	
    void Awake() {

        MeshFilter meshF = GetComponent(typeof(MeshFilter)) as MeshFilter;
        mesh = meshF.mesh;
        meshRenderer = GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        trailMaterial = meshRenderer.material;
    }
    public void StartTrail(float lifetime, float tailtime,float weaponLength,float fDelayTime){		
		life 		= 	lifetime;
		time		=	tailtime;
		height		=	weaponLength;
		if(weaponLength<0){
			height=3.0f;
		}
			
		currentlife	=	-fDelayTime;
		//if (time != desiredTime){
			//timeTransitionSpeed = Mathf.Abs(desiredTime -time) / fadeInTime;
		//}
		if(life<0.0f){
			life = 999999.0f;
		}
		if (time <= 0){
			time = 0.01f;
		}
    }

	public void FadeOut(float fadeTime){
		life 		= 	currentlife+fadeTime;
		//time		=	0;
		//currentlife	=	0.0f;
		//if (time >0){
			//timeTransitionSpeed = time / fadeTime;
		//}
	}
    public void SetTrailColor(Color color){
        trailMaterial.SetColor("_TintColor", color);
    }
    public void Itterate(float itterateTime) { // ** call everytime you sample animation **
   
        position = transform.position;
        now = itterateTime;
		if(currentlife<0.0f){
			return;
		}
		
		if(time < 0.0001f)
			return;
		Vector3	up = Vector3.up;
		if(!alwaysUp)
			up = Vector3.Normalize(transform.TransformDirection(Vector3.up));
		
		
		float lastDistance		=	0;//
		
		if(sections.Count>0){
			Vector3 vNear 	= 	position-sections[0].point;
			Vector3 vFar	=	(up-sections[0].upDir)*height+vNear;
			lastDistance	=	Vector3.Distance(vNear,Vector3.zero)+Vector3.Distance(vFar,Vector3.zero);
		}

        // Add a new trail section
        if (sections.Count == 0 || lastDistance > minDistance ) {
            TronTrailSection section = new TronTrailSection();
            section.point 	= 	position;
            section.upDir 	= 	up;
            section.time 	= 	now;
			
			if(sections.Count > 0)
			{
				section.length = lastDistance;
			}
            sections.Insert(0, section);
            
        }
    }
    public void UpdateTrail( float deltaTime) { // ** call once a frame **
    
		if(time < 0.0001f)
			return;
		if(life > currentlife){
			currentlife	+=deltaTime;
			if(currentlife<0){
				return;
			}
		}else{
			ClearTrail();
			return;
		}
        // Rebuild the mesh	
        mesh.Clear();
        //
        // Remove old sections
        while (sections.Count > 0 && currentlife > sections[sections.Count - 1].time + time) {
            sections.RemoveAt(sections.Count - 1);
        }
        // We need at least 2 sections to create the line
        if (sections.Count < 2)
            return;
		//
		int secCount = Mathf.Max(sections.Count,4);
        vertices = new Vector3[secCount];
		vertices2 = new Vector3[secCount];
        /*colors = new Color[sections.Count * 2];
        uv = new Vector2[sections.Count * 2];*/
		//
        currentSection = sections[0];
		//
        // Use matrix instead of transform.TransformPoint for performance reasons
        localSpaceTransform = transform.worldToLocalMatrix;

        // Generate vertex, uv and colors
        for (var i = 0; i < sections.Count; i++) {
			//
            currentSection = sections[i];
            // Calculate u for texture uv and color interpolation
            /*float u = 0.0f;
            if (i != 0)
                u = Mathf.Clamp01((currentTime - currentSection.time) / time);*/
			//
            // Calculate upwards direction
            Vector3 upDir = currentSection.upDir;

            // Generate vertices
            vertices[i /** 2 + 0*/] = /*localSpaceTransform.MultiplyPoint(*/currentSection.point/*)*/;
            vertices2[i /** 2 + 1*/] = /*localSpaceTransform.MultiplyPoint(*/currentSection.point + upDir * height/*)*/;

            /*uv[i * 2 + 0] = new Vector2(u, 0);
            uv[i * 2 + 1] = new Vector2(u, 1);

            // fade colors out over time
            Color interpolatedColor = Color.Lerp(startColor, endColor, u);
            colors[i * 2 + 0] = interpolatedColor;
            colors[i * 2 + 1] = interpolatedColor;*/
        }
		
		for(int i = sections.Count ; i < 4; i++)
		{
			vertices[i] = vertices[sections.Count - 1];
			vertices2[i] = vertices2[sections.Count - 1];
		}
		
		float totalLen = 0.0f;
		for(int i = 0; i < sections.Count - 1; i++)
		{
			totalLen += sections[i].length;
		}
		
		int lNum = (int)(totalLen / maxDistance);
		if(lNum < secCount-1)
			lNum = secCount-1;
		
		if(lNum > 256)
			lNum = 256;
		
		int k1 = 4;
		int num = secCount;
		float delta = 1.0f;
		float tv = 0.0f;
		
		float[] bspT = new float[num+k1];
		
		for(int i = k1-1; i < num + k1 - 3; i++)
		{
			bspT[i] = tv;
			tv += delta;
		}
		for(int i = 0; i < k1-1; i++)
		{
			bspT[i] = bspT[3];
		}
		for(int i = num + k1 - 3; i < num+k1; i++)
		{
			bspT[i] = bspT[num];
		}
		
		Vector3[] pointBuffer = new Vector3[lNum + 1]; 
		bspLine(ref vertices,ref bspT,num-1,k1,lNum,ref pointBuffer);
		Vector3[] pointBuffer2 = new Vector3[lNum + 1]; 
		bspLine(ref vertices2,ref bspT,num-1,k1,lNum,ref pointBuffer2);
		
		vertices = new Vector3[lNum * 2 + 2];
		
		colors = new Color[lNum * 2 + 2];
        uv = new Vector2[lNum * 2 + 2];
		
		float maxU = 0.0f;
		
		if(sections.Count > 0)
		{
			maxU = Mathf.Clamp01((currentlife - sections[sections.Count-1].time) / time);
		}
		
		for(int i = 0; i < lNum + 1; i++)
		{
			vertices[i*2] = localSpaceTransform.MultiplyPoint(pointBuffer[i]);
			vertices[i*2 + 1] = localSpaceTransform.MultiplyPoint(pointBuffer2[i]);
			
			float u = (i/((float)lNum)) * maxU;
			uv[i * 2 + 0] = new Vector2(u, 0);
            uv[i * 2 + 1] = new Vector2(u, 1);
			
			Color interpolatedColor = Color.Lerp(startColor, endColor, u);
            colors[i * 2 + 0] = interpolatedColor;
            colors[i * 2 + 1] = interpolatedColor;
		}
		

        // Generate triangles indices
        int[] triangles = new int[(/*sections.Count - 1*/lNum) * 2 * 3];
        for (int i = 0; i < triangles.Length / 6; i++) {
            triangles[i * 6 + 0] = i * 2;
            triangles[i * 6 + 1] = i * 2 + 1;
            triangles[i * 6 + 2] = i * 2 + 2;

            triangles[i * 6 + 3] = i * 2 + 2;
            triangles[i * 6 + 4] = i * 2 + 1;
            triangles[i * 6 + 5] = i * 2 + 3;
        }

        // Assign to mesh	
        mesh.vertices 	= vertices;
        mesh.colors 	= colors;
        mesh.uv 		= uv;
        mesh.triangles 	= triangles;
        //
        // Tween to the desired time
        //
		/*
        if (time > desiredTime){
			time -= deltaTime*timeTransitionSpeed;
			if(time <= desiredTime) time = desiredTime;
        } else if (time < desiredTime){
			time += deltaTime*timeTransitionSpeed;
			if(time >= desiredTime) time = desiredTime;
        }*/
    }
    public void ClearTrail() {
		life = 0;
		time = 0;
		currentlife=0;
        if (mesh != null) {
            mesh.Clear();
            sections.Clear();
        }
    }
	void	LateUpdate(){
		
		Itterate(Time.deltaTime);
		UpdateTrail(Time.deltaTime);
	}
}


