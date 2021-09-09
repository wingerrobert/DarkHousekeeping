using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnCreate : MonoBehaviour
{
    MeshRenderer _renderer;
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.enabled = false;
    }
}
