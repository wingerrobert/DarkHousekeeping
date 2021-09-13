using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12.0f;
    public float runMultiplier = 2.0f;
    
    Rigidbody _rigidBody;
    CapsuleCollider _collider;

    float _runSpeed;
    float _crouchSpeed;
    float _initialHeight;
    float _crouchingHeight;
    float _crouchVerticalOffset = 0.5f;

    bool _isCrouching = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        _runSpeed = speed * runMultiplier;
        _crouchSpeed = speed / runMultiplier;

        _initialHeight = _collider.height;
        _crouchingHeight = _initialHeight / 2;
    }

    void MoveUpdate()
    {
        float currentSpeed = (Input.GetKey(KeyCode.LeftShift)) ? _runSpeed : speed;

        if (_isCrouching)
        {
            currentSpeed = _crouchSpeed;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        _rigidBody.velocity = move * currentSpeed * Time.deltaTime;
    }

    void CheckCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DoCrouch();
            _isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            DoStand();
            _isCrouching = false;
        }
    }

    void DoCrouch()
    {
        if (!_isCrouching)
        {
            _collider.height = _crouchingHeight;
            transform.position = new Vector3(transform.position.x, transform.position.y - _crouchVerticalOffset, transform.position.z);
        }
    }

    void DoStand()
    { 
        if (_isCrouching)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + _crouchVerticalOffset, transform.position.z);
            _collider.height = _initialHeight;
        }
    }

    public bool IsCrouching()
    {
        return _isCrouching;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        CheckCrouch();
    }
}
