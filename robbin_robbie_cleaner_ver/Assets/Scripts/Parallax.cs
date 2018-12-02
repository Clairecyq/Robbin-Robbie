using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    private GameObject background;
    public float scrollspeed = 1;
    private Vector3 offset;
    private Vector3 start;

	// Use this for initialization
	void Awake () {
        background = GameObject.FindWithTag("Background");
        if (background != null)
        {
            offset = -transform.position + background.transform.position;
            start = background.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (background != null)
        {
            Vector3 newPos;
            Vector3 newOffset = -transform.position + background.transform.position;
            if (scrollspeed == 0)
            {
                newPos = transform.position + offset;
            } 
            else
            {
                newPos = ((offset - newOffset) / scrollspeed) + start;
            }
            background.transform.position = newPos;
        }
	}
}
