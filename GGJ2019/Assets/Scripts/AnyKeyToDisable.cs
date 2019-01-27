using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKeyToDisable : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            source.PlayOneShot(sound);
            gameObject.SetActive(false);
        }
    }
}
