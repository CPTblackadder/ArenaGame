using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManyScript : MonoBehaviour {
	public GameObject characterModel;
	public GameObject playingField;
	public Slider spawnNumberSlider;

	public void SpawnManyCharacters(){
		float minX = playingField.transform.position.x - (playingField.transform.lossyScale.x / 2);
		float maxX = playingField.transform.position.x + (playingField.transform.lossyScale.x / 2);
		float minY = playingField.transform.position.y - (playingField.transform.lossyScale.y / 2);
		float maxY = playingField.transform.position.y + (playingField.transform.lossyScale.y / 2);
		for (int i = 0; i < spawnNumberSlider.value; i++) {
			GameObject newCharacter = Instantiate (characterModel, new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),playingField.transform.position.z), Quaternion.identity);
			GlobalVariables.allCharacters.Add (newCharacter);
		}
	}
}
