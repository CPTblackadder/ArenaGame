using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NameEditor : MonoBehaviour
{

	CharacterStats current;
	public Text firstNameText;
	public Text lastNameText;
	public InputField firstNameInput;
	public InputField lastNameInput;
	public ListControlScript listControl;
	public Button saveButton;
	public Button deleteButton;
	public RawImage image;
	public ImageController imageController;



	public void OnEditFirstName (){
		Debug.Log ("new first name is: " + firstNameInput.text);
		current.firstname = firstNameInput.text;
	}

	public void OnEditLastName (){
		Debug.Log ("new last name is: " + lastNameInput.text);
		current.lastname = lastNameInput.text;
	}

	public void setCurrent(CharacterStats stats){
		current = stats;
		firstNameInput.interactable = true;
		firstNameInput.text = stats.firstname;
		lastNameInput.interactable = true;
		lastNameInput.text = stats.lastname;
		saveButton.interactable = true;
		deleteButton.interactable = true;
		image.texture = stats.GetImage();
	}

	public void SaveCharacter(){
		GameControl.control.EditCharacter (current);
		listControl.RefreshList ();
		//TODO decide if it should unload a character when saved
		//unloadCurrent ();
	}

	public void NewCharacter(){
		CharacterStats newChar = new CharacterStats ();
		newChar.Initialise ();
		setCurrent(newChar);
	}

	public void DeleteCharacter(){
		GameControl.control.RemoveCharacter (current);
		listControl.RefreshList ();
		unloadCurrent ();
	}

	private void unloadCurrent(){
		current = null;
		firstNameInput.interactable = false;
		firstNameInput.text = "";
		lastNameInput.interactable = false;
		lastNameInput.text = "";
		saveButton.interactable = false;
		deleteButton.interactable = false;
		Debug.Log (Texture2D.whiteTexture);
		image.texture = Texture2D.whiteTexture;
	}


	public void ChangeImage(){
		current.SetImage(imageController.texture);
		image.texture = current.GetImage ();
	}

}

