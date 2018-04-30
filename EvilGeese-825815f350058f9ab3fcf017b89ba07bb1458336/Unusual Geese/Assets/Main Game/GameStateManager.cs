using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
//stores a GameState and provides access to it in a component
/// <summary>
/// [EXTENSIONS] - Added getter for money and function to give/take money
/// 			 - Added sound effect for scene transitions
/// [CHANGES] - Changed condition to move player on scene change
/// </summary>
public class GameStateManager : MonoBehaviour{
	public List<CombatCharacterFactory.CombatCharacterPresets> availibleCharacters { 
		get {return state.availibleCharacters;}
		set {state.availibleCharacters = value;}
	}
	public List<CombatCharacterFactory.CombatCharacterPresets> currentTeam { 
		get {return state.currentTeam;}
		set {state.currentTeam = value;}
	}
	public bool movementEnabled {
		get {return state.movementEnabled;}
		set {state.movementEnabled = value;}
	}
	public bool isPaused{
		get { return state.isPaused; }
		set { state.isPaused = value; }
	}
	public Dictionary<InventoryItems.itemTypes, int> inventory {
		get {return state.inventory;}
	}

	/// <summary>
	/// [EXTENSION] - Added getter for money
	/// </summary>
	/// <value>The current amount of money</value>
	public int money {
		get { return state.money; }
	}

	public bool hasLoaded = false;
	public GameState state;

	// Use this for initialization
	void Start () {
		Debug.Log ("I exist");
		// if a GameStateManager already exists destroy this one;
		if (GameObject.FindGameObjectsWithTag ("GameStateManager").Length > 1) {
			Destroy (this.gameObject);
			return;
		}
		GameObject.DontDestroyOnLoad (this.gameObject);
		if (state == null) {
			Debug.Log ("the gamestate was null, creating new gamestate");
			state = new GameState ();
		}
		SceneManager.sceneLoaded += onSceneLoad;
	}

	/// <summary>
	/// [CHANGE] - Triggered player position change on any scene except the listed scens wit
	/// no player so that any scene can have different spawn locations (e.g. when leaving Nisa appear outside 
	/// Nisa doors in market square), without erroring as no player on WorldMap
	/// [EXTENSION] - Added sound effect for scene transitions
	/// </summary>
	/// <param name="scene">Scene.</param>
	/// <param name="mode">Mode.</param>
	void onSceneLoad(Scene scene, LoadSceneMode mode){
		SoundManager.instance.playSFX ("transition");
		Debug.Log (scene.name);
		string[] nonPlayerMaps = new string[] {"WorldMap", "GKMenu", "Main", "Main 1", "Main 2", "EndGame", "MenuScene", "Finish Game Screen"};
		if (!Array.Exists(nonPlayerMaps, element => element.Equals(scene.name))) {
			hasLoaded = false;
			try{
				PlayerMovement movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
				Debug.Log("X: " + state.playerX);
				movement.x = state.playerX;
				movement.y = state.playerY;
				movement.snapToGridPos();
			}catch (NullReferenceException e ){
				Debug.LogError (e);
			}

		}
		movementEnabled = true;
	}

	public string getGameVar(string varName){
		try{	
			return state.gameStateVars[varName];
		} catch (KeyNotFoundException){
			return "";
		}
	}

	public void setGameVar(string varName, string varValue){
		if (state.gameStateVars.ContainsKey (varName)) {
			state.gameStateVars [varName] = varValue;
		} else {
			state.gameStateVars.Add (varName, varValue);
		}
	}

	/// <summary>
	/// [EXTENSION] Give or take money from the current amount (take specified by a negative value)
	/// </summary>
	/// <param name="money">The money to give or take (-ve to take)</param>
	public void giveMoney(int money) {
		state.money += money;
		Debug.Log (money);
	}

	public void changeItem(InventoryItems.itemTypes itemType, int amount){
		if (inventory.ContainsKey(itemType)){
			inventory[itemType] += amount;
		}else{
			inventory.Add(itemType, amount);
		}
	}

	public int getItem(InventoryItems.itemTypes itemType){
		if (inventory.ContainsKey(itemType)){
			return inventory [itemType];
		}else{
			return 0;
		}
	}

	public void saveState(string saveName){
		GameSave.saveState (saveName, state);
	}

	public void loadState(string saveName){
		state = GameSave.loadState (saveName);
		SceneManager.LoadScene (state.sceneName);
		hasLoaded = true;
	}

	public static GameStateManager getGameStateManager(){
		return GameObject.FindGameObjectWithTag ("GameStateManager").GetComponent<GameStateManager> ();
	}
		

}
