using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : GameBase
{
    public GameObject objectPickPoint;

    //GameObject collidingItem;

    Transform collidingOldParent;
    //float oldY;

    bool picked;

    private GameBase m_baseGameObject = null;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    m_baseGameObject = null;
    //}

    // Update is called once per frame
    protected override void Update()
    {
        if (m_baseGameObject != null && Input.GetButtonUp("Fire1"))
        {
            if (picked)
            {
                // Release
                Debug.Log("Player release");
                picked = false;
                m_baseGameObject.transform.SetParent(collidingOldParent);
                m_baseGameObject.Rigidbody.useGravity = true;
                m_baseGameObject.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                /*collidingItem.transform.position.Set(
                    collidingItem.transform.position.x, 
                    oldY, 
                    collidingItem.transform.position.z);*/
            }
            else
            {
                if (!m_baseGameObject.IsPickable)
                    return;

                // Grab
                Debug.Log("Player grab");

                picked = true;
                collidingOldParent = m_baseGameObject.transform.parent;
                m_baseGameObject.transform.SetParent(objectPickPoint.transform);
                m_baseGameObject.Rigidbody.useGravity = false;
                m_baseGameObject.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                //oldY = objectPickPoint.transform.position.y;
                m_baseGameObject.transform.localPosition = new Vector3();
                //collidingItem.transform.Translate(
                //    0,
                //    objectPickPoint.transform.localPosition.y,
                //    0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (picked)
            return;

        GameBase _baseObject = other.gameObject.GetComponent<GameBase>();
        if (_baseObject != null)
        {
            m_baseGameObject = _baseObject;
            _baseObject.SetPlayerNearEffectOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameBase _baseObject = other.gameObject.GetComponent<GameBase>();
        if (!picked && _baseObject != null && _baseObject == m_baseGameObject)
        {
            m_baseGameObject.SetPlayerNearEffectOff();
            m_baseGameObject = null;
        }
    }
}
