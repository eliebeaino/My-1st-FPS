using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [Header("Storing Components")]
    [SerializeField] Camera fpsCamera;
    Animator animator;
    [SerializeField] RigidbodyFirstPersonController controller;

    [Header("Zoom Propreties")]
    [SerializeField] float sensitivityZoomed = 0.5f;
    float sensitivityDefault =2f;                               // mouse sensitivity default stored on start from player

    float zoomedOutFOV =60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomSpeed = 0.5f;
    [SerializeField] float changeZoomDelay = 1f;
    float t;                                                    // interpolation value for zoom value used with zoom speed and delta time

    bool zoomInToggle = false;                                  // store zoom state in/out
    bool zooming = false;                                       // are we currently zooming ?


    private void Start()
    {
        sensitivityDefault = controller.mouseLook.XSensitivity;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !zooming)
        {
            Zoom();
        }
    }

    // change zoom states and bool values, change the sensitivity depending on state, start zoom animations and zoom the camera.
    private void Zoom()
    {
        zooming = true;
        if (zoomInToggle == false)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    // toggle zoom - start animation - slow mouse speed
    private void ZoomIn()
    {
        zoomInToggle = true;
        animator.SetBool("Disable", false);
        animator.SetBool("ZoomedIn", true);
        controller.mouseLook.XSensitivity = sensitivityZoomed;
        controller.mouseLook.YSensitivity = sensitivityZoomed;
        StartCoroutine(SmoothZoomIn());
    }

    // toggle zoom off - revert back animation - reset mouse speed
    private void ZoomOut()
    {
        zoomInToggle = false;
        animator.SetBool("Disable", false);
        animator.SetBool("ZoomedIn", false);
        controller.mouseLook.XSensitivity = sensitivityDefault;
        controller.mouseLook.YSensitivity = sensitivityDefault;
        StartCoroutine(SmoothZoomOut());
    }

    // smoothly zoom in with adding delay to zoom out if spam clicking
    IEnumerator SmoothZoomIn()
    {
        while (fpsCamera.fieldOfView > zoomedInFOV)
        {
            t += Time.deltaTime * zoomSpeed;
            fpsCamera.fieldOfView = Mathf.Lerp(zoomedOutFOV, zoomedInFOV, t);
            yield return null;
        }
        yield return new WaitForSeconds(changeZoomDelay);
        if (fpsCamera.fieldOfView <= zoomedInFOV) zooming = false;
    }

    // smoothly zoom out with adding delay to zoom in if spam clicking
    IEnumerator SmoothZoomOut()
    {
        while (fpsCamera.fieldOfView < zoomedOutFOV)
        {
            
            t -= Time.deltaTime * zoomSpeed;
            fpsCamera.fieldOfView = Mathf.Lerp(zoomedOutFOV, zoomedInFOV, t);
            yield return null;
        }
        yield return new WaitForSeconds(changeZoomDelay);
        if (fpsCamera.fieldOfView >= zoomedOutFOV) zooming = false;
    }

    // resets position instantly (to avoid visual bug) - all parameter defaults (zooming/animation/sensitivity)
    private void OnDisable()
    {
        fpsCamera.fieldOfView = zoomedOutFOV;
        zooming = false;
        zoomInToggle = false;
        animator.SetBool("Disable", true);
        animator.SetBool("ZoomedIn", false);
        controller.mouseLook.XSensitivity = sensitivityDefault;
        controller.mouseLook.YSensitivity = sensitivityDefault;
    }
}
