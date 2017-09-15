using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class CharacterStats {
	public string firstname;
	public string lastname;
	public int strength;
	public int social;
	public int speed;
	private bool hasImage;
	[System.NonSerialized] private Texture2D texture;



	public CharacterStats(){

	}


	public void Initialise(){
		firstname = GlobalVariables.fnames[Random.Range(0,GlobalVariables.fnames.Length)];
		lastname = GlobalVariables.lnames[Random.Range(0,GlobalVariables.lnames.Length)];
		//default stats
		strength = Random.Range (5, 10);
		speed = Random.Range (5, 10);
		social = Random.Range (5, 10);
		hasImage = false;
	}

	public string GetName(){
		return firstname + " " + lastname;
	}

	public Texture2D GetImage(){
		if (!hasImage) {
			//default texture
			return Texture2D.whiteTexture;
		} else {
			return texture;
		}
	}

	public void SetImage(Texture2D newImage){
		texture = newImage;
		hasImage = true;
	}

	public bool HasImage(){
		return hasImage;
	}
}
