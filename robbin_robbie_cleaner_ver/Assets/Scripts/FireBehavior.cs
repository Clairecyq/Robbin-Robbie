using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour {

	// Use this for initialization
	// void Start () {
		
	// }
	
	// Update is called once per frame

	public int circularRotation = 90;

	public GameObject Spring; // used only in one level
	public GameObject energyShell; //
	public GameObject energyBar;

	void Update () {
		if (gameObject.name.Contains("UI")) {
			transform.Rotate (new Vector3 (0, 0, circularRotation) * Time.deltaTime);
		}
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
			if (this.name.Contains("spring")) {
				Spring.SetActive(true);
				GameObject.FindWithTag("Player").GetComponent<RobbieMovement>().canJump = true;
			}
			else {
				energyShell.SetActive(true);
				energyBar.SetActive(true);
				//Spring.SetActive(true);
				GameObject.FindWithTag("Player").GetComponent<RobbieMovement>().canHide = true;
			}
		}
    }
}
