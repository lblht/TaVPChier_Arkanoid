using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;
    [SerializeField] GameObject menu;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] LevelManager levelManager;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Unpause();
        }
    }

    public void Pause()
    {
        paused = true;
        menu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Unpause()
    {
        paused = false;
        menu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        levelManager.DoNotAcceptBlockDestroy();
        Unpause();
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        levelManager.DoNotAcceptBlockDestroy();
        Unpause();
        sceneLoader.LoadScene("StartMenu");
    }
}
