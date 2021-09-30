using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Range(0.0f, 100.0f)] public float flickerIntensity = 10.0f;
    [Range(0.0f, 100.0f)] public float flickerMaxIntensity = 5.0f;
    [Range(0.0f, 100.0f)] public float flickerMinIntensity = 0.0f;
    [Range(0.0f, 50.0f)] public float flickerRate = 10.0f;

    float _startItensity;

    Light _light;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        _startItensity = _light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        float random = Random.Range(0.0f, 1.0f);

        _light.intensity = Mathf.Clamp((_startItensity + Mathf.Sin(Time.time * flickerRate) * flickerIntensity) + (Mathf.Sin(Time.time / flickerRate)), flickerMinIntensity, flickerMaxIntensity);
    }
}
