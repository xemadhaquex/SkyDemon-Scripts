using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxeRecallTracker : MonoBehaviour
{
    GameObject Player;
    public TextMeshProUGUI throwsRemainingText;
    int throwsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        throwsRemaining=Player.GetComponent<Raycast_AxeThrow>().throwsRemaining;
        throwsRemainingText.text=throwsRemaining.ToString();
    }
}
