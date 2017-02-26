using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainGameScript : MonoBehaviour
{	
	private List<MoleScript> moles = new List<MoleScript>();
	private bool gameEnd;

	public float hitTimeLimit = 1f;
	public Vector2 TimeWaitBeforeInstaniate = new Vector2 (5f, 1000f);
	public int moleLimit = 5;
	public Camera gameCam;
	public tk2dSpriteAnimator dustAnimator;
	public AudioClip moleHit;
	public int gameTime = 300;
	public tk2dTextMesh timeDisplay;

	// Treat this class as a singleton.  This will hold the instance of the class.
	private static MainGameScript instance;
	
	public static MainGameScript Instance
	{
		get
		{
			// This should NEVER happen, so we want to know about it if it does 
			if(instance == null)
			{
				Debug.LogError("MainGameScript instance does not exist");	
			}
			return instance;	
		}
	}

	void Awake()
	{
		instance = this; 
	}
	
	IEnumerator Start () 
	{
		gameEnd = false;
		hitTimeLimit = 1f;

		if (moleLimit > moles.Count)
			moleLimit = moles.Count;
		if (hitTimeLimit < 1f)
			hitTimeLimit = 1f;
		
		// Yield here to give everything else a chance to be set up before we start our main game loop
		
		yield return 0;  // wait for the next frame!

		dustAnimator.gameObject.SetActive(false);
		StartCoroutine(MainGameLoop());
	}
	
	void Update()
	{
		// Check to see if mouse has been clicked, and if so check to see if it has 'hit' any of the moles, and check which mole.
		if(Input.GetButtonDown ("Fire1"))
		{
			Ray ray = gameCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			
			if(Physics.Raycast(ray, out hit))
			{
				foreach(MoleScript mole in moles)
				{
					if(mole.sprite.gameObject.activeSelf && mole.ColliderTransform == hit.transform)
					{
						AudioSource.PlayClipAtPoint(moleHit, new Vector3());
//						ScoreScript.Score += mole.Whacked ? 0 : 10;
						mole.Whack();
						StartCoroutine(CallAnim(mole));
					}
				}
			}
		}


		//Control game time
		float timeLeft = DownCounter();
		if (timeLeft <= 0) {
			print ("Gameover");
			gameEnd = true;
		} else {
			print (timeLeft + "seconds left");
			timeDisplay.text = "Time Left:"+timeLeft+"s";
		}


	}
	
	private IEnumerator MainGameLoop()
	{
		int randomMole;
		
		while(!gameEnd)
		{
			yield return StartCoroutine(OkToTrigger());
			yield return new WaitForSeconds((float)Random.Range((int)TimeWaitBeforeInstaniate.x, (int)TimeWaitBeforeInstaniate.y) / 1000.0f);
			
			// Check if there are any free moles to choose from
			int availableMoles = 0;
			for (int i = 0; i < moles.Count; ++i) {
				if (!moles[i].sprite.gameObject.activeSelf) {
					availableMoles++;
				}
			}

			if (availableMoles > 0) {
				randomMole = (int)Random.Range(0, moles.Count - 1);			
				while(moles[randomMole].sprite.gameObject.activeSelf)
				{
					randomMole = (int)Random.Range(0, moles.Count - 1);
				}
					
				moles[ randomMole ].Trigger(hitTimeLimit);
//				hitTimeLimit -= hitTimeLimit <= 0.0f ? 0.0f : 0.01f;	// Less time to hit the next mole
			}
						
			yield return null;
		}
	}
	
	public void RegisterMole(MoleScript who)
	{
		moles.Add(who);
	}
	
	// Currently only 3 moles at a time can be active.  So if there are that many, then we can't trigger another one...
	private IEnumerator OkToTrigger()
	{
		int molesActive;

		do
		{
			yield return null;
			molesActive = 0;
			
			foreach(MoleScript mole in moles)
			{
				molesActive += mole.sprite.gameObject.activeSelf ? 1 : 0;
			}
		}
		while(molesActive >= moleLimit);

		yield break;
	}
	
	private IEnumerator CallAnim(MoleScript mole)
	{
		yield return new WaitForSeconds(0.25f);
		
		tk2dSpriteAnimator newAnimator;
		newAnimator = Instantiate(dustAnimator, new Vector3(mole.transform.position.x, mole.transform.position.y, dustAnimator.transform.position.z), dustAnimator.transform.rotation) as tk2dSpriteAnimator; 
		newAnimator.gameObject.SetActive(true);
		newAnimator.Play("DustCloud");
		
		while(newAnimator.IsPlaying("DustCloud"))
		{
			yield return null;	
		}
		
		Destroy(newAnimator.gameObject);
	}


	private float DownCounter(){

		return gameTime - Time.timeSinceLevelLoad;
	}

}
