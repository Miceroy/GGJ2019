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
    private bool prevDay;

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

    private void Update()
    {
        if(!GameController.IsDay && prevDay)
        {
            // TODO: Day -> Night
            dayObject.SetActive(false);
            if (m_collideLevel == 2)
            {
                nightObjectIn.SetActive(true);
                nightObjectOut.SetActive(false);
            }
            else if(m_collideLevel == 1)
            {
                nightObjectIn.SetActive(true);
                nightObjectOut.SetActive(false);
            }
            else
            {
                dayObject.SetActive(true);
            }
        }
        if (GameController.IsDay && !prevDay)
        {
            // TODO: Night -> Day
            dayObject.SetActive(true);
            nightObjectIn.SetActive(false);
            nightObjectOut.SetActive(false);
        }

        prevDay = GameController.IsDay;
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
            else if ( m_collideLevel < 2 && _area.ID == GameBaseIDEnum.OuterArea)
            {
                dayObject.SetActive(false);
                nightObjectIn.SetActive(false);
                nightObjectOut.SetActive(true);
                m_collideLevel = 1;
            }
            /*else
            {
                dayObject.SetActive(true);
                nightObjectIn.SetActive(false);
                nightObjectOut.SetActive(false);
                m_collideLevel = 0;
            }*/

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
                dayObject.SetActive(true);
                nightObjectIn.SetActive(false);
                nightObjectOut.SetActive(false);
                m_collideLevel = 0;
            }
            /*else if (_area.ID == GameBaseIDEnum.OuterArea)
            {
                //    transitionIntoNightTimeArea(ItemLocationEnum.OutsideArea);
                dayObject.SetActive(true);
                nightObjectIn.SetActive(false);
                nightObjectOut.SetActive(false);
                m_collideLevel = 0;
            }*/

            Debug.Log("Collide level: " + m_collideLevel.ToString());
        }

        PlayerCharacterController player = other.gameObject.GetComponent<PlayerCharacterController>();
        if (player != null)
        {
            goToShowOnPlayerTrigger.SetActive(false);
        }
    }
}
