using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private GameManager manager;

    private TMP_Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.state != GameState.Redy )
        {
            scoreText.text = "Score:" + manager.count.ToString();
        }
        
    }
}
