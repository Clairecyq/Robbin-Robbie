using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public GameObject robbie;
    public float walkSpeed = 2.0f;
    public float chase = 3f;
    public float wallLeft;
    public float wallRight;
    public float vertRange = 5f;
    public float curSpeed;
    public float deceleration = 1.0f;
    public bool slowingDown = false;

    //private float alertTime = 0.0f;
    float walkingDirection = 1.0f;

    public bool m_facingRight = true;
    Vector2 walkAmount;

	// Update is called once per frame

    void Awake () {
        if (m_facingRight && walkSpeed > 0.0f) {
            enemy_flip();
            m_facingRight = !m_facingRight;
            walkingDirection *= -1.0f;
        }

        robbie = GameObject.FindGameObjectWithTag("Player");
        if (LoggingManager.instance != null && LoggingManager.instance.playerABValue == 2) {
            walkSpeed *= 0.7f;
        }
    }
	void Update () {
        //float sp = walkSpeed;
        curSpeed = curSpeed == 0 ? walkSpeed : curSpeed;

        bool robbieInRange = robbie.transform.position.x <= wallRight && robbie.transform.position.x >= wallLeft;

        bool facingRobbie = false;
        if (m_facingRight && robbie.transform.position.x > transform.position.x) 
            facingRobbie = true;
        else if (!m_facingRight && robbie.transform.position.x < transform.position.x) 
            facingRobbie = true;

        bool robbieHiding = robbie.GetComponent<RobbieMovement>().transformedToTrashCan;

        bool sameVerticalLevel = Mathf.Abs(robbie.transform.position.y - transform.position.y) <= vertRange;
        bool shouldBeAlerted = robbieInRange && facingRobbie && !robbieHiding && sameVerticalLevel;

        //Debug.Log("should alerted: " + shouldBeAlerted);
        //Debug.Log("alerted: " + gcameObject.GetComponent<Animator>().GetBool("alerted"));
        Debug.Log("curSpeed: " + curSpeed);
        if (shouldBeAlerted) {
            curSpeed = walkSpeed * chase;
            gameObject.GetComponent<Animator>().SetBool("alerted", true);
        }
        else if (gameObject.GetComponent<Animator>().GetBool("alerted")) {
            gameObject.GetComponent<Animator>().SetBool("alerted", false);
            slowingDown = true;
            slowDown();
        }
        else if (slowingDown) {
            slowDown();
        }
        else {
            gameObject.GetComponent<Animator>().SetBool("alerted", false);
            curSpeed = walkSpeed;
        }
        walkAmount.x = walkingDirection * curSpeed * Time.deltaTime;
        
        if (transform.position.x >= wallRight) {
            if (m_facingRight) {
                enemy_flip();
            }

        }
        else if (transform.position.x <= wallLeft) {
            if (!m_facingRight) {
                enemy_flip();
            }
        }
        if (!robbie.gameObject.GetComponent<Animator>().GetBool("died")) {
            transform.Translate(walkAmount);
        }
    }

    void slowDown() {
        if (Mathf.Abs(curSpeed)>Mathf.Abs(walkSpeed)) {
            if (m_facingRight)
            {
                if (transform.position.x >= wallRight) {
                    slowingDown = false;
                }
                else curSpeed -= deceleration * Time.deltaTime;
            }
            else
            {
                if (transform.position.x <= wallLeft) {
                    slowingDown = false;
                }
                else curSpeed += deceleration * Time.deltaTime;
            }
        } else {
            slowingDown = false;
        }
    }

    void enemy_flip() {
        m_facingRight = !m_facingRight;
        // Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
        walkingDirection *= -1.0f;
		transform.localScale = theScale;
    }

}
    
    //Physics based collisions
