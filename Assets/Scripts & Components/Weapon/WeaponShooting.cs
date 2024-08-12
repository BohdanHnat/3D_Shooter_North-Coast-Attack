using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem.HID;

public class WeaponShooting : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private string weaponDataKey;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask targetMask;
    
    public bool isEquipped;
    public WeaponData gunData { get; private set; } 

    private float timeSinceLastShot;
    public bool isReloading { get; private set; }
    public float currentAmmo { get; private set; }

    public event Action onShot;
    public event Action onReload;
    public Transform muzzle { get; private set; }

    private ObjectPool<Transform> shotParticlesPool;
    private ObjectPool<Transform> shotFXPool;

    private int ammoInStock;
    private GameObject bloodSplash;
    private void Awake()
    {
        muzzle = transform.Find("Muzzle");
    }
    private void Start() {
        gunData = Context.Instance.DataSystem.weaponDatas.FirstOrDefault(data => data.Name == weaponDataKey);
        currentAmmo = gunData.magSize;

        playerShooting.onShoot += Shoot;
        playerShooting.onReload += StartReload;

        bloodSplash = transform.parent.GetComponent<ParticleHandler>().bloodSplash;
    }
    private void OnDisable() => isReloading = false;

    public void StartReload() {
        ammoInStock = Context.Instance.PlayerBag.ammoInStock;

        if (!isReloading && gameObject.activeSelf && currentAmmo <= 0 && ammoInStock > 0)
        {
            StartCoroutine(Reload());
            onReload?.Invoke();
        }
    }
    private IEnumerator Reload() {
        isReloading = true;

        Context.Instance.AudioSystem.PlaySFX(new AudioData(gunData.reloadSound, volume: 0.4f));

        yield return new WaitForSeconds(gunData.reloadTime);

        ammoInStock = Context.Instance.PlayerBag.ammoInStock;

        if (ammoInStock >= gunData.magSize)
        {
            currentAmmo = gunData.magSize;
            Context.Instance.PlayerBag.OnAmmoDecrease(gunData.magSize);
        }
        else
        {
            currentAmmo = ammoInStock;
            Context.Instance.PlayerBag.OnAmmoSet(0);
        }

        isReloading = false;
    }

    private bool CanShoot() => !isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot() {
        if (CanShoot() && currentAmmo > 0) 
        {
            Camera cam1 = cam.GetComponent<Camera>();
            Vector3 rayOrigin = cam1.transform.position;

            var ray = new Ray(rayOrigin, cam.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, gunData.maxShootDistance)){
                    
                IDamageble damageable = hitInfo.transform.GetComponent<IDamageble>();
                damageable?.RegisterDamage(gunData.Damage);
                if (damageable != null) 
                {
                    Instantiate(bloodSplash, hitInfo.transform.position, hitInfo.transform.rotation);
                    Context.Instance.AudioSystem.PlaySFX(new AudioData("body_hit_finisher_27", volume: 0.4f));
                }
            }

            onShot?.Invoke();
            OnGunShot();
        }
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;
    }

    private void OnGunShot() 
    {
        Context.Instance.AudioSystem.PlaySFX(new AudioData(gunData.shootSound, volume: 0.4f));

        shotParticlesPool = transform.parent.GetComponent<PlayerShootPoolInitializer>().shotParticlesPool;
        shotFXPool = transform.parent.GetComponent<PlayerShootPoolInitializer>().shotFXPool;

        Transform particaleInstance = shotParticlesPool.GetObjectFromPool(muzzle.position, muzzle.rotation);
     
        Transform FXInstance = shotFXPool.GetObjectFromPool(muzzle.position, muzzle.rotation);

        if (FXInstance.gameObject.GetComponent<Rigidbody>() == null) 
            FXInstance.gameObject.AddComponent<Rigidbody>().AddForce(cam.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
        else FXInstance.gameObject.GetComponent<Rigidbody>().AddForce(cam.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);

        currentAmmo--;
        timeSinceLastShot = 0;
    }
}
