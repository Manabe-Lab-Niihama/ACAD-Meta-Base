using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;


    private float settime = 5f;

    private float settime02 = 20f;

    //[SerializeField] private GameObject boss;

    [SerializeField] private GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
        
    }

    void Awake()
    {
        manager.enemy = new GameObject[32];
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.state == GameState.Playing)
        {
            for(int i = 0; i < 20; i++)
            {
                if (manager.enemy[i] == null)
                {
                    int randomIndex = Random.Range(0, prefabs.Length - 1);

                    float x = Random.Range(-135f, 135f);
                    float z = Random.Range(-80f, 115f);

                    Vector3 spawnPos = new Vector3(x, 0f, z);

                    manager.enemy[i] = Instantiate(prefabs[randomIndex], spawnPos, Quaternion.identity);
                }
                
            }

            for(int i = 20; i < 30; i++)
            {
                if (manager.enemy[i] == null)
                {
                    int randomIndex = Random.Range(0, prefabs.Length - 1);

                    float x = Random.Range(-120f, -80f);
                    float z = Random.Range(-140f, -20f);

                    Vector3 spawnPos = new Vector3(x, 0f, z);

                    manager.enemy[i] = Instantiate(prefabs[randomIndex], spawnPos, Quaternion.identity);
                }
                
            }

            if (manager.gametime >= settime && manager.enemy[30] == null)
            {
                float x = Random.Range(-135f, 135f);
                float z = Random.Range(-80f, 115f);

                Vector3 spawnPos = new Vector3(x, 0f, z);

                manager.enemy[10] = Instantiate(prefabs[2], spawnPos, Quaternion.identity);

                manager.BossHP = 50;

                settime += 25f;
                //Debug.Log($"時間{settime}");
            }

            if (manager.gametime >= settime02 && manager.enemy[31] == null)
            {
                float x = Random.Range(-120f, -80f);
                float z = Random.Range(-140f, -20f);

                Vector3 spawnPos = new Vector3(x, 0f, z);

                manager.enemy[10] = Instantiate(prefabs[2], spawnPos, Quaternion.identity);

                manager.BossHP = 50;

                settime02 += 20f;
                //Debug.Log($"時間{settime}");
            }
        }

        if (manager.state == GameState.Finish)
        {
            for(int i = 0; i < 32; i++)
            {
                Destroy(manager.enemy[i]);
                
            }

        }
    }
}
