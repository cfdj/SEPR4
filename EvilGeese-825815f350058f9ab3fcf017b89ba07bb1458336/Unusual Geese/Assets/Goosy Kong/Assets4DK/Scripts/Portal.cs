using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour {
	public string leaveGame;
	public int currentScene;
	private int nextScene;

	private int x;

	public Text money;

	//display money on screen when loading scene
	void Start () {
		DisplayMoney ();

	}

	public void LeaveGame(string leaveGame){
		SceneManager.LoadScene (leaveGame);

	}
	//give player money when leaving game
	public void LeaveToMain(){
		GameStateManager state = GameObject.Find ("GameStateManager").GetComponent<GameStateManager> ();
		state.giveMoney (PlayerPrefs.GetInt ("Reward"));
		SceneManager.LoadScene ("GKMenu");
	}


	// checks which level player is on and determines which level to leave to
	// give player correct amount of money for each level
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Goose") {
			
			if(currentScene == 0){
				x = PlayerPrefs.GetInt ("Reward");
				x = x + 5;
				PlayerPrefs.SetInt ("Reward", x);
				nextScene = currentScene + 1;

				SceneManager.LoadScene ("Main " + nextScene);
			

			
			}
			if (currentScene == 1) {
				x = PlayerPrefs.GetInt ("Reward");
				x = x + 10;
				PlayerPrefs.SetInt ("Reward", x);
				nextScene = currentScene + 1;
				SceneManager.LoadScene ("Main " + nextScene);

			}

		}
	}
	void DisplayMoney(){
		money.text = "Money: " + PlayerPrefs.GetInt ("Reward").ToString ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
