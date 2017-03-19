using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStatusControl : MonoBehaviour {

	private MainGameScript mainGame;
	private static bool isPaused;

	bool playOnce = true;

	public static bool IsPaused {
		get {
			return isPaused;
		}
	}

//	public Button returnButton;
//	public Image gameOverImg;
	public GameObject pauseMenu;
	public GameObject StartInstruction;
	public tk2dTextMesh FinalScore;
	public tk2dSprite gameOverScreen;
	public AudioClip endSound;

	float time;
	public float exitTime = 1f;
	int count;

	// Use this for initialization
	void Start () {
		
		mainGame = gameObject.GetComponent<MainGameScript> ();
		if (mainGame == null) {
			Debug.LogError ("Unable to load MainGameScript");
		}

		pauseMenu.SetActive (false);
		gameOverScreen.gameObject.SetActive (false);
		StartInstruction.SetActive (true);

		// Show Start Instruction and pause game on default.
		Time.timeScale = 0f;
		isPaused = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (mainGame.GameEnd) {
			if(playOnce)
				GameOverFunc ();
			playOnce = false;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
//			print ("Escape pressed");
			OnPause ();
		}
			
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
			}
			if (time > exitTime)
				count = 0;

	}


	void GameOverFunc(){
		FinalScore.text = "Score: " + ScoreScript.Score;
		if (SettingsScript.IsBGMMute)
			AudioSource.PlayClipAtPoint (endSound, new Vector3 (), 0f);
		else
			AudioSource.PlayClipAtPoint (endSound, new Vector3 (), 1f);
		gameOverScreen.gameObject.SetActive (true);
		GameObject.Find ("GameBGM").GetComponent<AudioSource> ().Pause ();
//		returnButton.gameObject.SetActive (true);
		chooseMode.setDifficulty=0;

		// Show fade times
		print("Fade Times:"+gameObject.GetComponent<ScoreControlAbstract>().FadeCount);
	}

	// Start Instruction Function
	void OnGameStart(){
		StartInstruction.SetActive (false);
		OnResume ();
	}

	// Pause Menu Function
	void ReturnButtonPress(){
//		Debug.Log ("ReturnButton Pressed. Return to menu");
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu", UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	void ChangePictureWhenPress(tk2dUIItem who){
		string spriteName = who.gameObject.GetComponentInChildren<tk2dSprite> ().GetCurrentSpriteDef().name;
		who.gameObject.GetComponentInChildren<tk2dSprite> ().SetSprite (spriteName + "hit");
		who.gameObject.GetComponentInChildren<tk2dTextMesh> ().text = "";
	}

	void OnPause(){
//		print ("Game Paused");
		Time.timeScale = 0;
		isPaused = true;
		pauseMenu.SetActive (true);
	}
	void OnResume(){
//		print ("Game Resume");
		Time.timeScale = 1;
		isPaused = false;
		pauseMenu.SetActive (false);
	}
	void OnSettings(){
//		print ("Enter settings");
		for (int i = 0; i < 4; i++) {
			pauseMenu.transform.GetChild (i).gameObject.SetActive (false);
		}
		for (int i = 4; i < pauseMenu.transform.childCount; i++) {
			pauseMenu.transform.GetChild (i).gameObject.SetActive (true);
		}
	
	}
	// Settings Option Function
	void OnGoPressed(){
//		print ("Return to pause menu");
		for (int i = 0; i < 4; i++) {
			pauseMenu.transform.GetChild (i).gameObject.SetActive (true);
		}		
		for (int i = 4; i < pauseMenu.transform.childCount; i++) {
			pauseMenu.transform.GetChild (i).gameObject.SetActive (false);
		}
	}
	void OnRestart(){
//		print ("Reload Level");
		UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
		isPaused = false;
	}
	void OnQuit(){
//		print ("Quit Game");
		mainGame.GameEnd = true;
	}
}
	
