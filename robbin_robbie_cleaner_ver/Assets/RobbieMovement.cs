using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbieMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float speed;
    public float horizontalMove;
    public bool jump = false;

    GameObject robbie;

    public bool hide   = false;
    public bool hiding = false;

    void start () {
        //robbie = GameObject.FindGameObjectWithTag ("Player");
    }

	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
        if (Input.GetButtonDown("Hide")) {
            hide   = true;
            hiding = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            //gameObject.layer = LayerMask.NameToLayer("EnemyWeapon");
            //robbie.layer = LayerMask.NameToLayer("EnemyWeapon");
        }
        
        if (Input.GetButtonUp("Hide")) {
            hide = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
	}

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        // if (!hiding)
        //     hide = false;
    }

}
