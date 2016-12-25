using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class drawShell : MonoBehaviour {


	RequireComponent MeshFilter;
	MeshFilter theMeshFilter;
	Mesh theMesh;


	Vector3[] circleVertecies;
	int[] circleTriangles;
	Vector2[] circleUV;


	List<Vector3[]> discList;


	int polyAmount = 100;
	float deltaX = 10;
	float radius = 2;


	int discCounter = 0;
	int amountOfDiscs = 2;
	int verteciesCounter = 0;

	float[] discRadius;


	void init(){
		theMeshFilter = GetComponent<MeshFilter> ();
		theMesh = theMeshFilter.mesh;

		discList = new List<Vector3[]> ();
	}

	public void draw(float radius, float length, int polyLevel, int precisionLevel, float[] equationAnswers){
		init ();

		polyAmount = polyLevel;
		deltaX = length;
		this.radius = radius;
		discRadius = equationAnswers;

		setCircleVert (precisionLevel);
		setCircleTriangles (precisionLevel);
		setCircleUV (precisionLevel);

		drawVertecies ();
	}

	void drawVertecies(){
		theMesh.Clear ();
		theMesh.vertices = circleVertecies;
		theMesh.triangles = circleTriangles;
		theMesh.uv = circleUV;

		theMesh.Optimize ();
		theMesh.RecalculateNormals ();
	}
		


	void setCircleVert(int precisionLevel){
		float radIncrement = Mathf.PI*2 / polyAmount;

		circleVertecies = new Vector3[polyAmount * 2 + (precisionLevel -1)*polyAmount];


		for (int j = 0; j < precisionLevel; j++) {
		
			Vector3[] bottomCircle;
			Vector3[] topCircle;
			
			if (discCounter == 0) {
				bottomCircle = new Vector3[polyAmount];
				topCircle = new Vector3[polyAmount];
			}
			else {
				bottomCircle = discList [discCounter];
				topCircle = new Vector3[polyAmount];
			}
			
			
			for (int i = 0; i < polyAmount; i++) {
				float tempAngle = (Mathf.PI/2) -  (i * radIncrement);  
				
				if (discCounter == 0) {
					bottomCircle [i] = new Vector3 (Mathf.Cos (tempAngle) * discRadius[j], Mathf.Sin (tempAngle)  * discRadius[j], 0);
				}
				topCircle [i] = new Vector3 (Mathf.Cos (tempAngle) * discRadius[j], Mathf.Sin (tempAngle)  * discRadius[j], (deltaX/precisionLevel) * (j+1));
			}

			if (discCounter == 0) {
				for (int i = 0; i < bottomCircle.Length; i++) {
					circleVertecies [verteciesCounter] = bottomCircle [i];
					verteciesCounter++;
				}
			}
			for (int i = 0; i < topCircle.Length; i++) {
				circleVertecies [verteciesCounter] = topCircle [i];
				verteciesCounter++;
			}
			
			
			
			if (discCounter == 0) {
				discList.Add (bottomCircle);
				discList.Add(topCircle);
			}
			else {
				discList.Add (topCircle);
			}

			discCounter++;
		}

	}

	void setCircleTriangles(int precisionLevel){
		circleTriangles = new int[((polyAmount*2) * 3) +  (precisionLevel - 1) * polyAmount * 6];
		int triangleCounter = 0;

		for (int k = 0; k < precisionLevel; k++) {
		
			for (int i = 0; i < polyAmount; i++) {
				for (int j = 0; j < 6; j++) {
					
					if (j == 0) {
						circleTriangles [triangleCounter] = i + k*polyAmount;
					} 
					if (j == 1) {
						circleTriangles [triangleCounter] = i+polyAmount + k*polyAmount;
					}
					if (j == 2) {
						circleTriangles [triangleCounter] = i+1 + k*polyAmount;
					}
					if (j == 3) {
						if (i + polyAmount + 1 >= polyAmount * 2) {
							circleTriangles [triangleCounter] = polyAmount + k*polyAmount;
						}
						else {
							circleTriangles [triangleCounter] = i+polyAmount + k*polyAmount;
						}
					}
					if (j == 4) {
						if (i + polyAmount + 1 >= polyAmount * 2) {
							circleTriangles [triangleCounter] = 0 + k*polyAmount;
						} else {
							circleTriangles [triangleCounter] = i+polyAmount+1 + k*polyAmount;
						}
					}
					if (j == 5) {
						if (i + polyAmount + 1 >= polyAmount * 2) {
							circleTriangles [triangleCounter] = i + k*polyAmount;
						} else {
							circleTriangles [triangleCounter] = i+1 + k*polyAmount;
						}
					}
					
					triangleCounter++;
				}
			}
		}
	}

	void setCircleUV(int precisionLevel){
		circleUV = new Vector2[polyAmount * 2 + (precisionLevel -1)*polyAmount];
		int uvCounter = 0;

		for (int i = 0; i < polyAmount * 2 + (precisionLevel -1)*polyAmount; i++) {
			circleUV [i] = new Vector2 (0, 0);
		}
	}
}
