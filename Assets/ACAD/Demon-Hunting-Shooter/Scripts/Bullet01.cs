using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet01 : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

        rb.AddForce(this.transform.forward*40, ForceMode.Impulse); 
    }
    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = new Vector3(0,0,10);  
        
    }

    void OnCollisionEnter(Collision other) {
       //Destroy(this.gameObject);
       if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject oni = other.gameObject;

            if (oni != null)
            {
                Destroy(oni); 
                manager.PlusCount();
            }
        }

        if (other.gameObject.name == "Start")
        {
            GameObject start = other.gameObject;

            if (start != null)
            {
                Destroy(start); 
                manager.state = GameState.Playing;
            }
        }
       
       
    }
    
}
