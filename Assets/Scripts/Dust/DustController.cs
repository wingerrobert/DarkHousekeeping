using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DustController : MonoBehaviour
{
    public float destroyDistance = 0.05f;

    ParticleSystem[] _particleSystems = new ParticleSystem[GlobalValues.MAX_PARTICLE_SYSTEMS];

    int _systemIndex = 0;
    int _totalParticles = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Populate particle systems array with systems tagged "Dust"
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(GlobalValues.TagValues[GlobalValues.Tags.Dust])) 
        {
            if (_systemIndex < GlobalValues.MAX_PARTICLE_SYSTEMS) 
            {
                ParticleSystem system = obj.gameObject.GetComponent<ParticleSystem>();
                _totalParticles += system.emission.GetBurst(0).maxCount;
                _particleSystems[_systemIndex++] = system;
            }
        }
    }

    private void UpdateParticles() 
    {
        foreach (GameObject _suctionObject in GameObject.FindGameObjectsWithTag(GlobalValues.TagValues[GlobalValues.Tags.Suction]))
        {
            VacuumController _targetVacuum;
            _targetVacuum = _suctionObject.GetComponentInParent<VacuumController>();
            
            if (_targetVacuum == null)
            {
                return;
            }

            if (!_targetVacuum.isSucking)
            {
                return;
            }

            Transform _targetTransform = _suctionObject.transform;

            // Iterate and set velocity of particles towards target
            for (int i = 0; i < _systemIndex; i++)
            {
                ParticleSystem system = _particleSystems[i];
                
                if (system != null)
                {
                    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[system.particleCount];
                    system.GetParticles(particles);

                    for (int k = 0; k < system.particleCount; k++)
                    {
                        float distanceToTarget = Vector3.Distance(particles[k].position, _targetTransform.position);

                        // If distance to target is too close, remove particle
                        if (distanceToTarget > destroyDistance)
                        {
                            // Do not attract if greater than max distance
                            if (distanceToTarget < _targetVacuum.suctionDistance)
                            {
                                // Attract particles based on distance from player
                                particles[k].position = Vector3.MoveTowards(particles[k].position, _targetTransform.position, _targetVacuum.suctionStrength * Time.fixedDeltaTime * (1 / ((distanceToTarget * distanceToTarget) + 0.1f)));
                            }
                        }
                        else
                        {
                            _targetVacuum.StartIntaking();
                            particles[k].remainingLifetime = 0;
                            _totalParticles -= 1;
                            Debug.Log(_totalParticles);
                        }
                    }
                    particles = particles.Where(p => p.remainingLifetime > 0).ToArray();
                    system.SetParticles(particles);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateParticles();
    }
}
