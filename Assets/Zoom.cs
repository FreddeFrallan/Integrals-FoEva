using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

	float cameraDistanceMax = 20f;
	float cameraDistanceMin = 5f;
	float cameraDistance = 10f;
	float scrollSpeed = 5f;
	float touchSpeed = 0.01f;

	float cameraBonusZoom = 0;

	float theHighestValue = 0;

	// Update is called once per frame
	void FixedUpdate () {
		setScrollSpeed ();

		cameraDistance -= (Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);
		cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);


		Vector3 tempCampPos = Camera.main.transform.position;
		tempCampPos.z = -cameraDistance;
		Camera.main.transform.position = tempCampPos;
	}

	public void touchZoom(float deltaDist){
		setScrollSpeed ();

		cameraDistance -= deltaDist * touchSpeed;
		cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);


		Vector3 tempCampPos = Camera.main.transform.position;
		tempCampPos.z = -cameraDistance;
		Camera.main.transform.position = tempCampPos;
	}

	void setScrollSpeed(){
		float minScrollSpeed = 10;

		if((Input.GetAxis("Mouse ScrollWheel") > 0)){
			float distToMaxValue = cameraDistance - 2 * theHighestValue;
			scrollSpeed = distToMaxValue;			
		}
		else{
			float distToMaxValue = cameraDistanceMax - cameraDistance;
			scrollSpeed = distToMaxValue;
		}

		if (scrollSpeed < minScrollSpeed)
			scrollSpeed = minScrollSpeed;
	}


	public void setValues(float highestValue, float length){
		theHighestValue = highestValue;
		cameraDistanceMin = highestValue;

		float cameraRad = Mathf.Deg2Rad * (Camera.main.fieldOfView / 4);


		if (length > highestValue) {
			cameraDistanceMax = length + length/2;
			cameraDistanceMin = length/2;

		
			cameraDistance = length/2 + (highestValue / (Mathf.Sin (cameraRad))) * Mathf.Cos(cameraRad) * 2;
		} 
		else {
			cameraDistanceMax = (highestValue / (Mathf.Sin (cameraRad))) * Mathf.Cos(cameraRad);

			if (cameraDistanceMax < 10)
				cameraDistanceMax = 10;
			
			cameraDistance = cameraDistanceMax;
		}
			
		Vector3 tempCampPos = Camera.main.transform.position;
		tempCampPos.z = -cameraDistance;
		Camera.main.transform.position = tempCampPos;
	}


}
