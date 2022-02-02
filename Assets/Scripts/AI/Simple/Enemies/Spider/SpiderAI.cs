using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    public GameObject eyes;

    NavMeshAgent _navMeshAgent;
    GameObject _player;

    float _jitterRange = 1.5f;
    float _eyeFadeSpeed = 1.0f;
    float _currentIntensity = 1.5f;

    [SerializeField] private bool _isDead = false;

    Animator _animator;
    Material _eyesMaterial;
    Vector4 _emissionColor;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag(GlobalValues.TagValues[GlobalValues.Tags.Player]);
        _animator = GetComponent<Animator>();
        _eyesMaterial = eyes.GetComponent<Renderer>().material;
        _emissionColor = _eyesMaterial.GetColor("_EmissiveColor");
    }
    public void SetDieAnimation()
    {
        _animator.SetTrigger("Die");
        _isDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Fade eyes out
        if (_isDead && _currentIntensity > 0.0f)
        {
            _currentIntensity -= Time.deltaTime * _eyeFadeSpeed;
            _eyesMaterial.SetColor("_EmissiveColor", _emissionColor * _currentIntensity);
        }
        if (_navMeshAgent.enabled)
        {
            _navMeshAgent.SetDestination(_player.transform.position + new Vector3(0.0f, Random.Range(-_jitterRange, _jitterRange), 0.0f));
        }
    }
}
