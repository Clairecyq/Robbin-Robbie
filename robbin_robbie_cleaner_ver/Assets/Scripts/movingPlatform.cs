using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour {

	public int upperYBound;
	public int lowerYBound;

	public int lowerXBound;
	public int upperXBound;

	public bool movesHorizontal;
	public bool moveVertical;

	public bool goingUp = true;
	public bool goingRight = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (moveVertical) {
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
		}

		else if (movesHorizontal) {
			Vector2 position = new Vector2(transform.position.x, transform.position.y);

			if (goingRight) {
				position.x += 0.05f;
			}
			else {
				position.x -= 0.05f;
			}

			if (position.x >= upperXBound && goingRight) {
				goingRight = false;
			}
			else if (position.x <= lowerXBound && !goingRight) {
				goingRight = true;
			}
			transform.position = position;
		}

		// You can also rotate this too...just change the 3rd number
		//transform.Rotate (new Vector3 (0, 0, 0) * Time.deltaTime);
	}
}
