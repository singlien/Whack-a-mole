using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class chooseMode : MonoBehaviour {

	public Transform maps;
	public Transform planet;
	public Transform arrow;
	public Transform startMenu;

	//為方便直接用scene執行設3, 要記得改回0!!!
	public static int setDifficulty=3;	// 1=Hard, 2=Mid, 3=Easy

	private int currentIndex;

	float time;
	int count;
	public float exitTime = 1;

	void Start () {
		currentIndex = planet.GetComponent<SwipeControl> ().CurrentChoice;

		//先把地圖每一個物件關掉
		for (int i = 0; i < maps.childCount; i++) {
			maps.GetChild (i).gameObject.SetActive (false);
		}

		planet.gameObject.SetActive (false);
		startMenu.gameObject.SetActive (true);
		maps.gameObject.SetActive (true);
		arrow.gameObject.SetActive (false);
	}

	void Update(){

		// Double tab to escape
		time += Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			count++;
			if (time <= exitTime && count == 2) {
				Application.Quit ();
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying=false;
				#endif
			}
			time = 0f;
			if (!startMenu.gameObject.activeSelf) {
				if (planet.gameObject.activeSelf)
					ReturnToTaptoStartScreen ();
				else
					ReturnButtonPressed ();
			}
		}
		if (time > exitTime)
			count = 0;
	}

	void AdditionMode(tk2dUIItem called){
		switch (called.name) {

		case "map3-1":
//			print ("map3-1 clicked, Hard");
			setDifficulty = 1;
			break;
		case "map3-2":
//			print ("map3-2 clicked, Medium");
			setDifficulty = 2;
			break;
		case "map3-3":
//			print ("map3-3 clicked, Easy");
			setDifficulty = 3;
			break;
		default:
			Debug.LogError ("Cannot find object");
			setDifficulty = 0;
			break;
		}
		SceneManager.LoadScene ("Addition", LoadSceneMode.Single);
//		DontDestroyOnLoad (gameObject);

	}

	void substractionMode(tk2dUIItem called){
		
		switch (called.name) {

		case "map4-1":
//			print ("map4-1 clicked, Hard");
			setDifficulty = 1;
			break;
		case "map4-2":
//			print ("map4-2 clicked, Medium");
			setDifficulty = 2;
			break;
		case "map4-3":
//			print ("map4-3 clicked, Easy");
			setDifficulty = 3;
			break;
		default:
			Debug.LogError ("Cannot find object");
			setDifficulty = 0;
			break;
		}
//		GameObject.DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene ("Subtraction", LoadSceneMode.Single);
	}

	void divisionMode(tk2dUIItem called){
		switch (called.name) {

		case "map6-1":
//			print ("map6-1 clicked, Hard");
			setDifficulty = 1;
			break;
		case "map6-2":
//			print ("map6-2 clicked, Medium");
			setDifficulty = 2;
			break;
		case "map6-3":
//			print ("map6-3 clicked, Easy");
			setDifficulty = 3;
			break;
		default:
			Debug.LogError ("Cannot find object");
			setDifficulty = 0;
			break;
		}
//		GameObject.DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene ("Division", LoadSceneMode.Single);
	}

	void TapToStartPressed(){
		startMenu.gameObject.SetActive (false);
		planet.gameObject.SetActive (true);
		arrow.gameObject.SetActive (true);
	}

	void ReturnButtonPressed(){
		maps.GetChild (currentIndex).gameObject.SetActive (false);
		planet.gameObject.SetActive (true);
//		print ("return button pressed, currentIndex=" + currentIndex);
		arrow.gameObject.SetActive(true);
	}
	// Return to Tap to start screen
	void ReturnToTaptoStartScreen(){
		startMenu.gameObject.SetActive (true);
		planet.gameObject.SetActive (false);
		arrow.gameObject.SetActive (false);
		for (int i = 0; i < maps.childCount; i++) {
			maps.GetChild (i).gameObject.SetActive (false);
		}
	}
	void ChooseDifficulty(){
		currentIndex = planet.GetComponent<SwipeControl> ().CurrentChoice;
//		print ("chooseDifficulty button pressed, currentIndex=" + currentIndex);
		planet.gameObject.SetActive (false);
		maps.GetChild (currentIndex).gameObject.SetActive (true);
		maps.gameObject.SetActive (true);
		arrow.gameObject.SetActive (false);
	}
		
}
