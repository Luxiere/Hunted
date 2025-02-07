﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] GameObject[] chapters;
    [SerializeField] GameObject[] otherUI;

    int currentPickedup = 0;
    PauseMenu pause;
    GameManager gm;

    private IEnumerator Start()
    {
        pause = GetComponent<PauseMenu>();
        gm = GetComponent<GameManager>();
        yield return new WaitForSeconds(.25f);
        PickedUp();
    }

    public void PickedUp()
    {
        chapters[currentPickedup].SetActive(true);
        gm.player.GetComponent<PlayerShooting>().UIOnOff(true);
        foreach(GameObject UI in otherUI)
        {
            UI.SetActive(false);
        }
        Time.timeScale = 0;
    }

    public void Back()
    {
        chapters[currentPickedup].SetActive(false);
        gm.player.GetComponent<PlayerShooting>().UIOnOff(false);
        foreach (GameObject UI in otherUI)
        {
            UI.SetActive(true);
        }
        Time.timeScale = 1;
        if (currentPickedup >= chapters.Length - 1)
        {
            gm.HandleWinCondition();
        }
        else
        {
            currentPickedup += 1;
        }
    }
}
