using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    public float suctionDistance = 0.5f;
    public float suctionStrength = 1f;
    public float initialIntakeTime = 1.0f;  // The time it takes for dust intake sounds to stop
    
    [HideInInspector] public bool isSucking = false;
    [HideInInspector] public bool isIntaking = false;

    float _intakeTime;

    VacuumSoundController _soundController;

    private void Start()
    {
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
