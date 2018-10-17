using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbieMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float speed;

    public float hiding_speed_adjust = 0.35f;
    public float horizontalMove;
    public float bottomDeathPlane = -6f;
    public bool jump = false;
    public bool can_jump;

    public bool hide   = false;

    public bool qHide = false;
    public bool eHide = false;
    public bool hiding = false;

    void start () {
        //robbie = GameObject.FindGameObjectWithTag ("Player");
    }

	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        gameObject.GetComponent<Animator>().SetInteger("movement_speed", (int)Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump") && gameObject.GetComponent<CharacterController2D>().currentHidingPower > 9 && can_jump) {
            jump = true;  
        }
        if ((Input.GetButtonDown("Hide") || Input.GetButtonDown("Hide2")) && gameObject.GetComponent<CharacterController2D>().currentHidingPower > 0) {
            hide   = true;
            hiding = true;
            gameObject.GetComponent<Animator>().SetBool("hiding", true);
            if (Input.GetButtonDown("Hide")) {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                qHide = true;
                eHide = false;
            }
            else {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                qHide = false;
                eHide = true;

            }
        }
        
        if (Input.GetButtonUp("Hide") || Input.GetButtonUp("Hide2") || gameObject.GetComponent<CharacterController2D>().currentHidingPower == 0) {
            hide  = false;
            qHide = false;
            eHide = false;
            gameObject.GetComponent<Animator>().SetBool("hiding", false);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (GetComponent<Transform>().position.y < bottomDeathPlane) //robbie dies if he falls off the screen
        {
            GameController.instance.RobbieDied();
        }
	}

    void FixedUpdate() {
        if (hide) horizontalMove *= hiding_speed_adjust; 
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

}
