using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSleepingObject : GameBase
{
    [SerializeField]
    private GameObject gameObjectToActivate;

    private void Start()
    {
        SwitchToDay();
    }

    public override void SwitchToDay()
    {
        gameObjectToActivate.SetActive(true);
    }

    public override void SwitchToNight()
    {
        gameObjectToActivate.SetActive(false);
    }
}
