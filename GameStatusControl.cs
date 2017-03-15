using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStatusControl : MonoBehaviour {

	private MainGameScript mainGame;

	public Button returnButton;
	public Image gameOverImg;
	public GameObject pauseButton;

	// Use this for initialization
	void Start () {
		
		mainGame = gameObject.GetComponent<MainGameScript> ();
		if (mainGame == null) {
			Debug.LogError ("Unable to load MainGameScript");
		}

		pauseButton = GameObject.Find ("PauseMenu").gameObject;

		pauseButton.SetActive (false);
		gameOverImg.gameObject.SetActive (false);
		returnButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (mainGame.GameEnd) {
			GameOverFunc ();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			print ("Escape pressed");
			OnPause ();
		}
			
	}


	private void GameOverFunc(){
		gameOverImg.GetComponentInChildren<Text> ().text = "Score: " + ScoreScript.Score;
		gameOverImg.gameObject.SetActive (true);
		returnButton.gameObject.SetActive (true);
	}

	public void ReturnButtonPress(){
		Debug.Log ("Button Pressed. Return to menu");
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu", UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	private void OnPause(){
		print ("Game Paused");
		Time.timeScale = 0;
		pauseButton.SetActive (true);
	}
	private void OnResume(){
		print ("Game Resume");
		Time.timeScale = 1;
		pauseButton.SetActive (false);
	}
	private void OnRestart(){
		print ("Reload Level");
	}
	private void OnQuit(){
		print ("Quit Game");
		mainGame.GameEnd = true;
	}
}
