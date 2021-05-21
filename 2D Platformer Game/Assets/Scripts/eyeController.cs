using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eyeController : MonoBehaviour
{
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

    public int health = 100;

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
            health -= 5;
            if (health <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("Win Screen");
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
