using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submersible_Controller : MonoBehaviour
{
    [Tooltip("Speed of the submersible transform movements")]
    [SerializeField] private float throttleSpeed;
    [Tooltip("Speed of the submersible rotation movements")]
    [SerializeField] private float rotationSpeed;
    [Tooltip("Maximum angle of the submersible rotation movements")]
    [SerializeField] private float maximumRotationAngle;

    private float _forwardBackwardValue;
    private float _leftRightValue;
    private float _upDownValue;

    private float _pitch;
    private float _yaw;
    private float _roll;

    private bool _isMoving;

    private Quaternion _initialRotation;
    private Quaternion _targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        _initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        TransformMovements();
        CheckMovementUpdate();
        RotationMovements();
    }

    private void TransformMovements()
    {
        _forwardBackwardValue = Input.GetAxis("ForwardBackward");
        _leftRightValue = Input.GetAxis("LeftRight");
        _upDownValue = Input.GetAxis("UpDown");
        Vector3 direction = new Vector3(_leftRightValue, _upDownValue, _forwardBackwardValue);
        Vector3 movement = direction * throttleSpeed * Time.deltaTime;

        transform.Translate(movement, Space.World);
    }

    private void RotationMovements()
    {
        if (_isMoving)
        {
            _targetRotation = _initialRotation * Quaternion.Euler(_pitch, _yaw, _roll);
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, _initialRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckMovementUpdate()
    {
        if (_forwardBackwardValue > 0f)
        {
            _isMoving = true;
            _pitch = maximumRotationAngle;
        }
        else if (_forwardBackwardValue < 0f)
        {
            _pitch = -maximumRotationAngle;
            _isMoving = true;
        }

        if (_leftRightValue > 0f)
        {
            _isMoving = true;
            _roll = -maximumRotationAngle;
        }
        else if (_leftRightValue < 0f)
        {
            _roll = maximumRotationAngle;
            _isMoving = true;
        }
        else if (_forwardBackwardValue == 0 && _leftRightValue == 0)
        {
            _roll = 0f;
            _pitch = 0f;
            _isMoving = false;
        }
    }


}
