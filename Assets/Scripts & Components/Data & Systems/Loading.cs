using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private WeaponDataStorage weaponDataStorage;
    private void Awake()
    {
        Context.Initialize(weaponDataStorage);
    }
}
