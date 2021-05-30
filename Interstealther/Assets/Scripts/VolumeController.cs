using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSrc;
    private float musicVolume = 1f;

    void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("vol");
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
        {
            a.volume = musicVolume;
        }
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        PlayerPrefs.SetFloat("vol",vol);
    }
}
