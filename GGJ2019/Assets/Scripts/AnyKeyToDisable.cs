using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKeyToDisable : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource source;
	bool isPlaying =false;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying && Input.anyKey)
        {
			isPlaying=true;
            source.PlayOneShot(sound);
            StartCoroutine(disableLater());
        }
    }

    IEnumerator disableLater()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        //return null;
    }
}
