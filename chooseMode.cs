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
	public Transform nameSprite;
	public Transform inputField;
	public Transform rank;

	//為方便直接用scene執行設3, 要記得改回0!!!
	public static int setDifficulty = 3;	// 1=Hard, 2=Mid, 3=Easy
	public static string setGameMode =  null;		// default=null

	public static bool isGameLoaded = false;
  
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
	Transform chp;
	bool inRanking = false;

	public float exitTime = 1;
	private int currentActivePortrait;
	private int currentIndex;
		 
    void Start () {

		// DO NOT REMOVE THE CODE BELOW
		Screen.orientation = ScreenOrientation.Portrait;
		//
		currentIndex = planet.GetComponent<SwipeControl> ().CurrentChoice;

		chp = nameSprite.FindChild ("ChangeHeadPad");
		if (chp == null)
			Debug.LogError ("Unable to load ChangeHeadPad");
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
			nameSprite.gameObject.SetActive (false);
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
			TapToStartPressed ();
			LastPlanetSelected ();
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
			if (!inRanking) {
				if (!startMenu.gameObject.activeSelf) {
					if (planet.gameObject.activeSelf)
						ReturnToTaptoStartScreen ();
					else
						ReturnButtonPressed ();
				}
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
			nameSprite.gameObject.SetActive (true);
			
            GetPlayerName();
        }
	}

	void LastPlanetSelected(){

		SwipeControl sc = GameObject.Find ("PlanetChoice").GetComponent<SwipeControl> ();
		if (sc == null) {
			Debug.LogError ("Unable to get script.");
			return;
		}

		switch (setGameMode) {
		case "Addition":
			//預設就會到，不做事
			break;
		case "Division":
			//上划兩個
			sc.swipeAfter ();
			sc.swipeAfter ();
			break;
		case "Subtraction":
			//上划一個
			sc.swipeAfter ();
			break;
		default:
			Debug.Log ("Unable to get last planet set.");
			break;
		}
		setGameMode = null;
	}

	//
	// Name Input Field
	//
    void YesButtonPressed()//輸入名字的inputField
    {  
        
        inputfield = GameObject.Find("InputField").GetComponent<InputField>();
       
		if (inputfield.text != "" && inputfield != null) 
		{ 
			nameSprite.GetComponentInChildren<tk2dTextMesh> ().text = inputfield.text;	//設定頭像名字
			PlayerPrefs.SetString (PlayerSaveFileName, inputfield.text);				//名字存檔
//			print (inputfield.text);	//Debug
		} else   //未輸入名字
		{
//			print("End of function");	//Debug
			return; //結束函數
		}			

        UserNameInput.gameObject.SetActive(false);
        planet.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);

        inputfield.enabled = false;
		//載入影片
		SceneManager.LoadScene ("StartAnimation", LoadSceneMode.Single);
    }

    void GetPlayerName()
    {
        if (PlayerPrefs.HasKey(PlayerSaveFileName))
        {
			nameSprite.GetComponentInChildren<tk2dTextMesh> ().text = PlayerPrefs.GetString(PlayerSaveFileName);	//設定頭像名字
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
		nameSprite.gameObject.SetActive (true);
	}
	// Return to Tap to start screen
	void ReturnToTaptoStartScreen(){
		startMenu.gameObject.SetActive (true);
		planet.gameObject.SetActive (false);
		arrow.gameObject.SetActive (false);
		nameSprite.gameObject.SetActive (false);
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
		nameSprite.gameObject.SetActive (false);
	}

	//
	// Ranking
	//
	void RankPressed(tk2dUIItem pressed)
    {
        planet.gameObject.SetActive (false);
        arrow.gameObject.SetActive (false);
		nameSprite.gameObject.SetActive (false);
		Ranking.gameObject.SetActive (true);
		inRanking = true;
		switch(pressed.name){
		case "RankSpriteAdd": 
			// Call Add Ranking
			rank.gameObject.SetActive (true);
			Ranking.FindChild ("RankingSpriteAdd").gameObject.SetActive (true);
			rank.gameObject.GetComponent<RankControl> ().LoadScore ("Add");
			break;
		case "RankSpriteSub":
			// Call Sub Ranking
			rank.gameObject.SetActive (true);
			Ranking.FindChild("RankingSpriteSub").gameObject.SetActive(true);
			rank.gameObject.GetComponent<RankControl> ().LoadScore ("Sub");
			break;
		case "RankSpriteDiv":
			// Call Div Ranking
			rank.gameObject.SetActive (true);
			Ranking.FindChild("RankingSpriteDiv").gameObject.SetActive(true);
			rank.gameObject.GetComponent<RankControl> ().LoadScore ("Div");
			break;
		default:
			//Debug, suppose no excute
			print (pressed.name);
			Debug.LogError ("Unable locate Ranking pressed");
			break;
		}
    }
    void OKButtonPressed()
    {
		planet.gameObject.SetActive (true);
		arrow.gameObject.SetActive (true);
		nameSprite.gameObject.SetActive (true);
        Ranking.gameObject.SetActive (false);
		for(int i=0;i<Ranking.childCount;i++){	//turnoff all ranking child
			Ranking.GetChild (i).gameObject.SetActive (false);
		}
		rank.gameObject.SetActive (false);
		inRanking = false;
    }

	//
	// Portrait
	//
    void HeadPressed()
    {
		nameSprite.FindChild ("ChangeHeadPad").gameObject.SetActive (true);
    }
    void ChangeHead(tk2dUIItem selected)
    { 
		//Deselect All Portrait
		for (int i = 0; i < chp.childCount; i++) 
		{
			chp.GetChild (i).GetChild(0).gameObject.SetActive (false);
		}

		selected.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		getCurrentPortrait ();
		SetPortrait ();
    }

	void SetPortrait()
	{
		if (currentActivePortrait == -1) {
			Debug.LogError("Unable to set Portrait");
			return;
		}
		nameSprite.FindChild ("Portrait").gameObject.GetComponent<tk2dSprite> ().spriteId = currentActivePortrait;
		currentActivePortrait = -1;

		// Close ChangeHeadPad
		chp.gameObject.SetActive(false);
	}

	void getCurrentPortrait()
	{
		for (int i = 0; i < chp.childCount; i++) 
		{
			if(chp.GetChild(i).FindChild("isChecked").gameObject.activeSelf) //check is activated
			{
				currentActivePortrait = chp.GetChild (i).gameObject.GetComponent<tk2dSprite> ().spriteId;
			}
		}
	}
   
}
