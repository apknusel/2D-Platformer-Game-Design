using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public Transform firePoint;
    public Transform crouchFirePoint;
    public GameObject bulletPrefab;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

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

        if (Input.GetButtonDown("Fire1"))
        {
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
}
