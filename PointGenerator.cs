using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class PointGenerator : MonoBehaviour 
{

	protected List<int> generatedPoint = new List<int> ();

	public abstract int numberGenerator();

	//Debug
//	void OnDestroy(){
//		foreach(int i in generatedPoint){
//			print(i);
//		}
//		generatedPoint = null;
//	}


}
