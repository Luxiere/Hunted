using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] door;

    LevelSystem ls;
    SceneManagement sm;
    PlayerMovement player;
    int enemies;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        sm = GetComponent<SceneManagement>();
    }
    
    public void EnemyDeath()
    {
        enemies -= 1;
        if (enemies <= 0)
        {
            HandleWinCondition();
        }
    }

    public void HandleLoseCondition()
    {

    }

    public void HandleWinCondition()
    {
        Debug.Log("yay");
        if (!sm.CheckLastScene())
        {
            GetComponent<SceneManagement>().LoadNextScene();
            foreach (GameObject i in door)
            {
                i.SetActive(false);
            }
            Camera.main.transform.position = door[0].transform.position;
            StartCoroutine(ResetCamera());
        }
    }

    private IEnumerator ResetCamera()
    {
        yield return new WaitForSeconds(1f);
        Camera.main.transform.position = player.transform.position;
        Destroy(this);
    }


}
