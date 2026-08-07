using UnityEngine;

public class GunShot : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot() {
        Instantiate(Bullet, Vector3.zero, Quaternion.identity);
    }
}
