using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour {

    public bool isActivated;

	// Use this for initialization
	void Start () {
        isActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActivated)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
