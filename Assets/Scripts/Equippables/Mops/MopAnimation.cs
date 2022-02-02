using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopAnimation : MonoBehaviour
{
    public bool isSweeping = false;

    ArmsAnimation _armsAnimation;
    // Start is called before the first frame update
    void Start()
    {
        _armsAnimation = GetComponentInParent<ArmsAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        isSweeping = Input.GetKey(KeyCode.Mouse0);
        _armsAnimation.SetSweeping(isSweeping);
    }
}
