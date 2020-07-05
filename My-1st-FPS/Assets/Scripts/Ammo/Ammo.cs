using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public Ammotype ammotype;
        public int ammoAmount;
    }

    //how much corresponding ammo do we have?
    public int GetCurrentAmmo(Ammotype ammotype)
    {
        return GetAmmotSlot(ammotype).ammoAmount;
    }

    // reduce corresponding ammo when fired
    public void DecreaseAmmo(Ammotype ammotype)
    {
        GetAmmotSlot(ammotype).ammoAmount--;
    }

    // increase corresponding ammo on pickup
    public void IncreaseAmmo(Ammotype ammotype, int pickUpAmmo)
    {
        GetAmmotSlot(ammotype).ammoAmount = GetAmmotSlot(ammotype).ammoAmount + pickUpAmmo;
    }

    // check which weapon we're using and get the ammo type from it
    private AmmoSlot GetAmmotSlot(Ammotype ammotype)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammotype == ammotype)
            {
                return slot;
            }  
        }
        return null;
    }

    // updates the current corresponding ammo amount to UI
    public int GetAmmoUI(Ammotype ammotype)
    {
        return GetAmmotSlot(ammotype).ammoAmount;
    }
}
