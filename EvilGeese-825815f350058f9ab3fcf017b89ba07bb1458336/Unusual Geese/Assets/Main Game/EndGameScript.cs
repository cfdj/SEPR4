using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// [EXTENSIONS] - New class to mangage end game screen
/// </summary>
public class EndGameScript : MonoBehaviour {

	public void backToMenu() {
		SoundManager.instance.playBGM ("main");
		SceneManager.LoadScene ("MenuScene");
	}

}
