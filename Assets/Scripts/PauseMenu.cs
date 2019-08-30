using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject optionMenuUI;

    public static bool Paused = false;

    void Start()
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

    public void Options()
    {
        optionMenuUI.SetActive(true);
    }
    
    public void MainMenu()
    {
        GetComponent<SceneManagement>().LoadScene(0);
    }
}
