using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{
    public static AudioClip jumpSound, shootSound, pickupSound, noAmmoSound;
    static AudioSource audioSrc;

    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("jump");
        shootSound = Resources.Load<AudioClip>("shoot");
        pickupSound = Resources.Load<AudioClip>("pickup");
        noAmmoSound = Resources.Load<AudioClip>("no ammo");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "shoot":
                audioSrc.PlayOneShot(shootSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "pickup":
                audioSrc.PlayOneShot(pickupSound);
                break;
            case "no ammo":
                audioSrc.PlayOneShot(noAmmoSound);
                break;
        }
    }
}
