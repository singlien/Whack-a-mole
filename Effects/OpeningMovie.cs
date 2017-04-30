using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]//必須要有AudioSource，任何加上此腳本的物件將自動加入AudioSource
public class OpeningMovie : MonoBehaviour {

	public MovieTexture movieTexture;   //影片
	private AudioSource movieAudio;     //影片音軌


	private int skipPressCount = 0;
	private float skipPressInterval = 0f;
	private float skipPressIntervalWait = 1f;

	void Start()
	{
		// Set RawImage size



		#if UNITY_EDITOR
		//Get source
		GetComponent<RawImage>().texture = movieTexture;
		movieAudio = GetComponent<AudioSource>();
		movieAudio.clip = movieTexture.audioClip;//這個MovieTexture的音軌

		//Play
		movieTexture.Play();
		movieAudio.Play();
		#endif

		#if UNITY_ANDROID
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		Handheld.PlayFullScreenMovie (movieTexture.name+".mov", Color.black, 
			FullScreenMovieControlMode.Hidden,FullScreenMovieScalingMode.AspectFit);
		
		#endif

		//When load menu will enter PlanetChoice directly
		chooseMode.isGameLoaded = true;
	}


	void Update()
	{
		StartCoroutine(LoadScene());

		if (skipPressCount != 0) {
			skipPressInterval += Time.deltaTime;
			if (skipPressInterval > skipPressIntervalWait) {
				skipPressCount = 0;
				skipPressInterval = 0;
				print ("Reset skipPressCount");
			}
		}

	}

	IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(movieTexture.duration);//括號內填入影片時間
		Screen.orientation = ScreenOrientation.Portrait;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");//載入場景
	}

	public void SkipMovieButton(){
		print ("Skip movie pressed");

		skipPressCount++;
		if (skipPressCount == 2) {
			SkipMovie ();
			skipPressCount = 0;
		} else if (skipPressCount > 2) 
			Debug.LogError ("Skip movie count overbound!");
	}

	void SkipMovie(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");//載入場景
	}

}
