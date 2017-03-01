using UnityEngine;
using System.Collections;

//控制生成的數字
public class DivisionPointGenerator : PointGenerator
{
//	public Vector2 pointMinMax = new Vector2 (1f,9f);

	//宣告質數陣列
	private int[] prime = new int[6];


	// Use this for initialization
	void Start () {
		
		//定義質數陣列
		prime [0] = 2;
		prime [1] = 3;
		prime [2] = 5;
		prime [3] = 7;
		prime [4] = 11;
		prime [5] = 13;


	}


	public override int numberGenerator(){
		int point;
		int sequence;

		sequence = Random.Range (0, 6);
		point = prime [sequence];

		generatedPoint.Add (point);

		return point;
	}
}
