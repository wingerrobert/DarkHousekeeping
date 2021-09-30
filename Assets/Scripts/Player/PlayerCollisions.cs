using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.tag == GlobalValues.TagValues[GlobalValues.Tags.Door]))
        {
            return;
        }
        other.gameObject.GetComponentInParent<DoorController>().OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(other.tag == GlobalValues.TagValues[GlobalValues.Tags.Door]))
        {
            return;
        }
        other.gameObject.GetComponentInParent<DoorController>().CloseDoor();
    }
}
