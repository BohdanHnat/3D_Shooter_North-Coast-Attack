using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private Transform orientation;

    [SerializeField] private float objectHeight;

    [SerializeField] private float jumpForce;

    [SerializeField] private float speed;

    private bool isGrounded;

    [HideInInspector]
    public Vector3 _direction;

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, objectHeight * 0.5f + 0.2f);

        SpeedControl();

        var moveDirection = orientation.right * _direction.x + _direction.z * orientation.forward;

        _rigidbody.AddForce(moveDirection * speed * 10f, ForceMode.Force);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }
}
