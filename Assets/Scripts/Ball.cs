using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float speed = 10;
    Vector2 moveDir;

    public delegate void OnBallDestroyed(GameObject ball);
    public static event OnBallDestroyed onBallDestroyed;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && transform.parent != null)
        {
            transform.parent = null;
            moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 0.8f));
            rb.velocity = moveDir.normalized * speed;
        }
    }

    void FixedUpdate()
    {
        if(transform.position.y <= -7)
        {
            if(onBallDestroyed != null)
                onBallDestroyed(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Paddle")
            moveDir = -(collision.gameObject.transform.position - transform.position);
        else
            moveDir = Vector3.Reflect(moveDir, collision.contacts[0].normal);

        rb.velocity = moveDir.normalized * speed;
    }
}