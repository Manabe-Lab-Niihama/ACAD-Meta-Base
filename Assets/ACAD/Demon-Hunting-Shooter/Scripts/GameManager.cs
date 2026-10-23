using System.Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameState state;

    private int count;

    public void PlusCount()
    {
        count++;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = GameState.Redy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum GameState
{
        Redy,
        Playing,
        Finish
}
