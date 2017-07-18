using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	//stats
	public string firstname;
	public string lastname;
	public int health;
	public int strength;
	public int social;
	public int speed;
	public Clan clan;
	private int turnsToAttack;
	public BodyPart[] bodyParts = new BodyPart[7];
	private bool readyToAttack;
	public HashSet<Clan> poorRelations = new HashSet<Clan>();


	public Character(){
		clan = null;

	}

	// Use this for initialization
	void Start () {
		if (firstname == "") {
			firstname = GlobalVariables.fnames[Random.Range(0,GlobalVariables.fnames.Length)];
		}
		if (lastname == "") {
			lastname = GlobalVariables.lnames[Random.Range(0,GlobalVariables.lnames.Length)];
		}
		//default stats
		if (health == 0) {
			health = 100;
		}
		if (strength == 0) {
			strength = Random.Range (5, 10);
		}
		if (speed == 0) {
			speed = Random.Range (5, 10);
		}
		if (social == 0) {
			social = Random.Range (5, 10);
		}
		for (int i = 0; i < bodyParts.Length; i++) {
			bodyParts[i] = new BodyPart(this,(BodyPart.BodyParts) i);
		}
		clan = null;

	}





	// Update is called once per frame
	void FixedUpdate () {
		GameObject target = LocateTarget ();
		float step = speed * Time.deltaTime;
		if (target != null) {
			//transform.position = Vector3.MoveTowards (transform.position, target.transform.position, step);
			Vector2 moveDirection = new Vector2(target.transform.position.x - transform.position.x,target.transform.position.y - transform.position.y ).normalized;
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude < speed){
				gameObject.GetComponent<Rigidbody2D> ().AddForce (moveDirection * 10);
			}
		}
		if (turnsToAttack / 3 >= Random.Range (25, 30) - speed) {
			readyToAttack = true;
		} else {
			turnsToAttack += 1;
		}

	}


	void OnCollisionEnter2D(Collision2D collider){
		Character otherCharacter = collider.gameObject.GetComponent<Character> ();
		if (otherCharacter != null) {
			Clan.setUpClanStep (this, otherCharacter);
			if (otherCharacter.speed < speed + Random.Range (-2, 3) && clan != otherCharacter.clan) {
				turnsToAttack += 5;
			}
		}

	}

	void OnCollisionStay2D(Collision2D collider){
		Character otherCharacter = collider.gameObject.GetComponent<Character> ();

		if (otherCharacter != null ) {
			if (readyToAttack && clan != otherCharacter.clan) {
				Attack attack;
				List<BodyPart> functionalLimbs = GetFunctionalLimbs ();
				BodyPart attackLimb;
				if (functionalLimbs.Count == 0) {
					attackLimb = bodyParts [0];
				} else {
					attackLimb = functionalLimbs [Random.Range (0, functionalLimbs.Count)];
				}
				switch (attackLimb.partId) {
				case BodyPart.BodyParts.LeftArm:
				case BodyPart.BodyParts.RightArm:
					attack = new Attack (this, otherCharacter, (Attack.AttackDirection)Random.Range (0, 3), attackLimb, new Weapon ());
					break;
				case BodyPart.BodyParts.LeftLeg: 
				case BodyPart.BodyParts.RightLeg:
					attack = new Attack (this, otherCharacter, (Attack.AttackDirection)Random.Range (0, 2), attackLimb, new Weapon ());
					break;
				default : 
					attack = new Attack (this, otherCharacter, Attack.AttackDirection.Low, attackLimb, new Weapon (-2));//on the gound as both legs not working, so can only attack low with head
					break;
				}
				collider.gameObject.SendMessage ("RecieveAttack", attack);
				poorRelations.Add (otherCharacter.clan);
				readyToAttack = false;
				turnsToAttack = 0;
			}
		} else {
		}
	}

	void OnDestroy(){
		GlobalVariables.allCharacters.Remove (gameObject);
		UIControlScript.RemoveIfMe (gameObject);
	}


	//custom functions

	private List<BodyPart> GetFunctionalLimbs(){
		List<BodyPart> list = new List<BodyPart>();
		if (bodyParts[1].IsFunctional()){//tests if the right arm is functional
			list.Add(bodyParts[1]);
		}
		if (bodyParts[2].IsFunctional()){//left arm
			list.Add(bodyParts[2]);
		}
		if (bodyParts[5].IsFunctional()){//right leg
			list.Add(bodyParts[5]);
		}
		if (bodyParts[6].IsFunctional()){//left leg
			list.Add(bodyParts[6]);
		}
		return list;
	}


	public void RecieveAttack (Attack attack){
		if (attack.defenderHitIn != null) {
			int remainingDamage = attack.defenderHitIn.TakeDamage (attack.damage);
			health -= remainingDamage;
			if (health <= 0 || bodyParts[0].health <= 0) {
				CharacterDie ();
			}
		}
	}

	//TODO add leaving a corpse
	private void CharacterDie(){
		GlobalVariables.logger.AddStringToLog (GetName () + " has died\n");
		GameObject.Destroy (gameObject);
		if (clan != null) {
			clan.removeMember (this);
		}
	}
		
	//returns closest character not in same clan
	private GameObject LocateTarget () {
		float closestDistance = float.MaxValue;
		float tempDistance;
		GameObject possibleTarget = null;
		foreach (var target in GlobalVariables.allCharacters){
			tempDistance = Vector3.Distance (this.transform.position, target.transform.position);
			if (closestDistance > tempDistance && target != gameObject && (clan == null || clan != target.GetComponent<Character>().clan)) {
				closestDistance = tempDistance;
				possibleTarget = target;
			}
		}
		return possibleTarget;
	}

	public string GetName(){
		return firstname + " " + lastname;
	}
}
