using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public GameObject robbie;
    public float walkSpeed = 2.0f;
    public float chase = 1.5f;
    public float wallLeft;
    public float wallRight;
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
    }
	void Update () {
        float sp = walkSpeed;
        Debug.Log(chase);
        if (robbie.transform.position.x <= wallRight && robbie.transform.position.x >= wallLeft) {
            sp = sp * chase;
            Debug.Log(sp);
        }
        walkAmount.x = walkingDirection * sp * Time.deltaTime;
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
        transform.Translate(walkAmount);
    }

    void enemy_flip() {
        m_facingRight = !m_facingRight;
        // Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
        walkingDirection *= -1.0f;
		transform.localScale = theScale;
    }


    private void OnCollisionEnter2D(Collision2D c)
    {   
        if (c.gameObject.name == "Robbie") {
            GameController.instance.RobbieDied();
        }
    }
}
