using System.Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameState state;

    private int count;


    [SerializeField] GameObject start;

    public void PlusCount()
    {
        count++;
        Debug.Log($"{count}");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = GameState.Redy;
    }

    // Update is called once per frame
    void Update()
    {
        if (start == null && state != GameState.Playing)
        {
            state = GameState.Playing;
            Debug.Log("スタート");
        }

        if (state == GameState.Playing)
        {
            Debug.Log("プレイ中");
        }
    }
}

public enum GameState
{
        Redy,
        Playing,
        Finish
}
