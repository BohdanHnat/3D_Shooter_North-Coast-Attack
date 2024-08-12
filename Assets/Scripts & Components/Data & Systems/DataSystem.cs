using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDataSystem
{
    WeaponData[] weaponDatas { get; }
}
public class DataSystem : IDataSystem
{
    public WeaponData[] weaponDatas { get; }
    public DataSystem(WeaponDataStorage weaponDataStorage)
    {
        weaponDatas = weaponDataStorage.weaponDatas;
    }
}

