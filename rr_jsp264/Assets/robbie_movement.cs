using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robbie_movement : MonoBehaviour {

	public int jumpPower  = 5;
	public int x_velocity = 3;

	public bool isFacingRight  = true;
	private float move_X;


	// Use this for initialization
	// void Start () {
		
	// }
	
	// Update is called once per frame
	void Update () {
		moveRobbie();
	}

	void moveRobbie() {
		move_X = Input.GetAxis("Horizontal");
		if (Input.GetButtonDown("Jump")) {
			jump_();
		}
		if (move_X < 0.0f && !isFacingRight) {
			flipOrientation();
		}
		else if (move_X > 0.0f && isFacingRight) {
			flipOrientation();
		}

		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (move_X*x_velocity* (float)0.1, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

	void jump_() {
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
	}

	void flipOrientation() {
		isFacingRight = !isFacingRight;
		Vector2 spriteScale = gameObject.transform.localScale;

		spriteScale.x *= (float)-1.0;
		transform.localScale = spriteScale;
	}
}
