using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	#if UNITY_EDITOR || UNITY_STANDALONE
	public GameObject background;
	public const float cameraKeyboardPanSpeed = 2f;
	public const float cameraMousePanSpeed = 0.025f;
	public const float cameraScrollSpeed = 1f;
	public const int minZoomSize = 15;
	private bool middleMousePressed = false;
	private Vector3 mouseOrigin;


	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(2)){
			//start panning by mouse
			middleMousePressed = true;
			mouseOrigin = Input.mousePosition;
		} else if (Input.GetMouseButtonUp (2)) {
			middleMousePressed = false;
		}
		if (middleMousePressed) {
			Vector3 pos = (Input.mousePosition - mouseOrigin);
			translateCamera (pos.x * cameraMousePanSpeed, pos.y * cameraMousePanSpeed);
		} else {
			float xAxisValue = Input.GetAxis ("Horizontal") * cameraKeyboardPanSpeed;
			float yAxisValue = Input.GetAxis ("Vertical") * cameraKeyboardPanSpeed;
			translateCamera (xAxisValue, yAxisValue);
		}
		zoomCamera (-Input.mouseScrollDelta.y * cameraScrollSpeed);
	}

	private void translateCamera(float xAxisValue, float yAxisValue){
		float maxX = background.transform.position.x + (background.transform.lossyScale.x / 2) - (Camera.main.orthographicSize * Camera.main.aspect);
		float minX = background.transform.position.x - (background.transform.lossyScale.x / 2) + (Camera.main.orthographicSize * Camera.main.aspect);
		float maxY = background.transform.position.y + (background.transform.lossyScale.y / 2) - Camera.main.orthographicSize;
		float minY = background.transform.position.y - (background.transform.lossyScale.y / 2) + Camera.main.orthographicSize;
		Camera.main.transform.position = new Vector3 (
			Mathf.Min (Mathf.Max (Camera.main.transform.position.x + xAxisValue, minX), maxX) + (Mathf.Max(Camera.main.orthographicSize * 2 * Camera.main.aspect - background.transform.lossyScale.x,0))/2,
			Mathf.Min (Mathf.Max (Camera.main.transform.position.y + yAxisValue, minY), maxY) + (Mathf.Max(Camera.main.orthographicSize * 2 - background.transform.lossyScale.y,0))/2,
			Camera.main.transform.position.z
		);

	}

	//zoom negative to zoom in
	private void zoomCamera(float zoomAmount){
		float maxZoomSize = Mathf.Max (background.transform.lossyScale.x, background.transform.lossyScale.y) / 2;
		Camera.main.orthographicSize = Mathf.Max(Mathf.Min(Camera.main.orthographicSize+zoomAmount,maxZoomSize),minZoomSize);
		translateCamera (0.0f, 0.0f);
	}
	#endif
}
