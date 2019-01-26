using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightAwakingObject : GameBase
{
    [SerializeField]
    private GameObject gameObjectToActivate;

    private void Start()
    {
        SwitchToDay();
    }

    public override void SwitchToDay()
    {
        gameObjectToActivate.SetActive(false);
    }

    public override void SwitchToNight()
    {
        gameObjectToActivate.SetActive(true);
    }
}
