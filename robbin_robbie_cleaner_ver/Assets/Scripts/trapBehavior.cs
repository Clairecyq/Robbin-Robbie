using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapBehavior : MonoBehaviour {

	// Use this for initialization
	// void Start () {
		
	// }
	
	// Update is called once per frame
	// void Update () {
	// 	//some update here
	// }

	private void OnCollisionEnter2D(Collision2D c)
    {   
        if (c.gameObject.name == "Robbie"){
			GameController.instance.RobbieDied();
		}
    }
}
