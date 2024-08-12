using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{
    private static Context _instance;
    public static Context Instance => _instance ??= new Context();
    public IDataSystem DataSystem { get; private set; }
    public IPlayerBag PlayerBag { get; private set; }
    public IAudioSystem AudioSystem { get; private set; }
    public bool Initialized { get; private set; }
    private Context()
    {
    }
    public static void Initialize(WeaponDataStorage weaponDataStorage)
    {
        Instance.DataSystem = new DataSystem(weaponDataStorage);
        Instance.PlayerBag = new PlayerBag();
        Instance.AudioSystem = new AudioSystem();

        Instance.Initialized = true;
    }
}
