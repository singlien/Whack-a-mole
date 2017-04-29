﻿using UnityEngine;
using System.Collections;

public class DivisionDifficultyControl : MonoBehaviour {

	public enum difficulty
	{
		easy,normal,hard
	}

	public difficulty CurrentDifficulty;
	public tk2dSprite startInstruction;

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
        chooseMode.IsGameLoaded = true;

	}
	void Start(){

		//Set sprite
		switch(CurrentDifficulty){// H=7, M=8, E=6
		case difficulty.easy:
			startInstruction.spriteId = 6;
			break;
		case difficulty.normal:
			startInstruction.spriteId = 8;
			break;
		case difficulty.hard:
			startInstruction.spriteId = 7;
			break;
		default:
			Debug.LogWarning ("Unable to set startInstruction");
			break;
		}
		chooseMode.IsGameLoaded = true;

	}
}
