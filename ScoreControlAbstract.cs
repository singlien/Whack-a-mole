using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class ScoreControlAbstract : MonoBehaviour {

	public tk2dTextMesh bannedDisplay;
	public tk2dTextMesh targetDisplay;
	public tk2dTextMesh currentDisplay;
	public tk2dTextMesh scoreDisplay;

	public int gameTimeBonus = 10;
	public int gameTimeDeduct = -5;

	public Image fadeImage;
	public float fadeTime = 10f;



	protected void fade()
	{
		fadeImage.color = Color.red;
		StartCoroutine (recoverLoop ());
	}

	private IEnumerator recoverLoop()
	{
		// Turn back to clear slowly after amount of time
		while (fadeImage.color != Color.clear)
		{
			fadeImage.color = Color.Lerp (fadeImage.color, Color.clear, fadeTime * Time.deltaTime);
			yield return null;
		}
		StopCoroutine (recoverLoop());
	}
}
