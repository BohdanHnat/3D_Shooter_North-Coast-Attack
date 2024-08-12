using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public event Action onShoot;
    public event Action onReload;
    public bool isShooting { get; set; }
    public bool isReloading { get; set; }
    private void Update()
    {
        if (isShooting)
            onShoot?.Invoke();

        if (isReloading)
            onReload.Invoke();
    }
}
