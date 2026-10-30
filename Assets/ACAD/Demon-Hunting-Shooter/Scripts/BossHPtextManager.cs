using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHPtextManager : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    
    private TMP_Text BossHPText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BossHPText = GetComponent<TMP_Text>();
        BossHPText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.enemy[10] != null)
        {
            BossHPText.text = "Boss HP:" + manager.BossHP.ToString();

            //if (manager.BossHP == 0 && manager.bosstime - manager.gametime <= 1f)
            //{
                //BossHPText.text = "Boss HP: 0";
            //}
        }
        else
        {
            BossHPText.text = "";
        }

    }
}

