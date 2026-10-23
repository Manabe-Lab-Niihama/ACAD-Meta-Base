using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;

    [SerializeField] private GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
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
                    float x = Random.Range(-8f, 8f);
                    float z = Random.Range(-8f, 8f);

                    Vector3 spawnPos = new Vector3(x, 0f, z);

                    enemy[i] = Instantiate(enemy[i], spawnPos, Quaternion.identity);
                }
                
            }
        }
    }
}
