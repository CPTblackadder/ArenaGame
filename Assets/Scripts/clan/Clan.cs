using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clan {
	private static ColourManager colourManager = new ColourManager();
	public Character leader;
	public string name;
	public Color clanColour;
	private List<Character> members = new List<Character>();
	private int numberOfMembers = 0;



	public static void setUpClanStep(Character former1, Character former2){
		if (former1.clan != null && former2.clan == null) {//try to recruit collided
			if (former1.stats.social + former2.stats.social - former1.clan.GetNumbers () > Random.Range (10, 20) && !former2.poorRelations.Contains(former1.clan)) {
				former1.clan.addMember (former2);
			}
		} else if (former1.clan == null && former2.clan != null){//try to be recruited by collided
			if (former1.stats.social + former2.stats.social - former2.clan.GetNumbers () > Random.Range (10, 20) && !former1.poorRelations.Contains(former2.clan)) {
				former2.clan.addMember (former1);
			}
		} else if (former1.clan == null && former2.clan == null && colourManager.colourIsAvailable()) {
			if (former1.stats.social + former2.stats.social > Random.Range (10, 20)) {
				new Clan (former1, former2);
			}
		}
	}

	public Clan(Character former1, Character former2){
		if (former1.stats.social >= former2.stats.social) {
			leader = former1;
		} else {
			leader = former2;
		}
		members.Add (former1);
		members.Add (former2);
		former2.clan = this;
		former1.clan = this;
		name = former1.GetName() + "'s clan";
		numberOfMembers = 2;
		clanColour = colourManager.getAColour();
		former1.gameObject.GetComponent<SpriteRenderer> ().color = clanColour;
		former2.gameObject.GetComponent<SpriteRenderer> ().color = clanColour;
		Debug.Log(former1.GetName() + " and " + former2.GetName() + " set up " + name);
		GlobalVariables.logger.AddStringToLog (name + " is set up\n");
	}

	public Clan(){
		if (numberOfMembers == 0) {
			throw new System.SystemException ("can't create an empty clan\n");
		}
	}

	public void addMember(Character newMember){
		GlobalVariables.logger.AddStringToLog (newMember.GetName() + " joins " + name + "\n");
		Debug.Log (newMember.GetName () + " joins " + name);
		members.Add (newMember);
		newMember.clan = this;
		newMember.gameObject.GetComponent<SpriteRenderer> ().color = clanColour;
		numberOfMembers += 1;
	}

	public void removeMember(Character oldMember){
		members.Remove (oldMember);
		oldMember.clan = null;
		oldMember.gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
		numberOfMembers -= 1;
		if (numberOfMembers == 0) {
			disbandClan ();
		}
	}

	public int GetNumbers(){
		return numberOfMembers;
	}

	public void disbandClan(){
		foreach (Character m in members){
			m.clan = null;
			m.gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
		}
		GlobalVariables.logger.AddStringToLog (name + " is disbanded\n");
		colourManager.relenquishColour (clanColour);
	}


}


