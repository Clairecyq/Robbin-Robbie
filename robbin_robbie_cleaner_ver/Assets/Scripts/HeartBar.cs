using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBar : MonoBehaviour {

    [SerializeField]
    private HeartBar heartBar;
    public Image heart1;
    public Image heart2;
    public Image heartFull;
    public Image heartEmpty;
    public GameObject robbie;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        int health = robbie.gameObject.GetComponent<RobbieMovement>().health;
        if (health == 2)
        {
            heart1 = heartFull;
            heart2 = heartFull;
        }
        else if (health==1) {
            heart1 = heartFull;
            heart2 = heartEmpty;
        } else if (health == 0) {
            heart1 = heartEmpty;
            heart2 = heartEmpty;
        }

	}
}
