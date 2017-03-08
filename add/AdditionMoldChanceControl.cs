using UnityEngine;
using System.Collections;

public class AdditionMoldChanceControl : MonoBehaviour {

	[System.Serializable]
	public struct ChanceStruct
	{
		public Vector3 endValue;
		public int dist;
	}
		
	public ChanceStruct[] CSArray = new ChanceStruct[3];

	public float transitTime = 3f;

	private bool lerping;
	private int currentDist;
	private AdditionPointGenerator apg;
	private AdditionScoreControl asc;

	private Vector3 startValue;

	// Use this for initialization
	void Start () {
	
		apg = gameObject.GetComponent<AdditionPointGenerator> ();
		asc = gameObject.GetComponent<AdditionScoreControl> ();

		lerping = false;

		//Debug
		foreach(ChanceStruct a in CSArray){
			if (a.dist == 0) {
				Debug.LogWarning("AI距離設定不正確");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		currentDist = Mathf.Abs(asc.TargetPoint - ScoreScript.CurrentPoint);

		startValue.x = apg.typeChance [0];
		startValue.y = apg.typeChance [1];
		startValue.z = apg.typeChance [2];

//		print ("dist:" + currentDist + " lerp?:" + lerping);

		if (currentDist <= CSArray[0].dist) { //<10
			//如果不是在lerp才觸動AI
			if (!lerping && startValue != CSArray [0].endValue) {
				StartCoroutine (lerpLoop (startValue, CSArray [0].endValue, transitTime * Time.deltaTime));
			}
		}else if (CSArray [0].dist < currentDist && currentDist < CSArray[2].dist) { // 10~50
			//如果不是在lerp才觸動AI
			if (!lerping && startValue != CSArray [1].endValue) {
				StartCoroutine (lerpLoop (startValue, CSArray [1].endValue, transitTime * Time.deltaTime));
			}
		}else if (currentDist >= CSArray[2].dist) {
			//如果不是在lerp才觸動AI
			if (!lerping && startValue != CSArray [2].endValue) {
				StartCoroutine (lerpLoop (startValue, CSArray [2].endValue, transitTime * Time.deltaTime));
			}
		}

	}

	private IEnumerator lerpLoop(Vector3 start,Vector3 end, float time){

		Vector3 temp = start;
		lerping = true;
		// Wait for next frame
		yield return null;

		while (temp.x != end.x || temp.y != end.y || temp.z != end.z) {
			temp.x = Mathf.Lerp (temp.x, end.x, transitTime * Time.deltaTime);
			temp.y = Mathf.Lerp (temp.y, end.y, transitTime * Time.deltaTime);
			temp.z = Mathf.Lerp (temp.z, end.z, transitTime * Time.deltaTime);

			apg.typeChance [0] = temp.x;
			apg.typeChance [1] = temp.y;
			apg.typeChance [2] = temp.z;

			yield return null;
		}

		lerping = false;
		StopCoroutine ("lerpLoop");
	}
}
