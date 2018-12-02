using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloBehavior : MonoBehaviour {
    public float chargeSpeed = 5f;
    public bool facingRight = true;
    public float leftWall;
    public float rightWall;
    public float waitTime = 2f;

    public Sprite frame2;

    private Sprite frame1;
    private float frameTime = .1f;
    private float frameSwap = 0f;
    private bool charging = false;
    private float wait;
    private
        
    // Use this for initialization
	void Awake () {
        frame1 = GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 walkAmount = Vector3.zero;
        Sprite get = GetComponent<SpriteRenderer>().sprite;

        if (charging)
        {
            if (frameSwap <= 0f)
            {
                frameSwap = frameTime;
                if (get == frame1)
                {
                    GetComponent<SpriteRenderer>().sprite = frame2;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = frame1;
                }
            }

            frameSwap -= Time.deltaTime;
        }


        if (facingRight)
        {
            transform.localScale = new Vector3(-1, 1,1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

		if (charging)
        {
            if (facingRight) {
                if (transform.position.x > rightWall)
                {
                    facingRight = false;
                    wait = waitTime;
                    charging = false;
                } else { walkAmount.x = chargeSpeed * Time.deltaTime; }
                

            }
            else {
                if (transform.position.x < leftWall)
                {
                    facingRight = true;
                    wait = waitTime;
                    charging = false;
                }
                else { walkAmount.x = -chargeSpeed * Time.deltaTime; }
            }
            transform.Translate(walkAmount);
        }
        else
        {
            wait -= Time.deltaTime;
            if (wait < 0)
            {
                charging = true;
            }
        }

	}
}
