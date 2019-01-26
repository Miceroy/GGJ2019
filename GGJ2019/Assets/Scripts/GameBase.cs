using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : MonoBehaviour
{
    [SerializeField]
    protected string m_id = string.Empty;
    public string ID { get { return m_id; } }

    [SerializeField]
    protected Material m_hologramBaseMaterial = null;

    [SerializeField]
    protected Material m_dissolveBaseMaterial = null;

    protected string m_dissolveAmount = "Vector1_EFB0DCB7";

    private void OnEnable()
    {
        GameController.AddGameBaseObject(this);
    }

    private void OnDisable()
    {
        GameController.RemoveGameBaseObject(this);
    }

    protected virtual void Update()
    {
    }

    public virtual void SwitchToDay()
    {
    }

    public virtual void SwitchToNight()
    {
    }
}
