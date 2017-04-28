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
    public Transform PopChangeHeadPad;
    public Transform changeHead1;
    public Transform changeHead2;
    public Transform head;
	public Transform inputField;
	public Transform rank;

//    public Canvas canvas;
//    public Canvas RankCanvas;
    public tk2dTextMesh AdditiontextMesh;
    public tk2dTextMesh SubtrationtextMesh;
    public tk2dTextMesh DivisiontextMesh;
	//為方便直接用scene執行設3, 要記得改回0!!!
	public static int setDifficulty = 3;	// 1=Hard, 2=Mid, 3=Easy

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
  
    private string PlayerSaveFileName = "inputText";

    public string getPlayerSaveFileName
    {
        get
        {
            return PlayerSaveFileName;
        }
    }

	float time;
	int count;
	public float exitTime = 1;

//	void Awake(){
//		//關聯每一地圖上的物件
//		maps = GameObject.Find("MapChoice").transform;
//		planet = GameObject.Find("PlanetChoice").transform;
//		arrow = GameObject.Find("Arrow").transform;
//		startMenu = GameObject.Find("MenuSprite").transform;
//
//	}


   
    void Start () {
		currentIndex = planet.GetComponent<SwipeControl> ().CurrentChoice;

		//先把地圖每一個物件關掉
		for (int i = 0; i < maps.childCount; i++) {
			maps.GetChild (i).gameObject.SetActive (false);
		}

        if (!isGameLoaded)//遊戲開啟，進入tapToStart畫面
        {
            planet.gameObject.SetActive(false);
            startMenu.gameObject.SetActive(true);
            maps.gameObject.SetActive(true);
            arrow.gameObject.SetActive(false);

			Ranking.gameObject.SetActive (false);
			UserNameInput.gameObject.SetActive (false);

			//Canvus
			rank.gameObject.SetActive (false);
			inputField.gameObject.SetActive (false);

            if (PlayerPrefs.HasKey("isFirstTime"))// Not first time to excute the game
                GetPlayerName();
            //print(isGameLoaded);
        }
        else//玩完遊戲回到MENU
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

		// To check if it's our first time to load game
		if (!PlayerPrefs.HasKey ("isFirstTime") || PlayerPrefs.GetInt ("isFirstTime") != 1) { //第一次開啟遊戲//換成apk時要測試
			UserNameInput.gameObject.SetActive (true);
			inputField.gameObject.SetActive (true);
			PlayerPrefs.SetInt ("isFirstTime", 1);
		}
        else
        {	//直接進入planet選單
            planet.gameObject.SetActive(true);
            arrow.gameObject.SetActive(true);
			
            GetPlayerName();
        }
	}

	//
	// Name Input Field
	//
    void YesButtonPressed()//輸入名字的inputField
    {  
        
        inputfield = GameObject.Find("InputField").GetComponent<InputField>();
       
		if (inputfield.text != "" && inputfield != null) 
		{ 
			AdditiontextMesh.text = inputfield.text;
			PlayerPrefs.SetString (PlayerSaveFileName, inputfield.text);
			PlayerPrefs.Save ();
			print (inputfield.text);
		} else   //inputfield.text==""
		{
			return; //結束函數
			print("End of function");
		}
			

        UserNameInput.gameObject.SetActive(false);
        planet.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);

        inputfield.enabled = false;
        //canvas.gameObject.SetActive(false);

        SubtrationtextMesh.text = PlayerPrefs.GetString(PlayerSaveFileName);
        DivisiontextMesh.text = PlayerPrefs.GetString(PlayerSaveFileName);

		//載入影片
		SceneManager.LoadScene ("StartAnimation", LoadSceneMode.Single);
    }

    void GetPlayerName()
    {
        if (PlayerPrefs.HasKey(PlayerSaveFileName))
        {
            AdditiontextMesh.text = PlayerPrefs.GetString(PlayerSaveFileName);
            SubtrationtextMesh.text = PlayerPrefs.GetString(PlayerSaveFileName);
            DivisiontextMesh.text = PlayerPrefs.GetString(PlayerSaveFileName);
        }
        else
            Debug.LogError("Unable to load saved data!");
    }

    void NoButtonPressed()
    {
        startMenu.gameObject.SetActive (true);
        UserNameInput.gameObject.SetActive(false);
		inputField.gameObject.SetActive(false);
		PlayerPrefs.DeleteKey("isFirstTime");

    }
	// Return to planet choose
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

	//
	// Ranking
	//
    void RankPressed()
    {
        planet.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        Ranking.gameObject.SetActive(true);
		rank.gameObject.SetActive(true);
    }
    void OKButtonPressed()
    {
        planet.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);
        Ranking.gameObject.SetActive(false);
		rank.gameObject.SetActive(false);
    }
    void HeadPressed()
    {
        PopChangeHeadPad.gameObject.SetActive(true);


    }
    void ChangeHead()
    {       //如果小丸子的check圖沒有被active
        if (changeHead1.gameObject.activeInHierarchy == false)
        {
            changeHead1.gameObject.SetActive(true);
            changeHead2.gameObject.SetActive(false);
            head.gameObject.SetActive(true);
        }
        else
        {
            changeHead1.gameObject.SetActive(false);
            changeHead2.gameObject.SetActive(true);
            head.gameObject.SetActive(false);
        }
    }
   
   
}
