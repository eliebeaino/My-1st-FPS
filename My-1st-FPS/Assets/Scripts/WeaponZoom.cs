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

    //[SerializeField] float zoomedOutFOV;
    //[SerializeField] float zoomedInFOV = 20f;

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
            } 
            else
            {
                zoomInToggle = false;
                wpnAnimator.SetBool("ZoomedIn", false);
                controller.mouseLook.XSensitivity = sensitivityDefault;
                controller.mouseLook.YSensitivity = sensitivityDefault;
            }
        }
    }
}
