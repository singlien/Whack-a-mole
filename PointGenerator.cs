using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class PointGenerator : MonoBehaviour 
{
	//儲存出現過的點數
	protected List<Vector2> generatedPoint = new List<Vector2> ();

	public abstract Vector2 numberGenerator();


	//Debug
	void OnDestroy(){
		foreach(Vector2 i in generatedPoint){
			print("(point, sequence) = "+i);
		}
		generatedPoint = null;
	}


}
