using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public Transform firePoint;
    public Transform crouchFirePoint;
    public GameObject bulletPrefab;

    public int ammoCount = 10;
    private int tempAmmo;
    public  UIOverlay overlay;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {
        overlay.updateAmmo(ammoCount);
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
            ammoCount -= 1;
            overlay.updateAmmo(ammoCount);
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
        if (collision.gameObject.tag == "enemy")
        {
            SceneManager.LoadScene("Death Screen");
        }

        if (collision.gameObject.tag == "infiniteammo")
        {
            StartCoroutine(AmmoPowerup());
            Destroy(collision.gameObject);
        }
    }

    IEnumerator AmmoPowerup()
    {
        tempAmmo = ammoCount;
        ammoCount = -1;
        overlay.updateAmmo(ammoCount);
        yield return new WaitForSeconds(5);
        overlay.updateAmmo(tempAmmo);
        ammoCount = tempAmmo;
    }
}
