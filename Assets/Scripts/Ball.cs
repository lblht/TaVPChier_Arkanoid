using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float speed = 7;

    void Start()
    {
        rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
    }
}






/*
Vector2 moveDir;

    void Start()
    {
        moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = moveDir * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        moveDir = Vector3.Reflect(moveDir, collision.contacts[0].normal);
    }
*/