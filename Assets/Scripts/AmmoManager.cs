using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public WeaponSettings weaponSettings;
    private int currentAmmo;

    void Start()
    {
        currentAmmo = weaponSettings.ammoCapacity;
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }

    public void ConsumeAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            Debug.Log("Ammo remaining: " + currentAmmo);
        }
    }

    public void Reload()
    {
        currentAmmo = weaponSettings.ammoCapacity;
        Debug.Log("Ammo reloaded. Total ammo: " + currentAmmo);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}
