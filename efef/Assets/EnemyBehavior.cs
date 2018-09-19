using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public float walkSpeed = 2.0f;
    public float wallLeft = -4.0f;
    public float wallRight = 5.0f;
    float walkingDirection = 1.0f;
    Vector2 walkAmount;

	
	// Update is called once per frame
	void Update () {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        if (walkingDirection >= 0.0f && transform.position.x >= wallRight) walkingDirection = -1.0f;
        else if (walkingDirection <= 0.0f && transform.position.x <= wallLeft) walkingDirection = 1.0f;
        transform.Translate(walkAmount);
    }


    private void OnCollisionEnter2D()
    {
        GameController.instance.RobbieDied(); 
    }
}
