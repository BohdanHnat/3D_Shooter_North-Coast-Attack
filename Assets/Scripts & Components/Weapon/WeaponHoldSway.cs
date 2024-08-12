using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHoldSway : MonoBehaviour
{
    [SerializeField] private float smooth;
    [SerializeField] private float sensitivityMultiplier;   
    public float mouseXInput { private get; set; }
    public float mouseYInput { private get; set; }
    private void Update()
    {
        float mouseX = mouseXInput * sensitivityMultiplier;
        float mouseY = mouseYInput * sensitivityMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
