using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = getCenterPoint();
        transform.LookAt(center);
        center.x = 0;
        transform.position = center + new Vector3(0, 6, -7);
        //transform.Ali
    }

    Vector3 getCenterPoint()
    {
        if (players.Length == 1)
        {
            return players[0].transform.position;
        }

        Bounds bounds = new Bounds(players[0].transform.position, Vector3.zero);
        for (int i = 0; i<players.Length; ++i)
        {
            bounds.Encapsulate(players[i].transform.position);
        }

        return bounds.center;
    }
}
