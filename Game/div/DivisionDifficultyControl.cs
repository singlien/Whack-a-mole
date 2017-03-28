using UnityEngine;
using System.Collections;

public class DivisionDifficultyControl : MonoBehaviour {

	public enum difficulty
	{
		easy,normal,hard
	}

	public difficulty CurrentDifficulty;

	DivisionScoreControl dsc;
	// Use this for initialization
	void Awake () {
		
		dsc = gameObject.GetComponent<DivisionScoreControl> ();

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
			Debug.LogError ("Unable to set difficulty in division mode");
			break;
		}

		switch (CurrentDifficulty) {

		case difficulty.easy:
			for (int i = 0; i < 4; i++)
				dsc.indexOnOff [i] = true;
			dsc.indexAdditionMinMax = new Vector2 (2f, 3f);
			break;
		case difficulty.normal:
			for (int i = 0; i < 5; i++)
				dsc.indexOnOff [i] = true;
			dsc.indexAdditionMinMax = new Vector2 (3f, 5f);
			dsc.highIndexStart = 3;
			dsc.highIndexMinMax = new Vector2 (0f, 1f);
			break;
		case difficulty.hard:
			for (int i = 0; i < dsc.indexOnOff.Length; i++)
				dsc.indexOnOff [i] = true;
			dsc.indexAdditionMinMax = new Vector2 (5f, 7f);
			dsc.highIndexStart = 3;
			dsc.highIndexMinMax = new Vector2 (0f, 3f);
			break;
		}

	}

}
