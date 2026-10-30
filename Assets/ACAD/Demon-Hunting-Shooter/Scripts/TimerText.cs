using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    
    private TMP_Text timerText;

    private float timeleft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = GetComponent<TMP_Text>();
        timerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.state == GameState.Playing)
        {
            timeleft = 60f - manager.gametime;
            timerText.text = "" + timeleft.ToString("F1");
        }
        else if(manager.state == GameState.Finish)
        {
            timerText.text = "Finish";
        }
    }
}
