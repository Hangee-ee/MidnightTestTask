using System.Collections;
using UnityEngine;
using Cinemachine;

public class GunController : MonoBehaviour
{
    [SerializeField] public WeaponSettings weaponSettings;
    public Transform barrelTransform;
    public Transform bulletParent;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

    [SerializeField] public AmmoManager ammoManager;
    private bool canShoot = true;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public GameObject muzzleFlashPrefab;
    private AudioSource audioSource;
    [SerializeField] private AudioClip HandgunSFX;
    [SerializeField] private AudioClip AssaultRifleSFX;
    [SerializeField] private AudioSource reloadAudioSource;

    void Start()
    {
        if (cinemachineCamera == null)
        {
            cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isReloading)
        return;

        if (Input.GetButton("Fire1") && canShoot && ammoManager.HasAmmo())
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if ((Input.GetButtonUp("Fire1") || isReloading) && weaponSettings.weaponName == "Assault Rifle")
        {
            StopAssaultRifleSound();
        }
    }
IEnumerator ActivateMuzzleFlash()
{
    GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, barrelTransform.position, barrelTransform.rotation);
    muzzleFlash.transform.parent = barrelTransform;

    muzzleFlash.SetActive(true);

    yield return new WaitForSeconds(0.1f);

    muzzleFlash.SetActive(false);

    Destroy(muzzleFlash, 0.2f);
}

    IEnumerator Shoot()
    {
        canShoot = false;
        StartCoroutine(ActivateMuzzleFlash());

        RaycastHit hit;
        if (cinemachineCamera != null)
        {
            GameObject bullet = GameObject.Instantiate(weaponSettings.bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (Physics.Raycast(cinemachineCamera.transform.position, cinemachineCamera.transform.forward, out hit, Mathf.Infinity))
            {
                bulletController.target = hit.point;
                bulletController.hit = true;

                bulletController.damage = weaponSettings.damage;
            }
            else
            {
                bulletController.target = cinemachineCamera.transform.position + cinemachineCamera.transform.forward * weaponSettings.bulletHitMissDistance;
                bulletController.hit = false;

                bulletController.damage = weaponSettings.damage;
            }

            ammoManager.ConsumeAmmo();

            if (weaponSettings.weaponName == "Handgun")
            {
                audioSource.PlayOneShot(HandgunSFX);
            }
            else if (weaponSettings.weaponName == "Assault Rifle")
            {
                audioSource.PlayOneShot(AssaultRifleSFX);
            }
        }
        yield return new WaitForSeconds(weaponSettings.fireRate);

        canShoot = true;
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime - .25f);
        reloadAudioSource.Play();
        ammoManager.Reload();

        isReloading = false;
    }

    void StopAssaultRifleSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false;
            Debug.Log("Assault Rifle sound stopped");
        }
    }
}