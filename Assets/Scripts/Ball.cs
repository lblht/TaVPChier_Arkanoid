using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] AudioSource boingSound;
    float speed = 10;
    Vector2 moveDir;
    Vector3 lastFramePos;
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
        boingSound.Play();
        moveDir = rb.velocity.normalized;

        if(collision.gameObject.tag == "Paddle")
        {
            float minAngle = 0;
            float maxAngle = 80;
            float paddleWidth = collision.gameObject.GetComponent<Paddle>().GetPaddleWidth();
            float paddlePosX = collision.gameObject.transform.position.x;
            float distance = Mathf.Abs(transform.position.x - paddlePosX);
            float t = distance / paddleWidth;
            float finalAngle = Mathf.LerpAngle(minAngle, maxAngle, t) * -Mathf.Sign(transform.position.x - paddlePosX);
            Quaternion finalRotation = Quaternion.AngleAxis(finalAngle, Vector3.forward);
            moveDir = finalRotation * Vector3.up;
            rb.velocity = moveDir.normalized * speed;
        }
        else if(Vector3.Angle(moveDir, Vector3.Reflect(moveDir, collision.contacts[0].normal)) > 160 )
        {
            Quaternion finalRotation = Quaternion.AngleAxis(160, moveDir);
            moveDir = finalRotation * collision.contacts[0].normal;
            rb.velocity = moveDir.normalized * speed;
        }
    }
}