using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float WpnDmg = 1f;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {            
            DealDmg(hit);
            // todo add some hit efffect for visual players
        }
        else return;  // if we don't hit anything return to avoid null reference from raycast
    }

    private void DealDmg(RaycastHit hit)
    {
        if (hit.transform.tag == "Enemy")
        {
            hit.transform.GetComponent<EnemyHealth>().TakeDmg(WpnDmg);
        }
    }
}
