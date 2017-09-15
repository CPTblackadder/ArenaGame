using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class ImageListButtonScript : MonoBehaviour
{

	public Text ButtonText;
	public ImageListScript ScrollView;
	private FileInfo info;


	// Use this for initialization
	public void SetImage(FileInfo info)
	{
		this.info = info;
		ButtonText.text = info.Name;
	}

	public void Button_Click()
	{
		ScrollView.ButtonClicked(info, this);
	}
}
