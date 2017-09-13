using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInitialCharacters : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameControl.control.getStartingCharacters ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
