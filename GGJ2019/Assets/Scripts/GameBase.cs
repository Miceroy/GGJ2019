﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : MonoBehaviour
{
    [SerializeField]
    protected GameBaseIDEnum m_id = GameBaseIDEnum.None;
    public GameBaseIDEnum ID { get { return m_id; } }

    [SerializeField]
    protected GameBaseExtraFunctionality m_functionality = GameBaseExtraFunctionality.None;
    public GameBaseExtraFunctionality Functionality { get { return m_functionality; } }

    [SerializeField]
    protected bool m_isPickable = false;
    public bool IsPickable { get { return m_isPickable; } }

    //[SerializeField]
    //protected string m_id = string.Empty;
    //public string ID { get { return m_id; } }

    [Tooltip("For Debug purposes.")]
    [SerializeField]
    protected Material m_materialPrefab = null;

    private Rigidbody m_rigidBody = null;
    public Rigidbody Rigidbody
    {
        get
        {
            if (m_rigidBody == null)
                m_rigidBody = GetComponent<Rigidbody>();

            return m_rigidBody;
        }
    }

    protected readonly string m_dissolveAmount = "Vector1_8FBDE061";
    protected readonly string m_hologramToggle = "Boolean_4E61E6A7";

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

    public virtual void SetPlayerNearEffectOn()
    {
    }

    public virtual void SetPlayerNearEffectOff()
    {
    }
}
