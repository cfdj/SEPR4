using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Collision : MonoBehaviour {
	public String name1;
	//move player to final scene upon collision with goose(player)
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == name1)
		{
			Debug.Log ("Here");
			SceneManager.LoadScene("EndGame");
		}
	}


}
