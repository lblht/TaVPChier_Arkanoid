using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class BlockData
{
    public int blockType;
    public Vector2 position;
}

[System.Serializable]
public class CustomLevelSaveData
{
    public int background;
    public List<BlockData> blocks = new List<BlockData>();
}