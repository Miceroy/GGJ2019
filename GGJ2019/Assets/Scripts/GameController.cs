using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private bool m_isDay = true;
    public static bool IsDay
    {
        get
        {
            if (m_instance == null)
                return true;

            return m_instance.m_isDay;
        }
    }

    private static GameController m_instance = null;

    private List<GameBase> m_dayNightObjects = null;
    private Dictionary<string, GameBase> m_dayNightObjectsByID = null;

    private void Awake()
    {
        m_instance = this;
        m_dayNightObjects = new List<GameBase>();
        m_dayNightObjectsByID = new Dictionary<string, GameBase>();
    }

    private void Start()
    {
        m_isDay = true;
    }

    // Debug purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Button_ToggleDayNightCycle();
        }
    }

    public static void AddGameBaseObject(GameBase gameBase)
    {
        if (m_instance == null)
            return;

        if (m_instance.m_dayNightObjectsByID.ContainsKey(gameBase.ID))
            return;

        m_instance.m_dayNightObjectsByID.Add(gameBase.ID, gameBase);
        m_instance.m_dayNightObjects.Add(gameBase);
    }

    public static void RemoveGameBaseObject(GameBase gameBase)
    {
        if (m_instance == null)
            return;

        if (!m_instance.m_dayNightObjectsByID.ContainsKey(gameBase.ID))
            return;

        m_instance.m_dayNightObjectsByID.Remove(gameBase.ID);
        for (int i = 0; i < m_instance.m_dayNightObjects.Count; i++)
        {
            if (m_instance.m_dayNightObjects[i].ID == gameBase.ID)
            {
                m_instance.m_dayNightObjects.RemoveAt(i);
                break;
            }
        } 
    }

    public static void ToggleDayNightCycle()
    {
        if (m_instance == null)
            return;

        m_instance.Button_ToggleDayNightCycle();
    }

    public void Button_ToggleDayNightCycle()
    {
        m_isDay = !m_isDay;

        for (int i = 0; i < m_dayNightObjects.Count; i++)
        {
            if (m_isDay)
                m_dayNightObjects[i].SwitchToDay();
            else
                m_dayNightObjects[i].SwitchToNight();
        }
    }
}
