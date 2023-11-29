using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public int levelID;
    public int score;
    public int stars;
}

[System.Serializable]
public class SaveData
{
    public string playerName;
    public List<LevelData> levelSaves = new List<LevelData>(new LevelData[10]);
}