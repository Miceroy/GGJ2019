using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceObject1 : GameBase
{
    private enum ItemLocationEnum
    {
        None = 0,

       InAreaInner = 1,
       InAreaOuter = 2,
       OutsideArea = 3,
    }
	public bool CanCollide = false;

    [SerializeField]
    private GameObject goToShowOnPlayerTrigger;

    [SerializeField]
    private GameObject DayObject;

    [SerializeField]
    private GameObject NightObjectIn;

    [SerializeField]
    private GameObject NightObjectOut;

    [SerializeField]
    private float m_transitionSpeed = 1f; // How long single transition lasts in seconds (total amount is double of this time)

    [SerializeField]
    private float m_scaleTransitionSpeed = 1f;

    [SerializeField]
    private Vector3 m_dayScale = new Vector3(1f, 1f, 1f);

    [SerializeField]
    private Vector3 m_nightScale = new Vector3(2f, 2f, 2f);

    //private bool m_inPlaygroundArea = false;
    private ItemLocationEnum m_location = ItemLocationEnum.None;

    private bool m_inTransition = false;
    private bool m_firsObjectTransitioned = false;
    private float m_transitionTimer = 0f;

    private GameObject m_currentMesh = null;
    private GameObject m_targetMesh = null;

    private bool m_transitionScale = false;
    private bool m_targetIsNight = false;
    private float m_scaleTransitionTimer = 0f;

    private void Start()
    {
        DayObject.GetComponent<MeshRenderer>().material = new Material(m_materialPrefab);
        NightObjectIn.GetComponent<MeshRenderer>().material = new Material(m_materialPrefab);
        NightObjectOut.GetComponent<MeshRenderer>().material = new Material(m_materialPrefab);

        // Create runtime copy of default materials, so material shader edits doesn't affect every piece using same material.
        // Use these lines instead when objects are read and do not require "much" debugging.
        //DayObject.material = new Material(DayObject.material);
        //NightObjectIn.material = new Material(NightObjectIn.material);
        //NightObjectOut.material = new Material(NightObjectOut.material);

        m_currentMesh = DayObject;
        DayObject.GetComponent<MeshRenderer>().material.SetFloat(m_dissolveAmount, 0f);
        DayObject.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 0);

        NightObjectIn.GetComponent<MeshRenderer>().material.SetFloat(m_dissolveAmount, 1f);
        NightObjectIn.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 0);

        NightObjectOut.GetComponent<MeshRenderer>().material.SetFloat(m_dissolveAmount, 1f);
        NightObjectOut.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 0);


        if (goToShowOnPlayerTrigger)
        {
            goToShowOnPlayerTrigger.SetActive(false);
        }

        m_location = ItemLocationEnum.OutsideArea; // Determine area positon on spawning?
    }

    private void OnTriggerEnter(Collider other)
    {
        AreaObject _area = other.gameObject.GetComponent<AreaObject>();
        if (_area != null)
        {
            if (_area.ID == GameBaseIDEnum.InnerArea)
            {
                // In area
                //m_inPlaygroundArea = true;
                transitionIntoNightTimeArea(ItemLocationEnum.InAreaInner);
            }
            else if (_area.ID == GameBaseIDEnum.OuterArea)
            {
                // TODO
                transitionIntoNightTimeArea(ItemLocationEnum.InAreaOuter);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AreaObject _area = other.gameObject.GetComponent<AreaObject>();
        if (_area != null)
        {
            if (_area.ID == GameBaseIDEnum.InnerArea) // Left from inner area and transition to outer area
            {
                transitionIntoNightTimeArea(ItemLocationEnum.InAreaOuter);
            }
            else if (_area.ID == GameBaseIDEnum.OuterArea)
            {
                transitionIntoNightTimeArea(ItemLocationEnum.OutsideArea);
            }

            //if (_area.ID == GameBaseIDEnum.InnerArea)
            //{
            //    // Out of area
            //    m_inPlaygroundArea = false;
            //
            //    if (!GameController.IsDay)
            //    {
            //        transitionToNightOut();
            //    }
            //}
            //else if(!m_inPlaygroundArea && _area.ID == GameBaseIDEnum.OuterArea)
            //{
            //    // TODO
            //}
        }
    }

    public override void SetPlayerNearEffectOn()
    {
        goToShowOnPlayerTrigger.SetActive(true);

        DayObject.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 1);
        NightObjectIn.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 1);
        NightObjectOut.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 1);
    }

    public override void SetPlayerNearEffectOff()
    {
        goToShowOnPlayerTrigger.SetActive(false);

        DayObject.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 0);
        NightObjectIn.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 0);
        NightObjectOut.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 0);
    }


    protected override void Update()
    {
        if (m_inTransition)
        {
            m_transitionTimer += Time.deltaTime;
            float _transitionValue = m_transitionTimer / m_transitionSpeed;

            if (!m_firsObjectTransitioned)
                m_currentMesh.GetComponent<MeshRenderer>().material.SetFloat(m_dissolveAmount, _transitionValue);
            else
                m_currentMesh.GetComponent<MeshRenderer>().material.SetFloat(m_dissolveAmount, 1f - _transitionValue);

            if (_transitionValue >= 1f)
            {
                if (!m_firsObjectTransitioned)
                {
                    m_firsObjectTransitioned = true;
                    if (m_targetMesh != null)
                        m_currentMesh = m_targetMesh;
                    else
                        m_inTransition = false;

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
        switch (m_location)
        {
            case ItemLocationEnum.InAreaInner:
                m_targetMesh = NightObjectIn;
                break;
            case ItemLocationEnum.InAreaOuter:
                m_targetMesh = NightObjectOut;
                break;
            case ItemLocationEnum.OutsideArea:
                m_targetMesh = DayObject;
                break;
        }

        //m_targetMesh = m_loc ? NightObjectIn : NightObjectOut;
        m_transitionTimer = 0f;
        m_firsObjectTransitioned = false;
        m_inTransition = true;

        m_targetIsNight = true;
        m_scaleTransitionTimer = 0f;
        m_transitionScale = true;
    }

    private void transitionIntoNightTimeArea(ItemLocationEnum locationToTransitionIn)
    {
        if (m_location == locationToTransitionIn)
            return;

        m_location = locationToTransitionIn;

        if (GameController.IsDay)
            return;

       // Debug.LogError("Transition into [" + locationToTransitionIn + "]");

        switch (m_location)
        {
            case ItemLocationEnum.InAreaInner:
                m_targetMesh = NightObjectIn;
                break;
            case ItemLocationEnum.InAreaOuter:
                m_targetMesh = NightObjectOut;
                break;
            case ItemLocationEnum.OutsideArea:
                //m_targetMesh = DayObject;
                break;
        }

        m_transitionTimer = 0f;
        m_firsObjectTransitioned = false;
        m_inTransition = true;
    }

    //private void transitionToNightIn()
    //{
    //    m_targetMesh = NightObjectIn;
    //    m_transitionTimer = 0f;
    //    m_firsObjectTransitioned = false;
    //    m_inTransition = true;
    //}
    //
    //private void transitionToNightOut()
    //{
    //    m_targetMesh = NightObjectOut;
    //    m_transitionTimer = 0f;
    //    m_firsObjectTransitioned = false;
    //    m_inTransition = true;
    //}
}
