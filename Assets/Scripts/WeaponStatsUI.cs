using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponStatsUI : MonoBehaviour
{
    public TMP_Text damageText;
    public TMP_Text fireRateText;
    public TMP_Text ammoText;

    public WeaponSettings weaponSettings;

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (weaponSettings != null)
        {
            damageText.text = "Damage: " + weaponSettings.damage.ToString();
            fireRateText.text = "Fire Rate: " + weaponSettings.fireRate.ToString();
            ammoText.text = "Ammo: " + weaponSettings.ammoCapacity.ToString();
        }
        else
        {
            Debug.LogWarning("WeaponSettings not assigned to WeaponStatsUI!");
        }
    }
}
