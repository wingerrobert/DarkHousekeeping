using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Decisions/SquirtDecision")]
public class JuicerSquirtDecision : Decision
{
    public override bool Decide(JuicerStateController controller)
    {
        bool shouldSquirt = CheckShouldSquirt(controller);

        if (shouldSquirt)
        {
            OnSquirt(controller);
        }

        return shouldSquirt;
    }

    void OnSquirt(JuicerStateController controller)
    {
            controller.animator.SetTrigger("Squirt");
    }

    private bool CheckShouldSquirt(JuicerStateController controller)
    {
        if (Vector3.Distance(controller.juicerObject.transform.position, controller.currentTarget.transform.position) <= controller.listenDistance / 2.0 && !controller.isSquirting)
        {
            return true;
        }
        return false;
    }
}
