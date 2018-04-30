using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// [EXTENSIONS] - New class to provide a time limit while playing game demo 
/// </summary>
public class DemoTimer : MonoBehaviour {

	public int minutes = 5;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		StartCoroutine (timer ());
	}
	
	private IEnumerator timer() {
		yield return new WaitForSeconds (minutes * 60);
		SceneManager.LoadScene ("MenuScene");
	}

}
