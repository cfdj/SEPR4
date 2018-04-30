using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

//[Un-Two] a test for the Gorilla combat
public class GorillaTest {

	[Test]
	public void CombatSystemCharacterDamage() {
		CombatCharacter c1 = new CombatCharacter (100, 100, 100, 100, new GorrilaAttack (20, 20, "melee"));
		CombatCharacter c2 = new CombatCharacter (100, 100, 100, 100, new GorrilaAttack (20, 20, "melee"));

		Assert.AreEqual (true, c1.basicAttack.isGorrila);
		Assert.AreEqual (100, c1.health);
		List<CombatCharacter> l = new List<CombatCharacter> ();
		l.Add (c1);
		c2.basicAttack.doAbility(l, c2);
		Assert.AreEqual (80, c1.health);

	}
}
