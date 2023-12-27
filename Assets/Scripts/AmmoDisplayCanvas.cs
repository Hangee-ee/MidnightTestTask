using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoDisplayCanvas : MonoBehaviour
{
    public AmmoManager handgunAmmoManager;
    public AmmoManager arAmmoManager;

    public TextMeshProUGUI handgunAmmoText;
    public TextMeshProUGUI arAmmoText;

    void Update()
    {
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        handgunAmmoText.text = handgunAmmoManager.GetCurrentAmmo().ToString("000");
        arAmmoText.text = arAmmoManager.GetCurrentAmmo().ToString("000");
    }
}