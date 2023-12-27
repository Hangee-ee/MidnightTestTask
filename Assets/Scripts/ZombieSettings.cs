using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieSettings", menuName = "ScriptableObjects/ZombieSettings", order = 1)]
public class ZombieSettings : ScriptableObject
{
    public float detectionRadius = 10f;
    public float chaseSpeed = 3f;
    public float attackRange = 2f;
    public float softDetectionRadius = 15f;
    public float timeToLosePlayer = 5f;
    public int attackDamage = 10;
    public float maxHealth = 100f;

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public int AttackDamage
    {
        get { return attackDamage; }
    }
}
