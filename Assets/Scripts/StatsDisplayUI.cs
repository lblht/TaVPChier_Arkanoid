using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsDisplayUI : MonoBehaviour
{
    [SerializeField] int LevelID;
    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] List<Image> starSprites;
    [SerializeField] Color starColor;
    
    void Start()
    {
        LevelData levelData = GameManager.Instance.LoadLevelProgress(LevelID);
        scoreUI.text = levelData.score.ToString();

        for(int i = 0; i < levelData.stars; i++)
        {
            starSprites[i].color = starColor;
        }
    }
}
