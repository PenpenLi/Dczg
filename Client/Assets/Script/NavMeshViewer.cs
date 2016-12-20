using UnityEngine;
using System.Collections;

public class NavMeshViewer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3[] v  = null; 
        int[]  f = null;
        NavMesh.Triangulate(out v, out f);
        if (f != null)
        {
            MeshFilter mf   =   gameObject.AddComponent<MeshFilter>();
            MeshRenderer r = gameObject.AddComponent<MeshRenderer>();
            mf.mesh = new Mesh();
            mf.mesh.vertices = v;
            mf.mesh.triangles = f;
        }
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
