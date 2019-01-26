using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceObject : GameBase {
    public GameObject dayMesh;
    public GameObject inAreaMesh;
    public GameObject outAreaMesh;

    bool isInArea;
    bool isPrevDay;

    // Use this for initialization
    void Start () {
        checkSwitchMesh();
    }
	
	// Update is called once per frame
	void Update () {
        bool isNowDay = gameController().isDay();
        if (isPrevDay != isNowDay)
        {
            checkSwitchMesh();
        }
        isPrevDay = isNowDay;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Area")
        {
            Debug.Log("Piece enter to area");
            isInArea = true;
            checkSwitchMesh();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Area")
        {
            Debug.Log("Piece enter to area");
            isInArea = false;
            checkSwitchMesh();
        }
    }

    private void checkSwitchMesh()
    {
        if( gameController().isDay() )
        {
            Debug.Log("Switch day mesh");
            dayMesh.SetActive(true);
            inAreaMesh.SetActive(false);
            outAreaMesh.SetActive(false);
        }
        else
        {
            if(isInArea)
            {
                Debug.Log("Switch nught in area mesh");
                dayMesh.SetActive(false);
                inAreaMesh.SetActive(true);
                outAreaMesh.SetActive(false);
            }
            else
            {
                Debug.Log("Switch nught out area mesh");
                dayMesh.SetActive(false);
                inAreaMesh.SetActive(false);
                outAreaMesh.SetActive(true);
            }
        }
    }
}
