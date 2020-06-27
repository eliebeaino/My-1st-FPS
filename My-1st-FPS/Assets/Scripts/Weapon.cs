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
    [SerializeField] Ammotype ammotype;
    [SerializeField] AudioClip WpnSFX;
    [SerializeField] AudioSource audioSource;

    [Header("Weapon Stats")]
    [SerializeField] float range = 100f;
    [SerializeField] float WpnDmg = 25f;
    [SerializeField] float fireRate = 0.5f;
    bool canShoot = true;

    private void OnEnable()
    {
        StartCoroutine(ResetWpnCooldown());
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && HaveAmmo() && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    // check if we have enough ammo
    private bool HaveAmmo()
    {
        if (ammoSlot.GetCurrentAmmo(ammotype) > 0)
            return true;
        else
            return false;
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.DecreaseAmmo(ammotype);
        audioSource.PlayOneShot(WpnSFX);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
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

    IEnumerator ResetWpnCooldown()
    {
        if (canShoot)
        {
            yield break;
        }
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
