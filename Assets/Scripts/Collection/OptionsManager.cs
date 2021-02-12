using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager instance;

    public float volumeMusic = 1;
    public float volumeAmbient = 1;
    public float volumeFX = 1;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("OptionsManager already existing.");
            return;
        }

        instance = this;
    }

    private void Update()
    {
        RefreshVolume();
    }

    public float GetVolume(string id)
    {
        if(id == "ambient")
        {
            return volumeAmbient;
        }
        else if (id == "fx")
        {
            return volumeFX;
        }
        else if (id == "music")
        {
            return volumeMusic;
        }
        return 0;
    }
    public bool SetVolume(string id, float value)
    {
        if (id == "ambient")
        {
            volumeAmbient = value;
            return true;
        }
        else if (id == "fx")
        {
            volumeFX = value;
            return true;
        }
        else if (id == "music")
        {
            volumeMusic = value;
            return true;
        }
        return false;
    }

    public void RefreshVolume()
    {
        List<VolumeManager> managers = FindObjectsOfType<VolumeManager>().ToList();

        foreach (var item in managers)
        {
            item.SetVolume(GetVolume(item.type));
        }
    }
}
