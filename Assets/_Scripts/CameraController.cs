using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float fastMovespeed;
    [SerializeField] private float normalMovespeed;
    [SerializeField] private float stormMovespeed;

    [SerializeField] private float cameraMovespeed;
    [SerializeField] private float movementTime;
    [SerializeField] private float rotationAmount;

    [SerializeField] private Image flash;

    public Quaternion newRotation;
    public Vector3 newPosition;
    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;


    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

    }

    void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) { 
            cameraMovespeed = fastMovespeed;
        } else
        {
            cameraMovespeed = normalMovespeed;
        }
        
        if(Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * cameraMovespeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -cameraMovespeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -cameraMovespeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * cameraMovespeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }

    public IEnumerator HandleStormCameraPhysics(Vector3 direction, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            if (direction == Vector3.right)
            {
                newPosition += (transform.right * stormMovespeed);
            }
            else if (direction == Vector3.left)
            {
                newPosition += (transform.right * -stormMovespeed);
            }
            else if (direction == Vector3.forward)
            {
                newPosition += (transform.forward * -stormMovespeed);
            }
            else if (direction == Vector3.back)
            {
                newPosition += (transform.forward * stormMovespeed);
            }
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void Flash(float duration, float minAlpha, float maxAlpha)
    {
        if (duration <= 0) { return; }    // 0 speed wouldn't make sense

        StartCoroutine(CameraFlash(duration, minAlpha, maxAlpha));
    }

    IEnumerator CameraFlash(float secondsForOneFlash, float minAlpha, float maxAlpha)
    { 
        float flashInDuration = secondsForOneFlash / 2;
        float flashOutDuration = secondsForOneFlash / 2;
        //flash in
        for (float t = 0f; t <= flashInDuration; t += Time.deltaTime)
        {

            Color newColor = flash.color;
            newColor.a = Mathf.Lerp(minAlpha, maxAlpha, t / flashInDuration);
            flash.color = newColor;
            yield return null;
        }
        // flash out
        for (float t = 0f; t <= flashOutDuration; t += Time.deltaTime)
        {

            Color newColor = flash.color;
            newColor.a = Mathf.Lerp(maxAlpha, minAlpha, t / flashOutDuration);
            flash.color = newColor;
            yield return null;
        }
        SetAlphaToDefault();
    }
    private void SetAlphaToDefault()
    {
        Color newColor = flash.color;
        newColor.a = 0;
        flash.color = newColor;
    }
}
