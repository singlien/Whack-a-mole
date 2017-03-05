using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class ScoreControlAbstract : MonoBehaviour {

	public tk2dTextMesh bannedDisplay;
	public tk2dTextMesh targetDisplay;
	public tk2dTextMesh currentDisplay;
	public tk2dTextMesh scoreDisplay;
	public Image fadeImage;

	public float fadeTime = 10f;



	protected void fade(){
		fadeImage.color = Color.red;
		StartCoroutine (recoverLoop ());
	}

	private IEnumerator recoverLoop(){
		
		while (fadeImage.color != Color.clear)
		{
//			print ("color:"+fadeImage.color);
			fadeImage.color = Color.Lerp (fadeImage.color, Color.clear, fadeTime * Time.deltaTime);
			yield return null;
		}
	}
}
