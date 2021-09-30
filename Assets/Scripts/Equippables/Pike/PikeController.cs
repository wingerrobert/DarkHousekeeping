using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeController : MonoBehaviour
{
    public float attackSpeed = 1.0f;
    public bool isGrabbing = false;
    public bool hasTrash = false;
    public GameObject attachedTrash;

    ArmsAnimation _armsAnimation;
    float _lastTimeAttacked;


    // Start is called before the first frame update
    void Start()
    {
        _armsAnimation = GetComponentInParent<ArmsAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        TickAttack();
        TickDropTrash();
    }

    void TickDropTrash()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            attachedTrash.transform.SetParent(null);
            attachedTrash.GetComponent<Rigidbody>().isKinematic = false;
            hasTrash = false;
        }
    }

    void TickAttack()
    {
        if (Time.time > _lastTimeAttacked + attackSpeed)
        {
            isGrabbing = false;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                isGrabbing = true;
                _armsAnimation.TriggerPikeAttack();
                _lastTimeAttacked = Time.time;
            }
        }
    }
}
