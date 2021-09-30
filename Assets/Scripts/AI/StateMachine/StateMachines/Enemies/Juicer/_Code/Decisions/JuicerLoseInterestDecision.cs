using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Decisions/LoseInterestDecision")]
public class JuicerLoseInterestDecision : Decision
{
    public override bool Decide(JuicerStateController controller)
    {
        bool loseInterest = CheckLostInterest(controller);

        if (loseInterest)
        {
            OnLoseInterest(controller);
        }

        return loseInterest;
    }

    void OnLoseInterest(JuicerStateController controller)
    {
        controller.animator.SetTrigger("GoToSleep");
    }

    private bool CheckLostInterest(JuicerStateController controller)
    {
        if (Vector3.Distance(controller.juicerObject.transform.position, controller.currentTarget.transform.position) >= controller.listenDistance * 1.5)
        {
            return true;
        }
        return false;
    }
}
