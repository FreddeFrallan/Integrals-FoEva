using UnityEngine;
using System.Collections;

public class TouchZoom : MonoBehaviour {

	float lastDist = 0;
	public Zoom theZoomScript;

	// Update is called once per frame
	void Update () {

		if (Input.touchCount >= 2) {
			Touch firstFinger = Input.GetTouch (0);
			Touch secondFinger = Input.GetTouch (1);

			if (secondFinger.phase == TouchPhase.Began) {
				lastDist = Vector2.Distance (firstFinger.position, secondFinger.position);
			}

			float newDist = Vector2.Distance (firstFinger.position, secondFinger.position);
			float deltaDist = newDist - lastDist;
			lastDist = newDist;
			theZoomScript.enabled = false;
			theZoomScript.touchZoom (deltaDist);
		}
	
	}
}
