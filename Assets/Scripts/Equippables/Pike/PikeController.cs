using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PikeController : MonoBehaviour
{
    public PikeItemStats stats;
    public bool isGrabbing = false;
    public bool hasTrash = false;
    public GameObject attachedTrash;
    public Transform pikeEnd;

    ArmsAnimation _armsAnimation;
    float _lastTimeAttacked;
    CursorController _cursor;


    // Start is called before the first frame update
    void Start()
    {
        _armsAnimation = GetComponentInParent<ArmsAnimation>();
    }

    private void Awake()
    {
        _cursor = GameObject.FindGameObjectWithTag(GlobalValues.TagValues[GlobalValues.Tags.CursorController]).GetComponent<CursorController>();
    }

    // Update is called once per frame
    void Update()
    {
        TickAttack();
        TickGrab();
        TickDropTrash();
    }

    void TickGrab()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        LayerMask layerMask = LayerMask.GetMask(GlobalValues.LayerValues[GlobalValues.Layers.Trash], GlobalValues.LayerValues[GlobalValues.Layers.Enemy]);

        if (Physics.Raycast(ray, out hit, stats.range, layerMask))
        {
            Rigidbody targetRigidBody = hit.collider.gameObject.GetComponent<Rigidbody>();
            NavMeshAgent navMesh = hit.collider.gameObject.GetComponent<NavMeshAgent>();

            if (navMesh != null)
            {
                navMesh.enabled = false;
            }

            if (targetRigidBody == null)
            {
                return;
            }

            if (targetRigidBody.mass > stats.maxMassAttachable)
            {
                _cursor.setCursorValue(GlobalValues.CursorValue.Invalid);
                return;
            }
            if (isGrabbing && !hasTrash)
            {
                attachedTrash = hit.collider.gameObject;
                attachedTrash.transform.SetParent(pikeEnd);
                attachedTrash.transform.position = pikeEnd.position;
                attachedTrash.GetComponent<Rigidbody>().isKinematic = true;
                attachedTrash.GetComponent<Collider>().enabled = false;
                hasTrash = true;
            }
            _cursor.setCursorValue(GlobalValues.CursorValue.Active);
        }
        else 
        {
            _cursor.setCursorValue(GlobalValues.CursorValue.Idle);
        }
    }

    void TickDropTrash()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            SetEnemyDead();

            attachedTrash.transform.SetParent(null);
            attachedTrash.GetComponent<Rigidbody>().isKinematic = false;
            attachedTrash.GetComponent<Collider>().enabled = true;
            hasTrash = false;
        }
    }

    void TickAttack()
    {
        if (Time.time > _lastTimeAttacked + stats.attackSpeed)
        {
            isGrabbing = false;
            _armsAnimation.SetPikeAttacking(false);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                isGrabbing = true;
                _armsAnimation.SetPikeAttacking(true);
                _lastTimeAttacked = Time.time;
            }
        }
    }

    void SetEnemyDead()
    {
        if (attachedTrash.layer == LayerMask.NameToLayer(GlobalValues.LayerValues[GlobalValues.Layers.Enemy]))
        {
            SpiderAI spider = attachedTrash.GetComponent<SpiderAI>();
            spider.SetDieAnimation();
        }
    }
}
