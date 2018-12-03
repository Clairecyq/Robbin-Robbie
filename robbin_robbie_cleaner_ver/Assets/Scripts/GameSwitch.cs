using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSwitch : MonoBehaviour {

    bool isActivated;

    public Sprite switchOn;
    public Sprite switchOff;
    //private Vector3 originalPos;

    [SerializeField]
    public GameObject forceField;

    void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOn;
        //originalPos = transform.position;
        isActivated = false;
	}

    void FixedUpdate()
    {
        if (isActivated)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = switchOff;
            //this.gameObject.transform.position = originalPos + new Vector3(0, -0.6f, 0);
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = switchOn;
            //this.gameObject.transform.position = originalPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name == "Robbie" && !isActivated)
        {
            // gameObject.SetActive(false);
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            isActivated = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOff;
            forceField.GetComponent<Forcefield>().isActivated = true;
        }
    }
}
