using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] List<GameObject> blocks = new List<GameObject>();
    [SerializeField] Grid grid;
    [SerializeField] GameObject placementHighlitePrefab;

    GameObject placementHighlite;
    Vector3 mouseGridPos;
    int selectedBlockId = 1;
    CustomLevelSaveData saveData;
    string levelName;
    void Start()
    {
        placementHighlite = Instantiate(placementHighlitePrefab, Input.mousePosition, Quaternion.identity);
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

        GameObject block = Instantiate(blocks[selectedBlockId], mouseGridPos, Quaternion.identity);
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

    public void Save()
    {
        if(!Directory.Exists(Application.dataPath + "/CustomLevels"))
            Directory.CreateDirectory(Application.dataPath + "/CustomLevels");

        /*TODO -- populate saveData*/

        //string json = JsonUtility.ToJson(saveData, true);
        //File.WriteAllText(Application.dataPath + "/CustomLevels/" + levelName + ".json", json);
    }

    /*public CustomLevelSaveData Load(string levelName)
    {
        if(!Directory.Exists(Application.dataPath + "/CustomLevels"))
            Directory.CreateDirectory(Application.dataPath + "/CustomLevels");

        string json = File.ReadAllText(Application.dataPath + "/CustomLevels/" + levelName + ".json");
        return JsonUtility.FromJson<CustomLevelSaveData>(json);
    }*/
}
