using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsAnimation : MonoBehaviour
{
    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSucking(bool value)
    {
        _animator.SetBool("IsVacuuming", value);
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
        }
        _animator.SetTrigger(holdTypeValue);
    }
}
