using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public GameObject robbie;
    public float walkSpeed = 2.0f;
    public float wallLeft = -4.0f;
    public float wallRight = 5.0f;
    float walkingDirection = 1.0f;

    public bool m_facingRight = true;
    Vector2 walkAmount;

	
	// Update is called once per frame
	void Update () {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
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
        transform.Translate(walkAmount);
    }

    void enemy_flip() {
        m_facingRight = !m_facingRight;
        // Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }


    private void OnCollisionEnter2D(Collision2D c)
    {   
        //Debug.Log(c.otherCollider.ToString());
        if (c.otherCollider.gameObject.name == "Demon") {
            Debug.Log("DIED");
            GameController.instance.RobbieDied();
        } 
    }
}
