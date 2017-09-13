using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetUp : MonoBehaviour {

	public Text noOfPlayers;
	public List<CharacterStats> unpickedPlayers;
	public List<CharacterStats> pickedPlayers;
	public ListControlSetUpScript unpickedPlayersList;
	public ListControlSetUpScript pickedPlayersList;


	public void Start(){
	}

	public void SetUpLists(){
		unpickedPlayers = GameControl.control.GetCharacters ();
		pickedPlayers = new List<CharacterStats> ();
		refreshLists ();
	}

	public void moveCharacter(CharacterStats chara, bool fromUnPicked){
		if (fromUnPicked) {
			unpickedPlayers.Remove (chara);
			pickedPlayers.Add (chara);
			refreshLists ();
		} else {
			pickedPlayers.Remove (chara);
			unpickedPlayers.Add (chara);
			refreshLists ();
		}
	}

	public void updatePlayerNumber(float i){
		noOfPlayers.text = i.ToString ();
	}

	public List<CharacterStats> getUnPicked(){
		return unpickedPlayers;
	}

	public List<CharacterStats> getPicked(){
		return pickedPlayers;
	}


	private void refreshLists(){
		unpickedPlayersList.RefreshList ();
		pickedPlayersList.RefreshList ();
	}


	public void StartGame(){
		GameControl.control.setStartingCharacters (pickedPlayers);
		SceneManager.LoadScene ("Scenes/Main Scene");

	}
}
