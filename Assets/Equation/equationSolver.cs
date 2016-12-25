using UnityEngine;
using System.Collections;
using B83.ExpressionParser;
using UnityEngine.UI;

public class equationSolver : MonoBehaviour {

	public InputField theInputField;
	public Text startText;
	public Text endText;
	public ErrorMessageHandler theErrorHandler;
	public Zoom zoomScript;
	public Rotate theRotateScript;

	ExpressionParser theParser;

	GameObject createdObject;
	GameObject rotationObj;
	public GameObject drawPrefab;

	float theHighestValue;
	Vector3 spawnPos;
	Vector3 middlePoint;


	float length = 10;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Return)) {
			startRender ();
		}
	}

	public void startRender(){
		if (createdObject == null) {
			createdObject = (GameObject)Instantiate (drawPrefab, transform.position, Quaternion.identity);
		} else {
			Destroy (createdObject);
			createdObject = (GameObject)Instantiate (drawPrefab, transform.position, createdObject.transform.rotation);
		}

		theErrorHandler.setText ("");
		float start = getStartValue ();
		float end = getEndValue ();

		createdObject.GetComponent<drawCylinder> ().drawDaShit(this, start, end);
		createdObject.transform.eulerAngles = new Vector3 (0, 90, 0);

		setSpawnPos (start, end);
		zoomScript.setValues (theHighestValue,length);

		createdObject.transform.position = spawnPos;


		if (rotationObj != null)
			Destroy (rotationObj);


		GameObject rotationObject = new GameObject ();
		rotationObject.transform.position = middlePoint;
		createdObject.transform.parent = rotationObject.transform;
		theRotateScript.setRotateObject (rotationObject);
		rotationObj = rotationObject;
	}


	void setSpawnPos(float start, float end){

		length = end - start;
		if (end < start)
			length = 0;


		if (theHighestValue < length/2) {
			spawnPos = new Vector3 (-length / 2, 0, -length / 2);
			middlePoint = new Vector3 (0, 0, -length / 2);
		} 
		else {
			if (theHighestValue < 10)
				theHighestValue = 10;

			spawnPos = new Vector3 (-length / 2, 0, -theHighestValue);
			middlePoint = new Vector3 (0, 0, -theHighestValue);
		}

	}

	void setHighestValue(float[] theValues){
		float highestValue = 0;
		for (int i = 0; i < theValues.Length; i++) {
			if (theValues [i] > highestValue)
				highestValue = theValues [i];
		}

		theHighestValue = highestValue;
	}

	void scaleZAxis(GameObject spawnedObject){
		if (theHighestValue >= 1) {
			float zScale = theHighestValue / 10;
			float xScale = 1 / zScale;

			if (xScale < 0.1f)
				xScale = 0.1f;

			spawnedObject.transform.localScale = new Vector3 (xScale, xScale, zScale);
			Vector3 tempPos = spawnedObject.transform.position;
			tempPos.x = zScale * -5;
			spawnedObject.transform.position = tempPos;
		}
	}



	public float[] calcEquation(int precision){
		float start = getStartValue ();
		float end = getEndValue ();

		theParser = new ExpressionParser ();
		Expression theExpression;

		if (theInputField.text.Length > 0) {
			theExpression = theParser.EvaluateExpression (theInputField.text);
		} else {
			theExpression = theParser.EvaluateExpression ("0");
		}

		float xDist = end - start;
		float deltaX = xDist / precision;
		float[] returnValues = new float[precision];

		float XValue = start;
		bool hasxVar = false;
		bool hasXVar = false;

		if (theExpression.Parameters.ContainsKey ("x")) {
			hasxVar = true;
		}
		if (theExpression.Parameters.ContainsKey ("X")) {
			hasXVar = true;
		}


		for (int i = 0; i < precision; i++) {

			if (hasxVar) {
				theExpression.Parameters ["x"].Value = XValue;
			}
			if (hasXVar) {
				theExpression.Parameters ["X"].Value = XValue;
			}

			
			returnValues [i] = (float)theExpression.Value;

			XValue += deltaX;
		}

		for (int i = 0; i < returnValues.Length; i++) {
			returnValues [i] = Mathf.Abs (returnValues [i]);
		}


		setHighestValue (returnValues);

		return returnValues;
	}


	float getStartValue(){
		theParser = new ExpressionParser ();
		Expression theExpression;

		if (startText.text.Length > 0) {
			theExpression = theParser.EvaluateExpression (startText.text);
		} else {
			theExpression = theParser.EvaluateExpression ("0");
		}

		float temp = (float)theExpression.Value;
		return temp;
	}


	float getEndValue(){
		theParser = new ExpressionParser ();
		Expression theExpression;

		if (endText.text.Length > 0) {
			theExpression = theParser.EvaluateExpression (endText.text);
		} else {
			theExpression = theParser.EvaluateExpression ("10");
		}

		float temp = (float)theExpression.Value;
		return temp;
	}

}
