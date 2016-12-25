using UnityEngine;
using System.Collections;

public class drawCircle : MonoBehaviour {


	RequireComponent MeshFilter;
	MeshFilter theMeshFilter;
	Mesh theMesh;


	Vector3[] circleVertecies;
	int[] circleTriangles;
	Vector2[] circleUV;

	int polyAmount = 100;


	public void draw(float xPos, float radius, int polyLevel){

		polyAmount = polyLevel;

		setCircleVert (xPos ,radius);
		setCircleTriangles ();
		setCircleUV ();

		drawVertecies ();
	}

	void init(){
		theMeshFilter = GetComponent<MeshFilter> ();
		theMesh = theMeshFilter.mesh;
	}

	void drawVertecies(){
		init ();

		theMesh.Clear ();
		theMesh.vertices = circleVertecies;
		theMesh.triangles = circleTriangles;
		theMesh.uv = circleUV;

		theMesh.Optimize ();
		theMesh.RecalculateNormals ();
	}



	void setCircleVert(float xPos, float radius){
		float radIncrement = Mathf.PI*2 / polyAmount;

		circleVertecies = new Vector3[polyAmount + 1];
		circleVertecies [0] = new Vector3 (0, 0, -xPos);
	
		for (int i = 0; i < polyAmount; i++) {
			float tempAngle = (Mathf.PI/2) -  (i * radIncrement);  

			circleVertecies [i + 1] = new Vector3 (Mathf.Cos (tempAngle) * radius, Mathf.Sin (tempAngle)  * radius, -xPos);
		}
		
	}

	void setCircleTriangles(){
		circleTriangles = new int[polyAmount * 3];
		int triangleCounter = 0;

		for (int i = 0; i < polyAmount; i++) {
			for (int j = 0; j < 3; j++) {

				if (j == 0) {
					circleTriangles [triangleCounter] = 0;
				} 
				else {
					if (i + j > polyAmount) {
						circleTriangles [triangleCounter] = 1;
					} 
					else {
						circleTriangles [triangleCounter] = (i + j);
					}
				}
				triangleCounter++;
			}
		}
	}

	void setCircleUV(){
		circleUV = new Vector2[polyAmount + 1];
		int uvCounter = 0;

		for (int i = 0; i < polyAmount + 1; i++) {
			circleUV [i] = new Vector2 (0, 0);

			for (int j = 0; j < 3; j++) {

				if (j == 0) {
					circleUV [uvCounter] = new Vector2 (0, 0);
				} 
				else {
					circleUV [uvCounter] = new Vector2 (1, 1);
				}
			}
			uvCounter++;
		}
	}
}
