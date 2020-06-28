using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    TextMeshProUGUI ammoAmount;
    [SerializeField] Ammotype ammotype;
    Ammo ammo;

    // Start is called before the first frame update
    void Start()
    {
        ammoAmount = GetComponent<TextMeshProUGUI>();
        ammo = FindObjectOfType<Ammo>();
        ammoAmount.text = ammo.GetAmmoUI(ammotype).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ammoAmount.text = ammo.GetAmmoUI(ammotype).ToString();
    }
}
