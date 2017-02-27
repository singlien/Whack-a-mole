using UnityEngine;
using System.Collections;

public class MoleScript2 : MonoBehaviour {

	public tk2dSprite sprite;
	public tk2dTextMesh numberText;
	public Vector2 pointMinMax = new Vector2 (1f,9f);

	private float timeLimit;
	private bool isWhacked;			//is hit?
	private int molePoint = 0; 	//point on mole body
/*
	public void Trigger(float tl)
	{
		isWhacked = false;
		sprite.SetSprite("Mole_Normal");
		timeLimit = tl;
		//Set points on mole
		molePoint = (int)Random.Range (pointMinMax.x, pointMinMax.y);

		//Start the animation
		StartCoroutine (MainLoop());
	}

	void Start()
	{
		timeLimit = 1.0f;
		// Add mole to the main game script's mole container
		MainGameScript.Instance.RegisterMole(this);
	}

	// Main loop for the sprite.  Move up, then wait, then move down again. Simple.
	private IEnumerator MainLoop()
	{
		
		yield return StartCoroutine(WaitForHit());
	}

	// Give the player a chance to hit the mole.
	private void WaitForHit()
	{
		float time = 0.0f;

		while(!isWhacked && time < timeLimit)
		{
			time += Time.deltaTime;
			yield return null;
		}
	}
		
	// Mole has been hit
	public void Whack()
	{
		isWhacked = true;
		sprite.SetSprite("Mole_Hit");
	}

	public bool Whacked
	{
		get
		{
			return isWhacked;	
		}
	}

	public int MolePoint {
		get {
			return molePoint;
		}
	}
*/
}
