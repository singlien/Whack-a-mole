using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ScoreBoard : MonoBehaviour {

    List<int> mylist;
    public GameObject PlayerScoreListPrefab;
    public GameObject go;
    int r = 0, x = 1;
	// Use this for initialization
	void Start () {
          //due to score is static so u should use class to use its function
        if (ScoreScript.Score >= 0 && chooseMode.inputfield.text != null)
        {
            for (int i = 0; i < 8; i++)
            {
                go = (GameObject)Instantiate(PlayerScoreListPrefab);
                //SetParent(transform,false) the false can moderate the prefab scale instantiate on UI
                go.transform.SetParent(transform,false);
                go.transform.Find("RankText").GetComponent<Text>().text = "No."+x;

                if (r == 1)
                {
                    go.transform.Find("NameText").GetComponent<Text>().text = PlayerPrefs.GetString("inputfield.text");
                    go.transform.Find("ScoreText").GetComponent<Text>().text = PlayerPrefs.GetInt("ScoreScript.Score").ToString();
                    print(PlayerPrefs.GetInt("ScoreScript.Score").ToString());
                }
                else
                {
                    go.transform.Find("NameText").GetComponent<Text>().text = PlayerPrefs.GetString("inputfield.text");
                    go.transform.Find("ScoreText").GetComponent<Text>().text = PlayerPrefs.GetInt("ScoreScript.Score").ToString();
                    print(PlayerPrefs.GetInt("ScoreScript.Score").ToString());
                }


                x++;
            }
           
        }
        }

	
	// Update is called once per frame
	void Update () {
       
	}
}
