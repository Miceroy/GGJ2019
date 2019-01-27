using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : GameBase
{
    public float drop = 4.0f;
    public ThirdPersonCharacterOwn charCtrl;
    public Animator animator;
    public GameObject objectPickPoint;
    
    Transform collidingOldParent;

    private PieceObject2 m_pickedBaseObject = null;
    private PieceObject2 m_collidedBaseObject = null;

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Fire1");
            if (m_pickedBaseObject != null)
            {
                m_pickedBaseObject.transform.SetParent(collidingOldParent);
                m_pickedBaseObject.Rigidbody.useGravity = true;
                if (m_pickedBaseObject.CanCollide == true)
				{
					m_pickedBaseObject.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
					
				}
				else
				{
				    m_pickedBaseObject.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ; 
				}
                
                animator.SetBool("IsCarrying", false);
                charCtrl.m_antinSpeed += drop;
				Debug.Log("Meni tänne asti");
                m_pickedBaseObject.playdrop();
				m_pickedBaseObject = null;

            }
            else if (m_collidedBaseObject != null)
            {
                collidingOldParent = m_collidedBaseObject.transform.parent;

                m_pickedBaseObject = m_collidedBaseObject;

                m_pickedBaseObject.transform.SetParent(objectPickPoint.transform);
                m_pickedBaseObject.Rigidbody.useGravity = false;
                m_pickedBaseObject.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                m_pickedBaseObject.transform.localPosition = new Vector3();
                animator.SetBool("IsCarrying", true);
                charCtrl.m_antinSpeed -= drop;
				m_pickedBaseObject.playpick();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_pickedBaseObject != null)
            return;

        PieceObject2 _baseObject = other.gameObject.GetComponent<PieceObject2>();
        if (_baseObject != null)
        {
            Debug.Log("Vittu Enter!");
            if (_baseObject.IsPickable)
            {
                m_collidedBaseObject = _baseObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PieceObject2 _baseObject = other.gameObject.GetComponent<PieceObject2>();
        if (_baseObject != null)
        {
            Debug.Log("Vittu Exit!");
            if (_baseObject == m_collidedBaseObject)
            {
                m_collidedBaseObject = null;
            }
        }
    }
}
