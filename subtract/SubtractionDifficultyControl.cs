using UnityEngine;
using System.Collections;

public class SubtractionDifficultyControl : MonoBehaviour {

	public enum difficulty
	{
		easy,normal,hard
	}

	public difficulty CurrentDifficulty;

	SubtractionScoreControl ssc;

	// Use this for initialization
	void Awake () {
	
		ssc = gameObject.GetComponent<SubtractionScoreControl> ();

		switch (chooseMode.setDifficulty) {

		case 1:
			CurrentDifficulty = difficulty.hard;
			break;
		case 2:
			CurrentDifficulty = difficulty.normal;
			break;
		case 3:
			CurrentDifficulty = difficulty.easy;
			break;
		default:
			Debug.LogError ("Unable to set difficulty in subtraction mode");
			break;
		}

		switch (CurrentDifficulty) {

		case difficulty.easy:
			ssc.targetMinMax = new Vector2 (0f, 100f);
			ssc.distanceMinMax = new Vector2 (30f, 50f);
			ssc.BanSwitch = false;
			break;
		case difficulty.normal:
			ssc.targetMinMax = new Vector2 (0f, 100f);
			ssc.distanceMinMax = new Vector2 (50f, 80f);
			ssc.bannedMinMax = new Vector2 (0f, 9f);
			ssc.BanSwitch = true;
			ssc.bannedMode = false;
			break;
		case difficulty.hard:
			ssc.targetMinMax = new Vector2 (0f, 100f);
			ssc.distanceMinMax = new Vector2 (80f, 100f);
			ssc.bannedMinMax = new Vector2 (12f, 98f);
			ssc.BanSwitch = true;
			ssc.bannedMode = true;
			break;
		}
	}

}
