using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float WpnDmg = 1f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitSpark;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
    }

    // play the muzzle flash vfx when shooting
    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    // toggle raycast and check if it hits enemy to deal the damage.
    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDmg(WpnDmg);
            }
        }
        else return;  // if we don't hit anything return to avoid null reference from raycast
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject hitVFX = Instantiate(hitSpark, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitVFX, 0.1f);
    }
}
