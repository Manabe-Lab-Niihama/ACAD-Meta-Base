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
        scoreText.text = "Score:0";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:" + manager.count.ToString();
    }
}
