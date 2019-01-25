using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : MonoBehaviour {
    GameController controller;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();	
	}

    public GameController gameController() {
        return controller;
    }
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
