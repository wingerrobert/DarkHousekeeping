using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Actions/Chase")]
public class JuicerChaseAction : Action
{
    public override void Act(JuicerStateController controller)
    {
        Chase(controller);
    }

    private void Chase(JuicerStateController controller)
    {
        if (!controller.isChaseTargetSet)
        {
            controller.currentTarget = controller.targets[0];
            controller.isChaseTargetSet = true;
        }


        controller.navMeshAgent.SetDestination(controller.currentTarget.transform.position);
    }

    private void UpdateClosestTarget(JuicerStateController controller)
    {
        //float closestDistance = 0;
        //Vector3 closestTargetPosition = Vector3.zero;

        //foreach (GameObject targetObject in controller.targets)
        //{
        //    float currentDistance = Vector3.Distance(controller.juicerObject.transform.position, controller.currentTarget.transform.position);

        //    if (controller.currentTarget.transform.position == Vector3.zero)
        //    {
        //        controller.currentTarget.transform.position = targetObject.transform.position;
        //        closestDistance = currentDistance;
        //        closestTargetPosition = targetObject.transform.position;
        //    }
        //    else
        //    {
        //        if (currentDistance < closestDistance)
        //        {
        //            closestDistance = currentDistance;
        //            closestTargetPosition = targetObject.transform.position;
        //        }
        //    }
        //}

        //controller.currentTarget.transform. = closestTargetPosition;
    }

}
