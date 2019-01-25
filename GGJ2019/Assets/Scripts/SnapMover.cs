using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapMover : GameBase {
    // Public properties for designer
    public float moveSpeed = 5.0f;
    public float snapFactor = 1.0f;

    // Private for internal use
    Vector3 realPos;
    Rigidbody rb;
	
    // Use this for initialization
	void Start () {
        realPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void move(Vector2 delta) {
        if (!rb.useGravity) {
            delta *= moveSpeed * Time.deltaTime;
            realPos.x += delta.x;
            realPos.z += delta.y;
            int rx = (int)(snapFactor * realPos.x);
            int ry = (int)(snapFactor * realPos.y);
            int rz = (int)(snapFactor * realPos.z);

            // Set discrete position
            transform.position = new Vector3(
                (float)rx / snapFactor,
                (float)ry / snapFactor,
                (float)rz / snapFactor);
        }
        else {
            // Do nothing, because physics will do the stuff
        }
    }
}
