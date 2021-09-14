using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class PlayerInventory : MonoBehaviour
{
    GameObject _equipped;

    [SerializeField] ArmsAnimation _armsAnimation;
    [SerializeField] Transform _leftHand;
    [SerializeField] Transform _rightHand;

    List<GameObject> _inventory;

    int _currentSlot = 0;

    void Start()
    {
        _inventory = new List<GameObject>();

        List<GlobalValues.EquippableType> loadout = new List<GlobalValues.EquippableType>() { 
            GlobalValues.EquippableType.Dusterator,
            GlobalValues.EquippableType.OlBetsy,
            GlobalValues.EquippableType.DustyRag
        };

        loadout.ForEach(e => LoadEquippable(e));
    }

    public GameObject GetEquipped()
    {
        return _equipped;
    }

    void LoadEquippable(GlobalValues.EquippableType equippable)
    {
        Addressables.LoadAssetAsync<GameObject>(GlobalValues.EquippableAddressableNameMap[equippable]).Completed += OnEquippableLoadDone;
    }

    void OnEquippableLoadDone(AsyncOperationHandle<GameObject> equippableObjectHandle)
    {
        GameObject equippableObject = equippableObjectHandle.Result;

        if (equippableObject == null)
        {
            Debug.LogError("Equipple was not loaded successfully");
        }
        // equippable objects should be disabled by default
        
        GameObject equippableClone = Instantiate(equippableObject);
        equippableClone.SetActive(false);

        equippableClone = PositionEquipped(equippableClone);

        _inventory.Add(equippableClone);

        UpdateEquipped();
    }

    GameObject PositionEquipped(GameObject equippableObject)
    {
        EquippableItem currentEquippable = equippableObject.GetComponent<EquippableItem>();

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

        _armsAnimation.SetHoldType(currentEquippable.holdType);

        return equippableObject;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeInventorySlot();
    }

    void UpdateEquipped()
    {
        if (_equipped != null)
        { 
            _equipped.SetActive(false);
        }

        _equipped = _inventory[_currentSlot];
        _equipped.SetActive(true);

        _armsAnimation.SetHoldType(_equipped.GetComponent<EquippableItem>().holdType);
    }

    void ChangeInventorySlot()
    {
        // scroll is > 0 = scroll up, scroll is < 0 = scroll down, scroll = 0 = no scrolling
        float scroll = Input.mouseScrollDelta.y;

        if (scroll == 0)
        {
            return;
        }

        if (scroll > 0 && _currentSlot < _inventory.Count - 1) 
        {
            _currentSlot++;
        }
        else if (scroll < 0 && _currentSlot > 0)
        {
            _currentSlot--;
        }

        UpdateEquipped();
    }
}
