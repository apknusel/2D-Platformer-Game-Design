using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
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
        overlay.updateAmmo(ammoCount);
        activeShield = false;
        shield.SetActive(false);
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
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
        overlay.updateAmmo(ammoCount);
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
    }

    void crouchShoot()
    {
        Instantiate(bulletPrefab, crouchFirePoint.position, firePoint.rotation);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
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
            StartCoroutine(AmmoPowerup());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "ammo")
        {
            ammoCount += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "shield powerup")
        {
            if (!activeShield)
            {
                activeShield = true;
                shield.SetActive(true);
                Destroy(collision.gameObject);
            }
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
        infiniteAmmo = true;
        tempAmmo = ammoCount;
        ammoCount = -1;
        yield return new WaitForSeconds(5);
        infiniteAmmo = false;
        ammoCount = tempAmmo;
    }
}