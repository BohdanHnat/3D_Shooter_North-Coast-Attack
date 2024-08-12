using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private WeaponSwitch weaponSwitch;
    [SerializeField] private Transform player, camera;
     
    [SerializeField] private float pickUpRange, pickUpTime;
    [SerializeField] private float dropForwardForce, dropUpwardForce;

    [SerializeField] private LayerMask pickUpableMask;

    private static bool slotFull;

    private WeaponShooting gunScript;
    private Rigidbody rb;
    private BoxCollider coll;

    public event Action<GameObject> onPickedUp;
    public event Action onDropped;
    private void Start()
    {
        GetThisItemComponents(weaponSwitch.currentWeapon);

        weaponSwitch.OnWeaponSwitched += OnWeaponSelected;

        if (!gunScript.isEquipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.enabled = true;
        }
        if (gunScript.isEquipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.enabled = false;
            slotFull = true;
        }
    }
    private void Update()
    {
        Collider[] pickUpableInRange = Physics.OverlapSphere(transform.position, pickUpRange);
        
        if (pickUpableInRange != null)
        {
            foreach (var pickUpable in pickUpableInRange)
            {
                if (pickUpable.gameObject.layer == 6)
                {
                    GetThisItemComponents(pickUpable.gameObject);
                    if (!gunScript.isEquipped && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp(pickUpable.gameObject);
                }
            }
        }

        if (gunScript.isEquipped && Input.GetKeyDown(KeyCode.Q)) Drop(weaponSwitch.currentWeapon);
    }

    private void PickUp(GameObject item)
    {
        onPickedUp?.Invoke(item);

        item.tag = "PickedUp";

        gunScript.isEquipped = true;
        slotFull = true;

        item.transform.SetParent(gameObject.transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(Vector3.zero);
        item.transform.localScale = new Vector3(2, 2, 2);

        rb.isKinematic = true;
        coll.enabled = false;

        gunScript.enabled = true;
    }

    private void Drop(GameObject item)
    {
        onDropped?.Invoke();

        StartCoroutine(DropRoutine(item));
    }
    private IEnumerator DropRoutine(GameObject item)
    {
        yield return new WaitForSeconds(1.5f);

        GetThisItemComponents(item);

        item.tag = "Dropped";

        gunScript.isEquipped = false;
        slotFull = false;

        item.transform.SetParent(null);

        rb.isKinematic = false;
        coll.enabled = true;

        rb.AddForce(camera.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(camera.up * dropUpwardForce, ForceMode.Impulse);

        System.Random rand = new System.Random();
        float random = rand.Next(-1, 1);
        rb.AddTorque(new Vector3(random, random, random) * 10);
        gunScript.enabled = false;
    }
    private void GetThisItemComponents(GameObject item)
    {
        gunScript = item.GetComponent<WeaponShooting>();

        rb = item.GetComponent<Rigidbody>();
        if (rb == null) rb = item.AddComponent<Rigidbody>();
        
        coll = item.GetComponent<BoxCollider>();
    }
    private void OnWeaponSelected()
    {
        GetThisItemComponents(weaponSwitch.currentWeapon);

        rb.isKinematic = true;
        coll.enabled = false;
    }
}
