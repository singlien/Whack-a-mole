﻿using UnityEngine;
using System.Collections;

public class DivisionScoreControl : MonoBehaviour {

//	public tk2dTextMesh bannedDisplay;
	public tk2dTextMesh targetDisplay;
	public tk2dTextMesh currentDisplay;
	public tk2dTextMesh scoreDisplay;

	//目標相關	
	public int highIndexStart = 4;									//2,3,5,'7',11,13，高次方起始點
	public Vector2 indexAdditionMinMax = new Vector2 (0f, 15f);		//次方加總
	public Vector2 indexMinMax = new Vector2 (0f, 3f);				//一般指數次方大小
	public Vector2 highIndexMinMax = new Vector2 (0f, 1f);			//高指數次方大小
	public bool[] indexOnOff = new bool[6];							//指數開關：true=On, false=Off 


	private int targetPoint;
//	private int currentPoint;
//	private char[] bannedArray;

//	public int[] moleTypeArray = new int[6] { 2, 3, 5, 7, 11, 13 };

	// Use this for initialization
	void Start () {
		
		//目標數歸零
		//若直接等於resetTarget會陷入無限迴圈，目前無解
		ScoreScript.CurrentPoint = 0;

		//讓目標數顯示在UI上，target===1
		targetDisplay.text = "Target: 1";

		//讓遊戲分數歸零
		ScoreScript.Score = 0;
		targetPoint = 1;


		//Debug:所有開關ON
		for (int i = 0; i < indexOnOff.Length; i++)
			indexOnOff [i] = true;
		//Debug----------
	}
	
	// Update is called once per frame
	void Update () {
		
		//遊戲進行中不斷Update目前數字
		currentDisplay.text = string.Format("Now: {0}" , ScoreScript.CurrentPoint);
		scoreDisplay.text = string.Format("Score: {0}", ScoreScript.Score);
		scoreDisplay.Commit();

		//得分
		if(ScoreScript.CurrentPoint==targetPoint){	//現在點數=目標1，得分！

			//得分
			ScoreScript.Score += 10 ;
			//重設目標
			ScoreScript.CurrentPoint = resetTarget ();
		}

		//依各關不同控制
		//取得hitPoint然後依各關規則修改currentPoint
		if (ScoreScript.HitPoint != 0) {
			
			if ((ScoreScript.CurrentPoint % ScoreScript.HitPoint) == 0) {	//legal 
				ScoreScript.CurrentPoint /= ScoreScript.HitPoint;
			} else
				//illegal 重設目標
				ScoreScript.CurrentPoint = resetTarget ();
			
			ScoreScript.HitPoint = 0;
		}

		if (ScoreScript.CurrentPoint <= 0) {//出現不合理的數
			ScoreScript.CurrentPoint = resetTarget();
		}

	}



	//重設目標數
	int resetTarget(){
		int[] index = new int[6];						//質數的指數
		int target = 1;
//		int count = 0;

		while(indexIsLegal(index)){
			for (int i = 0; i < index.Length; i++) {	//決定指數大小

				if (indexOnOff [i]) {
					index [i] = (int)Random.Range (indexMinMax.x, indexMinMax.y);
					if (!(i < highIndexStart - 1))//1,2,3,'4'，大於高次指數的指數用下面再跑一次
						index [i] = (int)Random.Range (highIndexMinMax.x, highIndexMinMax.y);
				}
			}
			//效能致命弱點！！！
//			count++;

		} 


		//乘起來
		target = (int)(Mathf.Pow (2f, (float)index [0]) *
			Mathf.Pow (3f, (float)index [1]) *
			Mathf.Pow (5f, (float)index [2]) *
			Mathf.Pow (7f, (float)index [3]) *
			Mathf.Pow (11f, (float)index [4]) *
			Mathf.Pow (13f, (float)index [5]));

		return target;
	}

	bool indexIsLegal(int[] index){	//false小於最大目標和(合法), true爆掉了(不合法)
		int sum = 0;
		foreach (int i in index) {
			sum += i;
		}
		if (sum == 0)
			return true;
		if (sum >= indexAdditionMinMax.x && sum <= indexAdditionMinMax.y)
			return false;

		return true;
	}
		
		
}