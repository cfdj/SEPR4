using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartUponDeath : MonoBehaviour {
	public GameObject goose;

	// checks to see if player has fallen off the map, if so then end game
	void Update () {
		if (goose.transform.position.y < -7.8) {
			SceneManager.LoadScene ("EndGame");

		}
		
		
	}
}
