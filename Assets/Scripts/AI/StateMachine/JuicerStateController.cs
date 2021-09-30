using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class JuicerStateController : MonoBehaviour
{
    public State currentState;
    public State remainState;

    [Range(0.0f, 100.0f)] public float listenDistance = 10.0f;
    public AnimationClip wakeUpClip;

    [HideInInspector] public float time;

    [HideInInspector] public GameObject juicerObject;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public GameObject[] targets;
    [HideInInspector] public GameObject currentTarget;
    [HideInInspector] public Vector3 initialPosition;

    [HideInInspector] public bool isChaseTargetSet = false;
    [HideInInspector] public bool isWakingUp = false;

    [HideInInspector] public float chaseStartTime;

    [HideInInspector] public Animator animator;

    float _wakeUpTime;

    void Awake()
    {
        _wakeUpTime = wakeUpClip.length;

        animator = GetComponent<Animator>();
        juicerObject = gameObject;
        initialPosition = juicerObject.transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag(GlobalValues.TagValues[GlobalValues.Tags.Player]);
        currentTarget = targets[0];
    }

    public bool hasWokenUp()
    {
        bool wokenUp = Time.time >= chaseStartTime + _wakeUpTime;

        if (wokenUp)
        {
            isWakingUp = false;
        }

        return wokenUp;
    }

    void Update()
    {
        time = Time.time;
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    void OnExitState()
    {
    }
}
