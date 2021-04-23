using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    public void restart()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void quit()
    {
        Application.Quit();
    }
}
