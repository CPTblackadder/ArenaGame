using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
	public static GameControl control;

	private List<CharacterStats> characters;
	private List<CharacterStats> startingCharacters;


	// Use this for initialization
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (this != control) {
			Destroy (gameObject);
		}
		characters = new List<CharacterStats>();
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
		characters.Add (new CharacterStats ());
	}
		

	//Adds a new character, if one with the same name already exists replace it
	//TODO might like multiple characters with the same name
	public void EditCharacter(CharacterStats editedChar){
		CharacterStats existant = characters.Find (x => x.GetName () == editedChar.GetName ());
		if (existant == null) {
			characters.Add (editedChar);
		} else {
			existant = editedChar;
		}
	}

	public void RemoveCharacter(CharacterStats removeThis){
		characters.Remove (removeThis);
	}

	public List<CharacterStats> GetCharacters(){
		return characters;
	}

	public void setStartingCharacters(List<CharacterStats> start){
		startingCharacters = start;
	}

	public List<CharacterStats> getStartingCharacters(){
		return startingCharacters;
	}

}
