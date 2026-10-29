using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    [SerializeField] private GameObject[] enemy;

    private float settime = 5f;

    //[SerializeField] private GameObject boss;

    [SerializeField] private GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
        
    }

    void Awake()
    {
        enemy = new GameObject[11];
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.state == GameState.Playing)
        {
            for(int i = 0; i < 10; i++)
            {
                if (enemy[i] == null)
                {
                    int randomIndex = Random.Range(0, prefabs.Length - 1);

                    float x = Random.Range(-30f, 30f);
                    float z = Random.Range(-30f, 30f);

                    Vector3 spawnPos = new Vector3(x, 0f, z);

                    enemy[i] = Instantiate(prefabs[randomIndex], spawnPos, Quaternion.identity);
                }
                
            }

            if (manager.gametime >= settime && enemy[10] == null)
            {
                float x = Random.Range(-30f, 30f);
                float z = Random.Range(-30f, 30f);

                Vector3 spawnPos = new Vector3(x, 0f, z);

                enemy[10] = Instantiate(prefabs[2], spawnPos, Quaternion.identity);

                manager.BossHP = 50;

                settime += 25f;
                Debug.Log($"時間{settime}");
            }
        }

        if (manager.state == GameState.Finish)
        {
            for(int i = 0; i < 11; i++)
            {
                Destroy(enemy[i]);
                
            }

        }
    }
}
