using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootPoolInitializer : MonoBehaviour
{
    [SerializeField] private WeaponSwitch weaponSwitch;
    [SerializeField] private Transform shotParticlePrefab;
    [SerializeField] private Transform shotFXPrefab;
    private WeaponShooting weaponShooting;

    public ObjectPool<Transform> shotParticlesPool;
    public ObjectPool<Transform> shotFXPool;
    void Start()
    {
        weaponShooting = weaponSwitch.currentWeapon.GetComponent<WeaponShooting>();

        shotParticlesPool = new ObjectPool<Transform>(shotParticlePrefab, weaponShooting.muzzle.position, weaponShooting.muzzle.rotation, 5);
        shotFXPool = new ObjectPool<Transform>(shotFXPrefab, weaponShooting.muzzle.position, weaponShooting.muzzle.rotation, 5);
    }
}
