using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameControlManager m_controlManager = null;
    private bool m_night;

    // Debug purposes
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (!m_night)
            {
                Button_ToggleDayNightCycle();
                m_night = true;
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static void AddGameBaseObject(GameBase gameBase)
    {
        if (m_controlManager == null)
            m_controlManager = new GameControlManager();

        m_controlManager.AddGameBaseObject(gameBase);
    }

    public static void RemoveGameBaseObject(GameBase gameBase)
    {
        if (m_controlManager == null)
            m_controlManager = new GameControlManager();

        m_controlManager.RemoveGameBaseObject(gameBase);
    }

    public static void ToggleDayNightCycle()
    {
        if (m_controlManager == null){
            m_controlManager = new GameControlManager();
		}
		
        m_controlManager.ToggleDayNightCycle();
    }

    public static bool IsDay
    {
        get
        {
            if (m_controlManager == null)
                return true;

            return m_controlManager.IsDay;
        }
    }

    // Event for unity button.
    public void Button_ToggleDayNightCycle()
    {
        if (m_controlManager == null)
            m_controlManager = new GameControlManager();

        m_controlManager.ToggleDayNightCycle();
    }
}
