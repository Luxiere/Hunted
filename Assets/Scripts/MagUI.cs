using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MagUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentMag;
    [SerializeField] TextMeshProUGUI maxMag;
    [SerializeField] Image image;
    [SerializeField] Sprite arrow;
    [SerializeField] Sprite spear;
    [SerializeField] Sprite dagger;

    PlayerShooting player;

    void Start()
    {
        player = FindObjectOfType<PlayerShooting>();
    }

    private void Update()
    {
        currentMag.text = player.RemainingBulletInMag().ToString();
        maxMag.text = player.GetMaxMag().ToString();
        switch (PlayerShooting.weapon)
        {
            case Weapon.Bow:
                image.sprite = arrow;
                break;
            case Weapon.Knives:
                image.sprite = dagger;
                break;
            case Weapon.Spears:
                image.sprite = spear;
                break;
        }
    }

}
