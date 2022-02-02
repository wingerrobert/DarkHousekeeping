using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Decisions/AggravateDecision")]
public class JuicerAggravateDecision : Decision
{
    public override bool Decide(JuicerStateController controller)
    {
        bool wakeup = Listen(controller);

        if (wakeup)
        {
            OnAggravate(controller);
        }

        return wakeup;
    }

    private void OnAggravate(JuicerStateController controller)
    {
        controller.animator.SetTrigger("WakeUp");
    }

    /// <summary>
    /// The player will aggravate the Juicer if the player is: within distance + creating too much noise
    /// </summary>
    /// <returns>True if the player has been noticed</returns>
    private bool Listen(JuicerStateController controller)
    {
        if (controller.currentTargetMovement == null)
        {
            return false;
        }

        bool withinDistance = Vector3.Distance(controller.juicerObject.transform.position, controller.currentTarget.transform.position) <= controller.listenDistance;

        bool heardPlayer = controller.currentTargetMovement.steppingNoise > controller.listenSensitivity;

        if (withinDistance && heardPlayer)
        {
            return true;
        }

        return false;
    }
}
