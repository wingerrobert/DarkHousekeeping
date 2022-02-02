using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Decisions/WakeUpDecision")]
public class JuicerWakeUpDecision : Decision
{
    public override bool Decide(JuicerStateController controller)
    {
        return CheckWakeUp(controller);
    }

    private bool CheckWakeUp(JuicerStateController controller)
    {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).IsName(controller.chaseClip.name))
        {
            return true;
        }
        return false;
    }
}
