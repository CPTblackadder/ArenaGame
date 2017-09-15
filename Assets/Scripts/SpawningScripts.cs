using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawningScripts : MonoBehaviour {
	public GameObject characterModel;
	public GameObject playingField;
	public Slider spawnNumberSlider;
	public static SpawningScripts script;
	float minX;
	float maxX;
	float minY;
	float maxY;

	public void Start(){
		if (script == null) {
			script = this;
		} else if (this != script) {
			Destroy (gameObject);
		}
		minX = playingField.transform.position.x - (playingField.transform.lossyScale.x / 2);
		maxX = playingField.transform.position.x + (playingField.transform.lossyScale.x / 2);
		minY = playingField.transform.position.y - (playingField.transform.lossyScale.y / 2);
		maxY = playingField.transform.position.y + (playingField.transform.lossyScale.y / 2);
		if (GameControl.control.getStartingCharacters () != null) {
			foreach (CharacterStats ch in GameControl.control.getStartingCharacters ()) {
				spawnACharacter (ch);
			}
		}
	}


	public void SpawnManyCharacters(){
		for (int i = 0; i < spawnNumberSlider.value; i++) {
			GameObject newCharacter = Instantiate (characterModel, new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),playingField.transform.position.z), Quaternion.identity);
			newCharacter.GetComponent<Character> ().stats.Initialise ();
			GlobalVariables.aliveCharacters.Add (newCharacter);
		}
	}

	public void spawnACharacter(CharacterStats chara){
		GameObject newCharacter = Instantiate (characterModel, new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),playingField.transform.position.z), Quaternion.identity);
		Character initialisedCharacter = newCharacter.GetComponent<Character> ();
		initialisedCharacter.stats = chara;
		GlobalVariables.aliveCharacters.Add (newCharacter);
	}

	public void spawnACharacter(Vector3 position){
		GameObject newCharacter = Instantiate (characterModel, position, Quaternion.identity);
		newCharacter.GetComponent<Character> ().stats.Initialise ();
		GlobalVariables.aliveCharacters.Add (newCharacter);
	}


}
