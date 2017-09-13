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
	}

	public void SaveCharacter(){
		GameControl.control.EditCharacter (current);
		listControl.RefreshList ();
		unloadCurrent ();
	}

	public void NewCharacter(){
		setCurrent(new CharacterStats ());
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
	}
}

