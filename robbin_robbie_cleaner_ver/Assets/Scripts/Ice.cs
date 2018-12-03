using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {

    private Vector2 rightForce = new Vector2(100,0);
    private Vector2 leftForce = new Vector2(-100,0);
    private Vector2 Force;

    // Internal list that tracks objects that enter this object's "zone"
    private List<Collider2D> objects = new List<Collider2D>();

    // This function is called every fixed framerate frame
    void FixedUpdate()
    {
        // For every object being tracked
        for (int i = 0; i < objects.Count; i++)
        {
            // Get the rigid body for the object.
            Rigidbody2D body = objects[i].attachedRigidbody;
            //Debug.Log(objects[i].gameObject.name);

            if (body.gameObject.GetComponent<CharacterController2D>().m_FacingRight) Force = rightForce;
            else Force = leftForce;
            //else Force = Vector2.zero;
            // Apply the force
            body.AddForce(Force);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            objects.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            objects.Remove(other);
        }
    }
}
