using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColliderScript : MonoBehaviour {

    private GameObject robbie;


    void Awake () {
        robbie = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name == "Robbie")
        {
            robbie.GetComponent<RobbieMovement>().takeDamage(this.gameObject.name);
        }
    }
}
