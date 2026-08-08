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
        GameObject bullet = Instantiate(Bullet, Bullet.transform.position, Bullet.transform.rotation);
        bullet.SetActive(true);
    }
}
