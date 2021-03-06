using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ListControlSetUpScript : MonoBehaviour {

	public GameObject Button_Template;
	public GameSetUp gameSetUp;
	public bool isAll;
	private ButtonScript currentSelection;

	public void ButtonClicked(CharacterStats stats, ButtonSetUpScript button)
	{
		gameSetUp.moveCharacter (stats, isAll);
	}

	public void RefreshList(){
		foreach (Button button in Button_Template.transform.parent.GetComponentsInChildren<Button> ()) {
			if (button.name != "ButtonTemplate") {
				Destroy (button.gameObject);
			}
		}
		if (isAll){
			foreach(CharacterStats chara in gameSetUp.getUnPicked())
			{
				GameObject go = Instantiate(Button_Template) as GameObject;
				go.SetActive(true);
				ButtonSetUpScript TB = go.GetComponent<ButtonSetUpScript>();
				TB.SetCharacter(chara);
				go.transform.SetParent(Button_Template.transform.parent);
			}
		} else {
			foreach(CharacterStats chara in gameSetUp.getPicked())
			{
				GameObject go = Instantiate(Button_Template) as GameObject;
				go.SetActive(true);
				ButtonSetUpScript TB = go.GetComponent<ButtonSetUpScript>();
				TB.SetCharacter(chara);
				go.transform.SetParent(Button_Template.transform.parent);
			}
		}

	}

	public void SetSelection(ButtonScript newSelection){
		//clear current selection
		if (currentSelection != null) {
			currentSelection.GetComponent<Image> ().color = Color.white;
		}
		//set new one
		if (newSelection != null) {
			newSelection.GetComponent<Image> ().color = Color.red;
			currentSelection = newSelection;
		}

	}

	public void ClearSelection(){
		SetSelection (null);
	}
}
