using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothAnimation : MonoBehaviour
{
    ArmsAnimation _armsAnimation;

    // Start is called before the first frame update
    void Start()
    {
        _armsAnimation = GetComponentInParent<ArmsAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _armsAnimation.SetWiping(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _armsAnimation.SetWiping(false);
        }
    }
}
