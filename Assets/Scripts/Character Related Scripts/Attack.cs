using UnityEngine;
using System.Collections;

public class Attack {
	public enum AttackDirection {Low, Middle, High};
	public AttackDirection attackDirection;
	public Character attacker;
	public Character defender;
	public Weapon weapon;
	public BodyPart defenderHitIn;
	public BodyPart limbUsed;
	public int damage;

	public Attack(Character attacker, Character defender, AttackDirection attackDirection, BodyPart limbUsed, Weapon weapon){
		this.attacker = attacker;
		this.defender = defender;
		this.attackDirection = attackDirection;
		this.limbUsed = limbUsed;
		this.weapon = weapon;
		this.defenderHitIn = ChooseDefenderHitLocation ();
		this.damage = DetermineDamageDone ();
		GlobalVariables.logger.AddAttackToLog (this);
		defender.RecieveAttack (this);
	}

	//TODO add stuff to simulate being knocked down onto the ground
	private BodyPart ChooseDefenderHitLocation(){
		int random;
		switch (attackDirection){
		case AttackDirection.Low :
			random = Random.Range (0, 3);
			return defender.bodyParts[random + 4];
		case AttackDirection.Middle :
			random = Random.Range (0,4);
			return defender.bodyParts[random + 1];
		case AttackDirection.High :
			random = Random.Range (0,3);
			return defender.bodyParts[random];
		default: 
			return null;
		}
	}

	private int DetermineDamageDone (){
		int random = Random.Range (0, 7);
		return Mathf.Max(0,(random + attacker.strength - defender.strength + (weapon.damage * 2)));
	}

	public string WriteDescription(){
		return attacker.GetName () + " has " + GetAttackVerb() + " " + defender.GetName () + " in the " + defenderHitIn.GetDescription () + "\n";
	}

	private string GetAttackVerb(){
		switch (limbUsed.partId) {
		case BodyPart.BodyParts.Head:
			return "head butted";
		case BodyPart.BodyParts.LeftArm:
		case BodyPart.BodyParts.RightArm:
			return "punched";
		case BodyPart.BodyParts.LeftLeg:
		case BodyPart.BodyParts.RightLeg:
			return "kicked";
		default:
			return "attacked";
		}
	}

}

