using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBehavior : MonoBehaviour {
    public GameObject robbie;
	public float radius = 3f;

	public Vector3 offset;
    public float FlySpeed = 2.0f;
    public float wallLeft = -5.0f;
    public float wallRight = 5.0f;

    float walkingDirection = 1.0f;

    public bool m_facingRight = true;
    Vector2 walkAmount;

    private void Awake()
    {
        offset.x = this.transform.position.x;
        offset.y = this.transform.position.y;
        robbie = GameObject.FindGameObjectWithTag("Player");
        offset.x = this.transform.position.x;
        offset.y = this.transform.position.y;

    }

    // Update is called once per frame
    void Update () {
		transform.position = new Vector3(
            (radius * Mathf.Cos(Time.time*FlySpeed))+offset.x,
            (radius * Mathf.Sin(Time.time*FlySpeed))+offset.y,
            offset.z);
        //walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        if (walkingDirection >= 0.0f && transform.position.x >= wallRight) {
            walkingDirection = -1.0f;
            if (m_facingRight) {
                enemy_flip();
            }

        }
        else if (walkingDirection <= 0.0f && transform.position.x <= wallLeft) {
            walkingDirection = 1.0f;
            if (!m_facingRight) {
                enemy_flip();
            }
        }
        //transform.Translate(walkAmount);
    }

    void enemy_flip() {
        m_facingRight = !m_facingRight;
        // Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }


    private void OnTriggerEnter2D(Collider2D c)
    {   
        if (c.gameObject.name == "Robbie") {
            robbie.GetComponent<RobbieMovement>().takeDamage(this.gameObject.name);
            GameController.instance.packageInfo(22, "Damage Eagle");
        } else {
            enemy_flip();
        }
    }
}
