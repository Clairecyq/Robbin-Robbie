using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour {

	public int upperYBound;
	public int lowerYBound;

	public bool goingUp = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 position = new Vector2(transform.position.x, transform.position.y);

		if (goingUp) {
			position.y += 0.05f;
		}
		else {
			position.y -= 0.05f;
		}

		if (position.y >= upperYBound && goingUp) {
			goingUp = false;
		}
		else if (position.y <= lowerYBound && !goingUp) {
			goingUp = true;
		}
		transform.position = position;

		transform.Rotate (new Vector3 (0, 0, 20) * Time.deltaTime);
	}
}
