using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    ArmsAnimation _armsAnimation;

    public float suctionDistance = 0.5f;
    public float suctionStrength = 1f;
    public float initialIntakeTime = 1.0f;  // The time it takes for dust intake sounds to stop
    public float shootDelay = 1.0f;
    
    [HideInInspector] public bool isSucking = false;
    [HideInInspector] public bool isIntaking = false;

    float _intakeTime;
    float _previousShootTime = 0.0f;

    VacuumSoundController _soundController;

    private void Start()
    {
        _armsAnimation = GetComponentInParent<ArmsAnimation>();
        _intakeTime = initialIntakeTime;
        _soundController = GetComponent<VacuumSoundController>();
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
            if (Input.GetKey(KeyCode.Mouse1))
            {
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
}
