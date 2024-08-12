using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimating : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private WeaponSwitch weaponSwitch;
    [SerializeField] private WeaponPickUp weaponPickUp;
    public bool isShooting { get; set; }
    public bool isReloading { get; set; }
    public bool isAiming_In_Out { get; set; }

    private float reloadTime;
    private WeaponShooting shooting;
    private bool isInAimState;
    private void Start()
    {
        weaponSwitch.OnWeaponSwitched += onWeaponSwitched;
        weaponPickUp.onDropped += PlayDropAnim;

        onWeaponSwitched();

        isInAimState = false;
    }
    private void Update()
    {
        if (isAiming_In_Out) 
        {
            if(!isInAimState) StartCoroutine(AimingInRoutine());
            else StartCoroutine(AimingOutRoutine());
        } 
    }
    private void PlayShootAnim()
    {
        StartCoroutine(ShootRoutine());
    }
    private void PlayReloadAnim()
    {
        StartCoroutine(ReloadRoutine());
    }
    private void PlayDropAnim()
    {
        StartCoroutine(DropRoutine());
    }
    private void onWeaponSwitched()
    {
        shooting = transform.GetComponent<WeaponSwitch>().currentWeapon.GetComponent<WeaponShooting>();

        shooting.onShot += PlayShootAnim;
        shooting.onReload += PlayReloadAnim;

        if (shooting.gunData != null) reloadTime = shooting.gunData.reloadTime;
    }
    private IEnumerator ShootRoutine()
    {
        animator.SetBool("IsShooting", true);
        yield return new WaitForSeconds(0f);
        animator.SetBool("IsShooting", false);
    }
    private IEnumerator ReloadRoutine()
    {
        animator.SetBool("IsReloading", true);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("IsReloading", false);
    }
    private IEnumerator AimingInRoutine()
    {
        animator.SetBool("IsAimingIn", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsAimingIn", false);
        isInAimState = true;
    }
    private IEnumerator AimingOutRoutine()
    {
        animator.SetBool("IsAimingOut", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsAimingOut", false);
        isInAimState = false;
    }
    private IEnumerator DropRoutine()
    {
        animator.SetBool("IsDropped", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsDropped", false);
    }
}
