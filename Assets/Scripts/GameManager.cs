using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    LevelData lastLevel = new LevelData();
    SaveData saveData = new SaveData();
    string customLevelToLoad;
    int totalScore;
    int totalStars;

    void Awake() 
    { 
        DontDestroyOnLoad(gameObject);

        if(Instance != null && Instance != this) 
            Destroy(gameObject); 
        else
            Instance = this;   

        if(!File.Exists(Application.dataPath + "/save.json"))
        {
            SetPlayerName("UnknownPlayer");
            Save(saveData);
        }
        else
        {
            saveData = Load();
        }
    }

    public void SetPlayerName(string name)
    {
        if(name == "UnknownPlayer")
        {
            saveData.playerName = name;
        }
        else if(name != saveData.playerName)
        {
            File.Delete(Application.dataPath + "/save.json");
            saveData = new SaveData();
            saveData.playerName = name;
            Save(saveData);
        }
    }
    public string GetPlayerName()
    {
        return saveData.playerName;
    }

    public void SetTotalScore(int score)
    {
        totalScore = score;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public void SetTotalStars(int stars)
    {
        totalStars = stars;
    }

    public int GetTotalStars()
    {
        return totalStars;
    }

    public void LastLevelStats(int levelID, int score, int stars)
    {
        lastLevel.levelID = levelID;
        lastLevel.score = score;
        lastLevel.stars = stars;

        if(levelID >= 0)
        {
            LevelData savedLevel = LoadLevelProgress(lastLevel.levelID);
            if((savedLevel.score < lastLevel.score) || (savedLevel.score == lastLevel.score && savedLevel.stars < lastLevel.stars))
                SaveLevelProgress(lastLevel);
        }
    }

    public void SaveLevelProgress(LevelData levelData)
    {
        saveData.levelSaves[levelData.levelID-1].levelID = levelData.levelID;
        saveData.levelSaves[levelData.levelID-1].score = levelData.score;
        saveData.levelSaves[levelData.levelID-1].stars = levelData.stars;
    }

    public LevelData LoadLevelProgress(int levelID)
    {
        if(levelID == 0)
            return lastLevel;

        LevelData loadedLevelData = saveData.levelSaves[levelID-1];
        if(loadedLevelData == null)
            return new LevelData();
        else
            return loadedLevelData;
    }

    void OnApplicationQuit()
    {
        Save(saveData);
    }

    public void Save(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Application.dataPath + "/save.json", json);
    }

    public SaveData Load()
    {
        string json = File.ReadAllText(Application.dataPath + "/save.json");
        return JsonUtility.FromJson<SaveData>(json);
    }

    public SaveData GetSaveData()
    {
        return saveData;
    }

    public void SetCustomLevelToLoad(string levelName)
    {
        customLevelToLoad = levelName;
    }

    public string GetCustomLevelToLoad()
    {
        return customLevelToLoad;
    }
}
