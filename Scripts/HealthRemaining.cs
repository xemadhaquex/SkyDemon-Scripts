using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthRemaining : MonoBehaviour
{
    GameObject Player;
    public TextMeshProUGUI healthRemainingText;
    int healthRemaining;

    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        healthRemaining=Player.GetComponent<PlayerMovement>().health;
        healthRemainingText.text=healthRemaining.ToString();
    }
}
