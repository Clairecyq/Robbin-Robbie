using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D c)
    {
		if (c.gameObject.name == "Robbie") {GameController.instance.PickedDonut();
		} else if (c.gameObject.name.Contains("Demon")) {
			GameController.instance.RobbieDied();
		}
    }
}
