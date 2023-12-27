using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject handgun;
    public GameObject assaultRifle;

    void Start()
    {
        SwitchToHandgun();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToHandgun();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToAssaultRifle();
        }
    }

    public void SwitchToHandgun()
    {
        handgun.SetActive(true);
        assaultRifle.SetActive(false);
    }

    public void SwitchToAssaultRifle()
    {
        handgun.SetActive(false);
        assaultRifle.SetActive(true);
    }

}
