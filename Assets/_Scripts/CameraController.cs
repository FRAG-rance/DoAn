using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float cameraSpeed = 5f;
    [SerializeField] InputAction _action;
    private Vector3 direction;
    private bool smoothMovement;
    private Vector3 currentVelocity;

    [SerializeField] private float smoothTime = 0.1f;

    // Update is called once per frame
    void Update()
    {
        direction = _action.ReadValue<Vector3>();
        MoveCamera();
    }

    private void OnEnable()
    {
        _action?.Enable();
    }

    private void OnDisable()
    {
       _action?.Disable();
    }

    private Vector3 HandleDirectionsVector(Vector3 direction)
    {
        return new Vector3(direction.x, 0, direction.y);
    }

    private void MoveCamera()
    {
        if (direction == Vector3.zero)
            return;

        Vector3 normalizedDirection = HandleDirectionsVector(direction);

        Vector3 targetPosition = _camera.transform.position + normalizedDirection * cameraSpeed * Time.deltaTime;
        if (smoothMovement)
        {
            _camera.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        }
        else
        {
            _camera.transform.position = targetPosition;
        }
    }
}
