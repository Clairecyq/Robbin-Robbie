using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBar : MonoBehaviour {

    [SerializeField]
    private HeartBar heartBar;
    public Button heart1;
    public Button heart2;
    public Sprite heartFull;
    public Sprite heartEmpty;
    private GameObject robbie;

    // Use this for initialization
    void Awake () {
        robbie = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        int health = robbie.gameObject.GetComponent<RobbieMovement>().health;
        if (health == 2)
        {
            heart1.image.sprite = heartFull;
            heart2.image.sprite = heartFull;
        }
        else if (health==1) {
            heart1.image.sprite = heartFull;
            heart2.image.sprite = heartEmpty;
        } else if (health == 0) {

            heart1.image.sprite = heartEmpty;
            heart2.image.sprite = heartEmpty;
        }

	}
}
