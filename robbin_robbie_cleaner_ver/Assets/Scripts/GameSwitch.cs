using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSwitch : MonoBehaviour {

    bool isActivated;

    public Sprite switchOn;
    public Sprite switchOff;

    [SerializeField]
    public GameObject forceField;

    void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOn;
        isActivated = false;
	}

    void FixedUpdate()
    {
        if (isActivated)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = switchOff;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = switchOn;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name == "Robbie" && !isActivated)
        {
            isActivated = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOff;
            forceField.GetComponent<Forcefield>().isActivated = true;
        }
    }
}
