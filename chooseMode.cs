using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
            //選星球的
public class chooseMode : MonoBehaviour {

    public static InputField inputfield;

	public Transform maps;
	public Transform planet;
	public Transform arrow;
	public Transform startMenu;
    public Transform UserNameInput;
    public Transform Ranking;
    public Canvas canvas;
    public Canvas RankCanvas;
    public tk2dTextMesh AdditiontextMesh;
    public tk2dTextMesh SubtrationtextMesh;
    public tk2dTextMesh DivisiontextMesh;
	//為方便直接用scene執行設3, 要記得改回0!!!
	public static int setDifficulty=3;	// 1=Hard, 2=Mid, 3=Easy

	private int currentIndex;

    private static bool isGameLoaded = false;

    public static bool IsGameLoaded
    {
        get
        {
            return isGameLoaded;
        }
        set
        {
            isGameLoaded = value;
        }
    }
  


	float time;
	int count;
	public float exitTime = 1;

   
    void Start () {
		currentIndex = planet.GetComponent<SwipeControl> ().CurrentChoice;

		//先把地圖每一個物件關掉
		for (int i = 0; i < maps.childCount; i++) {
			maps.GetChild (i).gameObject.SetActive (false);
		}

        if (!isGameLoaded)
        {
            planet.gameObject.SetActive(false);
            startMenu.gameObject.SetActive(true);
            maps.gameObject.SetActive(true);
            arrow.gameObject.SetActive(false);
            //print(isGameLoaded);
        }
        else
        {
            arrow.gameObject.SetActive(true);
            planet.gameObject.SetActive(true);
            GetPlayerName();
            //print(isGameLoaded);
        }
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
        //UserNmaeInput.gameObject.SetActive(true);
       // canvas.gameObject.SetActive(true);

       if(!PlayerPrefs.HasKey("isFirstTime") || PlayerPrefs.GetInt("isFirstTime") != 1) //to check if it's our first time to load game
        {   //換成apk時要測試
            UserNameInput.gameObject.SetActive(true);
            canvas.gameObject.SetActive(true);
            PlayerPrefs.SetInt("isFirstTime", 1);
            PlayerPrefs.Save();
        }
        else
        {
            planet.gameObject.SetActive(true);
            arrow.gameObject.SetActive(true);

            AdditiontextMesh.text = PlayerPrefs.GetString("inputfield.text",inputfield.text);
            AdditiontextMesh.Commit();
            SubtrationtextMesh.text = PlayerPrefs.GetString("inputfield.text",inputfield.text);
            SubtrationtextMesh.Commit();
            DivisiontextMesh.text = PlayerPrefs.GetString("inputfield.text",inputfield.text);
            DivisiontextMesh.Commit();
        }
	}
    void YesButtonPressed()
    {  
        
        inputfield = GameObject.Find("InputField").GetComponent<InputField>();
       
        if (inputfield.text != null)
        { 
            AdditiontextMesh.text = inputfield.text;
            PlayerPrefs.SetString("inputfield.text", inputfield.text);
            print(inputfield.text);
            AdditiontextMesh.Commit();
        }else   //inputfield.text==null
            return;

        UserNameInput.gameObject.SetActive(false);
        planet.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);

        canvas.gameObject.SetActive(false);

        SubtrationtextMesh.text = PlayerPrefs.GetString("inputfield.text",inputfield.text);
        //SubtrationtextMesh.Commit();
        DivisiontextMesh.text = PlayerPrefs.GetString("inputfield.text",inputfield.text);
        //DivisiontextMesh.Commit();
    }

    void GetPlayerName()
    {
        AdditiontextMesh.text = PlayerPrefs.GetString("inputfield.text",inputfield.text);
        SubtrationtextMesh.text = PlayerPrefs.GetString("inputfield.text",inputfield.text);
        DivisiontextMesh.text = PlayerPrefs.GetString("inputfield.text",inputfield.text);
    }

    void NoButtonPressed()
    {
        startMenu.gameObject.SetActive (true);
        UserNameInput.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
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
    void RankPressed()
    {
        planet.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        Ranking.gameObject.SetActive(true);
        RankCanvas.gameObject.SetActive(true);
    }
    void OKButtonPressed()
    {
        planet.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);
        Ranking.gameObject.SetActive(false);
        RankCanvas.gameObject.SetActive(false);
    }
		
}
