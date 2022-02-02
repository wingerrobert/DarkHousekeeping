using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumSoundController : MonoBehaviour
{
    [HideInInspector] public bool isPlayingSuck = false;
    [HideInInspector] public bool isPlayingIntake = false;

    float _initialPitch;

    public List<AudioClip> vacuumSounds;

    AudioSource[] _audioSources;

    bool _isPaused;

    // Start is called before the first frame update
    void Start()
    {
        _audioSources = new AudioSource[2];
        _audioSources = GetComponents<AudioSource>();
        _initialPitch = _audioSources[0].pitch;
    }

    public void StartSuckSound()
    {
        if (_isPaused)
        {
            return;
        }

        isPlayingSuck = true;
        _audioSources[0].clip = vacuumSounds[0];
        _audioSources[0].Play();
    }

    private void Update()
    {
        if (_isPaused = Time.timeScale == 0)
        {
            return;
        }

        if (!isPlayingSuck) 
        {
            return;
        }

        if (_audioSources[0].clip == vacuumSounds[0]) 
        {
            if (_audioSources[0].time > vacuumSounds[0].length - 0.1f)
            {
                _audioSources[0].clip = vacuumSounds[1];
                _audioSources[0].loop = true;
                _audioSources[0].Play();
            }
        }
    }

    public void StopSuckSound()
    {
        if (_isPaused)
        {
            return;
        }
        isPlayingSuck = false;
        _audioSources[0].Stop();
        _audioSources[0].loop = false;
        _audioSources[0].PlayOneShot(vacuumSounds[2]);
    }

    public void PlayShootSound(float pitch = 1.0f)
    {
        if (_isPaused)
        {
            return;
        }
        _audioSources[0].pitch = pitch;
        _audioSources[0].PlayOneShot(vacuumSounds[3]);
        _audioSources[0].pitch = _initialPitch;
    }

    public void StartIntakeSound()
    {
        if (_isPaused)
        {
            return;
        }
        isPlayingIntake = true;
        _audioSources[1].Play();
    }

    public void StopIntakeSound()
    {
        if (_isPaused)
        {
            return;
        }
        isPlayingIntake = false;
        _audioSources[1].Stop();
    }
}
