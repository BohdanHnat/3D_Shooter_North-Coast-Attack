using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public string Name;

    public int Damage;
    public float maxShootDistance;

    public int magSize;
    
    public float fireRate;
    public float reloadTime;

    public string shootSound;
    public string reloadSound;
}
