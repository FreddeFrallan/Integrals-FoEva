using UnityEngine;
using System.Collections;

public class changeMaterial : MonoBehaviour {

	public Material theMaterial;

	public void turnSolid(){
		Color tempColor = theMaterial.color;
		tempColor.a = 1;
		theMaterial.color = tempColor;
	}

	public void turnTransparant(){
		Color tempColor = theMaterial.color;
		tempColor.a = 0.3f;
		theMaterial.color = tempColor;
	}
}
