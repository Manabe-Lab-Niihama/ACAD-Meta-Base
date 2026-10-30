using System.Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameState state;

    [SerializeField] public GameObject[] enemy;

    public int count;

    private float starttime;

    private float nowtime;

    public float gametime = 0f;

    //public float bosstime;

    public int BossHP;

    [SerializeField] GameObject start;

    public void OniCount()
    {
        count += 10;
        //Debug.Log($"{count}");
    }

    public void BossCount()
    {
        count += 50;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = GameState.Redy;

    }

    // Update is called once per frame
    void Update()
    {
        if (start == null && state == GameState.Redy)
        {
            state = GameState.Playing;
            Debug.Log("スタート");
            starttime = Time.time;
            //Debug.Log($"{starttime:F2}");

        }

        if (state == GameState.Playing)
        {
            nowtime = Time.time;
            gametime = nowtime-starttime;
            if(nowtime-starttime >= 60)
            {
                state = GameState.Finish;
                Debug.Log("フィニッシュ");
                //Debug.Log($"{nowtime-starttime:F2}");
                Debug.Log($"Score:{count}");
                //Debug.Log($"{gametime:F2}");
            }
        }
    }
}

public enum GameState
{
        Redy,
        Playing,
        Finish
}
