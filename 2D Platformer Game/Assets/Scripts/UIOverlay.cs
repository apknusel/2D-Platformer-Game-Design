using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{
    public Text ammoDisplay;

    public void updateAmmo(int ammoCount)
    {
        ammoDisplay.text = ammoCount.ToString();
    }
}