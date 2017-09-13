using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ListControlScript : MonoBehaviour {

	public GameObject Button_Template;
	public NameEditor nameEditor;
	private ButtonScript currentSelection;

	// Use this for initialization
	void Start () {
		RefreshList();
	}

	public void ButtonClicked(CharacterStats stats, ButtonScript button)
	{
		SetSelection (button);
		button.GetComponent<Image> ().color = Color.red;
		Debug.Log(stats.GetName() + " button clicked.");
		nameEditor.setCurrent (stats);
	}

	public void RefreshList(){
		foreach (Button button in Button_Template.transform.parent.GetComponentsInChildren<Button> ()) {
			if (button.name != "ButtonTemplate") {
				Destroy (button.gameObject);
			}
		}

		foreach(CharacterStats chara in GameControl.control.GetCharacters())
		{
			GameObject go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			ButtonScript TB = go.GetComponent<ButtonScript>();
			TB.SetCharacter(chara);
			go.transform.SetParent(Button_Template.transform.parent);
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
