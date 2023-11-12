using System.Collections.Generic;

public class LevelData
{
    public int score;
    public int stars;
}

public class SaveData
{
    public string playerName;
    public List<LevelData> levelSaves = new List<LevelData>(new LevelData[10]);
}