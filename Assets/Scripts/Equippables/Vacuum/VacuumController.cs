
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    ArmsAnimation _armsAnimation;

    public VacuumItemStats stats;
    
    public GameObject dustBall;
    public GameObject suctionLocation;

    public float minimumDustballSize = 0.25f;

    // How full the vacuum reservoir is (range of 0.0 - 1.0f)
    public float vacuumFill = 0.0f;
    public float initialIntakeTime = 1.0f;  // The time it takes for dust intake sounds to stop
    public float shootDelay = 1.0f;

    [HideInInspector] public GameObject[] reservoirUIElements;

    [HideInInspector] public bool isSucking = false;
    [HideInInspector] public bool isIntaking = false;

    float _intakeTime;
    float _previousShootTime = 0.0f;

    Vector3 _dustBallInitialScale;


    VacuumSoundController _soundController;

    private void OnEnable()
    {
        SetReservoirUIVisibility(true);
    }

    private void OnDisable()
    {
        SetReservoirUIVisibility(false);
    }

    private void Start()
    {
        _armsAnimation = GetComponentInParent<ArmsAnimation>();
        _intakeTime = initialIntakeTime;
        _soundController = GetComponent<VacuumSoundController>();
        _dustBallInitialScale = dustBall.transform.localScale;

        SetReservoirUIVisibility(false);
    }

    public void StartIntaking()
    {
        if (!_soundController.isPlayingIntake)
        {
            _soundController.StartIntakeSound();
        }
        isIntaking = true;
        _intakeTime = initialIntakeTime;
    }

    private void Update()
    {
        TickSucking();
        TickShooting();
    }

    private void TickShooting()
    {
        if (Time.time > shootDelay + _previousShootTime)
        {
            if (Input.GetKey(KeyCode.Mouse1) && vacuumFill > 0)
            {
                _soundController.PlayShootSound(Random.Range(0.5f, 1.0f));

                GameObject dustBallInstance = Instantiate(dustBall);

                dustBallInstance.transform.position = suctionLocation.transform.position;

                dustBallInstance.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * stats.shootForce);

                // Dust ball size is proportional to current vacuum fill
                Vector3 dustBallScale = _dustBallInitialScale * minimumDustballSize;

                if (vacuumFill > minimumDustballSize)
                {
                    dustBallScale = _dustBallInitialScale * vacuumFill;
                }

                dustBallInstance.transform.localScale = dustBallScale;

                // Empty vacuum after shooting dust ball
                vacuumFill = 0.0f;

                _armsAnimation.TriggerVacuumShooting();
                _previousShootTime = Time.time;
            }
        }
    }

    private void TickSucking()
    {
        if (_intakeTime > 0)
        {
            _intakeTime -= Time.deltaTime;
        }
        else
        {
            _soundController.StopIntakeSound();
            isIntaking = false;
            _intakeTime = 0;
        }

        isSucking = Input.GetMouseButton(0);

        _armsAnimation.SetSucking(isSucking);

        if (isSucking)
        {
            if (!_soundController.isPlayingSuck)
            {

                _soundController.StartSuckSound();
            }
        }
        else
        {
            if (_soundController.isPlayingSuck)
            {
                _soundController.StopSuckSound();
            }
        }
    }

    private void SetReservoirUIVisibility(bool visible)
    {
        if (reservoirUIElements.Length <= 0)
        {
            return;
        }
        foreach (GameObject uiElement in reservoirUIElements)
        {
            uiElement.SetActive(visible);
        }
    }
}
