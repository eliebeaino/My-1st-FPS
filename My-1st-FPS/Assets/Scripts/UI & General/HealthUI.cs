using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    [SerializeField] Image healthBar;
    float initialHealth;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        initialHealth = player.PlayerCurrentHealthLevel();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = player.PlayerCurrentHealthLevel() / initialHealth;
    }
}
