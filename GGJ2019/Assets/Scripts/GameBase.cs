using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : MonoBehaviour {
    GameController controller;

    public GameController gameController() {
        if(!controller) {
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        return controller;
    }
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
