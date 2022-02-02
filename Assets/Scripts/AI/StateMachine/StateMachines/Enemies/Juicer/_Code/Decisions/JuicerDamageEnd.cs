using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Decisions/DamageEndDecision")]
public class JuicerDamageEnd : Decision
{
    public override bool Decide(JuicerStateController controller)
    {
        return CheckEndDamage(controller);
    }

    private bool CheckEndDamage(JuicerStateController controller)
    {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).IsName(controller.damageClip.name) && controller.isBeingDamaged == false)
        {
            controller.isBeingDamaged = true;
        }
        if (controller.animator.GetCurrentAnimatorStateInfo(0).IsName(controller.chaseClip.name))
        {
            controller.isBeingDamaged = false;
            return true;
        }
        return false;
    }
}
