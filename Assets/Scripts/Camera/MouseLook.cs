using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform _playerBody;
    public float _sensitivity = 100f;

    float _xRotation = 0f;
    PlayerMovement _player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _player = _playerBody.gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Get mouse input
        float _mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        // Calculate and clamp x input to 90 degrees
        _xRotation -= _mouseY;

        float bottomXCap = 90f;

        // Limit cap if player is crouching with vacuum
        if (_player.IsCrouching())
        {
            bottomXCap = 60f;
        }

        _xRotation = Mathf.Clamp(_xRotation, -90f, bottomXCap);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        _playerBody.Rotate(Vector3.up, _mouseX);
    }
}
