using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class PointGenerator : MonoBehaviour 
{
	//儲存出現過的點數
	protected List<int> generatedPoint = new List<int> ();

	public abstract Vector2 numberGenerator();


	//Debug
//	void OnDestroy(){
//		foreach(int i in generatedPoint){
//			print(i);
//		}
//		generatedPoint = null;
//	}


}
