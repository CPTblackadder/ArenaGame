using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSetUpScript : MonoBehaviour {

	public Text ButtonText;
	public ListControlSetUpScript SetUp;
	public CharacterStats stats;



	public void SetCharacter(CharacterStats stats)
	{
		this.stats = stats;
		ButtonText.text = stats.GetName();
	}

	public void Button_Click()
	{
		Debug.Log (stats.GetName ());
		SetUp.ButtonClicked(stats,this);
	}
}
