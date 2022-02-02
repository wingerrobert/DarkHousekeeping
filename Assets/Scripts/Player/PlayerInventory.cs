using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class PlayerInventory : MonoBehaviour
{
    public List<GlobalValues.EquippableItem> loadout;
    public GameObject equippedItem;

    [SerializeField] ArmsAnimation _armsAnimation;
    [SerializeField] Transform _leftHand;
    [SerializeField] Transform _rightHand;

    List<GameObject> _inventory;

    int _currentSlot = -1;
    float _lastEquippableTime = 0.0f;
    float _changeEquippableDelay = 0.25f;

    void Start()
    {
        _inventory = new List<GameObject>();
    }

    public GameObject GetEquipped()
    {
        return equippedItem;
    }

    public void AddItemToInventory(GameObject item)
    {
        _inventory.Add(item);
        _currentSlot = _inventory.Count - 1;
        PositionEquipped(item);
        UpdateEquipped();
    }

    void DisableAllEquippables()
    {
        _inventory.ForEach(e => e.SetActive(false));
    }

    GameObject PositionEquipped(GameObject equippableObject)
    {
        EquippableHoldType currentEquippable = equippableObject.GetComponent<EquippableHoldType>();

        // Determine which hand the equippable is in
        Transform handTransform;
        handTransform = _leftHand;

        if (currentEquippable.handedness == GlobalValues.EquippableHandedness.Right)
        {
            handTransform = _rightHand;
        }

        // Position equippable and select correct holding animation
        equippableObject.transform.parent = handTransform;

        equippableObject.transform.localPosition = Vector3.zero;
        equippableObject.transform.localRotation = Quaternion.identity;

        return equippableObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _changeEquippableDelay + _lastEquippableTime)
        { 
            ChangeInventorySlot();
        }
    }

    void UpdateEquipped()
    {
        if (_inventory.Count <= 0)
        {
            return;
        }
        if (equippedItem != null)
        { 
            equippedItem.SetActive(false);
        }

        equippedItem = _inventory[_currentSlot];
        equippedItem.SetActive(true);

        _armsAnimation.SetHoldType(equippedItem.GetComponent<EquippableHoldType>().holdType);
    }

    void ChangeInventorySlot()
    {
        // scroll is > 0 = scroll up, scroll is < 0 = scroll down, scroll = 0 = no scrolling
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            if (scroll > 0 && _currentSlot < _inventory.Count - 1)
            {
                _currentSlot++;
            }
            else if (scroll < 0 && _currentSlot > 0)
            {
                _currentSlot--;
            }

            UpdateEquipped();

            _lastEquippableTime = Time.time;
        }
    }
}
