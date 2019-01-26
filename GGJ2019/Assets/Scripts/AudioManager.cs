using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioIDEnum
{
    None = 0,

    // Sound effects

    // Music

    // Ambient
}

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_instance = null;

    //private List<AudioComponent> m_gameAudios = null;
    private Dictionary<AudioIDEnum, AudioComponent> m_gameAudiosByID = null;

    private Dictionary<AudioIDEnum, AudioComponent> m_currentlyPlayingAudioDictionary = null;
    private List<AudioComponent> m_currentlyPlayingAudioList = null;

    private void Start()
    {
        m_instance = this;

        m_gameAudiosByID = new Dictionary<AudioIDEnum, AudioComponent>();
        for (int i = 0; i < transform.childCount; i++)
        {
            AudioComponent _component = transform.GetChild(i).GetComponent<AudioComponent>();
            if (_component != null)
            {
                //if (_component.AudioID != AudioIDEnum.None)
            }
        }
    }
}
