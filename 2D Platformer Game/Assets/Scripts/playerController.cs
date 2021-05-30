using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private pauseScreen pScreen;

    public SceneManager manager;

    public CharacterController2D controller;
    public Animator animator;

    public GameObject shield;
    private bool activeShield;

    public Transform firePoint;
    public Transform crouchFirePoint;
    public GameObject bulletPrefab;

    public int ammoCount;
    private bool infiniteAmmo;
    private int tempAmmo;
    public UIOverlay overlay;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {
        pScreen = FindObjectOfType<pauseScreen>();
        overlay.updateAmmo(ammoCount);
        activeShield = false;
        shield.SetActive(false);
    }

    void Update()
    {
        if (pScreen.IsGameRunning())
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("isJumping", true);
                musicPlayer.PlaySound("jump");
            }
            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }

            if (Input.GetButtonDown("Fire1") && ammoCount != 0)
            {
                if (!infiniteAmmo)
                {
                    ammoCount -= 1;
                }
                if (crouch == true)
                {
                    crouchShoot();
                }
                else
                {
                    animator.Play("stillshooting", 0, 0);
                    Shoot();
                }
            }
            else if (Input.GetButtonDown("Fire1") && ammoCount == 0)
            {
                musicPlayer.PlaySound("no ammo");
            }
            overlay.updateAmmo(ammoCount);
        }
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void onCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        musicPlayer.PlaySound("shoot");
    }

    void crouchShoot()
    {
        Instantiate(bulletPrefab, crouchFirePoint.position, firePoint.rotation);
        musicPlayer.PlaySound("shoot");
    }

    void FixedUpdate()
    {
        if (pScreen.IsGameRunning())
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "enemy" || collision.gameObject.tag == "enemybullet") && activeShield == true)
        {
            Destroy(collision.gameObject);
            activeShield = false;
            shield.SetActive(false);
        }
        else if ((collision.gameObject.tag == "enemy" || collision.gameObject.tag == "enemybullet") && activeShield == false)
        {
            manager.LoadScene("Death Screen");
        }

        if (collision.gameObject.tag == "infiniteammo")
        {
            musicPlayer.PlaySound("pickup");
            StartCoroutine(AmmoPowerup());
            ammoCount = tempAmmo;
            overlay.infiniteAmmo(true);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "ammo")
        {
            musicPlayer.PlaySound("pickup");
            ammoCount += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "shield powerup")
        {
            if (!activeShield)
            {
                musicPlayer.PlaySound("pickup");
                activeShield = true;
                shield.SetActive(true);
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "spikes")
        {
            manager.LoadScene("Death Screen");
        }
    }

    public bool ActiveShield
    {
        get
        {
            return activeShield;
        }
        set
        {
            activeShield = value;
        }
    }

    public void disableShield()
    {
        activeShield = false;
    }

    IEnumerator AmmoPowerup()
    {
        tempAmmo = ammoCount;
        infiniteAmmo = true;
        yield return new WaitForSeconds(5);
        infiniteAmmo = false;
        ammoCount = tempAmmo;
        overlay.infiniteAmmo(false);
    }
}