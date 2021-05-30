using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eyeController : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public GameObject heart6;

    public SceneManager manager;
    public Vector3 startPosition;
    public Vector3[] moveToPoints;
    public Vector3 currentPoint;

    public float moveSpeed;

    public int pointSelection;

    public Transform target;
    public Transform shootPoint;
    public GameObject bullet;
    public float fireRate = 3000f;
    public float shootingPower = 20f;

    public Animator animator;

    private float shootingTime;

    public int health;

    void Start()
    {
        this.transform.position = startPosition;
    }

    private void Update()
    {
        Fire();
        Move();
    }

    private void Fire()
    {
        if (Time.time > shootingTime && Time.time >= 3)
        {
            animator.Play("shoot", 0, 0);
            shootingTime = Time.time + fireRate / 1000;
            Vector2 myPos = new Vector2(shootPoint.position.x, shootPoint.position.y);
            GameObject projectile = Instantiate(bullet, myPos, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            health -= 1;
            if (health == 5)
            {
                heart6.SetActive(false);
            }
            else if (health == 4)
            {
                heart5.SetActive(false);
            }
            else if (health == 3)
            {
                heart4.SetActive(false);
            }
            else if (health == 2)
            {
                heart3.SetActive(false);
            }
            else if (health == 1)
            {
                heart2.SetActive(false);
            }
            if (health <= 0)
            {
                Destroy(gameObject);
                manager.LoadScene("Win Screen");
            }
        }
    }

    void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentPoint, Time.deltaTime * moveSpeed);
        if (this.transform.position == currentPoint)
        {
            pointSelection++;
            if (pointSelection == moveToPoints.Length)
            {
                pointSelection = 0;
            }
            currentPoint = moveToPoints[pointSelection];
        }
    }
}
