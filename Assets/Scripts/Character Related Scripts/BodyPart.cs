using UnityEngine;
using System.Collections;

[System.Serializable]
public class BodyPart {
	public enum BodyParts {Head,RightArm,LeftArm,Chest,Stomach,RightLeg,LeftLeg};
	public enum PartStatus {Unhurt, Light, Moderate, Broken, Mangled, LoppedOff};
	public BodyParts partId;
	public Character belongsTo;
	public int health;
	public PartStatus status;

	public static bool isAnArm(BodyPart part){
		return (part.partId == BodyParts.RightArm || part.partId == BodyParts.LeftArm);
	}

	public BodyPart(Character partOf,BodyParts part){
		partId = part;
		status = PartStatus.Unhurt;
		belongsTo = partOf;
		health = 50;
	}

	//TODO add chopped off body parts
	public int TakeDamage(int damageToTake){
		health -= damageToTake;
		if (health >= 0) {
			if (health < 50 && health >= 35 && status != PartStatus.Light) {
				status = PartStatus.Light;
				GlobalVariables.logger.AddStringToLog(belongsTo.GetName() + "'s " + GetDescription() + " is lightly wounded\n");
			} else if (health < 35 && health >= 10 && status != PartStatus.Moderate) {
				status = PartStatus.Moderate;
				GlobalVariables.logger.AddStringToLog(belongsTo.GetName() + "'s " + GetDescription() + " is moderately wounded\n");
			} else if (health < 10 && health >= -5 && status != PartStatus.Mangled) {
				status = PartStatus.Mangled;
				GlobalVariables.logger.AddStringToLog(belongsTo.GetName() + "'s " + GetDescription() + " is mangled to pieces\n");
			} else if (health <= -5 && status != PartStatus.LoppedOff) {
				status = PartStatus.LoppedOff;
				GlobalVariables.logger.AddStringToLog(belongsTo.GetName() + "'s " + GetDescription() + " has been cut off\n");
			}
			return 0;
		}
		return damageToTake;
	}

	public bool IsFunctional(){
		return (status == PartStatus.Unhurt || status == PartStatus.Light || status == PartStatus.Moderate);
	}

	public string GetDescription(){
		switch (partId){
		case BodyParts.Head: 
			return "head";
		case BodyParts.Chest:
			return "chest";
		case BodyParts.LeftArm:
			return "left arm";
		case BodyParts.LeftLeg:
			return "left leg";
		case BodyParts.RightArm:
			return "right arm";
		case BodyParts.RightLeg:
			return "right leg";
		case BodyParts.Stomach:
			return "stomach";
		default :
			return "";
		}
	}

	public string getStatus(){
		switch (status) {
		case PartStatus.Unhurt:
			return "fine";
		case PartStatus.Light:
			return "scratched";
		case PartStatus.LoppedOff:
			return "cut off";
		case PartStatus.Mangled:
			return "mangled";
		case PartStatus.Moderate:
			return "wounded";
		case PartStatus.Broken:
			return "broken";
		default :
			return "";
		}
	}
}

