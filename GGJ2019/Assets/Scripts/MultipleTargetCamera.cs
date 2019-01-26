using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    GameObject[] players;
    Vector3[] avgBuffer;
    int curIndex;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        avgBuffer = new Vector3[30];
        curIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = getCenterPoint();
        transform.LookAt(center);
        center.x = 0;
        center += new Vector3(0, 6, -7);
        avgBuffer[curIndex] = center;
        curIndex = (curIndex + 1) % avgBuffer.Length;

        center = Vector3.zero;
        for(int i=0; i< avgBuffer.Length; ++i)
        {
            center += avgBuffer[i];
        }

        center /= avgBuffer.Length;
        transform.position = center;
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

        return new Vector3(bounds.center.x, 1, bounds.center.y);
    }
}
