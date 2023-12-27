using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieSettings zombieSettings;

    private Transform player;
    private Animator animator;
    private bool isChasing = false;
    private float timeSincePlayerLeftSoftRange = 0f;
    private ZombieHealth zombieHealth;
    private float timeSinceLastAttack = 0f;
    public float attackCooldown = 2f;

    private Rigidbody zombieRigidbody;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        zombieHealth = GetComponent<ZombieHealth>();
        zombieHealth.Initialize(zombieSettings.maxHealth);

        zombieRigidbody = GetComponent<Rigidbody>();
        if (zombieRigidbody == null)
        {
            zombieRigidbody = gameObject.AddComponent<Rigidbody>();
        }

        zombieRigidbody.useGravity = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= zombieSettings.detectionRadius)
        {
            isChasing = true;
            timeSincePlayerLeftSoftRange = 0f;
        }

        if (distanceToPlayer > zombieSettings.attackRange && distanceToPlayer > zombieSettings.softDetectionRadius)
        {
            timeSincePlayerLeftSoftRange += Time.deltaTime;

            if (timeSincePlayerLeftSoftRange >= zombieSettings.timeToLosePlayer)
            {
                isChasing = false;
            }
        }

        if (isChasing)
        {
            ChasePlayer();

            if (distanceToPlayer <= zombieSettings.attackRange)
            {
                StopChasing();
                AttackPlayer();
            }
        }
        else
        {
            StopChasing();
        }
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= zombieSettings.attackRange)
        {
            StopMoving();

            Debug.Log("Zombie attacked player!");

            animator.SetTrigger("Attack");
        }
        else
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            zombieRigidbody.velocity = direction * zombieSettings.chaseSpeed;

            animator.SetBool("Chasing", true);
        }
    }

    void StopMoving()
    {
        zombieRigidbody.velocity = Vector3.zero;

        animator.SetBool("Chasing", false);
    }

    void StopChasing()
    {
        animator.SetBool("Chasing", false);
        isChasing = false;
    }

    void AttackPlayer()
    {
        Debug.Log("Zombie attacked player!");

        animator.SetTrigger("Attack");

        if (Time.time - timeSinceLastAttack >= attackCooldown)
        {
            Debug.Log("Zombie attacked player!");

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(zombieSettings.attackDamage);
                Debug.Log("Player health: " + playerHealth.currentHealth);
            }

            timeSinceLastAttack = Time.time;
        }
    }
}