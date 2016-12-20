using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttachedEffect
{
	public GameObject effect = null;
	public float lifeTime = 0.0f;
	public float life = 0.0f;
	public string path;
}
	
//真正的角色结点放在子节点//因为UNITY的root motion有个问题就是不随缩放改变
//所以我们根据父节点的缩放值和子节点的localPosition共同生成rootMotion的移动
[AddComponentMenu("SNDA/GameLogic/SDPlayer")]
public class SDPlayer : MonoBehaviour {
	public Vector3 initDir = new Vector3(0.0f,0.0f,1.0f);//初始方向
	private Vector3 currentDir;//当前运动方向
	private Vector3 lastFaceDir;//旋转之前面对的朝
	private Vector3 currentFaceDir;//当前面对的朝	
	private float rotLerp;//旋转插值系	
	private Vector3 targetPos;//移动的目标位
	private float runSpeed;//用于控制动画融合的速度参数
	private bool running;
	//角色放技能时禁止移动
	private bool skillLock = false;
	
	private Vector3 frameMove;//移动向量
	
	private GameObject renderNode;
	private Animator anim;
	public GameObject[] renderMeshes;
	
	private int MaxHP = 100;
	public int HP = 100;
	public int MP = 100;
	public float DisapearTime = 3.0f;//死后多久消失
	private bool disapeared = false;
	
	private bool isMainChar = false;
	private bool isHitted = false;
	private int	hitType = 0;//1=抖动,2=硬直,3=击飞
	private float hitPriority =0.0f;
	private Vector3 beatBackDir;//击退方向
	private float beatBackV = 0.0f;//击退速度
	private bool beatBack = false;
	
	//受击反白
	private float hitLightTmp = 0.0f;
	private bool hitLight = false;
	private Color hitColor = new Color(0.0f,0.0f,0.0f,1.0f);
	
	
	//private List<AttachedEffect> attachedEffs = new List<AttachedEffect>();
	
	private GameObject hpUI;
	private GameObject hpSprite;
	private UIAnchor hpAnchor;
	private bool hpUIActive = false;
	Vector2 hpDimention;
	
	private Transform dummyIcon;
	
	private NavMeshAgent navAgent;
	
	void Awake()
	{
		//SystemInfo.deviceUniqueIdentifier;
		currentDir =  transform.TransformDirection(initDir);
		lastFaceDir = currentDir;
		currentFaceDir = lastFaceDir;
		rotLerp = 1.0f;
		targetPos = transform.position;
		runSpeed = 0.0f;
		running = false;
		frameMove = new Vector3(0,0,0);
		
		//isMainChar = (GetComponent<SDMainChar>() != null);
		
		dummyIcon = findBone(transform,"$FxIcon");
		
		MaxHP = HP;
		
		navAgent = gameObject.GetComponent<NavMeshAgent>();
		
		renderNode = transform.FindChild("@RenderNode").gameObject;
		anim = renderNode.GetComponent<Animator>();
	}
	
	// Use this for initialization
	void Start () {
	}
	
