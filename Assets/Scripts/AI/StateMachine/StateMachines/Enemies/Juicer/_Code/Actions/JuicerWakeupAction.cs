using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Actions/WakeUp")]
public class JuicerWakeupAction : Action
{
    public override void Act(JuicerStateController controller)
    {
        WakeUp(controller);
    }

    private void WakeUp(JuicerStateController controller)
    {
    }
}
