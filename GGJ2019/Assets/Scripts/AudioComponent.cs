using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    [SerializeField]
    private AudioIDEnum m_audioID = AudioIDEnum.None;
    public AudioIDEnum AudioID { get { return m_audioID; } }


    [SerializeField]
    private AudioClip m_audioClip = null;
    public AudioClip AudioClip { get { return m_audioClip; } }
}
