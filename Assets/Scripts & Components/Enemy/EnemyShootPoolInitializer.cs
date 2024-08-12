using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootPoolInitializer : MonoBehaviour
{
    [SerializeField] private Transform shotParticlePrefab;
    [SerializeField] private Transform shotFXPrefab;
    private EnemyWeaponShooting weaponShooting;

    public ObjectPool<Transform> shotParticlesPool;
    public ObjectPool<Transform> shotFXPool;
    void Start()
    {
        weaponShooting = transform.GetComponentInChildren<EnemyWeaponShooting>();

        shotParticlesPool = new ObjectPool<Transform>(shotParticlePrefab, weaponShooting.muzzle.position, weaponShooting.muzzle.rotation, 2);
        shotFXPool = new ObjectPool<Transform>(shotFXPrefab, weaponShooting.muzzle.position, weaponShooting.muzzle.rotation, 2);
    }
}
