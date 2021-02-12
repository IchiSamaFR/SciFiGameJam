using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VolumeManager))]
[RequireComponent(typeof(AudioSource))]
public class SoundsMaker : MonoBehaviour
{
    [System.Serializable]
    public struct SoundsSpike
    {
        public AudioClip audio;
        public float time;
    }
    
    public List<SoundsSpike> soundsSpikes = new List<SoundsSpike>();

    [SerializeField]
    private float volume;
    private float actualTime;
    private int index;
    private AudioSource audioSource;

    void Start()
    {
        GetComponent<VolumeManager>().externalVolume = volume;
        audioSource = GetComponent<AudioSource>();
        soundsSpikes = soundsSpikes.OrderBy(s => s.time).ToList();
    }
    
    void Update()
    {
        actualTime += Time.deltaTime;

        if (soundsSpikes[index].time < actualTime)
        {
            audioSource.clip = soundsSpikes[index].audio;
            audioSource.Play();
            index++;
        }

        if(index >= soundsSpikes.Count)
        {
            index = 0;
            actualTime = 0;
        }
    }
}
