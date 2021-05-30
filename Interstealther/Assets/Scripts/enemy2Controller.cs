using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2Controller : MonoBehaviour
{
    public float speed;
    bool MoveRight;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float firerate;
    float nextfire;

    void Start()
    {
        nextfire = Time.time;
    }

    void Update()
    {
        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(2, 2);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-2, 2);
        }
        if (Time.time > nextfire)
        {
            if (MoveRight == false)
            {
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                nextfire = Time.time + firerate;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("turn"))
        {
            if (MoveRight)
            {
                MoveRight = false;
            }
            else
            {
                MoveRight = true;
            }
        }
        if (trig.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject);
        }

        if (trig.gameObject.CompareTag("shield"))
        {
            Destroy(gameObject);
        }
    }
}
