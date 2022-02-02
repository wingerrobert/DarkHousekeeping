using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class MopController : MonoBehaviour
{
    public float attackRange = 6.0f;
    public float cleanSpeed = 10.0f;

    [SerializeField] ParticleSystem _particleSystem;

    CursorController _cursor;
    Ray _ray;

    // Start is called before the first frame update
    void Start()
    {
        _cursor = GameObject.FindGameObjectWithTag(GlobalValues.TagValues[GlobalValues.Tags.CursorController]).GetComponent<CursorController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMopTarget();
    }

    void CheckMopTarget()
    {
        _ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        LayerMask layerMask = LayerMask.GetMask(GlobalValues.LayerValues[GlobalValues.Layers.GrimeDecal]);

        if (Physics.Raycast(_ray, out hit, attackRange, layerMask))
        {
            _cursor.setCursorValue(GlobalValues.CursorValue.Active);

            if (Input.GetKey(KeyCode.Mouse0))
            {
                MopUpGrime(hit.collider.gameObject);
            }
        }
        else
        {
            _cursor.setCursorValue(GlobalValues.CursorValue.Idle);
        }
    }

    void MopUpGrime(GameObject target)
    {
        DecalProjector projector = target.GetComponent<DecalProjector>();

        if (projector.fadeFactor > 0)
        {
            projector.fadeFactor -= cleanSpeed * Time.deltaTime;
        }
        else
        {
            SpawnCleanParticles();
            Destroy(target);
        }
    }

    void SpawnCleanParticles()
    {
        _particleSystem.Play();
    }
}
