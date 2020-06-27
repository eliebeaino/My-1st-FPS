using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [Header("Storing Components")]
    [SerializeField] Camera fpsCamera;
    [SerializeField] Animator wpnAnimator;
    [SerializeField] RigidbodyFirstPersonController controller;

    [Header("Zoom Propreties")]
    [SerializeField] float sensitivityZoomed = 0.5f;
    float sensitivityDefault;                                   // mouse sensitivity default stored on start from player

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
            zoomInToggle = true;
            wpnAnimator.SetBool("ZoomedIn", true);
            controller.mouseLook.XSensitivity = sensitivityZoomed;
            controller.mouseLook.YSensitivity = sensitivityZoomed;
            StartCoroutine(ZoomIn());
        }
        else
        {
            zoomInToggle = false;
            wpnAnimator.SetBool("ZoomedIn", false);
            controller.mouseLook.XSensitivity = sensitivityDefault;
            controller.mouseLook.YSensitivity = sensitivityDefault;
            StartCoroutine(ZoomOut());
        }
    }

    // smoothly zoom in with adding delay to zoom out if spam clicking
    IEnumerator ZoomIn()
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
    IEnumerator ZoomOut()
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
}
