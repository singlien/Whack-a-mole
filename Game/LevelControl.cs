using UnityEngine;
using System.Collections;

public class LevelControl : MonoBehaviour {

	public Vector2 unlockPosition = new Vector2(.35f, .5f); //解鎖Medium & Hard的時機
	public bool[] isUnlock = new bool[3]; //Easy, Medium, Hard lock
	public GameObject maps;

	//經驗值
	private static float exp = 0f;

	public static float Exp {
		get {
			return exp;
		}
	}
	static bool isExpModify = false;

	public static void AddExperience(float score){	//增加經驗值
		float e = score / 1000f;
		exp += e;
		isExpModify = true;
		print ("Current exp: " + exp);
	}

	// Use this for initialization
	void Start () {

		// 遊戲進度歸0
		isUnlock [0] = false;
		isUnlock [1] = false;
		isUnlock [2] = false;

		//讀取玩家記錄
		Load ();
		ExpValidation ();	//解鎖玩家目前關卡
	}

	void Update () {
		if (isExpModify) {
			ExpValidation ();
			Save ();
			isExpModify = false;
		}
	}
	void OnDestroy (){
		Save ();
		print ("玩家經驗值已存檔");
	}
				
	public void ExpValidation(){	//只有在增加經驗值的時候執行
		if (exp >= 100f) {
			//拿到心之碎片	
		} else if (exp > unlockPosition.y) {
			//Unlock Hard level
			isUnlock [0] = true;	//Easy
			isUnlock [1] = true;	//Medium
			isUnlock [2] = true;	//Hard

		} else if (exp > unlockPosition.x) {
			//Unlock Medium level
			isUnlock [0] = true;	//Easy
			isUnlock [1] = true;	//Medium
			isUnlock [2] = false;	//Hard, locked
		} else
			isUnlock [0] = true; 	//Easy 預設解鎖

		UnlockLevel ();
	}

	void UnlockLevel(){
		// Lock all level
		for(int j=0;j<maps.transform.childCount;j++){
			for (int k = 0; k < 3; k++) {
				maps.transform.GetChild (j).GetChild (k).gameObject.SetActive (false);
			}
		}

		if (isUnlock [0]) {	//Easy is unlocked
			for (int i = 0; i < maps.transform.childCount; i++) {
				maps.transform.GetChild (i).GetChild (2).gameObject.SetActive (true);
			}
		}
		if (isUnlock [1]) {	//Medium is unlocked
			for (int i = 0; i < maps.transform.childCount; i++) {
				maps.transform.GetChild (i).GetChild (1).gameObject.SetActive (true);
			}
		}
		if (isUnlock [2]) {	//Hard is unlocked
			for (int i = 0; i < maps.transform.childCount; i++) {
				maps.transform.GetChild (i).GetChild (0).gameObject.SetActive (true);
			}
		}
	}


	//
	// Save & Load
	//
	void Save(){
		PlayerPrefs.SetFloat ("PlayerExperience", exp);
	}
	void Load(){
		if (PlayerPrefs.HasKey ("PlayerExperience")) {
			exp = PlayerPrefs.GetFloat ("PlayerExperience");
		}
	}


}
