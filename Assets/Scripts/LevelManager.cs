using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform gridTransform;
    [SerializeField] SceneLoader sceneLoader;

    int numberOfBlocks = 0;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Block.onBlockDestroyed += BlockDestroyed;
        Block.onBlockEnabled += BlockEnabled;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Block.onBlockDestroyed -= BlockDestroyed;
        Block.onBlockEnabled -= BlockEnabled;
    }

    void BlockEnabled()
    {
        numberOfBlocks++;
    }

    void BlockDestroyed()
    {
        numberOfBlocks--;
        Debug.Log(numberOfBlocks);
        if(numberOfBlocks <= 0)
            sceneLoader.LoadScene("WinScreen");
    }
}
