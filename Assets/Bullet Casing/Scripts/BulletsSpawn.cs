using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class BulletsSpawn: MonoBehaviour
{
    [SerializeField] private WeaponSwitch weaponSwitch;
    [SerializeField] private Transform Cardridge;

    private WeaponShooting weaponShooting;
    private Transform spawnPosition;

    private ObjectPool<Transform> bulletsPool;
    private void Start()
    {
        OnWeaponSwitched();
        bulletsPool = new ObjectPool<Transform>(Cardridge, spawnPosition.position, spawnPosition.rotation, 10);
        weaponSwitch.OnWeaponSwitched += OnWeaponSwitched;
    }
    private void SpawnBullet()
    {
        var instance = bulletsPool.GetObjectFromPool(spawnPosition.position, spawnPosition.rotation);
        
        var rb = instance.GetComponent<Rigidbody>();
        if (rb == null) rb = instance.AddComponent<Rigidbody>();

        instance.GetComponent<BoxCollider>().enabled = true; 

        rb.AddForce(transform.right * 2f, ForceMode.Impulse);
        rb.AddForce(transform.up * 2f, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
    }
    private void OnWeaponSwitched()
    {
        weaponShooting = weaponSwitch.currentWeapon.GetComponent<WeaponShooting>();
        weaponShooting.onShot += SpawnBullet;
        spawnPosition = weaponSwitch.currentWeapon.transform.Find("BulletsSpawn");
    }
}

