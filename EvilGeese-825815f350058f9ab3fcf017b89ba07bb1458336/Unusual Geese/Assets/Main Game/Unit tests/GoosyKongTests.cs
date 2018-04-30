using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GoosyKongTests {
	bool sceneLoaded = false;
	GameObject player;
	Movement movementScript;

	public IEnumerator Setup(){
		if (!sceneLoaded) {
			SceneManager.LoadScene ("Main");
			yield return null;
			sceneLoaded = true;
		}
		player = GameObject.Find ("Goose");
		movementScript = player.GetComponent<Movement> ();
		PlayerPrefs.SetInt ("Reward", 0);
		yield return null;
	}


	[UnityTest]
	public IEnumerator AGooseMovement() {
		yield return Setup ();
		//right
		yield return moveforFrames (100, "right");
		Assert.AreEqual (4 , Math.Round (player.transform.position.x));
		//return to original position
		yield return moveforFrames (100, "left");
		Assert.Less (Math.Abs (player.transform.position.x + 5.7), 0.1);
		//cancel momentum before next test
		yield return moveforFrames (10, "right");
		yield return new WaitForSeconds(1);
	}

	[UnityTest]
	public IEnumerator BGooseJumpAndPlatformCollisions() {
		yield return Setup();
		movementScript.Jump ();
		float maxHeight = 0;
		for (int i = 0; i < 50; i++) {
			if (player.transform.position.y > maxHeight) {
				maxHeight = player.transform.position.y;
			}
			yield return new WaitForFixedUpdate ();
		}
		//check max height isn't through the platform above
		Assert.Less (Math.Abs (player.transform.position.y + 5.9), 0.1);
		//check goose rests back on platform
		Assert.AreEqual(0, maxHeight);
	}

	[UnityTest]
	public IEnumerator CPortalAndMoneyTest() {
		yield return Setup ();
		string originalScene = SceneManager.GetActiveScene ().name;
		player.transform.position = new Vector3 (0.53f, 7.22f, 0f);
		for(int i = 0; i < 50; i++){
			yield return null;
		}
		string currentScene = SceneManager.GetActiveScene ().name;
		Assert.AreEqual (currentScene, "Main 1");
		Assert.AreEqual (5, PlayerPrefs.GetInt ("Reward"));
	}

	[UnityTest]
	public IEnumerator DBarrelCollision() {
		yield return Setup ();
		player.transform.position = new Vector3 (-0.3f, 3.9f, 0);
		yield return new WaitForSeconds (5);
		Assert.AreEqual ("EndGame", SceneManager.GetActiveScene ().name);
	}

	public IEnumerator moveforFrames(int frames,string direction){
		for (int i = 0; i < frames; i++) {
			switch (direction) {
			case "left":
				movementScript.moveLeft ();
				break;
			case "right":
				movementScript.moveRight ();
				break;

			}
			yield return new WaitForFixedUpdate();


		}
	}


}

