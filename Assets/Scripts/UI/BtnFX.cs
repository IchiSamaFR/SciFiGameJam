using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnFX : MonoBehaviour
{
    [SerializeField]
    private bool HimSelf = false;

    AudioSource audioSource;

    private void Awake()
    {
        if (HimSelf)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void MakeSound()
    {
        if (!audioSource)
        {
            HimSelf = false;
        }
        if (!HimSelf)
        {
            FXAudio fxAudio = Instantiate(PrefabCollection.instance.GetPrefab("fxAudioBTN")).GetComponent<FXAudio>();
            fxAudio.Set(AudioCollection.instance.GetAudio("button"));
        }
        else if(HimSelf)
        {
            if (audioSource.clip == null)
            {
                audioSource.clip = AudioCollection.instance.GetAudio("button");
            }
            audioSource.Play();
        }


    }
}
