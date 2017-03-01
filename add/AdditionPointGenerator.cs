using UnityEngine;
using System.Collections;

public class AdditionPointGenerator : PointGenerator  {

	const int SIZE = 3;
	public Vector2[] moleKind = new Vector2[SIZE];
//	public Vector2 pointMinMax = new Vector2 (1f,9f);

	// Use this for initialization
	void Start () {
		
		for (int i=0;i<SIZE;i++) {
			if (moleKind[i].x == 0 || moleKind[i].y == 0) {
				Debug.LogWarning ("Using default number area, Please check Settings in AdditionPointGenerator");
				moleKind[i].x = 1;
				moleKind[i].y = 9;
			}
		}
	}
	
	public override int numberGenerator(){
		int point;
		int moleType;

		//選擇地鼠種類
		moleType = typeChooser ();
		
		float min, max;
		min = moleKind [moleType].x;
		max = moleKind [moleType].y;

		point = (int)Random.Range (min, max);
		return point;
	}

	//控制不同種類的數字產生的機率
	private int typeChooser(){
		int type = 0;
		type = Random.Range (0,SIZE);

		return type;
	}
}
