using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform gridTransform;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject paddlePrefab;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Image livesUI;
    [SerializeField] TextMeshProUGUI scoreUI;
    float paddlePosY = -5;
    GameObject paddle;
    List<GameObject> balls = new List<GameObject>();
    int maxLives = 3;
    int currentLives;
    int numberOfBlocks = 0;
    int score;
    bool doNotAcceptBlockDestroy = false;

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
        UpdateLives(maxLives);
    }

    void AddPaddleAndBall()
    {
        paddle = Instantiate(paddlePrefab, new Vector3(0, paddlePosY, 0), Quaternion.identity);
        balls.Add(Instantiate(ballPrefab, paddle.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity));
        balls[0].transform.parent = paddle.transform;
    }

    void BlockEnabled()
    {
        numberOfBlocks++;
    }

    void BlockDestroyed(int score)
    {
        if(doNotAcceptBlockDestroy)
        {
            return;
        }
        else
        {
            numberOfBlocks--;
            this.score += score;
            UpdateScore();
            if(numberOfBlocks <= 0)
            {
                string levelName = SceneManager.GetActiveScene().name; 
                int levelID = (int)char.GetNumericValue(levelName[levelName.Length - 1]);
                if(levelName == "CustomLevel")
                    levelID = -1;
                GameManager.Instance.LastLevelStats(levelID, this.score, currentLives);
                sceneLoader.LoadScene("WinScreen");
            }
        }
    }

    public void DoNotAcceptBlockDestroy()
    {
        doNotAcceptBlockDestroy = true;
    }

    void BallDestroyed(GameObject ball)
    {
        balls.Remove(ball);
        if(balls.Count <= 0)
        {
            UpdateLives(currentLives-1);
            Destroy(paddle);
            AddPaddleAndBall();
        }
    }

    void UpdateLives(int lives)
    {
        if(lives > 0)
        {
            currentLives = lives;
            livesUI.rectTransform.sizeDelta = new Vector2(currentLives * 100, livesUI.sprite.rect.height);
        }
        else
        {
            doNotAcceptBlockDestroy = true;
            sceneLoader.LoadScene("LoseScreen");
        }
    }

    void UpdateScore()
    {
        scoreUI.text = score.ToString();
    }

}
