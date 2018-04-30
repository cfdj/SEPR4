using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// stores the data for the various inventory item types

/// <summary>
/// [EXTENSIONS] - Added new item types: JagerGrenade and LuckySpoon
/// </summary>
public static class InventoryItems {
	/// <summary>
	/// [EXTENSION] - Added new item types (listed in class summary)
	/// </summary>
	public enum itemTypes{
		Beak,
		PlasticFork,
		JagerGrenade,
		LuckySpoon
	}

	/// <summary>
	/// [EXTENSION] - Added new item display names (listed in class summary)
	/// </summary>
	/// <returns>The name to display for the item</returns>
	/// <param name="itemType">The type of item in question</param>
	public static string itemDisplayName(itemTypes itemType){
		switch (itemType) {
		case itemTypes.Beak:
			return "Goose Beak";
		case itemTypes.PlasticFork:
			return "Plastic Fork";
		case itemTypes.JagerGrenade:
			return "JägerGrenade";
		case itemTypes.LuckySpoon:
			return "Lucky Spoon";
		default:
			return "error: invalid item";
		}

	}

	/// <summary>
	/// [EXTENSION] - Added new item descriptions (listed in class summary)
	/// </summary>
	/// <returns>The description of the item</returns>
	/// <param name="itemType">The item type in question</param>
	public static string itemDescription (itemTypes itemType){
		switch (itemType) {
		case itemTypes.Beak:
			return "It appears to the the beak of a goose";
		case itemTypes.PlasticFork:
			return "It's an ordinary plastic fork... why did you keep this?";
		case itemTypes.JagerGrenade:
			return "A variation on the Jägerbomb that can cause 30-40 damage";
		case itemTypes.LuckySpoon:
			return "Spin the wheel! Does anywhere between 0 and 40 damage";
		default:
			return "error: No descrition exists for this item";	
		}
	}

	public static bool itemHasAbility(itemTypes itemType){
			return itemAbility (itemType) != null;
	}

	/// <summary>
	/// [EXTENSION] - Added new item properties (listed in class summary)
	/// </summary>
	/// <returns><c>true</c>, if item is consumed on use, <c>false</c> otherwise.</returns>
	/// <param name="itemType">The item type in question</param>
	public static bool itemConsumedOnUse(itemTypes itemType){
		switch (itemType) {
		case itemTypes.Beak:
			return false;
		case itemTypes.PlasticFork:
			return false;
		case itemTypes.JagerGrenade:
			return true;
		case itemTypes.LuckySpoon:
			return false;
		default:
			return false;
		}
	}

	/// <summary>
	/// [EXTENSION] - Added new item abilities(listed in class summary)
	/// </summary>
	/// <returns>The action performed when the item is used</returns>
	/// <param name="itemType">The item type in question</param>
	public static CombatAbility itemAbility(itemTypes itemType){
		switch (itemType) {
		case itemTypes.Beak:
			return new SimpleAttack (1, 1, "beak", 0, "Beak Poke");
		case itemTypes.PlasticFork:
			return new SimpleAttack (10, 20, "melee", 0, "Fork Stab");
		case itemTypes.JagerGrenade:
			return new SimpleAttack (30, 40, "range", 0, "Grenade Throw");
		case itemTypes.LuckySpoon:
			return new SimpleAttack (0, 50, "melee", 0, "flick");
		default:
			return null;
		}
	}
}
