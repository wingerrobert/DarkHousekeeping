using System.Collections;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;

public class LaptopController : MonoBehaviour
{
    public ComputerUIController computerUIController;
    bool isInView = false;
    Outlinable outlinable;

    void Awake()
    {
        outlinable = GetComponent<Outlinable>();
    }

    // NOTE that in this script we are only disabling on lateupdate, the enabling is done on MouseLook.cs
    private void Update()
    {
        UpdateOutline();
    }

    private void LateUpdate()
    {
        isInView = false;
    }

    void UpdateOutline()
    {
        outlinable.enabled = isInView;
    }

    public void ShowStoreUI()
    {
        if (!computerUIController.isShowing)
        {
            computerUIController.SetComputerUIVisibility(true);
        }
    } 
}