	void FixedUpdate()
	{
		if(isMainChar)
			return;
			
		Vector3 disVec = targetPos - transform.position;
		disVec.y = 0;
		
		//frameMove =  new Vector3(0,0,0);
		if(renderNode != null)
		{
			frameMove = renderNode.transform.position - transform.position;
			frameMove = new Vector3(frameMove.x * transform.localScale.x,
				frameMove.y * transform.localScale.y,frameMove.z * transform.localScale.z);
			
				renderNode.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		}
		
		if(rotLerp < 0.9999f)
		{
			rotLerp += Time.deltaTime * 5;
			
			if(rotLerp >= 1.0f)
			{
				if(Vector3.Dot(initDir,currentDir) < -0.999f)
					transform.rotation = Quaternion.Euler(new Vector3(0.0f,180.0f,0.0f));
				else
					transform.rotation = Quaternion.FromToRotation(initDir,currentDir);
				lastFaceDir = currentDir;
				rotLerp = 1.0f;
				currentFaceDir = lastFaceDir;
			}
			else
			{
				currentFaceDir = Vector3.Slerp(lastFaceDir,currentDir,rotLerp);
				currentFaceDir.Normalize();
				if(Vector3.Dot(initDir,currentFaceDir) < -0.999f)
					transform.rotation = Quaternion.Euler(new Vector3(0.0f,180.0f,0.0f));
				else
					transform.rotation = Quaternion.FromToRotation(initDir,currentFaceDir);
			}
		}
		
		frameMove += disVec;
		
		if(beatBack && beatBackV > 0.001f)
		{
			frameMove += beatBackDir * beatBackV * Time.deltaTime;
			beatBackV -= SDGlobal.beatBackA * Time.deltaTime;
			
			if(beatBackV < 0.0f)
			{
				beatBackV = 0.0f;
				beatBack = false;
			}
		}
		
		if(disVec.x*disVec.x + disVec.z*disVec.z > 0.1f && running)
		{
			runSpeed += 4.0f * Time.deltaTime;
			if(runSpeed > 1.0f)
				runSpeed = 1.0f;
			
			if(anim != null)
				anim.SetFloat("runspeed",runSpeed);
		}
		else
		{
			running = false;
			runSpeed -= 8.0f * Time.deltaTime;
			if(runSpeed < 0.0f)
				runSpeed = 0.0f;
			if(anim != null)
				anim.SetFloat("runspeed",runSpeed);
		}
		
		//if(charCtrl != null)
		//	charCtrl.Move(frameMove);
		//navAgent.SetDestination(transform.position + frameMove);
	}
	
	public void DirectRotate(Vector3 dir)
	{
		dir.y = 0.0f;
		dir.Normalize();
		currentDir = dir;
		if(Vector3.Dot(initDir,currentDir) < -0.999f)
				transform.rotation = Quaternion.Euler(new Vector3(0.0f,180.0f,0.0f));
		else
				transform.rotation = Quaternion.FromToRotation(initDir,currentDir);
		lastFaceDir = currentDir;
		rotLerp = 1.0f;
		currentFaceDir = lastFaceDir;
	}
	
	public void setAnimatorParam(string name,float val)
	{
		anim.SetFloat(name,val);
	}
	
	public void setAnimatorParam(int hash,float val)
	{
		anim.SetFloat(hash,val);
	}
	
	public void setAnimatorParam(string name,int val)
	{
		anim.SetInteger(name,val);
	}
	
	public void setAnimatorParam(int hash,int val)
	{
		anim.SetInteger(hash,val);
	}
	
	public void setAnimatorParam(string name,bool val)
	{
		anim.SetBool(name,val);
	}
	
	public void setAnimatorParam(int hash,bool val)
	{
		anim.SetBool(hash,val);
	}
	
	// Update is called once per frame
	void Update () {
		if(isMainChar)
			return;
		
		UpdateHitLight();
		//UpdateEffect();
		//UpdateHPUIPos();
		if(HP == 0)
		{
			DestroyHPUI();
			DisapearTime -= Time.deltaTime;
			if(DisapearTime < 0.0f)
			{
				DisapearTime = 0.0f;
				disapeared = true;
			}
		}
		
		//test go to mainchar
		//GameObject mainChar = GameObject.Find("@MainCharacter");
		//if(gameObject != mainChar)
		//runToTarget(mainChar.transform.position);
	}
	
	public bool isDisapeared()
	{
		return disapeared;
	}
	
	public void setSkillLock(bool b)
	{
		skillLock = b;
	}
	
	public void stopRunning()
	{
		running = false;
		runSpeed = 0.0f;		
		setAnimatorParam("runspeed",runSpeed);
	}
	
	public float getRadius()
	{
		return navAgent.radius * transform.localScale.x;
	}
	
	public Vector3 getCurrentDir()
	{
		return currentDir;
	}
	
