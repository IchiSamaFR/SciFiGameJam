using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXAudio : MonoBehaviour
{
    AudioSource audioSource;

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
    public void Set(AudioClip clip, bool loop = false)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
