using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class ImageListScript : MonoBehaviour {

	public GameObject Button_Template;
	public ImageController imageController;
	private ImageListButtonScript currentSelection;

	// Use this for initialization
	void Start () {
		RefreshList();
	}

	public void ButtonClicked(FileInfo file,ImageListButtonScript button)
	{
		Debug.Log (file.Name + " pressed " + file.Extension);
		SetSelection (button);
		imageController.setCurrent (file);
	}

	public void RefreshList(){
		foreach (Button button in Button_Template.transform.parent.GetComponentsInChildren<Button> ()) {
			if (button.name != "ButtonTemplate") {
				Destroy (button.gameObject);
			}
		}
		var info = new DirectoryInfo (Application.dataPath + "/images");

		foreach (FileInfo file in info.GetFiles ()){
			if (file.Extension != ".meta") {
				
				Debug.Log (file.Name);
				GameObject go = Instantiate (Button_Template) as GameObject;
				go.SetActive (true);
				ImageListButtonScript TB = go.GetComponent<ImageListButtonScript> ();
				TB.SetImage (file);
				go.transform.SetParent (Button_Template.transform.parent);
			}
		}
	}

	public void SetSelection(ImageListButtonScript newSelection){
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
