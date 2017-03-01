using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class PointGenerator : MonoBehaviour 
{

	protected List<int> generatedPoint = new List<int> ();

	// Use this for initialization
	void Start (){}

	public abstract int numberGenerator();

	//Debug
	void OnDestroy(){
		foreach(int i in generatedPoint){
			print(i);
		}
		generatedPoint = null;
	}


}
