using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ErrorMessageHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void setText(string newText){
		GetComponent<Text> ().text = newText;
	}
}
