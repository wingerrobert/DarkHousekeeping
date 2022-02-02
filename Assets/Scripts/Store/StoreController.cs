using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public InventoryItem[] inventoryItems;
    public GameObject[] webpageTabs;

    PlayerInventory _playerInventory;
    GameObject _player;
    int _currentTabIndex = 0;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(GlobalValues.TagValues[GlobalValues.Tags.Player]);
        _playerInventory = _player.GetComponentInChildren<PlayerInventory>();

        for (int i = 0; i < webpageTabs.Length; i++)
        {
            if (i != 0)
            {
                GameObject currentTab = webpageTabs[i];
                currentTab.SetActive(false);
            }
        }
    }

    public void PurchaseItem(GameObject item)
    {
        _playerInventory.AddItemToInventory(Instantiate(item));
    }

    public void SetTab(int index)
    {
        webpageTabs[_currentTabIndex].SetActive(false); // Hide last active tab
        
        _currentTabIndex = index;
        webpageTabs[index].SetActive(true);
    }

}
