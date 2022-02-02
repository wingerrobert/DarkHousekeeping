using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Decisions/DamagedDecision")]

public class JuicerDamagedDecision : Decision
{
    public override bool Decide(JuicerStateController controller)
    {
        bool damaged = CheckDamaged(controller);

        if (damaged)
        {
            OnAggravate(controller);
        }

        return damaged;
    }

    private void OnAggravate(JuicerStateController controller)
    {
        controller.animator.SetTrigger("Damaged");
    }

    private bool CheckDamaged(JuicerStateController controller)
    {
        if (controller.currentTargetInventory != null)
        {
            PikeController pikeController = controller.currentTargetInventory.equippedItem.GetComponent<PikeController>();

            if (pikeController == null)
            {
                return false;
            }

            RaycastHit raycastHit;

            Physics.Raycast(controller.currentTargetEyes.transform.position, controller.currentTargetEyes.transform.forward, out raycastHit, pikeController.stats.range);

            if (raycastHit.transform == null || raycastHit.transform.gameObject.GetComponent<JuicerStateController>() == null)
            {
                return false;
            }

            if (!pikeController.hasTrash && controller.currentTargetArmsAnimation.isStabbing == true)
            {
                return true;
            }
        }
        return false;
    }
}
