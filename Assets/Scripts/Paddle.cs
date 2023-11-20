using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    float paddleSpeed = 60;
    float posClamp = 8f - 0.223f - 1.25f;
    float paddleWidth = 2.5f;
    [SerializeField] bool autoPlayer = false;

    void Update()
    {
        float mouseX;
        if(!autoPlayer)
        {
            mouseX = Input.GetAxis("Mouse X");
        }
        else
        {
            Transform ball = FindObjectOfType<Ball>().transform;
            if(ball.position.x > transform.position.x + 0.4f)
                mouseX = 0.2f;
            else if(ball.position.x < transform.position.x - 0.4f)
                mouseX = -0.2f;
            else 
                mouseX = 0;
        }
        float amountToMove = mouseX * paddleSpeed * Time.deltaTime;
        float paddlePosX = transform.position.x + amountToMove;
        transform.position = new Vector3(Mathf.Clamp(paddlePosX, -posClamp, posClamp), transform.position.y, 0);
    }

    public float GetPaddleWidth()
    {
        return paddleWidth;
    }
}
