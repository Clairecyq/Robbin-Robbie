using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSwitch : MonoBehaviour {

    bool isActivated;

    [SerializeField]
    public GameObject forceField;

    void Start () {
        isActivated = false;
	}

    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name == "Robbie" && !isActivated)
        {
            // gameObject.SetActive(false);
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            isActivated = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            forceField.GetComponent<Forcefield>().isActivated = true;
        }
    }
}
