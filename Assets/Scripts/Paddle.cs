using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    float paddleSpeed = 60;
    float posClamp = 8f - 0.223f - 1.25f;
    float paddleHeight = -4;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float amountToMove = mouseX * paddleSpeed * Time.deltaTime;
        float paddlePosX = transform.position.x + amountToMove;
        transform.position = new Vector3(Mathf.Clamp(paddlePosX, -posClamp, posClamp), paddleHeight, 0);
    }
}
