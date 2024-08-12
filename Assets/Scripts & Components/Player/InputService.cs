using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputService : MonoBehaviour
{
    public bool isAiming_In_Out { get; private set; }
    public bool isJumping { get; private set; }
    public bool isShooting { get; private set; }
    public bool isReloading { get; private set; }
    public Vector2 weaponSwitchingValue { get; private set; }
    public Vector3 moveDirection {  get; private set; }
    public Vector2 OnLook { get; private set; }
    public void OnLooking(InputAction.CallbackContext context)
    {
        OnLook = context.ReadValue<Vector2>();
    }
    public void OnMoving(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector3>();
    }
    public void OnJumping(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
    }
    public void OnShooting(InputAction.CallbackContext context)
    {
        isShooting = context.ReadValueAsButton();
    }
    public void OnReloading(InputAction.CallbackContext context)
    {
        isReloading = context.ReadValueAsButton();
    }
    public void OnWeaponSwitch(InputAction.CallbackContext context)
    {
        weaponSwitchingValue = context.ReadValue<Vector2>();
    }
    public void OnAiming_In_Out(InputAction.CallbackContext context)
    {
        isAiming_In_Out = context.ReadValueAsButton();
    }
}
