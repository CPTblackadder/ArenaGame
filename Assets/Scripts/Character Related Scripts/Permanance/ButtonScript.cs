using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public Text ButtonText;
	public ListControlScript ScrollView;
	public CharacterStats stats;



	public void SetCharacter(CharacterStats stats)
	{
		this.stats = stats;
		ButtonText.text = stats.GetName ();
	}

	public void Button_Click()
	{
		Debug.Log (stats.GetName ());
		ScrollView.ButtonClicked(stats,this);
	}
}