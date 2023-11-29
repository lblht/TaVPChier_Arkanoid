using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TotalScoreCalculator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalScoreUI;
    [SerializeField] TextMeshProUGUI totalStarsUI;
    
    void Start()
    {
        SaveData saveData = GameManager.Instance.GetSaveData();
        int totalScore = 0;
        int totalStars = 0;

        foreach(LevelData levelData in saveData.levelSaves)
        {
            totalScore += levelData.score;
            totalStars += levelData.stars;
        }

        totalScoreUI.text = totalScore.ToString();
        totalStarsUI.text = totalStars.ToString();
    }
}
