using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControlScript : MonoBehaviour {

	public static UIControlScript me;
	public GameObject currentView;
	public Character currentChar;
	public Text statsViewerText;
	public GameObject spawnABunch;
	public Text swapModeButtonText;
	public Text logText;
	public RawImage characterPortrait;
	const int MAX_LOG_LENGTH = 4;
	private string[] textForTheLog = new string[MAX_LOG_LENGTH];
	private int numOfLogItems;
	private int oldestLogItem;

	UIControlScript(){
		GlobalVariables.logger = this;
	}


	void Awake () {
		if (me == null) {
			me = this;
			characterPortrait.enabled = false;
		} else if (this != me) {
			Destroy (gameObject);
		}
	}


	// Update is called once per frame
	void Update () {
		if (currentChar != null) {
			string bodyPartStatus = "";
			for (int i = 0; i < 7; i++) {
				bodyPartStatus += "\n" + currentChar.bodyParts[i].GetDescription() + ": " + currentChar.bodyParts[i].getStatus();
			}
			string clanPart;
			if (currentChar.clan != null) {
				clanPart = "\nClan: " + currentChar.clan.name;
			} else {
				clanPart = "";
			}
			statsViewerText.text = "Name: " + currentChar.stats.firstname + " " + currentChar.stats.lastname + 
				clanPart +
				"\nHealth: " + currentChar.health +
				"\nStrength: " + currentChar.stats.strength +
				"\nSpeed: " + currentChar.stats.speed + 
				"\nSocial: " + currentChar.stats.social +
				bodyPartStatus;
		} else {
			statsViewerText.text = "";
		}
	}

	public void SetChar(GameObject newView){
		if (newView == null) {
			currentChar = null;
			currentView = null;
			characterPortrait.enabled = false;
		} else {
			if (newView.name == "Player Circle(Clone)") {
				currentView = newView;
				currentChar = currentView.GetComponent<Character> ();
				if (currentChar.stats.HasImage ()) {
					characterPortrait.texture = currentChar.stats.GetImage ();
					characterPortrait.enabled = true;
				} else {
					characterPortrait.enabled = false;
				}
			} else {
				characterPortrait.enabled = false;
			}
		}
	}

	public void RemoveIfMe(GameObject possibleView){
		if (currentView == possibleView) {
			currentView = null;
			currentChar = null;
			characterPortrait.enabled = false;
		}
	}

	public void ToggleMode(){
		GlobalVariables.statsViewerOn = !GlobalVariables.statsViewerOn;
		if (GlobalVariables.statsViewerOn) {
			spawnABunch.SetActive (false);
			statsViewerText.enabled = true;
			swapModeButtonText.text = "View";
		} else {
			spawnABunch.SetActive (true);
			statsViewerText.enabled = false;
			swapModeButtonText.text = "Spawn";
		}
	}


	public void AddStringToLog(string message){
		if (numOfLogItems == MAX_LOG_LENGTH) {
			textForTheLog [oldestLogItem] = message;
			oldestLogItem = (oldestLogItem + 1) % MAX_LOG_LENGTH;
		} else {
			textForTheLog [numOfLogItems] = message;
			numOfLogItems += 1;
			oldestLogItem = (oldestLogItem + 1) % MAX_LOG_LENGTH;
		}
		logText.text = "";
		for (int i = 0; i < numOfLogItems; i++) {
			logText.text += textForTheLog [i];
		}
	}

	public void AddAttackToLog(Attack attack){
		AddStringToLog (attack.WriteDescription ());
	}


}