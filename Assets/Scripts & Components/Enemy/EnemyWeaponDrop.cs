using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponDrop : MonoBehaviour
{
    [SerializeField] private Transform enemyVision;
    [SerializeField] private float dropForwardForce;
    [SerializeField] private float dropUpwardForce;
    [SerializeField] private HealthComponent healthComponent;

    private EnemyWeaponShooting enemyWeaponShooting;
    private BoxCollider boxCollider;
    private void Start()
    {
        healthComponent.OnDie += WeaponDrop;
        enemyWeaponShooting = GetComponent<EnemyWeaponShooting>();

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }
    private void WeaponDrop()
    {
        transform.tag = "Dropped";

        transform.SetParent(null);
        
        boxCollider.enabled = true;
        boxCollider.isTrigger = false;

        enemyWeaponShooting.enabled = false;
    }
}
