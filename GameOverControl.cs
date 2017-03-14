using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverControl : MonoBehaviour {

	private MainGameScript mainGame;

	public Button returnButton;
	public Image gameOverImg;

	// Use this for initialization
	void Start () {
		
		mainGame = gameObject.GetComponent<MainGameScript> ();
		if (mainGame == null) {
			Debug.LogError ("Unable to load MainGameScript");
		}

		gameOverImg.gameObject.SetActive (false);
		returnButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (mainGame.GameEnd) {
			GameOverFunc ();
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
}
