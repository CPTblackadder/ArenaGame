using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
	public static GameControl control;

	private string filePath;
	private List<CharacterStats> characters;
	private List<CharacterStats> startingCharacters;


	// Use this for initialization
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
			filePath = Application.persistentDataPath + "/characters.dat";
			characters = new List<CharacterStats>();
		} else if (this != control) {
			Destroy (gameObject);
		}
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

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (filePath);
		CharacterData data = new CharacterData (characters);

		bf.Serialize (file, data);
		file.Close ();
		//save images
		foreach (CharacterStats chara in characters){
			SavePicture(chara);
		}
	}

	public void Load(){
		if (File.Exists (filePath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (filePath,FileMode.Open);
			CharacterData data = (CharacterData)bf.Deserialize (file);
			file.Close();
			characters = data.characters;
			//load images
			foreach (CharacterStats chara in characters){
				LoadPicture(chara);
			}
		} else {
			Debug.Log ("No save file found");
		}
	}

	private void SavePicture(CharacterStats chara){
		if (!Directory.Exists(Application.persistentDataPath + "/images")){
			Directory.CreateDirectory (Application.persistentDataPath + "/images");
		}
		if (chara.HasImage()){
			File.WriteAllBytes (Application.persistentDataPath + "/images/" + chara.GetName() + ".png", chara.GetImage().EncodeToPNG());
		}
	}

	private void LoadPicture(CharacterStats chara){
		if (chara.HasImage ()) {
			byte[] image = File.ReadAllBytes (Application.persistentDataPath + "/images/" + chara.GetName () + ".png");
			Texture2D newtexture = new Texture2D (1, 1);
			newtexture.LoadImage (image);
			chara.SetImage (newtexture);
		}
	}

}

[Serializable]
class CharacterData {
	public List<CharacterStats> characters;

	public CharacterData(){
	}

	public CharacterData(List<CharacterStats> charas){
		characters = charas;
	}
}