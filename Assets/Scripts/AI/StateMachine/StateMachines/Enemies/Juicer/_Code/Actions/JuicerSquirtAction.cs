using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Actions/Squirt")]
public class JuicerSquirtAction : Action
{
    public override void Act(JuicerStateController controller)
    {
        Squirt(controller);
    }

    private void Squirt(JuicerStateController controller)
    {
        if (controller.isChaseTargetSet)
        {
            controller.navMeshAgent.SetDestination(controller.juicerObject.transform.position);
            controller.isChaseTargetSet = false;
        }
    }
}
