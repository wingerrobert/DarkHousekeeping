using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSway : MonoBehaviour
{
    public float swayAmount = 0.01f;
    public float swayMax = 0.1f;
    public float smoothAmount = 6.0f;

    Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float movementX = Mathf.Clamp(-mouseX * swayAmount, -swayMax, swayMax);
        float movementY = Mathf.Clamp(-mouseY * swayAmount, -swayMax, swayMax);

        Vector3 targetPosition = new Vector3(movementX, movementY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition + _initialPosition, Time.deltaTime * smoothAmount);
    }
}
