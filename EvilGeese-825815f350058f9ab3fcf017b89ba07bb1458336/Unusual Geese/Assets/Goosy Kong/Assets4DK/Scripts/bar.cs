using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bar : MonoBehaviour {

	public GameObject barrel; //object to instantiate
	public Vector3 whereToSpawn; // location
	public float spawnRate;
	float nextSpawn;




	void Update () {
		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + spawnRate;
			whereToSpawn = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			Instantiate (Resources.Load("Barrel2", typeof(GameObject)) as GameObject, whereToSpawn, Quaternion.identity);
		}






	}


}
