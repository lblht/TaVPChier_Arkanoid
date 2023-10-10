using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform gridTransform;
    [SerializeField] SceneLoader sceneLoader;
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Block.onBlockDestroyed += BlockDestroyed;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Block.onBlockDestroyed -= BlockDestroyed;
    }

    void BlockDestroyed()
    {
        if(gridTransform.childCount <= 1)
            sceneLoader.LoadScene("WinScreen");
    }
}
