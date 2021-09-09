using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    GameObject equipped;

    [SerializeField] ArmsAnimation _armsAnimation;
    [SerializeField] Transform _leftHand;
    [SerializeField] Transform _rightHand;

    List<GameObject> _inventory;

    int _currentSlot = 0;

    void Start()
    {
        _inventory = new List<GameObject>();

        LoadEquippable(GlobalValues.EquippableType.Dusterator);
        LoadEquippable(GlobalValues.EquippableType.OlBetsy);
        UpdateEquipped(_inventory[_currentSlot]);
    }

    

    void LoadEquippable(GlobalValues.EquippableType equippable)
    {
        // equippable objects should be disabled by default
        GameObject equippableObject = Instantiate(Resources.Load(GlobalValues.EquippableResourceMap[equippable]) as GameObject);
        equippableObject.SetActive(false);

        equippableObject = PositionEquipped(equippableObject);

        _inventory.Add(equippableObject);
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

        _armsAnimation.SetHoldType(currentEquippable.holdType);

        return equippableObject;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeInventorySlot();
    }

    void UpdateEquipped(GameObject equippable)
    {
        if (equipped != null)
        { 
            equipped.SetActive(false);
        }

        equipped = _inventory[_currentSlot];
        equipped.SetActive(true);

        _armsAnimation.SetHoldType(equipped.GetComponent<EquippableItem>().holdType);
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

        UpdateEquipped(_inventory[_currentSlot]);
    }
}
