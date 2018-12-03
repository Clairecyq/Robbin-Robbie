using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    private LineRenderer lineRenderer;
    public Vector2 colliderOffset = Vector2.zero;
    public Transform LaserHit;
    BoxCollider2D boxCollider;
    Rigidbody2D rigidBody;
    public float LineWidth = 0.2f; // use the same as you set in the line renderer.

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        LaserHit.position = hit.point;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, LaserHit.position);

        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        rigidBody = gameObject.AddComponent<Rigidbody2D>();
        rigidBody.simulated = true;
        rigidBody.bodyType = RigidbodyType2D.Static;

        Vector2 boxSize = new Vector2(LineWidth, (Math.Abs((transform.position - LaserHit.position).magnitude)));
        Debug.DrawLine(transform.position, LaserHit.position );
        boxCollider.size = boxSize;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D c)
    {
    }
}
