using UnityEngine;
using System.Collections;

public class PhoneSettings : MonoBehaviour {

	public Zoom theZoomScript;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		if (SystemInfo.deviceType == DeviceType.Handheld) {
			theZoomScript.enabled = false;
		}
	}
	

}
