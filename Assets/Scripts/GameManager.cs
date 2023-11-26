using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] SaveLoadSystem saveLoadSystem;
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
            saveLoadSystem.Save(saveData);
        }
        else
        {
            saveData = saveLoadSystem.Load();
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
        lastLevel.leveID = levelID;
        lastLevel.score = score;
        lastLevel.stars = stars;

        LevelData savedLevel = LoadLevelProgress(lastLevel.leveID);
        if((savedLevel.score < lastLevel.score) || (savedLevel.score == lastLevel.score && savedLevel.stars < lastLevel.stars))
            SaveLevelProgress(lastLevel);
    }

    public void SaveLevelProgress(LevelData levelData)
    {
        saveData.levelSaves[levelData.leveID-1] = levelData;
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
        saveLoadSystem.Save(saveData);
    }
}
