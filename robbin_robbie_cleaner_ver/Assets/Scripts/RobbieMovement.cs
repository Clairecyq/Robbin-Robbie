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
    //public bool canJump;
    public bool canMove;
    public bool transformed = false;

    public Transformations currentTransformation = Transformations.Normal;

    GameObject robbie;

    public enum Transformations
    {
        Bush,
        Normal,         
        Rabbit, 
    }

    void Start () {
        robbie = GameObject.FindGameObjectWithTag ("Player");
        canMove = true;
        //canJump = false;
    }

	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        gameObject.GetComponent<Animator>().SetInteger("movement_speed", (int)Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && canMove) {
            jump = true;  
        }
       
        checkTransform();

        if (GetComponent<Transform>().position.y < bottomDeathPlane) //robbie dies if he falls off the screen
        {
            GameController.instance.RobbieDied();
        }
	}

    void checkTransform()
    {        
        if (gameObject.GetComponent<CharacterController2D>().currentHidingPower == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            currentTransformation = Transformations.Normal;
            return;
        }

        else if (gameObject.GetComponent<CharacterController2D>().currentHidingPower > 0 && (Input.GetButtonDown("Transformation0")) || 
            (Input.GetButtonDown("Transformation1")) || Input.GetButtonDown("Transformation2") ||
            (Input.GetButtonDown("Transformation3")))
        {
            
            //gameObject.GetComponent<Animator>().SetBool("transformed", true);
            if (Input.GetButtonDown("Transformation0"))
            {
                //To reset possible movements 
                canMove = true;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                currentTransformation = Transformations.Normal;
            }
            else if (Input.GetButtonDown("Transformation1"))
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                currentTransformation = Transformations.Bush;
                canMove = false;
            }
            else if (Input.GetButtonDown("Transformation2"))
            {
                //To reset possible movements 
                canMove = true;
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                currentTransformation = Transformations.Rabbit;
            }
        }
    }

    void FixedUpdate() {
        if (canMove) 
        {        
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        }

        else
        {
            controller.Move(0, false, jump);
        }

        jump = false;
    }

}
