using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] int ammoAmount = 20;
    [SerializeField] Ammotype ammotype;

    private void OnTriggerEnter(Collider collision)
    {
        collision.GetComponent<Ammo>().IncreaseAmmo(ammotype, ammoAmount);
        Destroy(gameObject);
    }
}
