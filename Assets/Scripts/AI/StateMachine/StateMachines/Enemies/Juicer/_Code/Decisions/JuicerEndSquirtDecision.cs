using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Decisions/EndSquirtDecision")]
public class JuicerEndSquirtDecision : Decision
{
    public override bool Decide(JuicerStateController controller)
    {
        return CheckEndSquirt(controller);
    }

    private bool CheckEndSquirt(JuicerStateController controller)
    {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).IsName(controller.squirtClip.name) && controller.isSquirting == false)
        {
            controller.EmitPus();
            controller.isSquirting = true;
        }
        if (controller.animator.GetCurrentAnimatorStateInfo(0).IsName(controller.chaseClip.name))
        {
            controller.isSquirting = false;
            return true;
        }
        return false;
    }
}
