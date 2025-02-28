﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* RUSHED
 * WIP, CLEAN UP
 * Middle mouse click drag
 * Space bar return to center
 */


public class CameraControl : MonoBehaviour 
{
    public float cameraDragSpeed = 1.0f;
    public float weight = 20.0f;
    public Vector2 clampPosition = new Vector2(10.0f, 10.0f);

    private Camera mainCamera;
    private MouseInput mouseInput;
    private KeyboardInput keyboardInput;

    private bool startDragging = false;
    private Vector2 mouseStartPosition;
    private Vector3 cameraStartPosition;
    private Vector3 targetPosition;

    private void Awake() {
        mouseInput = new MouseInput();
        keyboardInput = new KeyboardInput();
        mainCamera = GetComponent<Camera>();
    }

    private void OnEnable() {
        mouseInput.Enable();
        keyboardInput.Enable();
    }

    private void OnDisable() {
        mouseInput.Disable();
        keyboardInput.Disable();
    }

    private void Start() {
        targetPosition = mainCamera.transform.position;
        mouseInput.Mouse.MouseMiddleClick.performed += _ => GetStartingPosition();
        mouseInput.Mouse.MouseMiddleRelease.performed += _ => MouseMiddleReleased();
        keyboardInput.Keyboard.SpaceBar.performed += _ => ReturnToCenterPosition();
    }

    private void Update() {
        if (startDragging) {
            Vector2 mouseCurrent = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
            Vector3 relativePosition = (mouseStartPosition - mouseCurrent) / weight;

            targetPosition = cameraStartPosition + relativePosition;
            targetPosition.x = Mathf.Clamp(targetPosition.x, -clampPosition.x, clampPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, -clampPosition.y, clampPosition.y);
        }

        transform.position = Vector3.Lerp(transform.position, 
            targetPosition, 
            Time.deltaTime * cameraDragSpeed);        

    }

    private void GetStartingPosition() {
        Vector2 mouseScreen = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        cameraStartPosition = mainCamera.transform.position;
        mouseStartPosition = mouseScreen;
        startDragging = true;
    }

    private void ReturnToCenterPosition() {
        targetPosition = new Vector3(0, 0, -10);
    }

    private void MouseMiddleReleased() {
        startDragging = false;
    }

    public void OnDestroy() {   
        mouseInput.Mouse.MouseMiddleClick.performed -= _ => GetStartingPosition();
        mouseInput.Mouse.MouseMiddleRelease.performed -= _ => MouseMiddleReleased();
        keyboardInput.Keyboard.SpaceBar.performed -= _ => ReturnToCenterPosition();
        keyboardInput.Disable();
        mouseInput.Disable();
    }
}
