using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class StoreUIController : MonoBehaviour
{
    public GameObject storeUI;
    public GameObject postProcessingController;

    public GameObject pikeMenuItem;
    public GameObject vacuumMenuItem;
    public GameObject pikeContainer;
    public GameObject vacuumContainer;

    public StoreController storeController;
    
    [SerializeField] private float menuItemVerticalOffset = 190.0f;

    //public GameObject vacuumContainer;
    //public GameObject mopContainer;


    public bool isShowing = false;
    DepthOfField _depthOfField;

    // Start is called before the first frame update
    void Start()
    {
        InitializeStoreInventory();
    }

    void InitializeStoreInventory()
    {
        int pikeCount = 0;
        int mopCount = 0;
        int vacuumCount = 0;

        foreach (InventoryItem item in storeController.inventoryItems)
        {
            switch (item.category)
            {
                case GlobalValues.EquippableCategory.Pike:
                    InitializePike(item, ref pikeCount);

                    break;
                case GlobalValues.EquippableCategory.Vacuum:
                    InitializeVacuum(item, ref vacuumCount);

                    break;
            }
        }
    }

    private void InitializeVacuum(InventoryItem item, ref int vacuumCount)
    {
        GameObject vacuumMenuItemInstance = Instantiate(vacuumMenuItem);

        VacuumMenuItemController vacuumMenuItemController = vacuumMenuItemInstance.GetComponent<VacuumMenuItemController>();
        VacuumItemStats vacuumItemStats = (VacuumItemStats)item.stats;

        item.linkedObject.GetComponentInChildren<VacuumController>().stats = vacuumItemStats; // link up linked object with stats

        vacuumMenuItemController.suctionPowerElement.text = vacuumItemStats.suctionStrength.ToString();
        vacuumMenuItemController.suctionWidthElement.text = vacuumItemStats.suctionSize.ToString();
        vacuumMenuItemController.shootForceElement.text = vacuumItemStats.shootForce.ToString();
        vacuumMenuItemController.reservoirSizeElement.text = vacuumItemStats.reservoirSize.ToString();

        vacuumMenuItemController.decriptionElement.text = item.description;
        vacuumMenuItemController.imageElement.sprite = item.thumbnail;
        vacuumMenuItemController.nameElement.text = item.itemName;
        vacuumMenuItemController.priceElement.text = item.price.ToString();

        vacuumMenuItemController.purchaseButton.onClick.AddListener(() => { 
            storeController.PurchaseItem(item.linkedObject);
            vacuumMenuItemController.purchaseButton.gameObject.SetActive(false);
        });

        vacuumMenuItemInstance.transform.SetParent(vacuumContainer.transform);
        vacuumMenuItemInstance.transform.localPosition = new Vector3(0.0f, -(vacuumCount++ * menuItemVerticalOffset), 0.0f);
    }

    private void InitializePike(InventoryItem item, ref int pikeCount)
    {
        GameObject pikeMenuItemInstance = Instantiate(pikeMenuItem);

        PikeMenuItemController pikeMenuItemController = pikeMenuItemInstance.GetComponent<PikeMenuItemController>();
        PikeItemStats pikeStats = (PikeItemStats)item.stats;

        item.linkedObject.GetComponent<PikeController>().stats = pikeStats; // link up linked object with stats

        pikeMenuItemController.damageElement.text = pikeStats.damage.ToString();
        pikeMenuItemController.rangeElement.text = pikeStats.range.ToString();
        pikeMenuItemController.speedElement.text = pikeStats.attackSpeed.ToString();

        pikeMenuItemController.decriptionElement.text = item.description;
        pikeMenuItemController.imageElement.sprite = item.thumbnail;
        pikeMenuItemController.nameElement.text = item.itemName;
        pikeMenuItemController.priceElement.text = item.price.ToString();

        pikeMenuItemController.purchaseButton.onClick.AddListener(() => { 
            storeController.PurchaseItem(item.linkedObject);
            pikeMenuItemController.purchaseButton.gameObject.SetActive(false);
        });

        pikeMenuItemInstance.transform.SetParent(pikeContainer.transform);
        pikeMenuItemInstance.transform.localPosition = new Vector3(0.0f, -(pikeCount++ * menuItemVerticalOffset), 0.0f);
    }
}
