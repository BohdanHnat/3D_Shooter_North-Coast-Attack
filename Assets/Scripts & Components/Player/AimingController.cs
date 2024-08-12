using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingController : MonoBehaviour
{
    [SerializeField] private WeaponSwitch weaponSwitch;
    [SerializeField] private float sightOffset;
    [SerializeField] private float aimingInTime;

    private Transform sightPosition;
    public bool isAimingIn { get; set; }
    private void Update()
    {
        sightPosition = weaponSwitch.currentWeapon.transform.Find("AimSightPosition").transform;

        var targetPosition = transform.position;
        
        if (isAimingIn)
        {
            targetPosition = Camera.main.transform.position + (weaponSwitch.currentWeapon.transform.position - sightPosition.position) + (Camera.main.transform.forward * sightOffset);
        }

        if (weaponSwitch.currentWeapon.tag != "Dropped")
        {
            weaponSwitch.currentWeapon.transform.position = Vector3.Lerp(weaponSwitch.currentWeapon.transform.position, targetPosition, aimingInTime);
        }
    }
}
