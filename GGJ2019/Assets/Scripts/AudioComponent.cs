using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    [SerializeField]
    private AudioIDEnum m_audioID = AudioIDEnum.None;
    public AudioIDEnum AudioID { get { return m_audioID; } }


    [SerializeField]
    private AudioSource m_audioSource = null;
    public AudioSource AudioSource { get { return m_audioSource; } }

    public bool FollowingTransform { get; set; }
}
