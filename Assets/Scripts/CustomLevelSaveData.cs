using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class BlockData
{
    public int blockType;
    public UnityEngine.Vector3 position;
}

[System.Serializable]
public class CustomLevelSaveData
{
    public int background;
    public List<BlockData> blocks = new List<BlockData>();
}