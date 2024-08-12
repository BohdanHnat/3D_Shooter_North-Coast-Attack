using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class EnemyWeaponShooting : MonoBehaviour
{
    [SerializeField] private EnemyShootPoolInitializer shootingPoolInitializer; 
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Transform enemyVision;
    [SerializeField] private string weaponDataKey;
    [SerializeField] private GameObject shotParticlePrefab;
    [SerializeField] private GameObject shotFXPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask targetMask;
    public WeaponData gunData { get; private set; }

    private float timeSinceLastShot;
    public bool isReloading { get; private set; }
    public float currentAmmo { get; private set; }
    public Transform muzzle { get; private set; }

    private ObjectPool<Transform> shotParticlesPool;
    private ObjectPool<Transform> shotFXPool;

    private GameObject bloodSplash;
    private void Awake()
    {
        muzzle = transform.Find("Muzzle");
    }
    private void Start()
    {
        gunData = Context.Instance.DataSystem.weaponDatas.FirstOrDefault(data => data.Name == weaponDataKey);
        currentAmmo = gunData.magSize;
        
        enemyController.onAttackingTarget += Shoot;
        enemyController.onDie += DisableScript;

        bloodSplash = transform.parent.GetComponent<ParticleHandler>().bloodSplash;
    }
    private void OnDisable() => isReloading = false;

    public void StartReload()
    {
        if (!isReloading && gameObject.activeSelf && currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload()
    {
        isReloading = true;

        Context.Instance.AudioSystem.PlaySFX(new AudioData(gunData.reloadSound, volume: 0.4f));
        
        yield return new WaitForSeconds(gunData.reloadTime);

        currentAmmo = gunData.magSize;

        isReloading = false;
    }

    private bool CanShoot() => !isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot()
    {
        if (CanShoot() && currentAmmo > 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(enemyVision.position, enemyVision.forward, out hit, gunData.maxShootDistance))
            {
                IDamageble damageable = hit.transform.GetComponent<IDamageble>();
                damageable?.RegisterDamage(gunData.Damage);

                if (damageable != null)
                {
                    Instantiate(bloodSplash, hit.transform.position, hit.transform.rotation);
                    Context.Instance.AudioSystem.PlaySFX(new AudioData("body_hit_finisher_27", volume: 0.4f));
                }

                timeSinceLastShot = 0;
            }
            OnGunShot();            
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        StartReload();
    }

    private void OnGunShot()
    {
        if (muzzle != null)
        {
            Context.Instance.AudioSystem.PlaySFX(new AudioData(gunData.shootSound, volume: 0.4f));

            shotParticlesPool = shootingPoolInitializer.shotParticlesPool;
            shotFXPool = shootingPoolInitializer.shotFXPool;

            Transform particaleInstance = shotParticlesPool.GetObjectFromPool(muzzle.position, muzzle.rotation);

            Transform FXInstance = shotFXPool.GetObjectFromPool(muzzle.position, muzzle.rotation);

            if (FXInstance.gameObject.GetComponent<Rigidbody>() == null)
                FXInstance.gameObject.AddComponent<Rigidbody>().AddForce(enemyVision.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
            else FXInstance.gameObject.GetComponent<Rigidbody>().AddForce(enemyVision.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);

            currentAmmo--;
        }        
    }
    private void DisableScript()
    {
        enabled = false;
    }
}
