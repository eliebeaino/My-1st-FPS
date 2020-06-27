using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Import Assets")]
    [SerializeField] Camera FPCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitSpark;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AudioClip WpnSFX;
    [SerializeField] AudioSource audioSource;

    [Header("Weapon Stats")]
    [SerializeField] float range = 100f;
    [SerializeField] float WpnDmg = 25f;
    [SerializeField] float InitFireRate = 0.5f;
    float fireRate = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && CanWeShoot() && FireRate())
        {
            Shoot();
        }
        else if (Input.GetMouseButtonDown(0) && CanWeShoot() && FireRate())
        {
            Shoot();
        }
        else if (fireRate > 0)
        { 
            fireRate -= Time.deltaTime;
        }
    }

    private bool FireRate()
    {
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
            return false;
        }
        else
        {
            fireRate = InitFireRate;
            return true;
        }
    }

    // check we can shoot
    private bool CanWeShoot()
    {
        if (ammoSlot.GetCurrentAmmo() > 0)
            return true;
        else 
            return false;
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.DecreaseAmmo();
        audioSource.PlayOneShot(WpnSFX);
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
            if (hit.transform.GetComponent<Enemy>())
            {
                hit.transform.GetComponent<Enemy>().TakeDmg(WpnDmg);
            }
        }
        else return;  // if we don't hit anything return to avoid null reference from raycast
    }

    // special effect on impact of the hit point
    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject hitVFX = Instantiate(hitSpark, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitVFX, 0.1f);
    }
}
