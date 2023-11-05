using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake() 
    { 
        DontDestroyOnLoad(gameObject);

        if(Instance != null && Instance != this) 
            Destroy(gameObject); 
        else
            Instance = this; 
    }
}
