using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]//必須要有AudioSource，任何加上此腳本的物件將自動加入AudioSource
public class OpeningMovie : MonoBehaviour {

	#if UNITY_EDITOR
	public MovieTexture movieTexture;   //影片
	private AudioSource movieAudio;     //影片音軌
	#endif

	private int skipPressCount = 0;
	private float skipPressInterval = 0f;
	private float skipPressIntervalWait = 1f;
	private float movieDuration = 128.4f;	//行動裝置無法使用movieTexture，只能自定義影片時長

	void Start()
	{

		#if UNITY_EDITOR
		//Get source
		movieDuration = movieTexture.duration;
		GetComponent<RawImage>().texture = movieTexture;
		movieAudio = GetComponent<AudioSource>();
		movieAudio.clip = movieTexture.audioClip;//這個MovieTexture的音軌

		//Play
		movieTexture.Play();
		movieAudio.Play();

		#elif UNITY_ANDROID
		gameObject.GetComponent<RawImage>().color = Color.black;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		StartCoroutine(PlayOnMobile());

		#endif

		//When load menu will enter PlanetChoice directly
		chooseMode.isGameLoaded = true;
	}


	void Update()
	{
		// Skip for UNITY_EDITOR, debug only
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
		// Another program will load while playing the movie, thus the game will pause until the movie play was done.
		// Game will replay when the movie was finished, and we shall wait until then.
		yield return new WaitForEndOfFrame();	
		// Turn screen back to Portrait
		if(Screen.orientation!=ScreenOrientation.Portrait)
			Screen.orientation = ScreenOrientation.Portrait;
		// Wait until screen orientation turns to portrait
		while (Screen.currentResolution.height < Screen.currentResolution.width) {	//still landscape
			yield return null;
		}

		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");	//載入場景
	}

	IEnumerator PlayOnMobile(){
		// Wait until screen orientation turns to landscape
		while (Screen.currentResolution.height > Screen.currentResolution.width) {	// still portrait
			yield return null;
		}

		print("開始播放片頭");
		Handheld.PlayFullScreenMovie ("s2.mp4", Color.black, 
			FullScreenMovieControlMode.CancelOnInput,FullScreenMovieScalingMode.AspectFit);

		yield return null;	// Wait until next frame

		StartCoroutine (LoadScene ());
		
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
		if(Screen.orientation!=ScreenOrientation.Portrait)
			Screen.orientation = ScreenOrientation.Portrait;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");//載入場景
	}

}
