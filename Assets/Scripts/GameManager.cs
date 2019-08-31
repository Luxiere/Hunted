using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] door;
    [SerializeField] GameObject UI;

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    public AudioSource chill;
    public AudioSource fight;

    LevelSystem ls;
    SceneManagement sm;

    [HideInInspector]
    public PlayerMovement player;

    int enemies;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        sm = GetComponent<SceneManagement>();
        chill.volume = PlayerPrefsController.GetMusicVolume();
        fight.volume = 0;
    }

    public void EnemyDeath()
    {
        enemies -= 1;
        if (enemies <= 0)
        {
            MusicMaster.FadeIn(chill, fight, .5f);
        }
    }

    public void AddEnemy(int enemies)
    {
        this.enemies += enemies;
    }

    public void HandleLoseCondition()
    {
        loseScreen.SetActive(true);
    }

    public void HandleWinCondition()
    {
        winScreen.SetActive(true);
        MenuManager.won = true;
    }

    private IEnumerator ResetCamera()
    {
        yield return new WaitForSeconds(1f);
        Camera.main.transform.position = player.transform.position;
        Destroy(this);
    }
}
