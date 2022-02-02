using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public PlayerInventory playerInventory;
    public float sensitivity = 100f;
    public Texture2D cursorTexture;

    float _xRotation = 0f;
    PlayerMovement _player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _player = playerBody.gameObject.GetComponent<PlayerMovement>();
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // Get mouse input
        float _mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;

        // Calculate and clamp x input to 90 degrees
        _xRotation -= _mouseY;

        float bottomXCap = 90f;

        GameObject equippedObject = playerInventory.GetEquipped();

        if (equippedObject != null)
        {
            EquippableHoldType equippedItem = equippedObject.GetComponent<EquippableHoldType>();

            // Limit cap if player is crouching with standing vacuum (it will clip through floor if not capped)
            if (_player.IsCrouching() && equippedItem.holdType == GlobalValues.EquippableHoldType.StandingVacuum)
            {
                bottomXCap = 75f;
            }
        }

        _xRotation = Mathf.Clamp(_xRotation, -90f, bottomXCap);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up, _mouseX);
    }
}
