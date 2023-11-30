using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelDataScriptableObject", order = 1)]
public class LevelDataScriptableObject : ScriptableObject
{
    [SerializeField] List<GameObject> blocks = new List<GameObject>();
    [SerializeField] List<Sprite> backgrounds = new List<Sprite>();

    public List<GameObject> GetBlocks()
    {
        return blocks;
    }

    public List<Sprite> GetBackgrounds()
    {
        return backgrounds;
    }
}

