using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform gridTransform;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject paddlePrefab;
    [SerializeField] GameObject ballPrefab;
    
    GameObject paddle;
    List<GameObject> balls = new List<GameObject>();

    int numberOfBlocks = 0;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Block.onBlockDestroyed += BlockDestroyed;
        Block.onBlockEnabled += BlockEnabled;
        Ball.onBallDestroyed += BallDestroyed;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Block.onBlockDestroyed -= BlockDestroyed;
        Block.onBlockEnabled -= BlockEnabled;
        Ball.onBallDestroyed -= BallDestroyed;
    }

    void Start()
    {
        AddPaddleAndBall();
    }

    void AddPaddleAndBall()
    {
        paddle = Instantiate(paddlePrefab, new Vector3(0, -4, 0), Quaternion.identity);
        balls.Add(Instantiate(ballPrefab, paddle.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity));
        balls[0].transform.parent = paddle.transform;
    }

    void BlockEnabled()
    {
        numberOfBlocks++;
    }

    void BlockDestroyed()
    {
        numberOfBlocks--;
        if(numberOfBlocks <= 0)
            sceneLoader.LoadScene("WinScreen");
    }

    void BallDestroyed(GameObject ball)
    {
        balls.Remove(ball);
        if(balls.Count <= 0)
        {
            Destroy(paddle);
            AddPaddleAndBall();
        }
    }
}