	public Vector3 getFaceDir()
	{
		return currentFaceDir;
	}
	
	public bool doHit(float power,Vector3 dir)
	{
		return false;
	}
	
	public void runToTarget(Vector3 pos)
	{
		if(skillLock)
		{
			targetPos = pos;
			Vector3 dir2 = pos - transform.position;
			dir2.y = 0;
			if(Mathf.Abs(dir2.x) > 0.01f &&
				Mathf.Abs(dir2.z) > 0.01f)
			{
				dir2.Normalize();
				//if(Mathf.Abs(Vector3.Angle(dir2,currentDir)) > 10.0f)
				{
					rotLerp = 0.0f;
					lastFaceDir = currentFaceDir;
				}
				currentDir = dir2;
			}
			return;
		}
		targetPos = pos;
		Vector3 dir = pos - transform.position;
		dir.y = 0;
		if(Mathf.Abs(dir.x) > 0.01f &&
				Mathf.Abs(dir.z) > 0.01f)
		{
			dir.Normalize();
			//if(Mathf.Abs(Vector3.Angle(dir,currentDir)) > 10.0f)
			{
				rotLerp = 0.0f;
				lastFaceDir = currentFaceDir;
			}
			currentDir = dir;
		}
		running = true;
	}
	
	public Vector3 getCenterPos()
	{
		return transform.position;
	}
	
	private Transform findBone(Transform parent,string name)
	{
		Transform result = parent.Find(name);
		if(result != null)
			return result;
		
		for(int i = 0; i < parent.childCount; i++)
		{
			result = findBone(parent.GetChild(i),name);
			if(result != null)
				return result;
		}
		
		return null;
	}
	
	public bool isRunning()
	{
		return running;
	}
	
	public bool GetIsHitted()
	{
		return isHitted;
	}
	
	public void SetIsHitted(bool b)
	{
		isHitted = b;
	}
	
	public int GetHitType()
	{
		return hitType;
	}
	
	public float GetHitPriority()
	{
		return hitPriority;
	}
	
	public void SetHitType(int type,float priority)
	{
		hitType = type;
		hitPriority = priority;
	}
	
	public void BeatBack(Vector3 sourcePos,float distance)
	{
		beatBackDir = transform.position - sourcePos;
		beatBackDir.y = 0.0f;
		beatBackDir.Normalize();
		
		beatBackV = Mathf.Sqrt(distance*SDGlobal.beatBackA*2);
	}
	
	public void EnableBeatBack()
	{
		beatBack = true;
	}
	
	public float GetBeatBackV()
	{
		return beatBackV;
	}
	
	public Vector3 GetBeatBackDir()
	{
		return beatBackDir;
	}
	
	public void DoHitLight(Color hitLightColor)
	{
		hitLightTmp = 0.0f;
		hitLight = true;
		hitColor = hitLightColor;
	}
	
