using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioIDEnum
{
    None = 0,

    // Music
    Music_SimpleBackground = 10,

    // Ambient
    Ambient_SimpleAmbient = 100,

    // Effects
    Effects_SimpleEffect = 200,
}

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_instance = null;

    //private List<AudioComponent> m_gameAudios = null;
    private Dictionary<AudioIDEnum, AudioComponent> m_gameAudiosByID = null;

    private List<AudioComponent> m_currentlyPlayingAudioList = null;
    private Dictionary<AudioIDEnum, AudioComponent> m_currentlyPlayingAudioDictionary = null;

    // TODO?
    //private Dictionary<AudioIDEnum, List<AudioSource>> m_currentlyPlayingAllSources = null;

    private void Start()
    {
        m_instance = this;

        m_gameAudiosByID = new Dictionary<AudioIDEnum, AudioComponent>();
        for (int i = 0; i < transform.childCount; i++)
        {
            AudioComponent _component = transform.GetChild(i).GetComponent<AudioComponent>();
            if (_component != null)
            {
                if (_component.AudioID != AudioIDEnum.None)
                {
                    if (m_gameAudiosByID.ContainsKey(_component.AudioID))
                    {
                        Debug.LogError("There is already audio ID [" + _component.AudioID + "] in audio collection! GameObject [" + _component.gameObject.name + "]");
                    }
                    else

                    {
                        m_gameAudiosByID.Add(_component.AudioID, _component);
                    }
                }
            }
        }

        m_currentlyPlayingAudioList = new List<AudioComponent>();
        m_currentlyPlayingAudioDictionary = new Dictionary<AudioIDEnum, AudioComponent>();
        //m_currentlyPlayingAllSources = new Dictionary<AudioIDEnum, List<AudioSource>>();
    }

    public static AudioComponent PlaySound(AudioIDEnum audioToPlay, bool loop = false)
    {
        if (m_instance == null)
            return null;

        if (m_instance.m_gameAudiosByID.ContainsKey(audioToPlay))
        {
            AudioComponent _audioComponent = m_instance.m_gameAudiosByID[audioToPlay];

            //if (loop)
            {
                if (m_instance.m_currentlyPlayingAudioDictionary.ContainsKey(audioToPlay))
                {
                    Debug.LogWarning("Trying to play already playing audio!");
                }
                else
                {
                    m_instance.m_currentlyPlayingAudioList.Add(_audioComponent);
                    m_instance.m_currentlyPlayingAudioDictionary.Add(audioToPlay, _audioComponent);
                    _audioComponent.AudioSource.loop = loop;
                    _audioComponent.AudioSource.Play();
                    return _audioComponent;
                }
            }
            //else
            //{
            //    if ()
            //
            //    if (m_instance.m_currentlyPlayingAllSources.ContainsKey(audioToPlay))
            //    {
            //        m_instance.m_currentlyPlayingAllSources[audioToPlay].Add(_audioComponent.AudioSource);
            //    }
            //}
        }
        else
        {
            Debug.LogError("There was no AudioId [" + audioToPlay + "] in audio collection!");
        }

        return null;
    }

    public static void PlaySound(AudioIDEnum audioToPlay, Vector3 position, bool loop = false)
    {
        AudioComponent _component = PlaySound(audioToPlay, loop);
        if (_component != null)
        {
            _component.transform.position = position;
        }
    }

    public static void PlaySound(AudioIDEnum audioToPlay, Transform transformToFollow, bool loop = false)
    {
        AudioComponent _component = PlaySound(audioToPlay, loop);
        if (_component != null)
        {
            _component.transform.SetParent(transformToFollow);
            _component.transform.localPosition = new Vector3();
            _component.FollowingTransform = true;
        }
    }

    public static void PauseSound(AudioIDEnum audioToPause)
    {
        if (m_instance.m_currentlyPlayingAudioDictionary.ContainsKey(audioToPause))
        {
            AudioComponent _component = m_instance.m_currentlyPlayingAudioDictionary[audioToPause];

            if (_component.AudioSource.isPlaying)
                _component.AudioSource.Pause();
            else
                _component.AudioSource.UnPause();
        }
    }

    public static void StopSound(AudioIDEnum audioToStop)
    {
        if (m_instance.m_currentlyPlayingAudioDictionary.ContainsKey(audioToStop))
        {
            AudioComponent _component = m_instance.m_currentlyPlayingAudioDictionary[audioToStop];
            if (_component.FollowingTransform)
            {
                _component.transform.SetParent(m_instance.transform);
                _component.transform.localPosition = new Vector3();
                _component.FollowingTransform = false;
                _component.AudioSource.Stop();

                m_instance.m_currentlyPlayingAudioDictionary.Remove(audioToStop);
                m_instance.m_currentlyPlayingAudioList.Remove(_component);
            }
        }
    }
}
