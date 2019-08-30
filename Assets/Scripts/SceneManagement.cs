using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    }

    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    public bool CheckLastScene()
    {
        return SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
