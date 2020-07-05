using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightPickUP : MonoBehaviour
{
    [SerializeField] int lightIntensity = 2;
    [SerializeField] int spotAngle = 20;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponentInChildren<Flashlight>().RestoreLight(spotAngle, lightIntensity);
            Destroy(gameObject);
        }
    }
}
