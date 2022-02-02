using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     public float steppingNoise = 0.0f;
    public float speed = 12.0f;
    public float runMultiplier = 2.0f;

    [SerializeField] float _steppingNoiseBase = 10.0f;
    
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

        Vector3 movementVector = (transform.forward * Input.GetAxis("Vertical") * currentSpeed * Time.fixedDeltaTime) + (transform.right * Input.GetAxis("Horizontal") * currentSpeed * Time.fixedDeltaTime);

        _rigidBody.MovePosition(transform.localPosition + movementVector);

        steppingNoise = movementVector.magnitude * _steppingNoiseBase;
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
    void FixedUpdate()
    {
        MoveUpdate();
    }

    private void Update()
    {
        CheckCrouch();
    }
}
