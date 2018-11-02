using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbieMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float speed;

    public float hiding_speed_adjust = 0.35f;
    public float horizontalMove;
    public float bottomDeathPlane = -15f;
    public bool jump = false;

    public bool canMove;
    public bool transformedToTrashCan = false;

    public Transformations currentTransformation = Transformations.Normal;

    GameObject robbie;
    
    public enum Transformations
    {
        Bush,
        Normal,         
        Rabbit, 
    }

    private bool pulse = false;

    void Start () {
        robbie = GameObject.FindGameObjectWithTag ("Player");
        canMove = true;
        //canJump = false;
    }

	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        gameObject.GetComponent<Animator>().SetInteger("movement_speed", (int)Mathf.Abs(horizontalMove));

        //Debug.Log(Input.GetButtonDown("Jump"));

        if (Input.GetButtonDown("Jump") && canMove) {
            if (LoggingManager.instance != null ) LoggingManager.instance.RecordEvent(6, "Successful Jump!");
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
            gameObject.GetComponent<Animator>().SetBool("transformed", false);
            //LoggingManager.instance.RecordEvent(5, "Not Enough Stamina");
            currentTransformation = Transformations.Normal;
            canMove = true;
            return;
        }

        else if (gameObject.GetComponent<CharacterController2D>().currentHidingPower > 0 && (
            Input.GetButtonDown("Transformation1") || Input.GetButtonDown("Transformation2")))
        {            
            if (Input.GetButtonDown("Transformation0"))
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                currentTransformation = Transformations.Normal;
                canMove = true;                 //To reset possible movements 
                transformedToTrashCan = false;
                gameObject.GetComponent<Animator>().SetBool("transformed", false);
            }
            else if (Input.GetButtonDown("Transformation1"))
            {
                //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                currentTransformation = Transformations.Bush;
                canMove = false;
                transformedToTrashCan = true;
                gameObject.GetComponent<Animator>().SetBool("transformed", true);
            }
            else if (Input.GetButtonDown("Transformation2"))
            {

                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                currentTransformation = Transformations.Rabbit;
                //gameObject.GetComponent<Animator>().SetBool("transformed", true);
                canMove = true;                 //To reset possible movements 
            }
            //gameObject.GetComponent<Animator>().SetBool("transformed", true);
        }

        else if ((Input.GetButtonUp("Transformation0") || Input.GetButtonUp("Transformation1") || Input.GetButtonUp("Transformation2")))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            if (LoggingManager.instance != null ) LoggingManager.instance.RecordEvent(2, "End Hiding - Left or Hide");
            currentTransformation = Transformations.Normal;
            canMove = true;                 //To reset possible movements 
            transformedToTrashCan = false;
            gameObject.GetComponent<Animator>().SetBool("transformed", false);
        }

        else
        {
            //gameObject.GetComponent<Animator>().SetBool("transformed", false);
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
        BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();

        if (!transformedToTrashCan)
        {
            if (pulse) { box.size = new Vector2(box.size.x, box.size.y - .05f); }
            else { box.size = new Vector2(box.size.x, box.size.y + .05f); }
        }
        pulse = !pulse;

        jump = false;
    }

}
