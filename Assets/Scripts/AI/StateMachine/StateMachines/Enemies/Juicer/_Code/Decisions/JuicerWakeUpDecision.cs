using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Decisions/WakeUpDecision")]
public class JuicerWakeUpDecision : Decision
{
    public override bool Decide(JuicerStateController controller)
    {
        bool wakeup = Listen(controller);

        if (wakeup)
        {
            controller.chaseStartTime = controller.time;
            OnWakeup(controller);
        }

        return wakeup;
    }

    private void OnWakeup(JuicerStateController controller)
    {
        if (!controller.isWakingUp)
        {
            controller.animator.SetTrigger("WakeUp");
            controller.isWakingUp = true;
        }
    }

    private bool Listen(JuicerStateController controller)
    {
        if (Vector3.Distance(controller.juicerObject.transform.position, controller.currentTarget.transform.position) <= controller.listenDistance)
        {
            return true;
        }
        return false;
    }
}
