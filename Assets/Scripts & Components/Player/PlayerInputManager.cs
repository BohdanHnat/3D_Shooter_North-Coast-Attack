using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private InputService inputService;

    [SerializeField] private MovementComponent movementComponent;

    [SerializeField] private CameraRotation cameraRotation;

    [SerializeField] private WeaponHoldSway weaponSway;

    [SerializeField] private PlayerShooting playerShooting;

    [SerializeField] private WeaponSwitch weaponSwitch;

    [SerializeField] private WeaponAnimating weaponAnimating;
    private void Update()
    {
        cameraRotation.look = inputService.OnLook;

        movementComponent._direction = inputService.moveDirection;
        if (inputService.isJumping) movementComponent.Jump();

        weaponSway.mouseXInput = inputService.OnLook.x;
        weaponSway.mouseYInput = inputService.OnLook.y;

        playerShooting.isShooting = inputService.isShooting;
        playerShooting.isReloading = inputService.isReloading;

        weaponSwitch.SwitchKeyValue = inputService.weaponSwitchingValue;

        weaponAnimating.isShooting = inputService.isShooting;
        weaponAnimating.isReloading = inputService.isReloading;
        weaponAnimating.isAiming_In_Out = inputService.isAiming_In_Out;
    }
}
