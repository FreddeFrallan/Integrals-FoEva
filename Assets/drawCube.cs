using UnityEngine;
using System.Collections;

public class drawCube : MonoBehaviour {

	RequireComponent MeshFilter;
	MeshFilter theMeshFilter;
	Mesh theMesh;

	// Use this for initialization
	void Start () {
		theMeshFilter = GetComponent<MeshFilter> ();
		theMesh = theMeshFilter.mesh;
	
		drawVertecies ();
	}



	void drawVertecies(){

		Vector3[] vertecies = new Vector3[] {
			// Front Side
			new Vector3(-1,1,-1),	// TL 0
			new Vector3(1,1,-1),		// TR 1
			new Vector3(-1,-1,-1),	// BL 2
			new Vector3(1,-1,-1),		// BR 3


			// Back Side
			new Vector3(-1,1,1),		// TL 4
			new Vector3(1,1,1),		// TR 5
			new Vector3(-1,-1,1),		// BL 6
			new Vector3(1,-1,1)		// BR 7
		};

		int[] triangles = new int[] {
		
			//Front
			0, 1, 3,
			2, 0, 3,

			//Back
			5,4,7,
			4,6,7,

			//Top
			1,0,5,
			4,5,0,

			//Bot
			2,3,7,
			2,7,6,

			//Left Side
			0,2,4,
			6,4,2,

			//Right Side
			3,1,5,
			5,7,3

		};

		Vector2[] uvs = new Vector2[] {
			new Vector2(0,0),
			new Vector2(0,0),
			new Vector2(0,0),
			new Vector2(0,0),

			new Vector2(0,0),
			new Vector2(0,0),
			new Vector2(0,0),
			new Vector2(0,0),
		};


		theMesh.Clear ();
		theMesh.vertices = vertecies;
		theMesh.triangles = triangles;
		theMesh.uv = uvs;

		theMesh.Optimize ();
		theMesh.RecalculateNormals ();
	}

}
