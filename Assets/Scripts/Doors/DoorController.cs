using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openSpeed = 200.0f;
    public float openAngle = 115.0f;

    [SerializeField] Transform _doorTransform;

    Coroutine runningRoutine;
    Quaternion _initRotation;
    float _targetOpenRotation;

    bool _doorOpening = false;

    // Start is called before the first frame update
    void Start()
    {
        _initRotation = _doorTransform.localRotation;
        _targetOpenRotation = 360.0f - openAngle;
    }

    public bool GetIsDoorOpening()
    {
        return _doorOpening;
    }

    public void OpenDoor()
    {
        if (runningRoutine != null)
        {
            StopCoroutine(runningRoutine);
        }

        _doorOpening = true;
        runningRoutine = StartCoroutine("TickDoor", _doorOpening);
    }

    public void CloseDoor()
    {
        if (runningRoutine != null)
        {
            StopCoroutine(runningRoutine);
        }

        _doorOpening = false;
        runningRoutine = StartCoroutine("TickDoor", _doorOpening);
    }

    IEnumerator TickDoor(bool isOpening)
    {
        if (isOpening)
        {
            while (_doorTransform.localRotation.eulerAngles.y > _targetOpenRotation || _doorTransform.localRotation.eulerAngles.y == 0)
            {
                _doorTransform.Rotate(new Vector3(0, -Time.deltaTime * openSpeed, 0));
                yield return null;
            }
            _doorTransform.localRotation = Quaternion.Euler(new Vector3(_doorTransform.localRotation.eulerAngles.x, _targetOpenRotation, _doorTransform.localRotation.eulerAngles.z));
        }
        else
        {
            while (_doorTransform.localRotation.eulerAngles.y >= _targetOpenRotation)
            {
                _doorTransform.Rotate(new Vector3(0, Time.deltaTime * openSpeed, 0));
                yield return null;
            }
            _doorTransform.localRotation = _initRotation;
        }
    }
}
