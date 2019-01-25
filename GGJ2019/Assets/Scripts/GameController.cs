using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Public properties for designer
    public bool m_isDay;

    // Private

    // Use this for initialization
    void Start () {
        m_isDay = true;
    }
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

    public bool isDay() {
        return m_isDay;
    }
}
