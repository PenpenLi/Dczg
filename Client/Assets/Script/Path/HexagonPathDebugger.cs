using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HexagonPathDebugger : MonoBehaviour {
    sdGameActor actor = null;  
    AutoFight auto = null;
    List<Vector3> walkPath=null;
    Vector3[] vertex = new Vector3[100];
    int[] face = null;
    int lastCount = 0;
    float t = 0.0f;
    GameObject renderobj = null;
	// Use this for initialization
	void Start () {
        actor   =   gameObject.GetComponent<sdGameActor>();
        if (actor == null)
        {
            actor = gameObject.GetComponent<sdGameMonster>();
        }
        if (actor != null)
        {
            auto = actor.AutoFightSystem;
            //walkPath = auto.GetCurrentPath();
        }

        

        renderobj = new GameObject();
        renderobj.name = gameObject.name + "_AI_Path";
        renderobj.transform.position = Vector3.zero;
        renderobj.transform.rotation = Quaternion.identity;

        renderobj.AddComponent<MeshFilter>();
        MeshRenderer r = renderobj.AddComponent<MeshRenderer>();
        
        Shader shader = Shader.Find("SDShader/SelfIllum_Object");
        r.material = new Material(shader);
        Vector4 color   =   new Vector4(Random.Range(0.1f,1.0f),Random.Range(0.1f,1.0f),Random.Range(0.1f,1.0f));
        r.material.SetVector("_Color", color);
	}
	
	// Update is called once per frame
	void Update () {
        List<Vector3> newPath   =   null;
        if (auto == null)
        {
            if (actor != null)
            {
                auto = actor.AutoFightSystem;
            }
        }
        if (auto != null)
        {
            newPath = auto.GetCurrentPath();
        }
        if (newPath!=null)
        {
            t += Time.deltaTime;
            if (t > 1.0f)
            {
                lastCount = 0;
                t = 0.0f;
            }
            if (newPath.Count > 0 && lastCount != newPath.Count)
            {
                renderobj.transform.position = newPath[0];

                MeshFilter mf = renderobj.GetComponent<MeshFilter>();

                Vector3 Y = new Vector3(0, 1, 0);
                int vertexcount =   (newPath.Count -1)* 4;
                if(vertex.Length < vertexcount)
                {
                    vertex = new Vector3[vertexcount];
                }
                face = new int[(newPath.Count -1)* 6];
                Vector3 vOffset = new Vector3(0, 0.05f, 0);
                for (int i = 0; i < newPath.Count-1; i++)
                {
                    Vector3 v0 = newPath[i] + vOffset - renderobj.transform.position;
                    Vector3 v1 = newPath[i + 1] + vOffset - renderobj.transform.position;

                    Vector3 dir =   (v1 - v0).normalized;
                    Vector3 right = Vector3.Cross(Y, dir); 

                    vertex[i * 4]       = v1    -   right   *   0.1f;
                    vertex[i * 4 + 1]   = v1    +   right   *   0.1f;
                    vertex[i * 4 + 2]   = v0    - right * 0.1f;
                    vertex[i * 4 + 3]   = v0    + right * 0.1f;

                    face[i*6]   =   i*4;
                    face[i*6+1]   =   i*4+1;
                    face[i*6+2]   =   i*4+2;
                    face[i*6+3]   =   i*4+2;
                    face[i*6+4]   =   i*4+1;
                    face[i*6+5]   =   i*4+3;

                }
                //mf.mesh.vertexCount = vertex.Length;
                mf.mesh.vertices = vertex;
                mf.mesh.triangles = face;
                // mf.mesh.t
                //mf.mesh.
                
            }
        }
	}
}