	protected void UpdateHitLight()
	{
		if(hitLight)
		{
			hitLightTmp += Time.deltaTime;
			
			float lightTime = 0.15f;
			
			float lightParam = (hitLightTmp/lightTime)*Mathf.PI;
			
			if(lightParam > Mathf.PI)
			{
				hitLight = false;
				hitLightTmp = 0.0f;
				lightParam = 0.0f;
			}
			
			lightParam = Mathf.Sin(lightParam);
			for(int i = 0; i < renderMeshes.Length; i++)
			{
				if(renderMeshes[i] != null)
				{
					MeshRenderer meshRenderer =  renderMeshes[i].GetComponent<MeshRenderer>();
					SkinnedMeshRenderer skinMeshRenderer = renderMeshes[i].GetComponent<SkinnedMeshRenderer>();
					
					if(meshRenderer != null)
					{
						for(int k = 0; k < meshRenderer.materials.Length; k++)
						{
							if(meshRenderer.materials[k] != null)
							{
								meshRenderer.materials[k].SetColor("_AddColor",hitColor*lightParam);
							}
						}
					}
					
					if(skinMeshRenderer != null)
					{
						for(int k = 0; k < skinMeshRenderer.materials.Length; k++)
						{
							if(skinMeshRenderer.materials[k] != null)
							{
								skinMeshRenderer.materials[k].SetColor("_AddColor",hitColor*lightParam);
							}
						}
					}
				}
			}	
		}
	}
	
//	public void AddEffect(string anchor,string path,float life,bool follow)
//	{
//		Transform boneTrans = findBone(transform,anchor);
//		if(boneTrans == null)
//			boneTrans = transform;
//		
//		AttachedEffect eff = new AttachedEffect();
//		eff.effect = (GameObject)GameObject.Instantiate(Resources.Load(path));
//		eff.path = path;
//		if(follow)
//		{
//			eff.effect.transform.parent = boneTrans;
//			eff.effect.transform.localPosition = Vector3.zero;
//			eff.effect.transform.localRotation = Quaternion.identity;
//			eff.effect.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
//		}
//		else
//			eff.effect.transform.position = boneTrans.position;
//		eff.lifeTime = 0.0f;
//		eff.life = life;
//		
//		for(int i = 0; i < attachedEffs.Count; i++)
//		{
//			if(attachedEffs[i] == null)
//			{
//				attachedEffs[i] = eff;
//				return;
//			}
//		}
//		
//		attachedEffs.Add(eff);
//	}
	
//	private void UpdateEffect()
//	{
//		for(int i = 0; i < attachedEffs.Count; i++)
//		{
//			if(attachedEffs[i] != null)
//			{
//				attachedEffs[i].lifeTime += Time.deltaTime;
//				
//				if(attachedEffs[i].lifeTime > attachedEffs[i].life)
//				{
//					GameObject.Destroy(attachedEffs[i].effect);
//					attachedEffs[i] = null;
//					//attachedEffs[i].effect.SetActive(false);
//				}
//			}
//		}
//	}
	
	/*public void UpdateHPUIPos()
	{
		if(!hpUIActive)
			return;
		
		Vector3 pos = getCenterPos();
		
		if(dummyIcon != null)
		{
			pos = dummyIcon.position;
		}
		
		Camera c = SDScriptInterface.getMainCamera().camera;
		Vector3 viewPos = c.WorldToViewportPoint(pos);
		
		float hpScale = 1.0f;
		
		if(MaxHP > 0)
		{
			hpScale = (float)HP/(float)MaxHP;
			hpSprite.transform.localScale = new Vector3(hpScale,hpUI.transform.localScale.y,hpUI.transform.localScale.z);
		}
		
		float relX = - hpDimention.x*(1.0f- hpScale)*0.5f;
		
		hpAnchor.relativeOffset = new Vector2(viewPos.x,viewPos.y);
		
		hpSprite.transform.localPosition = new Vector3(relX,0.0f,0.0f);
	}*/
	
	public void ActiveHPUI()
	{
		if(hpUIActive)
			return;
		
		hpUI = (GameObject)GameObject.Instantiate(Resources.Load("Ruantianlong/HPUI"));
		
		GameObject uiRoot = GameObject.Find("@UI");
		Transform panel = findBone(uiRoot.transform,"Panel");
		hpAnchor = hpUI.GetComponent<UIAnchor>();
		hpUI.transform.parent = panel;
		hpUI.transform.localPosition = Vector3.zero;
		hpUI.transform.localRotation = Quaternion.identity;
		hpUI.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		//hpUI.SetActive(false);
		hpUIActive = true;
		//hpUI.SetActive(true);
		hpDimention = new Vector2(100.0f/**Screen.width/1280.0f*/,8.0f);
		hpSprite = hpUI.transform.GetChild(0).gameObject;
	}
	
	public void DestroyHPUI()
	{
		if(hpUI != null)
		{
			GameObject.Destroy(hpUI);
			hpUI = null;
			hpUIActive = false;
		}
	}
}