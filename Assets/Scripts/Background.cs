using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class Background : MonoBehaviour {

	public GameObject playerModel;
	public Text text;
	public Text modeButtonText;

	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR || UNITY_STANDALONE
		if (Input.GetMouseButtonDown (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
			if (!GlobalVariables.statsViewerOn){
				RaycastHit hit;
				Ray ray;
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast (ray, out hit)) {
					GameObject newCharacter = Instantiate(playerModel,hit.point,Quaternion.identity);
					GlobalVariables.allCharacters.Add (newCharacter);
				}
			} else {
				RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
				if(hit.collider != null){
					if (hit.collider.gameObject.name == "Player Circle(Clone)"){
						UIControlScript.SetChar(hit.collider.gameObject);
					}
				} else {
					UIControlScript.SetChar(null);
				}
			}

		}
		#elif UNITY_IOS || UNITY_ANDROID
		foreach (var touch in Input.touches) {
		if (touch.phase == TouchPhase.Began) {
		// Construct a ray from the current touch coordinates
		RaycastHit hit;
		Ray ray;
		ray = Camera.main.ScreenPointToRay (touch.position);
		if (Physics.Raycast (ray,out hit)) {
		// Create a particle if hit
		GameObject newCharacter = Instantiate (playerModel, hit.point, Quaternion.identity);
		GlobalVariables.allCharacters.Add (newCharacter);
		int newCount = GlobalVariables.allCharacters.Count;
		if (newCount > GlobalVariables.maxObjectsEver) {
		GlobalVariables.maxObjectsEver = newCount;
		}
		}
		}
		}
		#endif



	}


	//custom functions



}
