using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class JuicerStateController : MonoBehaviour
{
    public State currentState;
    public State remainState;

    public static bool playerInventoryHasInit = false;

    [Range(0.0f, 100.0f)] public float listenSensitivity = 10.0f;
    [Range(0.0f, 100.0f)] public float listenDistance = 10.0f;
    
    public AnimationClip chaseClip;
    public AnimationClip squirtClip;
    public AnimationClip damageClip;

    public GameObject juicerGrubPrefab;
    public GameObject pusEmitter;

    [HideInInspector] public float time;

    [HideInInspector] public GameObject juicerObject;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public GameObject[] targets;
    [HideInInspector] public GameObject currentTarget;
    [HideInInspector] public Camera currentTargetEyes;

    [HideInInspector] public PlayerInventory currentTargetInventory;
    [HideInInspector] public PlayerMovement currentTargetMovement;
    [HideInInspector] public ArmsAnimation currentTargetArmsAnimation;

    [HideInInspector] public Vector3 initialPosition;

    [HideInInspector] public bool isChaseTargetSet = false;
    [HideInInspector] public bool isWakingUp = false;
    [HideInInspector] public bool isSquirting = false;
    [HideInInspector] public bool isBeingDamaged = false;

    [HideInInspector] public float chaseStartTime;

    [HideInInspector] public Animator animator;

    bool playerInventorySet = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        juicerObject = gameObject;

        initialPosition = juicerObject.transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag(GlobalValues.TagValues[GlobalValues.Tags.Player]);

        currentTarget = targets[0];

        currentTargetEyes = currentTarget.GetComponentInChildren<Camera>();

        currentTargetArmsAnimation = currentTarget.GetComponentInChildren<ArmsAnimation>();
        currentTargetMovement = currentTarget.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //if (playerInventoryHasInit)
        //{
        //    if (!playerInventorySet)
        //    {
        //        currentTargetInventory = currentTarget.GetComponentInChildren<PlayerInventory>();
        //        playerInventorySet = true;
        //    }
        //    time = Time.time;
            currentState.UpdateState(this);
        //}    
        
    }

    public void EmitPus()
    {
        Vector3 juicerPosition = transform.position;
        GameObject pusEmitterInstance = Instantiate(pusEmitter);
        pusEmitterInstance.transform.position = new Vector3(juicerPosition.x, juicerPosition.y + 0.5f, juicerPosition.z);
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
