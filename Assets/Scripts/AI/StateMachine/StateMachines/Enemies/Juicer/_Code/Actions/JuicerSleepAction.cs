using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Actions/Sleep")]
public class JuicerSleepAction : Action
{
    public override void Act(JuicerStateController controller)
    {
        Sleep(controller);
    }

    private void Sleep(JuicerStateController controller)
    {
        if (controller.isChaseTargetSet)
        {
            controller.navMeshAgent.SetDestination(controller.juicerObject.transform.position);
        }
    }
}
