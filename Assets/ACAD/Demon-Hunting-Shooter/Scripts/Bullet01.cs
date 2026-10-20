using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet01 : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

        rb.AddForce(this.transform.forward*60, ForceMode.Impulse); 
    }
    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = new Vector3(0,0,10);  
        
    }

    void OnCollisionEnter(Collision collision) {
       //Destroy(this.gameObject);
       GameObject oni = GameObject.Find("Capsule");
       if (oni != null)
        {
           Destroy(oni); 
        }
       
    }
    
}
