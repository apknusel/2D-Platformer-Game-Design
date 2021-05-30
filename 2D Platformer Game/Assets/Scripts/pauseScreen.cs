using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    public bool isPaused;
    public GameObject PauseMenu;

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
