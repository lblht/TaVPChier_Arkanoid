using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
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

    /*public void SavePlayerName(string playerName)
    {
        saveData.playerName = playerName;
        Save();
    }

    public string LoadPlayerName()
    {
        return Load().playerName;
    }

    public void SaveLevel(int leveID, LevelData levelData)
    {
        saveData.levelSaves[leveID] = levelData;
        Save();
    }

    public LevelData LoadLevel(int levelID)
    {
        return Load().levelSaves[levelID];
    }*/
}
