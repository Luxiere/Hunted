using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    LevelSystem ls;
    SceneManagement sm;
    GameObject player;
    int enemies;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        sm = GetComponent<SceneManagement>();

    }
    
    void Update()
    {

    }

    public void HandleLoseCondition()
    {
        //load lose screen
    }

    public void HandleWinCondition()
    {
        //load win screen
    }
}
