using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Rigidbody rb; 
    public float force=20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.Rotate(0, 90, 90);
        Destroy(gameObject,5f);
        //rb.AddForce(transform.InverseTransformDirection(transform.forward)*force);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime*force);
        
    }
}
