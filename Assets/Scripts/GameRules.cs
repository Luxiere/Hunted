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


    }
    
    public void EnemyDeath()
    {
        enemies -= 1;
        if (enemies <= 0)
        {

        }
    }

    public void HandleLoseCondition()
    {

    }

    public void HandleWinCondition()
    {
        GetComponent<SceneManagement>().LoadNextScene();
        Destroy(this);
    }
}
