using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlManager
{
    private bool m_isDay = true;
    public bool IsDay
    {
        get
        {
            return m_isDay;
        }
    }

    private List<GameBase> m_dayNightObjects = null;
    private Dictionary<string, GameBase> m_dayNightObjectsByID = null;

    public GameControlManager()
    {
        initialize();
    }

    private void initialize()
    {
        m_dayNightObjects = new List<GameBase>();
        m_dayNightObjectsByID = new Dictionary<string, GameBase>();
        m_isDay = true;
    }

    public void AddGameBaseObject(GameBase gameBase)
    {
        if (m_dayNightObjectsByID.ContainsKey(gameBase.ID))
            return;

        m_dayNightObjectsByID.Add(gameBase.ID, gameBase);
        m_dayNightObjects.Add(gameBase);
    }

    public void RemoveGameBaseObject(GameBase gameBase)
    {
        if (!m_dayNightObjectsByID.ContainsKey(gameBase.ID))
            return;

        m_dayNightObjectsByID.Remove(gameBase.ID);
        for (int i = 0; i < m_dayNightObjects.Count; i++)
        {
            if (m_dayNightObjects[i].ID == gameBase.ID)
            {
                m_dayNightObjects.RemoveAt(i);
                break;
            }
        }
    }

    public void ToggleDayNightCycle()
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
