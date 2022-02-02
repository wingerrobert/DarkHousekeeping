using System.Collections;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;

public class EnableOutlineOnLook : MonoBehaviour
{
    Camera _cam;
    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        OutlineOnLookUpdate();
    }

    void OutlineOnLookUpdate()
    {
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        LayerMask layerMask = LayerMask.GetMask(GlobalValues.LayerValues[GlobalValues.Layers.Outlinable], GlobalValues.LayerValues[GlobalValues.Layers.Outlinable]);

        if (Physics.Raycast(ray, out hit, 2.0f, layerMask))
        {
            Outlinable outlinable = hit.collider.gameObject.GetComponent<Outlinable>();

            if (outlinable != null)
            {
                outlinable.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DoOutlineAction(hit.collider.gameObject);
            }
        }
    }

    void DoOutlineAction(GameObject outlined)
    {
        LaptopController laptopController = outlined.GetComponent<LaptopController>();
        
        if (laptopController != null)
        {
            laptopController.ShowStoreUI();
        }
    }
}
