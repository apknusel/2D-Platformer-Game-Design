using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeDisplay;

    void Start()
    {
        timeDisplay.text = "Time: " + PlayerPrefs.GetString("time");
    }
}
