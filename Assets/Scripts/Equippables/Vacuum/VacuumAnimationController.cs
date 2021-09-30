using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumAnimationController : MonoBehaviour
{
    public Material vacuumOnMaterial;
    public Material vacuumOffMaterial;

    VacuumController _vacuumController;
    
    Color _onInitialColor;
    Color _offInitialColor;

    void Start()
    {
        _onInitialColor = Color.green;
        _offInitialColor = Color.red;

        _vacuumController = GetComponent<VacuumController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_vacuumController.isSucking)
        {
            vacuumOnMaterial.SetColor("_BaseColor", _onInitialColor);
            vacuumOffMaterial.SetColor("_BaseColor", Color.black);
        }
        else 
        {
            vacuumOffMaterial.SetColor("_BaseColor", _offInitialColor);
            vacuumOnMaterial.SetColor("_BaseColor", Color.black);
        }
    }
}
