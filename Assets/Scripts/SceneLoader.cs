using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator animator;
    AsyncOperation asyncLoad;

    void Awake()
    {
        animator.Play("TransitionOut");
    }
    public void LoadScene(string sceneName)
    {
        animator.Play("TransitionIn");
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
    }

    public void SceneLoadAnimDone()
    {
        asyncLoad.allowSceneActivation = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
