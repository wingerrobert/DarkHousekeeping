using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrubAI : MonoBehaviour
{
    NavMeshAgent _agent;
    GameObject _target;
    Rigidbody _rigidBody;

    bool _isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag(GlobalValues.TagValues[GlobalValues.Tags.Player]);
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalValues.LayerValues[GlobalValues.Layers.Ground]))
        {
            _rigidBody.isKinematic = true;
            _isGrounded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGrounded)
        { 
            _agent.SetDestination(_target.transform.position);
        }
    }
}
