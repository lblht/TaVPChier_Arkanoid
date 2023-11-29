using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    LevelData lastLevel = new LevelData();
    SaveData saveData = new SaveData();

    void Awake() 
    { 
        DontDestroyOnLoad(gameObject);

        if(Instance != null && Instance != this) 
            Destroy(gameObject); 
        else
            Instance = this;   

        if(!File.Exists(Application.dataPath + "/save.json"))
        {
            SetPlayerName("UnknowPlayer");
            Save(saveData);
        }
        else
        {
            saveData = Load();
        }
    }

    public void SetPlayerName(string name)
    {
        saveData.playerName = name;
    }
    public string GetPlayerName()
    {
        return saveData.playerName;
    }

    public void LastLevelStats(int levelID, int score, int stars)
    {
        lastLevel.levelID = levelID;
        lastLevel.score = score;
        lastLevel.stars = stars;

        LevelData savedLevel = LoadLevelProgress(lastLevel.levelID);
        if((savedLevel.score < lastLevel.score) || (savedLevel.score == lastLevel.score && savedLevel.stars < lastLevel.stars))
            SaveLevelProgress(lastLevel);
    }

    public void SaveLevelProgress(LevelData levelData)
    {
        saveData.levelSaves[levelData.levelID-1] = levelData;
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
}
