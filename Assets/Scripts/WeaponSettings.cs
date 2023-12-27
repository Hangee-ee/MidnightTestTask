using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSettings", menuName = "ScriptableObjects/WeaponSettings", order = 1)]
public class WeaponSettings : ScriptableObject
{
    public string weaponName;
    public float damage = 10f;
    public float range = 100f;
    public GameObject bulletPrefab;
    public float bulletHitMissDistance = 25f;
    public float fireRate = 0.2f;
    public int ammoCapacity = 30;
}