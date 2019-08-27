using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelSystem))]
[RequireComponent(typeof(SceneManagement))]
public class GameRules : MonoBehaviour
{
    LevelSystem ls;
    SceneManagement sm;
    GameObject player;
    GameObject[] enemies;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ls = GetComponent<LevelSystem>();
        sm = GetComponent<SceneManagement>();
        sm.OnNewScene(() =>
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerLive>().OnDead(() =>
            {
                Lose();
            });
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        });
    }
    
    void Update()
    {
        foreach (GameObject e in enemies)
            if (e != null)
                return;
        Win();
    }

    void Lose()
    {
        sm.ReloadScene();
    }

    void Win()
    {
        ls.LoadNext();
    }
}
