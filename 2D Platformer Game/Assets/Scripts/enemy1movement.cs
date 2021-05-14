using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1movement : MonoBehaviour
{
    public float speed;
    bool MoveRight;

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
