using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon : MonoBehaviour
{
    public void SetKnives()
    {
        PlayerShooting.weapon = Weapon.Knives;
    }

    public void SetBow()
    {
        PlayerShooting.weapon = Weapon.Bow;
    }

    public void SetSpear()
    {
        PlayerShooting.weapon = Weapon.Spears;
    }
}
