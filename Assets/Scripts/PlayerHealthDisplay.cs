using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthDisplay : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI healthText;

    void Update()
    {
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        if (playerHealth != null && healthText != null)
        {
            healthText.text = playerHealth.currentHealth.ToString("000") + "/" + playerHealth.maxHealth.ToString("000");
        }
    }
}