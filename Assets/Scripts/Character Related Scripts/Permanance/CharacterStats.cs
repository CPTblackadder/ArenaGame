using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStats {
	public string firstname;
	public string lastname;
	public int strength;
	public int social;
	public int speed;



	public CharacterStats(){
		firstname = GlobalVariables.fnames[Random.Range(0,GlobalVariables.fnames.Length)];
		lastname = GlobalVariables.lnames[Random.Range(0,GlobalVariables.lnames.Length)];
		//default stats
		strength = Random.Range (5, 10);
		speed = Random.Range (5, 10);
		social = Random.Range (5, 10);

	}


	public string GetName(){
		return firstname + " " + lastname;
	}

}
