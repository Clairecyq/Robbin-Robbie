using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour {

	// Use this for initialization
	// void Start () {
		
	// }
	
	// Update is called once per frame

	public int circularRotation = 90;

	void Update () {
		 transform.Rotate (new Vector3 (0, circularRotation, 0) * Time.deltaTime);
	}

	public void setCircularRotation(int newRot) {
		circularRotation = newRot;
	}

	 private void OnTriggerEnter2D(Collider2D c)
    {   
        if (c.gameObject.name == "Robbie" && this.isActiveAndEnabled){
            // gameObject.SetActive(false);
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
			GameController.instance.obtainCoin();
		}
    }
}
