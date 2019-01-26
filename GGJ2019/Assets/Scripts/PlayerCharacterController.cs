using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : GameBase
{
    public GameObject objectPickPoint;

    GameObject collidingItem;
    Transform collidingOldParent;
    float oldY;
    bool picked;

    // Start is called before the first frame update
    void Start()
    {
        collidingItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (collidingItem && Input.GetButtonUp("Fire1"))
        {
            if (picked)
            {
                // Release
                Debug.Log("Player release");
                picked = false;
                collidingItem.transform.SetParent(collidingOldParent);
                collidingItem.transform.position.Set(
                    collidingItem.transform.position.x, 
                    oldY, 
                    collidingItem.transform.position.z);
            }
            else
            {
                // Grab
                Debug.Log("Player grab");
                picked = true;
                collidingOldParent = collidingItem.transform.parent;
                collidingItem.transform.SetParent(objectPickPoint.transform);
                oldY = objectPickPoint.transform.position.y;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Piece")
        {
            Debug.Log("Player Collide enter to piece");
            collidingItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!picked && other.gameObject.tag == "Piece")
        {
            Debug.Log("Player Collide leave to piece");
            collidingItem = null;
        }
    }
}
