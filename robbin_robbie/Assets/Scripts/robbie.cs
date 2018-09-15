using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class robbie : MonoBehaviour {
	public float accel;
	public float maxSpeed;

	private Rigidbody2D rigidBody;
	private KeyCode[] inputKeys;
	private Vector2[] directionsForKeys;

	// Use this for initialization
	void Start () {
		inputKeys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
  		directionsForKeys = new Vector2[] { Vector2.up, Vector2.left, Vector2.down, Vector2.right };
  		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int i = 0; i < inputKeys.Length; i++){
			var key = inputKeys[i];

			// 2
			if(Input.GetKey(key)) {
				// 3
				Vector2 movement = directionsForKeys[i] * accel * Time.deltaTime;
				movePlayer(movement);
			}
  		}
	}

	void movePlayer(Vector2 movement) {
		if (rigidBody.velocity.magnitude * accel > maxSpeed) {
			rigidBody.AddForce(movement * -1);
		} else {
			rigidBody.AddForce(movement);
		}
	}

}
