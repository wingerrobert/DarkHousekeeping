using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    public bool thrownOut = false;

    Rigidbody rb;
    GameObject parentPike;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (thrownOut)
        {
            return;
        }

        if (other.tag == GlobalValues.TagValues[GlobalValues.Tags.PikeEnd])
        {
            PikeController pikeController = other.GetComponentInParent<PikeController>();
         
            if (pikeController.isGrabbing && !pikeController.hasTrash)
            {
                parentPike = other.gameObject;
                rb.isKinematic = true;
                pikeController.attachedTrash = gameObject;
                pikeController.hasTrash = true;
                transform.SetParent(other.transform);
                transform.localPosition = Vector3.zero;
            }
        }
        if (other.tag == GlobalValues.TagValues[GlobalValues.Tags.GarbageArea])
        {
            if (parentPike == null)
            {
                return;
            }
            PikeController pikeController = parentPike.GetComponentInParent<PikeController>();
            
            pikeController.hasTrash = false;
            
            transform.SetParent(null);
            rb.isKinematic = false;
            thrownOut = true;
        }
    }
}
