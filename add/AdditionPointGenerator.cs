using UnityEngine;
using System.Collections;

public class AdditionPointGenerator : PointGenerator  {

	public Vector2[] moleKind = new Vector2[3];

	// Use this for initialization
	void Start () {

		//Check the settings
		for (int i=0;i<moleKind.Length;i++) 
		{
			if (moleKind[i].x == 0 || moleKind[i].y == 0) {
				Debug.LogWarning ("Using default number area, Please check Settings in AdditionPointGenerator");
				// Reset to default
				moleKind[i].x = 1;
				moleKind[i].y = 9;
			}
		}
	}

	public override Vector2 numberGenerator(){
		Vector2 pointAndSeq;

		//控制生成種類
		pointAndSeq.y = Random.Range (0, moleKind.Length);

		float min, max;
		min = moleKind [(int)pointAndSeq.y].x;
		max = moleKind [(int)pointAndSeq.y].y;

		//依種類生成數字
		pointAndSeq.x = (int)Random.Range (min, max);

		//儲存生成數字
		generatedPoint.Add (pointAndSeq);

		return pointAndSeq;
	}


}
