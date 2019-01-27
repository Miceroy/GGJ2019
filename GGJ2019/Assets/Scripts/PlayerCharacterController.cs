using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : GameBase
{
    public float drop = 4.0f;
    public ThirdPersonCharacterOwn charCtrl;
    public Animator animator;
    public GameObject objectPickPoint;

    //GameObject collidingItem;

    Transform collidingOldParent;
    //float oldY;

    private GameBase m_pickedBaseObject = null;
    private GameBase m_collidedBaseObject = null;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    m_baseGameObject = null;
    //}

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            if (m_pickedBaseObject != null)
            {
                m_pickedBaseObject.transform.SetParent(collidingOldParent);
                m_pickedBaseObject.Rigidbody.useGravity = true;
                m_pickedBaseObject.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                m_pickedBaseObject = null;
                // TODO
                animator.SetBool("IsCarrying", false);
                charCtrl.m_antinSpeed += drop;

            }
            else if (m_collidedBaseObject != null)
            {
                collidingOldParent = m_collidedBaseObject.transform.parent;

                m_pickedBaseObject = m_collidedBaseObject;

                m_pickedBaseObject.transform.SetParent(objectPickPoint.transform);
                m_pickedBaseObject.Rigidbody.useGravity = false;
                m_pickedBaseObject.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                m_pickedBaseObject.transform.localPosition = new Vector3();
                // TODO
                animator.SetBool("IsCarrying", true);
                charCtrl.m_antinSpeed -= drop;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_pickedBaseObject != null)
            return;

        GameBase _baseObject = other.gameObject.GetComponent<GameBase>();
        if (_baseObject != null)
        {
            if (_baseObject.IsPickable)
                m_collidedBaseObject = _baseObject;

            _baseObject.SetPlayerNearEffectOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameBase _baseObject = other.gameObject.GetComponent<GameBase>();
        if (_baseObject != null)
        {
            if (_baseObject == m_collidedBaseObject)
                m_collidedBaseObject = null;

            _baseObject.SetPlayerNearEffectOff();
        }
    }

    //// Debugging
    //bool _isSoundPlaying = false;
    //public override void SwitchToNight()
    //{
    //    if (_isSoundPlaying)
    //        return;
    //
    //    AudioManager.PlaySound(AudioIDEnum.Music_SimpleBackground, true);
    //    _isSoundPlaying = true;
    //}
    //
    //public override void SwitchToDay()
    //{
    //    if (!_isSoundPlaying)
    //        return;
    //
    //    AudioManager.PauseSound(AudioIDEnum.Music_SimpleBackground);
    //}
}
