using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class CustomLevelListLoader : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject canvas;
    [SerializeField] SceneLoader sceneLoader;
    void Start()
    {
        if(!Directory.Exists(Application.dataPath + "/CustomLevels"))
            return;
        
        string[] files = Directory.GetFiles(Application.dataPath + "/CustomLevels");

        int offsetY = 0;
        foreach(string file in files)
        {
            string fileName = Path.GetFileName(file);
            fileName = Path.GetFileNameWithoutExtension(fileName);
            GameObject button = Instantiate(buttonPrefab, transform.position, Quaternion.identity);
            button.transform.parent = canvas.transform;
            button.transform.localPosition = new Vector3(-300, 300 - offsetY, 0);
            button.GetComponent<Button>().onClick.AddListener(() => LoadCustomLevel(fileName));
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = fileName;

            offsetY += 100;
        }
    }

    public void LoadCustomLevel(string levelName)
    {
        GameManager.Instance.SetCustomLevelToLoad(levelName);
        sceneLoader.LoadScene("CustomLevel");
    }

}
