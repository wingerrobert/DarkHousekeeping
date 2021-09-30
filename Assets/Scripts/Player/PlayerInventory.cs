using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class PlayerInventory : MonoBehaviour
{
    public List<GlobalValues.EquippableType> loadout;
    public float _changeEquippableDelay = 0.25f;

    GameObject _equipped;

    [SerializeField] ArmsAnimation _armsAnimation;
    [SerializeField] Transform _leftHand;
    [SerializeField] Transform _rightHand;

    List<GameObject> _inventory;
    GameObject[] _wipableSurfaces;

    int _currentSlot = 0;

    float _lastEquippableTime = 0.0f;

    bool _equippableLoadDone = false;

    void Start()
    {
        _wipableSurfaces = GameObject.FindGameObjectsWithTag(GlobalValues.TagValues[GlobalValues.Tags.WipableSurface]);

        _inventory = new List<GameObject>();

        loadout = new List<GlobalValues.EquippableType>() { 
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

        equippableClone = PositionEquipped(equippableClone);

        _inventory.Add(equippableClone);

        if (_inventory.Count == loadout.Count)
        {
            _equippableLoadDone = true;
            InitializeWipableSurfaces();
            DisableAllEquippables();
            UpdateEquipped();
        }
    }

    void DisableAllEquippables()
    {
        _inventory.ForEach(e => e.SetActive(false));
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

        return equippableObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_equippableLoadDone)
        {
            return;
        }

        if (Time.time > _changeEquippableDelay + _lastEquippableTime)
        { 
            ChangeInventorySlot();
        }
    }

    void InitializeWipableSurfaces()
    {
        for (int i = 0; i < _wipableSurfaces.Length; i++)
        {
            _wipableSurfaces[i].GetComponent<GenerateCleanSurfaceMap>().InitializeWipableSurface();
        }
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
