using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ImageController : MonoBehaviour {

	public RawImage image;
	public Texture2D texture;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void loadWhite(){
		image.texture = Texture2D.whiteTexture;
	}

	public void setCurrent(FileInfo file){
		byte[] fileData = File.ReadAllBytes (file.FullName);
		texture = new Texture2D (1, 1);
		texture.LoadImage (fileData);
		image.texture = texture;
	}
}
