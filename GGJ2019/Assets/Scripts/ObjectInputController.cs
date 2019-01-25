using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInputController : GameBase
{
    SnapMover mover;

	// Use this for initialization
	void Start () {
        mover = GetComponent<SnapMover>();	
	}
	
	// Update is called once per frame
	void Update () {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        mover.move(new Vector2(dx, dy));
    }
}
