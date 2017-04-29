using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ScoreBoard : MonoBehaviour {

    public static List<int> myList = new List<int>();   //暫存分數的清單

    public GameObject PlayerScoreListPrefab;
	private GameObject instGameObj;	//分數物件生成的

    private chooseMode cm;

    private string PlayerScoreSavedFileName = "PlayerScore";

	// Use this for initialization
	void Start () {

        cm = GameObject.Find("GameManager").GetComponent<chooseMode>();
        LoadScore();

        //due to score is static so u should use class to use its function
        if (ScoreScript.Score >= 0)
        {
            
            while (myList.Count <= 8)
            {
                myList.Add(0);
            }
			// 清空排行榜
			ClearLayout();
			// 生成排行榜
			for (int i = 0; i < 8; i++)
			{
				instGameObj = (GameObject)Instantiate (PlayerScoreListPrefab);
				//SetParent(transform,false) the false can moderate the prefab scale instantiate on UI
				instGameObj.transform.SetParent (transform, false);
				instGameObj.transform.Find ("RankText").GetComponent<Text> ().text = "No." + (i+1);

				instGameObj.transform.Find ("NameText").GetComponent<Text> ().text = PlayerPrefs.GetString (cm.getPlayerSaveFileName);
				instGameObj.transform.Find ("ScoreText").GetComponent<Text> ().text = myList [i].ToString ();
				//print(PlayerPrefs.GetInt("ScoreScript.Score").ToString());
   			}
           
        }
    }
 
    void OnDestroy(){
        SaveScore();
    }

    public static void SortList(){ //大到小排序
        myList.Sort();
        myList.Reverse();
    }

    void SaveScore(){
        SortList();
        if (myList.Count > 0){
            for (int i = 0; i < 8; i++)
            {
				try{
					PlayerPrefs.SetInt(PlayerScoreSavedFileName + i, myList[i]);
				}catch{
					PlayerPrefs.SetInt(PlayerScoreSavedFileName + i, 0);
				}
            }
        }
    }

    void LoadScore(){
        SaveScore();
        myList.Clear();
        for (int i = 0; i < 8; i++)
        {
            myList.Add(PlayerPrefs.GetInt(PlayerScoreSavedFileName + i));
        }
    }

	void ClearLayout(){
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			Destroy (gameObject.transform.GetChild (i).gameObject);
		}
	}
}
