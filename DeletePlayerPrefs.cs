using UnityEngine;
using System.Collections;

//
// DEBUG ONLY
//
public class DeletePlayerPrefs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll ();
		print ("All PlayerPrefs deleted");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
