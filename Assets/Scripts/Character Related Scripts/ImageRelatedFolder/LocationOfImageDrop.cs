using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationOfImageDrop : MonoBehaviour {
	public Text text;


	// Use this for initialization
	void Start () {
		text.text = ("Put images you may want to select into this folder:\n" + Application.dataPath + "/images");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
