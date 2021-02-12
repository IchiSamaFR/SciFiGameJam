using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeManager : MonoBehaviour
{
    public float volume = 1;
    public string type = "ambient";

    public float externalVolume = -1;

    private void Start()
    {
        SetVolume();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        if (externalVolume != -1)
        {
            if (volume == 0)
            {
                GetComponent<AudioSource>().volume = 0;
            }
            else
            {
                GetComponent<AudioSource>().volume = volume * externalVolume;
            }
        }
        else
        {
            GetComponent<AudioSource>().volume = volume;
        }
    }
    public void SetVolume()
    {
        volume = OptionsManager.instance.GetVolume(type);
        if (externalVolume != -1)
        {
            GetComponent<AudioSource>().volume = volume * externalVolume;
        }
        else
        {
            GetComponent<AudioSource>().volume = volume;
        }
    }
}
