using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Piece") {
            Debug.Log("Collide enter to piece");
            highlight(true, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Piece")
        {
            Debug.Log("Collide leave to piece");
            highlight(false, other.gameObject);
        }
    }

    private void highlight(bool isOn, GameObject go) {
    }
}
