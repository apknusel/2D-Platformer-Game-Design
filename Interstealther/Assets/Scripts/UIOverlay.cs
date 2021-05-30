using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{
    public float StartTime;
    public Text ammoDisplay;
    public Text timeDisplay;
    public Image infAmmo;
    private float t;

    void Update()
    {
        t = Time.time - StartTime;
        timeDisplay.text = t.ToString("f3");
    }

    void Start()
    {
        StartTime = Time.time;
        infiniteAmmo(false);
    }

    public void updateAmmo(int ammoCount)
    {
        ammoDisplay.text = ammoCount.ToString();
    }

    public void infiniteAmmo(bool inf)
    {
        infAmmo.gameObject.SetActive(inf);
        ammoDisplay.gameObject.SetActive(!inf);
    }

    void OnDestroy()
    {
        PlayerPrefs.SetString("time", t.ToString("f3"));
    }
}