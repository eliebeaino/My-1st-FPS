using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minAngle = 40f;
    [SerializeField] float maxAngle = 90f;
    [SerializeField] float maxIntensity = 8f;

    Light mylight;

    private void Start()
    {
        mylight = GetComponent<Light>();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    // decreases light intensity overtime
    private void DecreaseLightAngle()
    {
        if (mylight.spotAngle > minAngle)
        {
            mylight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }

    // decreases spot angle size overtime
    private void DecreaseLightIntensity()
    {
        if (mylight.intensity > 0)
        {
            mylight.intensity -= lightDecay * Time.deltaTime;
        }
    }

    // replenishes intensity and spot light upon pickup
    public void RestoreLight(float restoreAngle, float restoreIntensity)
    {
        mylight.intensity = Mathf.Clamp(mylight.intensity + restoreIntensity ,0, maxIntensity);
        mylight.spotAngle = Mathf.Clamp(mylight.spotAngle + restoreAngle ,minAngle , maxAngle);
    }
}
