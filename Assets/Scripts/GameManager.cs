using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] SaveLoadSystem saveLoadSystem;

    string playerName;

    void Awake() 
    { 
        DontDestroyOnLoad(gameObject);

        if(Instance != null && Instance != this) 
            Destroy(gameObject); 
        else
            Instance = this;   

        if(!File.Exists(Application.dataPath + "/save.json"))
            SetPlayerName("UnknowPlayer");
        else
            LoadPlayerName();
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
        saveLoadSystem.SavePlayerName(playerName);
    }
    public string GetPlayerName()
    {
        return playerName;
    }

    string LoadPlayerName()
    {
        return playerName = saveLoadSystem.LoadPlayerName();
    }
}
