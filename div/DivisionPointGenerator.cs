using UnityEngine;
using System.Collections;

//控制生成的數字
public class DivisionPointGenerator : PointGenerator
{
	// Attention!!
	// 這邊的變數要和moleScript的陣列大小設一樣，若設定不一樣，遊戲會運作錯誤，且unity不會提出警告
	public int moleKinds;

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

	public override Vector2 numberGenerator(){
		
		Vector2 pointAndSeq;

		pointAndSeq.y = Random.Range (0, moleKinds);		//sequence
		pointAndSeq.x = (int)prime [(int)pointAndSeq.y];	//point

		generatedPoint.Add ((int)pointAndSeq.x);

		return pointAndSeq;
	}
		
}
