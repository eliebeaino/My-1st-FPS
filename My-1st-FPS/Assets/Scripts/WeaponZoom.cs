using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] Animator wpnAnimator;
    [SerializeField] RigidbodyFirstPersonController controller;
    [SerializeField] float sensitivityZoomed = 0.5f;
    float sensitivityDefault;
    bool zoomInToggle = false;

    [SerializeField] float zoomedOutFOV =60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomSpeed = 0.5f;
    float t;

    private void Start()
    {
        sensitivityDefault = controller.mouseLook.XSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
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
    }

    IEnumerator ZoomIn()
    {
            while (fpsCamera.fieldOfView > zoomedInFOV)
            {
                t += Time.deltaTime * zoomSpeed;
                fpsCamera.fieldOfView = Mathf.Lerp(zoomedOutFOV, zoomedInFOV, t);
                yield return null;
            }
            t = 0;
    }
    IEnumerator ZoomOut()
    {
        while (fpsCamera.fieldOfView < zoomedOutFOV)
        {
            t += Time.deltaTime * zoomSpeed;
            fpsCamera.fieldOfView = Mathf.Lerp(zoomedInFOV, zoomedOutFOV, t);
            yield return null;
        }
        t = 0;
    }
}
