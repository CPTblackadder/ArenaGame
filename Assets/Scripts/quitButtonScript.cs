using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class quitButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("escape")){
			QuitProgram();
		}
	}

	void QuitProgram(){
		Analytics.CustomEvent ("Quit", new Dictionary<string, object>
		{
				{"time spent playing",Time.fixedTime}
		});
		Debug.Log ("Quit Application");
		Application.Quit ();
	}
}
