using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour
{
	static private tk2dTextMesh scoreText;
	static private int score;
	static private int currentPoint;

	static public int CurrentPoint {
		get {
			return currentPoint;
		}
		set {
			currentPoint = value;
		}
	}

	static public int Score
	{
		get
		{
			return score;	
		}
		
		set
		{
			score = value;	
		}
	}


	// Use this for initialization
	void Start ()
	{
		score = 0;
		currentPoint = 0;
		scoreText = gameObject.GetComponent<tk2dTextMesh> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = string.Format("Score: {0}", score);
		scoreText.Commit();
		Debug.Log ("Score:" + score + ", now:" + currentPoint);
	}
}
