using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{
    public Text ammoDisplay;

    public void updateAmmo(int ammoCount)
    {
        if (ammoCount == -1)
        {
            ammoDisplay.text = "INFINITE";
        }
        else
        {
            ammoDisplay.text = ammoCount.ToString();
        }
    }
}