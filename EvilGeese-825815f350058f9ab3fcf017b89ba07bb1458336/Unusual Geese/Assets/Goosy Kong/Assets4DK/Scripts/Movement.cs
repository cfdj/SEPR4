using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {
	public float moveSpeed;
	public float jumpHeight;
	public GameObject goose;
	public bool facingRight;

	// used to check if player is grounded
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	bool grounded = true;



	// Use this for initialization
	void Start () {
		// boolean value to determine direction sprite should be facing
		facingRight = true; // initialise player to be facing right
		


	}

	// continuously check if players collision box is in radius of platform
	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			Jump ();

		}
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			moveRight ();
		}
			
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A))
			moveLeft ();
				
	

		
	}

	public void Jump(){
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight);
	}

	public void moveRight(){
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		//if moving right and sprite is facing left then flip
		if ((facingRight) == false) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			goose.transform.localScale = theScale;
			facingRight = true;
		}
	}

	public void moveLeft(){
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		//if moving left and sprite if facing right then flip
		if ((facingRight) == true) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			goose.transform.localScale = theScale;
			facingRight = false;
		}
	}
		
} 

