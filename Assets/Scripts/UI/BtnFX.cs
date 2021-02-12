using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BtnFX : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void MakeSound()
    {
        if(audioSource.clip == null)
        {
            audioSource.clip = AudioCollection.instance.GetAudio("button");
        }
        audioSource.Play();
    }
}
