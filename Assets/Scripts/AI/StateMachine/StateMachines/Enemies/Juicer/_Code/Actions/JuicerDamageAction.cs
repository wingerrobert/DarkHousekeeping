using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Juicer/Actions/Damage")]
public class JuicerDamageAction : Action
{
    public override void Act(JuicerStateController controller)
    {
        Damage(controller);
    }

    private void Damage(JuicerStateController controller)
    {
        for (int i = 0; i < 4; i++)
        { 
            GameObject spawnedGrub = Instantiate(controller.juicerGrubPrefab, controller.juicerObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);

            Rigidbody grubRigidBody = spawnedGrub.GetComponent<Rigidbody>();
            
            Vector3 shootingForce = Quaternion.Euler(0.0f, i * 90.0f, 0.0f) * new Vector3(5.0f * Random.value, (210.0f * Random.value) + 10.0f, 0.0f);

            grubRigidBody.AddForce(shootingForce, ForceMode.Impulse);

            NavMeshAgent navMeshAgent = spawnedGrub.GetComponent<NavMeshAgent>();

            navMeshAgent.speed = (navMeshAgent.speed * (Random.value * 2.0f)) + 1.0f;
        }

        Destroy(controller.juicerObject);
    }
}
