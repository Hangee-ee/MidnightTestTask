using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private ZombieSettings zombieSettings;
    public WinManager winManager;

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    public bool IsAlive
    {
        get { return currentHealth > 0; }
    }

    void Awake()
    {
        zombieSettings = Resources.Load<ZombieSettings>("ZombieSettings");
        winManager = FindObjectOfType<WinManager>();
    }

    public void Initialize(float startingHealth)
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsAlive)
        {
            currentHealth -= damage;
            Debug.Log($"Zombie took {damage} damage. Remaining health: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
        Destroy(gameObject, 1f);
        winManager.ZombieDied();
    }
}
