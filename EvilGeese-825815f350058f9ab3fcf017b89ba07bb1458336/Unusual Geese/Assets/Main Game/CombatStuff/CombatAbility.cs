﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface CombatAbility {
	int minTargets {get;}
	int maxTargets {get;}
	int energyCost {get;}
	bool isAssist {get;}
	bool isGorrila { get; }
	string abilityName {get;}

	void doAbility (List<CombatCharacter> targets, CombatCharacter user);

}
