using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsAnimation : MonoBehaviour
{
    public bool isStabbing = false;

    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PikeStabStart()
    {
        isStabbing = true;
    }

    public void PikeStabEnd()
    {
        isStabbing = false;
    }

    public void SetPikeAttacking(bool val)
    {
        _animator.SetBool("IsPikeAttacking", val);
    }

    public void TriggerVacuumShooting()
    { 
        _animator.SetTrigger("ShootVacuum");
    }

    public void SetSucking(bool value)
    {
        _animator.SetBool("IsVacuuming", value);
    }

    public void SetWiping(bool value)
    {
        _animator.SetBool("IsWiping", value);
    }

    public void SetSweeping(bool value)
    {
        _animator.SetBool("IsSweeping", value);
    }

    public void SetHoldType(GlobalValues.EquippableHoldType holdType)
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        
        string holdTypeValue = string.Empty;
        switch (holdType)
        {
            case GlobalValues.EquippableHoldType.SmallHandHeldVacuum:
                holdTypeValue = "HandVacuumHold";
                break;
            case GlobalValues.EquippableHoldType.StandingVacuum:
                holdTypeValue = "StandingVacuumHold";
                break;
            case GlobalValues.EquippableHoldType.Cloth:
                holdTypeValue = "ClothHold";
                break;
            case GlobalValues.EquippableHoldType.Pike:
                holdTypeValue = "PikeHold";
                break;
        }
        _animator.SetTrigger(holdTypeValue);
    }
}
