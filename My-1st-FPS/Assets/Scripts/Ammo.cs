using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;

    // how much ammo do we have?
    public int GetCurrentAmmo()
    {
        return ammoAmount;
    }

    // reduce ammo when fired
    public void DecreaseAmmo()
    {
        ammoAmount--;
    }

    // increase ammo on pickup
    public void IncreaseAmmo(int pickUpAmmo)
    {
        ammoAmount = ammoAmount + pickUpAmmo;
    }
}
