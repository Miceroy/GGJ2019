using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : GameBase
{
    public GameObject objectPickPoint;

    GameObject collidingItem;
    Transform collidingOldParent;
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
                //TODO: Release
                Debug.Log("Player release");
                picked = false;
                collidingItem.transform.SetParent(collidingOldParent);
            }
            else
            {
                // Grab
                Debug.Log("Player grab");
                picked = true;
                collidingOldParent = collidingItem.transform.parent;
                collidingItem.transform.SetParent(objectPickPoint.transform);                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Piece")
        {
            Debug.Log("Collide enter to piece");
            //  highlight(true, other.gameObject);
            collidingItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!picked && other.gameObject.tag == "Piece")
        {
            Debug.Log("Collide leave to piece");
            //  highlight(false, other.gameObject);
           // if (!picked)
           // {
            //    Debug.Log("Player collidingItem==null");
                collidingItem = null;
        //    }
        }
    }

   /* private void highlight(bool isOn, GameObject go)
    {
        if (isOn)
        {
            collidingItem = go;
        }
        else
        {
            if (!picked)
            {
                Debug.Log("Player collidingItem==null");
                collidingItem = null;
            }
        }
    }*/
}
