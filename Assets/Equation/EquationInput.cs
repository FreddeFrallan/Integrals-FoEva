using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using B83.ExpressionParser;

public class EquationInput : MonoBehaviour {

	private class customStackNode{
	
		private customOperator theOperator;
		private bool isOperator = false;
		private double value;



		public customStackNode(bool isOperator){
			this.isOperator = isOperator;
		}

		public void setValue(double newValue){
			value = newValue;
		}
		public void setOperatorValue(customOperator newOperator){
			theOperator = newOperator;
		}

		public bool getIsOperatr(){
			return isOperator;
		}
		public customOperator getOperator(){
			return theOperator;
		}
		public double getValue(){
			return value;
		}
		public char getSign(){
			return theOperator.getSign ();
		}
	}

	private class customOperator{
		int amount = 2;
		int precedence = 1;
		char sign;

		public customOperator(char sign){
			this.sign = sign;
			setValue();
		}

		private void setValue(){
			switch (sign) {
			case '+':
				amount = 2;
				precedence = 1;
				break;

			case '-':
				amount = 2;
				precedence = 1;
				break;

			case '*':
				amount = 2;
				precedence = 2;
				break;

			case '/':
				amount = 2;
				precedence = 2;
				break;
			
			}
		}

		public int getPrecedence(){
			return precedence;
		}

		public char getSign(){
			return sign;
		}

	}

	public InputField theInputField;
	public GameObject RotationPrefab;

	GameObject createdRotationObject;


	//Decimal Controller
	bool gettingDecimal = false;
	int decimalCounter = 0;
	int intCounter = 0;

	double currentDouble = 0;
	string currentInt = "";
	//


	List<customOperator> operationStack;
	List<double> numberStack;
	List<customStackNode> executionOrder;

	void Start(){
		operationStack = new List<customOperator> ();
		numberStack = new List<double> ();
		executionOrder = new List<customStackNode> ();
	}

	// Update is called once per frame
	void Update () {
		getInput ();
	}


	void getInput(){
		if (Input.GetKeyDown (KeyCode.Return)) {
			Debug.ClearDeveloperConsole ();

		//	ExpressionParser theParser = new ExpressionParser ();
			string temp = theInputField.text;
		//	Expression theExpression = theParser.EvaluateExpression(temp);
			/*
			if (stringParser (theInputField.text)) {
				//enabled = false;
			}
			*/
		}
	}

	void printNumberList(){
		for (int i = 0; i < executionOrder.Count; i++) {
			
			if (executionOrder [i].getIsOperatr()) {
				Debug.Log (executionOrder [i].getSign ());
			} else {
				Debug.Log (executionOrder [i].getValue());
			}

		}
	}

	bool stringParser(string inputText){
		char[] stringArray = inputText.ToCharArray();

		for (int i = 0; i < inputText.Length; i++) {

			if (char.IsDigit (stringArray [i])) {
				numberHandler (stringArray[i] );
			} 
			else {
				if(! operatorHandler( stringArray[i]) ){ // If operation handler does not recognize operation abort equation
					return false;
				}
			}
		}
		breakCurrentNumber ();
		popAllOperations ();
		printNumberList ();
		return true;
	}



	bool operatorHandler(char currentOperator){
		
		switch (currentOperator) {
		case '.':
			gettingDecimal = true;
		break;

		case ',':
			gettingDecimal = true;
		break;	

		case ' ':
			breakCurrentNumber ();
			break;
			
		case '+':
			breakCurrentNumber ();
			addOperation (currentOperator);
		break;

		case '-':
			breakCurrentNumber ();
			addOperation (currentOperator);
			break;

		case '*':
			breakCurrentNumber ();
			addOperation (currentOperator);
			break;

		case '/':
			breakCurrentNumber ();
			addOperation (currentOperator);
			break;

		default:
			Debug.LogWarning ("Wrong format of equation");
			breakCurrentNumber ();
			return false;
			break;
		}
		return true;
	}

	void addOperation(char currentOperation){
		customOperator tempOperation = new customOperator (currentOperation);

		bool isInserted = false;
		int counter = operationStack.Count-1;

		while (isInserted == false && counter >= 0) {
			
			if (tempOperation.getPrecedence() < operationStack [counter].getPrecedence()) {				// Pop and add to executionStack
				customStackNode tempExeNode = new customStackNode (true);
				tempExeNode.setOperatorValue (operationStack [counter]);
				executionOrder.Add (tempExeNode);
				operationStack.RemoveAt (counter);

				counter--;
			} 
			else {
				operationStack.Add (tempOperation);
				isInserted = true;
			}
		}

		if (isInserted == false) {
			operationStack.Add (tempOperation);
		}

	}

	void popAllOperations(){
		for (int i = operationStack.Count-1; i >= 0; i--) {
			customStackNode tempNode = new customStackNode (true);
			tempNode.setOperatorValue (operationStack [i]);
			executionOrder.Add (tempNode);
			operationStack.RemoveAt (i);
		}
	}

	void addNumber(double theValue){
		customStackNode tempNode = new customStackNode (false);
		tempNode.setValue (theValue);
		executionOrder.Add (tempNode);
	}


	void breakCurrentNumber(){
		parseCurrentNumber ();

		if (decimalCounter != 0 || intCounter != 0) {
			numberStack.Add (currentDouble);
			addNumber (currentDouble);
		}
		gettingDecimal = false;
		decimalCounter = 0;
		intCounter = 0;
		currentInt = "";
		currentDouble = 0;
	}

	void parseCurrentNumber(){
		for (int i = 0; i < currentInt.Length; i++) {
			currentDouble += (int)char.GetNumericValue (currentInt [i]) * Mathf.Pow (10, currentInt.Length - i - 1);
		}
	}

	void numberHandler(char currentDigit){
		if(!gettingDecimal){
			currentInt += currentDigit;
			intCounter++;
		}
		else{
			currentDouble += (int)char.GetNumericValue(currentDigit) / Mathf.Pow (10, (1+decimalCounter));
			decimalCounter++;
		}
	}
}
