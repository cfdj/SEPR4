﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
//CombatAbility that takes no targets and applys a CombatEffect to the user
public class SelfEffect : CombatAbility {
	string ownName;
	public string abilityName { get { return ownName; } }
	public int minTargets { get { return 0; } }
	public int maxTargets { get { return 0; } }
	int ownCost;
	public int energyCost { get { return ownCost; } }
	bool ownAssist;
	public bool isAssist { get {return ownAssist; } }
	CombatEffect ownEffect;
	public bool isGorrila { get { return false; } }

	public SelfEffect(CombatEffect effect, int energyCost, string abilityName){
		ownName = abilityName;
		ownAssist = isAssist;
		ownCost = energyCost;
		ownEffect = effect;
	} 

	public void doAbility (List<CombatCharacter> targets, CombatCharacter user){
		if (user.energy < energyCost) {
			throw new ArgumentException ("this ability can't be used because the user doesn't have enough energy");
		}
		if (targets.Count > maxTargets || targets.Count < minTargets) {
			throw new ArgumentException (string.Format ("invalid target count: {C0}, acceptable range: {C1}-{C2}", targets.Count, minTargets, maxTargets));
		}
		user.energy -= energyCost;
		user.updateEntityBars ();
		user.addEffect (ownEffect);
	}

}