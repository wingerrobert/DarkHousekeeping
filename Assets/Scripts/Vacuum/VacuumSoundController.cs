using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumSoundController : MonoBehaviour
{
    [HideInInspector] public bool isPlayingSuck = false;
    [HideInInspector] public bool isPlayingIntake = false;

    public List<AudioClip> vacuumSounds;

    AudioSource[] _audioSources;

    // Start is called before the first frame update
    void Start()
    {
        _audioSources = new AudioSource[2];
        _audioSources = GetComponents<AudioSource>();
    }

    public void StartSuckSound()
    {
        isPlayingSuck = true;
        _audioSources[0].clip = vacuumSounds[0];
        _audioSources[0].Play();
    }

    private void Update()
    {
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
        isPlayingSuck = false;
        _audioSources[0].Stop();
        _audioSources[0].loop = false;
        _audioSources[0].PlayOneShot(vacuumSounds[2]);
    }

    public void StartIntakeSound()
    {
        isPlayingIntake = true;
        _audioSources[1].Play();
    }

    public void StopIntakeSound()
    {
        isPlayingIntake = false;
        _audioSources[1].Stop();
    }
}
