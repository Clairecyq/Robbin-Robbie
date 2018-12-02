using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour {

    private HeartBar heartBar;
    public GameObject heart1;
    public GameObject heart2;
    public Sprite heartFull;
    public Sprite heartEmpty;
    public GameObject robbie;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        int health = robbie.gameObject.GetComponent<RobbieMovement>().health;
        if (health == 2)
        {
            heart1.gameObject.GetComponent<SpriteRenderer>().sprite = heartFull;
            heart2.gameObject.GetComponent<SpriteRenderer>().sprite = heartFull;
        }
        else if (health==1) {
            heart1.gameObject.GetComponent<SpriteRenderer>().sprite = heartFull;
            heart2.gameObject.GetComponent<SpriteRenderer>().sprite = heartEmpty;
        } else if (health == 0) {
            heart1.gameObject.GetComponent<SpriteRenderer>().sprite = heartEmpty;
            heart2.gameObject.GetComponent<SpriteRenderer>().sprite = heartEmpty;
        }

	}
}
