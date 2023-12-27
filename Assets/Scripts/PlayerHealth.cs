using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool IsDead = false;

    private CharacterMovement characterMovement;
    private ZombieSettings zombieSettings;
    private Animator animator;
    public Canvas youLostCanvas;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        currentHealth = maxHealth;
        zombieSettings = Resources.Load<ZombieSettings>("ZombieSettings");
        animator = GetComponent<Animator>();
        youLostCanvas.enabled = false;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            Debug.Log("Player took damage. Current health: " + currentHealth);

            animator.SetTrigger("Hit");
            StartCoroutine(ResetHitTrigger());

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    IEnumerator ResetHitTrigger()
    {
        yield return new WaitForSeconds(1f);

        animator.ResetTrigger("Hit");
    }

    void Die()
    {
        IsDead = true;
        characterMovement.enabled = false;
        animator.SetTrigger("Die");
        Time.timeScale = 0;
        youLostCanvas.enabled = true;
        GameManager.Instance.IncrementLosses();
    }
}
