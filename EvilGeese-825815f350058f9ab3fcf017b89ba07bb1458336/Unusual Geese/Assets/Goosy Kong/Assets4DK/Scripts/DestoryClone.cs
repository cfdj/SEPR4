using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DestoryClone : MonoBehaviour {
	public GameObject barrel;

	//check if barrel falls off screen and destroy if it has
	void Update () {
		if (barrel.transform.position.y < -7.7) {
			Debug.Log ("hello");

			Destroy (gameObject);
		}
	}
}
