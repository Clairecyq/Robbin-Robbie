using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject robbie;
    public bool FollowY = false;
    public float SetY = 0; //only used if FollowY is true, the y to set the camera at
    public float SetDepth = -10; //the depth of the camera, usually less than the depth of anything else in the scene

    private Vector3 offset;

    private float rightBound;
    private float leftBound;
    private float topBound;
    private float bottomBound;

    private Transform target;
    public SpriteRenderer spriteBounds;
    public Transform spriteTransform;
    public bool manualOffset = false;

	// Use this for initialization
	void Start () {
        if (manualOffset)
        {
            offset = transform.position - robbie.transform.position;
        }
        else { offset = new Vector3(0f, 0f, 0f); }

        float xmult = spriteTransform.lossyScale.x;
        float ymult = spriteTransform.lossyScale.y;
        float xadd = spriteTransform.position.x;
        float yadd = spriteTransform.position.y;
        float vertExtent = GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        leftBound = (float)(xadd + horzExtent - (spriteBounds.sprite.bounds.size.x * xmult) / 2.0f);
        rightBound = (float)((spriteBounds.sprite.bounds.size.x * xmult) / 2.0f - horzExtent + xadd);
        bottomBound = (float)(yadd + vertExtent - (spriteBounds.sprite.bounds.size.y * ymult) / 2.0f);
        topBound = (float)((spriteBounds.sprite.bounds.size.y * ymult) / 2.0f - vertExtent + yadd);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = new Vector3(0, SetY, SetDepth);
        pos.x = Mathf.Clamp(robbie.transform.position.x + offset.x, leftBound, rightBound);
        if (FollowY)
        {
            pos.y = Mathf.Clamp(robbie.transform.position.y + offset.y, bottomBound, topBound);
        }
        transform.position = pos;
	}
}
