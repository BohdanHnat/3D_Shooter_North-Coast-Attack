using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private float switchTime;
    [SerializeField] private WeaponPickUp weaponPickUp;
    public Vector2 SwitchKeyValue { get; set; }

    private Transform[] weapons;

    private int selectedWeaponIndex;
    private float timeSinceLastSwitch;

    public event Action OnWeaponSwitched;
    public GameObject currentWeapon;
    private void Awake()
    {
        InitializeWeapons();
        SelectWeapon(selectedWeaponIndex);

        weaponPickUp.onPickedUp += OnWeaponPickedUp;
        timeSinceLastSwitch = 0f;
    }

    private void InitializeWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            weapons[i] = transform.GetChild(i);
    }

    private void Update()
    {
        InitializeWeapons();

        var previousSelectedWeaponIndex = selectedWeaponIndex;

        if (SwitchKeyValue.y == 120f && timeSinceLastSwitch >= switchTime)
        {
            if (selectedWeaponIndex >= transform.childCount - 1) selectedWeaponIndex = 0;
            else selectedWeaponIndex++;
        } else if (SwitchKeyValue.y == -120f && timeSinceLastSwitch >= switchTime)
        {
            if (selectedWeaponIndex <= 0) selectedWeaponIndex = transform.childCount - 1;
            else selectedWeaponIndex--;
        }

        if (previousSelectedWeaponIndex != selectedWeaponIndex &&
            currentWeapon.GetComponent<WeaponShooting>().isReloading == false)
        {
            SelectWeapon(selectedWeaponIndex);
        }

        timeSinceLastSwitch += Time.deltaTime;
    }

    private void SelectWeapon(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }

        currentWeapon = weapons.FirstOrDefault(weapon => weapon.gameObject.activeSelf == true)?.gameObject;

        timeSinceLastSwitch = 0f;
        OnWeaponSelected();
    }

    private void OnWeaponSelected()
    {
        OnWeaponSwitched?.Invoke();
    }
    private void OnWeaponPickedUp(GameObject pickedUpWeapon)
    {
        currentWeapon = pickedUpWeapon;
    }
}
