using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;

    public static bool Paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0;
        Paused = true;
    }

    public void Unpause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        Paused = false;
    }
    
    public void MainMenu()
    {
        GetComponent<SceneManagement>().LoadScene(0);
    }
}
