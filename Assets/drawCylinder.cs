using UnityEngine;
using System.Collections;

public class drawCylinder : MonoBehaviour {
	public drawCircle frontCircle, backCircle;

	public drawShell cylinderShell;

	float radius = 2;
	float length = 10;

	int polyLevel = 100;
	int precisionLevel = 400;



	public void drawDaShit(equationSolver theSolver, float start, float end){
		length = end - start;
		if (end < start)
			length = 0;

		float[] eqAnswers = theSolver.calcEquation (precisionLevel);


		frontCircle.draw (0, eqAnswers[0], polyLevel);
		cylinderShell.draw (radius, length, polyLevel, precisionLevel, eqAnswers);
		backCircle.draw (length, eqAnswers[precisionLevel-1], polyLevel);

		backCircle.transform.localEulerAngles = new Vector3 (180, 0, 0);
	}

}
