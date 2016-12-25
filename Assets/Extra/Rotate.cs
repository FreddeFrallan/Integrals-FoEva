using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public GameObject spawnedObj;

	float lastMouseX, lastMouseY;
	float rotateSpeed = 0.7f;

	bool resetTouch = true;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			lastMouseY = Input.mousePosition.y;
			lastMouseX = Input.mousePosition.x;
		}

		if (Input.touchCount == 0) {
			resetTouch = true;
		}
		if (Input.touchCount >= 2) {
			resetTouch = false;
		}

		if (spawnedObj != null) {
		
			rotateXAxis ();
		}
	
	}

	void rotateXAxis(){
		if (Input.GetMouseButton (0) && Input.touchCount <= 1 && resetTouch) {
			float deltaX = Input.mousePosition.x - lastMouseX;
			lastMouseX = Input.mousePosition.x;

			Vector3 tempRot = spawnedObj.transform.eulerAngles;
			tempRot.y -= deltaX * rotateSpeed;

			spawnedObj.transform.eulerAngles = tempRot;
		}
	}

	void rotateYAxis(){
		if (Input.GetMouseButton (0)) {
			float deltaY = Input.mousePosition.y - lastMouseY;
			lastMouseY = Input.mousePosition.y;

			Debug.Log (deltaY);

			Vector3 tempRot = spawnedObj.transform.eulerAngles;
			tempRot.x -= deltaY * rotateSpeed;

			spawnedObj.transform.eulerAngles = tempRot;
		}
	}

	public void setRotateObject(GameObject theObj){
		spawnedObj = theObj;
	}
}
