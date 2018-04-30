using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
/// <summary>
/// [EXTENSIONS] - Added giveMoney, increaseCharacterHealth, increaseCharacterAttack, startMiniGame and endGame as an action type
/// </summary>
public class DialogAction {
	public enum actionType{
		setGameVar,
		giveMoney,
		startCombat,
		giveItem,
		setCharacterAvailibility,
		increaseCharacterHealth,
		increaseCharacterAttack,
		startMiniGame,
		endGame
	}
	public actionType ownActionType;
	public string gameVarName = "";
	public string gameVarValue = "";
	public int gameVarNumber = 0;
	public List<CombatCharacterFactory.CombatCharacterPresets> combatEnemies;
	public InventoryItems.itemTypes itemType;
	public int itemAmount;

	public CombatCharacterFactory.CombatCharacterPresets character;
	public bool charAvailible;

	public DialogAction (){
		combatEnemies = new List<CombatCharacterFactory.CombatCharacterPresets> ();
	}
		 
	/// <summary>
	/// [EXTENSION] - Call GameStateManager.giveMoney to give or take the specified money if giveMoney selected
	/// 			- Increase CombatCharacterFactory bonusHealth by 10 on increaseCharacterHealth
	/// 			- Increase CombatCharacterFactory bonusAttack by 5 on increaseCharacterAttack
	/// 		    - Load Goosy Kong scene if startMiniGame selected
	/// </summary>
	public void doAction(){
		GameStateManager state = GameObject.FindGameObjectWithTag ("GameStateManager").GetComponent<GameStateManager> ();
		switch (ownActionType) {
		case actionType.setGameVar:
			state.setGameVar (gameVarName, gameVarValue);
			break;
		case actionType.giveMoney:
			state.giveMoney (gameVarNumber);
			break;
		case actionType.startCombat:
			CombatManager.startCombat (combatEnemies);
			break;
		case actionType.giveItem:
			state.changeItem (itemType, itemAmount);
			break;
		case actionType.setCharacterAvailibility:
			if (charAvailible) {
				if (!state.availibleCharacters.Contains (character)) {
					state.availibleCharacters.Add (character);
				}
			} else {
				if (state.availibleCharacters.Contains (character)) {
					state.availibleCharacters.Remove (character);
				}
				if (state.currentTeam.Contains (character)) {
					state.currentTeam.Remove (character);
				}
			}
			break;
		case actionType.increaseCharacterHealth:
			CombatCharacterFactory.bonusHealth += 10;
			break;
		case actionType.increaseCharacterAttack:
			CombatCharacterFactory.bonusAttack += 5;
			break;
		case actionType.startMiniGame:
			Debug.Log ("Minigame");
			SoundManager.instance.playBGM ("minigame");
			SceneManager.LoadScene ("Goosy Kong/Assets4DK/Scenes/GKMenu");
			break;
		case actionType.endGame:
			SoundManager.instance.playBGM ("victory");
			SceneManager.LoadScene ("Finish Game Screen");
			break;
		}

	}
}
