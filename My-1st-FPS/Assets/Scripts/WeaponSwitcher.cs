﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        SetWeaponActive();
    }

    void Update()
    {
        int previousWeapon = currentWeapon;
        ProcessInput();
        ProcessMouseWheel();
        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    // Enables current weapon and disables the others
    private void SetWeaponActive()
    {
        int weaponIndex = 0;
        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentWeapon = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) currentWeapon = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) currentWeapon = 2;
    }

    private void ProcessMouseWheel()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            if (currentWeapon >= transform.childCount - 2)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount -2;
            }
            else
            {
                currentWeapon--;
            }
        }
    }
}
