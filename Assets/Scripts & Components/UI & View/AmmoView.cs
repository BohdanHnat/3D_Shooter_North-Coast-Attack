using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class AmmoView : MonoBehaviour
{
    [SerializeField] private WeaponSwitch weaponSwitch;
    [SerializeField] private WeaponPickUp weaponPickUp;
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI ammoInStockText;
    [SerializeField] private int startingAmmoAmount;

    private GameObject currentWeapon;
    private IPlayerBag playerBag;
    private void Start()
    {
        currentWeapon = weaponSwitch.currentWeapon;
        
        weaponSwitch.OnWeaponSwitched += OnWeaponSwitched;
        weaponPickUp.onPickedUp += OnWeaponPickedUp;

        playerBag = Context.Instance.PlayerBag;
        
        Context.Instance.PlayerBag.OnStartingAmmoSet(startingAmmoAmount);
    }
    private void Update()
    {
        var script = currentWeapon.GetComponent<WeaponShooting>();

        currentAmmoText.text = $"{script.currentAmmo}/{script.gunData?.magSize}";
        ammoInStockText.text = $"{playerBag.ammoInStock}";
    }
    private void OnWeaponSwitched()
    {
        currentWeapon = weaponSwitch.currentWeapon;
    }
    private void OnWeaponPickedUp(GameObject item)
    {
        currentWeapon = weaponSwitch.currentWeapon;
    }
}
