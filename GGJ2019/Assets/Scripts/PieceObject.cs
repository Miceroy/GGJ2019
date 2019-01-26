﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceObject : GameBase
{
    [SerializeField]
    private GameObject goToShowOnPlayerTrigger;

    [SerializeField]
    private MeshRenderer DayObject;

    [SerializeField]
    private MeshRenderer NightObjectIn;

    [SerializeField]
    private MeshRenderer NightObjectOut;

    [SerializeField]
    private float m_transitionSpeed = 1f; // How long single transition lasts in seconds (total amount is double of this time)

    [SerializeField]
    private float m_scaleTransitionSpeed = 1f;

    [SerializeField]
    private Vector3 m_dayScale = new Vector3(1f, 1f, 1f);

    [SerializeField]
    private Vector3 m_nightScale = new Vector3(2f, 2f, 2f);

    private bool m_inPlaygroundArea = false;

    private bool m_inTransition = false;
    private bool m_firsObjectTransitioned = false;
    private float m_transitionTimer = 0f;

    private MeshRenderer m_currentMesh = null;
    private MeshRenderer m_targetMesh = null;

    private bool m_transitionScale = false;
    private bool m_targetIsNight = false;
    private float m_scaleTransitionTimer = 0f;

    private void Start()
    {
        DayObject.material = new Material(m_materialPrefab);
        NightObjectIn.material = new Material(m_materialPrefab);
        NightObjectOut.material = new Material(m_materialPrefab);

        // Create runtime copy of default materials, so material shader edits doesn't affect every piece using same material.
        // Use these lines instead when objects are read and do not require "much" debugging.
        //DayObject.material = new Material(DayObject.material);
        //NightObjectIn.material = new Material(NightObjectIn.material);
        //NightObjectOut.material = new Material(NightObjectOut.material);

        m_currentMesh = DayObject;
        DayObject.material.SetFloat(m_dissolveAmount, 0f);
        DayObject.material.SetInt(m_hologramToggle, 0);

        NightObjectIn.material.SetFloat(m_dissolveAmount, 1f);
        NightObjectIn.material.SetInt(m_hologramToggle, 0);

        NightObjectOut.material.SetFloat(m_dissolveAmount, 1f);
        NightObjectOut.material.SetInt(m_hologramToggle, 0);


        if (goToShowOnPlayerTrigger)
        {
            goToShowOnPlayerTrigger.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AreaObject _area = other.gameObject.GetComponent<AreaObject>();
        if (_area != null)
        {
            // In area
            m_inPlaygroundArea = true;

            if (!GameController.IsDay)
            {
                transitionToNightIn();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AreaObject _area = other.gameObject.GetComponent<AreaObject>();
        if (_area != null)
        {
            // Out of area
            m_inPlaygroundArea = false;

            if (!GameController.IsDay)
            {
                transitionToNightOut();
            }
        }
    }

    public override void SetPlayerNearEffectOn()
    {
        goToShowOnPlayerTrigger.SetActive(true);

        DayObject.material.SetInt(m_hologramToggle, 1);
        NightObjectIn.material.SetInt(m_hologramToggle, 1);
        NightObjectOut.material.SetInt(m_hologramToggle, 1);
    }

    public override void SetPlayerNearEffectOff()
    {
        goToShowOnPlayerTrigger.SetActive(false);

        DayObject.material.SetInt(m_hologramToggle, 0);
        NightObjectIn.material.SetInt(m_hologramToggle, 0);
        NightObjectOut.material.SetInt(m_hologramToggle, 0);
    }

    protected override void Update()
    {
        if (m_inTransition)
        {
            m_transitionTimer += Time.deltaTime;
            float _transitionValue = m_transitionTimer / m_transitionSpeed;

            if (!m_firsObjectTransitioned)
                m_currentMesh.material.SetFloat(m_dissolveAmount, _transitionValue);
            else
                m_currentMesh.material.SetFloat(m_dissolveAmount, 1f - _transitionValue);

            if (_transitionValue >= 1f)
            {
                if (!m_firsObjectTransitioned)
                {
                    m_firsObjectTransitioned = true;
                    m_currentMesh = m_targetMesh;
                    m_targetMesh = null;
                    m_transitionTimer = 0f;
                }
                else
                    m_inTransition = false;
            }
        }

        if (m_transitionScale)
        {
            m_scaleTransitionTimer += Time.deltaTime;
            float _transitionValue = m_scaleTransitionTimer / m_scaleTransitionSpeed;

            Vector3 _from = m_targetIsNight ? m_dayScale : m_nightScale;
            Vector3 _to = m_targetIsNight ? m_nightScale : m_dayScale;

            Vector3 _curScale = Vector3.Lerp(_from, _to, _transitionValue);

            transform.localScale = _curScale;

            if (_transitionValue >= 1f)
            {
                m_transitionScale = false;
            }
        }

        if(GetComponent<Rigidbody>().useGravity == false && goToShowOnPlayerTrigger.activeSelf)
        {
            goToShowOnPlayerTrigger.SetActive(false);
        }
    }

    public override void SwitchToDay()
    {
        m_targetMesh = DayObject;
        m_transitionTimer = 0f;
        m_firsObjectTransitioned = false;
        m_inTransition = true;

        m_targetIsNight = false;
        m_scaleTransitionTimer = 0f;
        m_transitionScale = true;
    }

    public override void SwitchToNight()
    {
        m_targetMesh = m_inPlaygroundArea ? NightObjectIn : NightObjectOut;
        m_transitionTimer = 0f;
        m_firsObjectTransitioned = false;
        m_inTransition = true;

        m_targetIsNight = true;
        m_scaleTransitionTimer = 0f;
        m_transitionScale = true;
    }

    private void transitionToNightIn()
    {
        m_targetMesh = NightObjectIn;
        m_transitionTimer = 0f;
        m_firsObjectTransitioned = false;
        m_inTransition = true;
    }

    private void transitionToNightOut()
    {
        m_targetMesh = NightObjectOut;
        m_transitionTimer = 0f;
        m_firsObjectTransitioned = false;
        m_inTransition = true;
    }
}
