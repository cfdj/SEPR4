using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
	private int x;
	public Text moneyText;
	//move player from final level to end game scene upon colliding with flag
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Goose") {
			Debug.Log ("Failed mate");
			x = PlayerPrefs.GetInt ("Reward");
			x = x + 30;
			PlayerPrefs.SetInt ("Reward", x);

			SceneManager.LoadScene ("EndGame");


		}
	}
	void DisplayMoney(){
		moneyText.text = "Money: " + PlayerPrefs.GetInt ("Reward").ToString ();


	}

	//displays money on screen upon loading into scene
	void Start () {
		DisplayMoney ();
	}
	

}
