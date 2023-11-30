using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] LevelDataScriptableObject levelData;
    [SerializeField] Grid grid;
    [SerializeField] GameObject placementHighlitePrefab;
    [SerializeField] SpriteRenderer backgroundImage;
    [SerializeField] TextMeshProUGUI backgroundText;
    [SerializeField] TMP_InputField levelNameInput;
    [SerializeField] SceneLoader sceneLoader;

    GameObject placementHighlite;
    Vector3 mouseGridPos;
    int selectedBlockId = 1;
    CustomLevelSaveData saveData = new CustomLevelSaveData();
    int background = 0;
  
    void Start()
    {
        placementHighlite = Instantiate(placementHighlitePrefab, Input.mousePosition, Quaternion.identity);
        ChangeBackground(0);
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 mouseWorldPos  = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int cellPosition = grid.WorldToCell(mouseWorldPos);
        mouseGridPos = grid.GetCellCenterWorld(cellPosition);
        mouseGridPos = new Vector3(mouseGridPos.x - 0.025f, mouseGridPos.y - 0.025f, 0f);

        if(cellPosition.x >= -5 && cellPosition.x <= 4 && cellPosition.y >= -3 && cellPosition.y <= 6)
        {
            placementHighlite.SetActive(true);
            placementHighlite.transform.position = mouseGridPos;

            if(Input.GetMouseButtonDown(0))
                PlaceBlock();

            if(Input.GetMouseButtonDown(1))
                DeleteBlock();
        }
        else
        {
            placementHighlite.SetActive(false);
        }
    }
    
    void PlaceBlock()
    {
        foreach(Transform existingBlock in grid.gameObject.transform)
        {
            if(existingBlock.position == mouseGridPos)
            {
                Destroy(existingBlock.gameObject);
                break;
            }
        }

        GameObject block = Instantiate(levelData.GetBlocks()[selectedBlockId], mouseGridPos, Quaternion.identity);
        block.transform.parent = grid.gameObject.transform;
    }

    void DeleteBlock()
    {
        foreach(Transform existingBlock in grid.gameObject.transform)
        {
            if(existingBlock.position == mouseGridPos)
            {
                Destroy(existingBlock.gameObject);
                return;
            }
        }
    }

    public void SelectBlock(int id)
    {
        selectedBlockId = id;
    }

    public void ChangeBackground(int dir)
    {
        background += dir;
        if(background < 0)
            background = levelData.GetBackgrounds().Count -1;
        if(background > levelData.GetBackgrounds().Count -1)
            background = 0;
        
        backgroundImage.sprite = levelData.GetBackgrounds()[background];
        backgroundText.text = "Background " + background.ToString();
    }

    public void Save()
    {
        if(!Directory.Exists(Application.dataPath + "/CustomLevels"))
            Directory.CreateDirectory(Application.dataPath + "/CustomLevels");

        string levelName = levelNameInput.text;
        if(levelName == "")
            return;

        foreach(Transform existingBlock in grid.gameObject.transform)
        {
            BlockData block = new BlockData();
            if(existingBlock.GetComponent<Block>() == null)
                block.blockType = 0;
            else
                block.blockType = existingBlock.gameObject.GetComponent<Block>().GetBlockID();
            block.position = existingBlock.position;
            saveData.blocks.Add(block);     
        }
        saveData.background = background;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Application.dataPath + "/CustomLevels/" + levelName + ".json", json);

        sceneLoader.LoadScene("CustomLevels");
    }
}
