using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour
{

	static private int score;
	static private int currentPoint = 0;
	static private int hitPoint;

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

	static public int CurrentPoint {
		get {
			return currentPoint;
		}
		set {
			currentPoint = value;
		}
	}

	public static int HitPoint {
		get {
			return hitPoint;
		}
		set {
			hitPoint = value;
		}
	}

	// Use this for initialization
	void Start ()
	{
		score = 0;
		hitPoint = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		Debug.Log ("Score:" + score + ", now:" + currentPoint);
	}
}
