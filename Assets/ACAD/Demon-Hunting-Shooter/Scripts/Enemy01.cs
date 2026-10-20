using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float x = Random.Range(-8f, 8f);
        float z = Random.Range(-8f, 8f);

        Vector3 spawnPos = new Vector3(x, 0f, z);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
