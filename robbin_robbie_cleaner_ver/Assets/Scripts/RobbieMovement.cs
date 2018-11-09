using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbieMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float speed;

    public bool canJump = true;

    public float hiding_speed_adjust = 0.35f;
    public float horizontalMove;
    public float bottomDeathPlane = -15f;
    public bool jump = false;

    public bool canMove;
    public bool canHide = true;
    public bool transformedToTrashCan = false;

    public Transformations currentTransformation = Transformations.Normal;

    GameObject robbie;

    public int maxHealth = 2;
    public int health;
    private float iTimeMax = 1;
    public float iTime = 0;
    private float kTimeMax = .5f;
    private float kTime = 0;
    private bool visible = true;

    public enum Transformations
    {
        Bush,
        Normal,         
        Rabbit, 
    }

    private GameObject heart;
    private GameObject spring;
    private bool pulse = false;

    void Awake () {
        robbie = GameObject.FindGameObjectWithTag ("Player");
        heart = GameObject.Find("heart");
        heart.GetComponent<Image>().type = Image.Type.Filled;
        spring = GameObject.Find("Boot");
        canMove = true;
        health = maxHealth;
        //canJump = false;
    }

	// Update is called once per frame
	void Update () {
        if (!canJump)
        {
            if (spring != null)
            {
                spring.SetActive(false);
            }
        }

        if (heart != null)
        {
            if (health == 1)
            {
                heart.GetComponent<Image>().fillAmount = 0.5f;
            }
            else if (health == 0) {
                heart.SetActive(false);
            }
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        gameObject.GetComponent<Animator>().SetInteger("movement_speed", (int)Mathf.Abs(horizontalMove));

        //Debug.Log(Input.GetButtonDown("Jump"));

        if (Input.GetButtonDown("Jump") && canMove) {
            GameController.instance.packageInfo(16, "Successful Jump!");
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
             else if (Input.GetButtonDown("Transformation1") && canHide)
            {
                endRabbit();
                //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                currentTransformation = Transformations.Bush;
                canMove = false;
                transformedToTrashCan = true;
                gameObject.GetComponent<Animator>().SetBool("transformed", true);
            }
             else if (Input.GetButtonDown("Transformation2") && canJump)
            {
                endTrash();
                gameObject.GetComponent<Animator>().SetBool("jumping", true);
                //gameObject.GetComponent<SpriteRenderer>().color = new Color(Color.green.r, Color.green.g, Color.green.b,
                //                                                    gameObject.GetComponent<SpriteRenderer>().color.a);
                currentTransformation = Transformations.Rabbit;
                //gameObject.GetComponent<Animator>().SetBool("transformed", true);
                             //To reset possible movements 
            }
            //gameObject.GetComponent<Animator>().SetBool("transformed", true);
        }

        else if (Input.GetButtonUp("Transformation1"))
        {

            endTrash();
            
        }
        else if (Input.GetButtonUp("Transformation2"))
        {
            endRabbit();
            gameObject.GetComponent<Animator>().SetBool("jumping", false);
        }

    }

    void endRabbit()
    {
        if (currentTransformation == Transformations.Rabbit)
        {
            GameController.instance.packageInfo(12, "End Rabbit");
            currentTransformation = Transformations.Normal;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(Color.white.r, Color.white.g, Color.white.b,
                                                                gameObject.GetComponent<SpriteRenderer>().color.a);
        }
    }

    private void endTrash() {
        if (currentTransformation == Transformations.Bush){
             GameController.instance.packageInfo(12, "End Trash Can");
            currentTransformation = Transformations.Normal;
            canMove = true;                 //To reset possible movements 
            transformedToTrashCan = false;
            gameObject.GetComponent<Animator>().SetBool("transformed", false);
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


        iTime -= Time.deltaTime;
        kTime -= Time.deltaTime;

        if (iTime > 0)
        {
            visible = !visible;
        }
        if (iTime < 0)
        {
            visible = true;
        }
        if (visible)
        {
            Color c = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1.0f);
        }
        else {
            Color c = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 0f);
        }

    }

    public void takeDamage()
    {
        if (iTime < 0)
        {
            if (health > 1)
            {
                health -= 1;
                iTime = iTimeMax;
                kTime = kTimeMax;
            }
            else
            {
                GameController.instance.RobbieDied();
            }
        }
    }

    public void killHeart() {
        heart.SetActive(false);
    }

}
