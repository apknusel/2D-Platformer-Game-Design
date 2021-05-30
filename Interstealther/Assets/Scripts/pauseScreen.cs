using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseScreen : MonoBehaviour
{
    public Slider volumeSlider;
    public bool isPaused;
    public GameObject PauseMenu;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("vol");
    }

    void Update()
    {
        if (isPaused)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;

            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach(AudioSource a in audios)
            {
                a.Pause();
            }
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            
            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios)
            {
                a.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    public bool IsGameRunning()
    {
        return !isPaused;
    }
}
