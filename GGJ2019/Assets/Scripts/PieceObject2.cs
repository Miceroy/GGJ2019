using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceObject2 : GameBase
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
    private GameObject dayObject;

    [SerializeField]
    private GameObject nightObjectIn;

    [SerializeField]
    private GameObject nightObjectOut;

    private int m_collideLevel;

    
    private void Start()
    {
        dayObject.SetActive(true);
        nightObjectIn.SetActive(false);
        nightObjectOut.SetActive(false);
        goToShowOnPlayerTrigger.SetActive(false);
        m_collideLevel = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        AreaObject _area = other.gameObject.GetComponent<AreaObject>();
        if (!GameController.IsDay && _area != null)
        {
            if (_area.ID == GameBaseIDEnum.InnerArea)
            {
                dayObject.SetActive(false);
                nightObjectIn.SetActive(true);
                nightObjectOut.SetActive(false);
                m_collideLevel = 2;
            }
            else if (m_collideLevel < 2 && _area.ID == GameBaseIDEnum.OuterArea)
            {
                dayObject.SetActive(false);
                nightObjectIn.SetActive(false);
                nightObjectOut.SetActive(true);
                m_collideLevel = 1;
            }

            Debug.Log("Collide level: " + m_collideLevel.ToString());
        }

        PlayerCharacterController player = other.gameObject.GetComponent<PlayerCharacterController>();
        if (player != null)
        {
            goToShowOnPlayerTrigger.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AreaObject _area = other.gameObject.GetComponent<AreaObject>();
        if (!GameController.IsDay && _area != null)
        {
            if (_area.ID == GameBaseIDEnum.InnerArea) // Left from inner area and transition to outer area
            {
                //    transitionIntoNightTimeArea(ItemLocationEnum.InAreaOuter);
                dayObject.SetActive(false);
                nightObjectIn.SetActive(false);
                nightObjectOut.SetActive(true);
                m_collideLevel = 1;
            }
            else if (_area.ID == GameBaseIDEnum.OuterArea)
            {
                //    transitionIntoNightTimeArea(ItemLocationEnum.OutsideArea);
                dayObject.SetActive(true);
                nightObjectIn.SetActive(false);
                nightObjectOut.SetActive(false);
                m_collideLevel = 0;
            }

            Debug.Log("Collide level: " + m_collideLevel.ToString());
        }

        PlayerCharacterController player = other.gameObject.GetComponent<PlayerCharacterController>();
        if (player != null)
        {
            goToShowOnPlayerTrigger.SetActive(false);
        }
    }

    //public override void SetPlayerNearEffectOn()
    //{
    //    goToShowOnPlayerTrigger.SetActive(true);

    //    DayObject.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 1);
    //    NightObjectIn.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 1);
    //    NightObjectOut.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 1);
    //}

    //public override void SetPlayerNearEffectOff()
    //{
    //    goToShowOnPlayerTrigger.SetActive(false);

    //    DayObject.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 0);
    //    NightObjectIn.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 0);
    //    NightObjectOut.GetComponent<MeshRenderer>().material.SetInt(m_hologramToggle, 0);
    //}

    protected override void Update()
    {
        //if (m_inTransition)
        //{
        //    m_transitionTimer += Time.deltaTime;
        //    float _transitionValue = m_transitionTimer / m_transitionSpeed;

        //    if (!m_firsObjectTransitioned)
        //        m_currentMesh.GetComponent<MeshRenderer>().material.SetFloat(m_dissolveAmount, _transitionValue);
        //    else
        //        m_currentMesh.GetComponent<MeshRenderer>().material.SetFloat(m_dissolveAmount, 1f - _transitionValue);

        //    if (_transitionValue >= 1f)
        //    {
        //        if (!m_firsObjectTransitioned)
        //        {
        //            m_firsObjectTransitioned = true;
        //            if (m_targetMesh != null)
        //                m_currentMesh = m_targetMesh;
        //            else
        //                m_inTransition = false;

        //            m_targetMesh = null;
        //            m_transitionTimer = 0f;
        //        }
        //        else
        //            m_inTransition = false;
        //    }
        //}

        //if (m_transitionScale)
        //{
        //    m_scaleTransitionTimer += Time.deltaTime;
        //    float _transitionValue = m_scaleTransitionTimer / m_scaleTransitionSpeed;

        //    Vector3 _from = m_targetIsNight ? m_dayScale : m_nightScale;
        //    Vector3 _to = m_targetIsNight ? m_nightScale : m_dayScale;

        //    Vector3 _curScale = Vector3.Lerp(_from, _to, _transitionValue);

        //    transform.localScale = _curScale;

        //    if (_transitionValue >= 1f)
        //    {
        //        m_transitionScale = false;
        //    }
        //}

        //if(GetComponent<Rigidbody>().useGravity == false && goToShowOnPlayerTrigger.activeSelf)
        //{
        //    goToShowOnPlayerTrigger.SetActive(false);
        //}
    }

    public override void SwitchToDay()
    {
        //m_targetMesh = DayObject;
        //m_transitionTimer = 0f;
        //m_firsObjectTransitioned = false;
        //m_inTransition = true;

        //m_targetIsNight = false;
        //m_scaleTransitionTimer = 0f;
        //m_transitionScale = true;
    }

    public override void SwitchToNight()
    {
        //switch (m_location)
        //{
        //    case ItemLocationEnum.InAreaInner:
        //        m_targetMesh = NightObjectIn;
        //        break;
        //    case ItemLocationEnum.InAreaOuter:
        //        m_targetMesh = NightObjectOut;
        //        break;
        //    case ItemLocationEnum.OutsideArea:
        //        m_targetMesh = DayObject;
        //        break;
        //}

        ////m_targetMesh = m_loc ? NightObjectIn : NightObjectOut;
        //m_transitionTimer = 0f;
        //m_firsObjectTransitioned = false;
        //m_inTransition = true;

        //m_targetIsNight = true;
        //m_scaleTransitionTimer = 0f;
        //m_transitionScale = true;
    }

    private void transitionIntoNightTimeArea(ItemLocationEnum locationToTransitionIn)
    {
       // if (m_location == locationToTransitionIn)
       //     return;

       // m_location = locationToTransitionIn;

       // if (GameController.IsDay)
       //     return;

       //// Debug.LogError("Transition into [" + locationToTransitionIn + "]");

       // switch (m_location)
       // {
       //     case ItemLocationEnum.InAreaInner:
       //         m_targetMesh = NightObjectIn;
       //         break;
       //     case ItemLocationEnum.InAreaOuter:
       //         m_targetMesh = NightObjectOut;
       //         break;
       //     case ItemLocationEnum.OutsideArea:
       //         //m_targetMesh = DayObject;
       //         break;
       // }

       // m_transitionTimer = 0f;
       // m_firsObjectTransitioned = false;
       // m_inTransition = true;
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
