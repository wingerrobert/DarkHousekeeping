using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardController : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(bool isOpening)
    { 
        _animator.SetTrigger(isOpening ? "openCupboard" : "closeCupboard");
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayAnimation(true);
    }
    private void OnTriggerExit(Collider other)
    {
        PlayAnimation(false);
    }
}
