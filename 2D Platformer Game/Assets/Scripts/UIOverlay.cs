using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{
    public Text ammoDisplay;
    public Image infAmmo;

    void Start()
    {
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
}