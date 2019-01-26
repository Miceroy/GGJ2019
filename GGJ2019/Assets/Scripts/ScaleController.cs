using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : GameBase
{
    [SerializeField]
    private float m_scaleTransitionSpeed = 1f;

    [SerializeField]
    private Vector3 m_dayScale = new Vector3(1f, 1f, 1f);

    [SerializeField]
    private Vector3 m_nightScale = new Vector3(2f, 2f, 2f);

    private bool m_transitionScale = false;
    private float m_scaleTransitionTimer = 0f;
    private bool m_targetIsNight = false;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    protected override void Update()
    {
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
    }

    public override void SwitchToDay()
    {
        m_targetIsNight = false;
        m_scaleTransitionTimer = 0f;
        m_transitionScale = true;
    }

    public override void SwitchToNight()
    {
        m_targetIsNight = true;
        m_scaleTransitionTimer = 0f;
        m_transitionScale = true;
    }
}
