using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBehavior : MonoBehaviour {

	// Use this for initialization
	// void Start () {
		
	// }
	
	// Update is called once per frame
	void Update () {
		 transform.Rotate (new Vector3 (0, 0, 90) * Time.deltaTime);
	}

	//  private void OnCollisionEnter2D(Collision2D c)
    // {   
    //     if (c.gameObject.name == "Robbie" && this.isActiveAndEnabled){
    //         gameObject.SetActive(false);
	// 		GameController.instance.obtainCoin();
	// 	}
    // }
}
