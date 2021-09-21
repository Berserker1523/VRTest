using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    float speed=-2000f;
    // Update is called once per frame
    private void Start()
    {
        
    }
    void FixedUpdate()
    {
        
            rb.AddForce(0, 0, speed * Time.deltaTime);
       
        
    }
}
