using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    [SerializeField] private GameObject[] enemy;

    [SerializeField] private GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
        
    }

    void Awake()
    {
        enemy = new GameObject[10];
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
                    int randomIndex = Random.Range(0, prefabs.Length);

                    float x = Random.Range(-8f, 8f);
                    float z = Random.Range(-8f, 8f);

                    Vector3 spawnPos = new Vector3(x, 0f, z);

                    enemy[i] = Instantiate(prefabs[randomIndex], spawnPos, Quaternion.identity);
                }
                
            }
        }
    }
}
