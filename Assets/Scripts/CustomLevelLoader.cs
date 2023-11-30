using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

public class CustomLevelLoader : MonoBehaviour
{
    [SerializeField] LevelDataScriptableObject levelData;
    [SerializeField] SpriteRenderer backgroundImage;
    [SerializeField] Grid grid;
    CustomLevelSaveData saveData = new CustomLevelSaveData();
    void Start()
    {
        string levelName = GameManager.Instance.GetCustomLevelToLoad();
        string json = File.ReadAllText(Application.dataPath + "/CustomLevels/" + levelName + ".json");
        saveData = JsonUtility.FromJson<CustomLevelSaveData>(json);

        backgroundImage.sprite = levelData.GetBackgrounds()[saveData.background];
        foreach(BlockData block in saveData.blocks)
        {
            GameObject newBlock = Instantiate(levelData.GetBlocks()[block.blockType], block.position, Quaternion.identity);
            newBlock.transform.parent = grid.gameObject.transform;
        }
    }
}
