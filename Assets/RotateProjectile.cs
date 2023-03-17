using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateProjectile : MonoBehaviour
{
    private Quaternion initialRotation;
    private Rigidbody rb;
    [SerializeField] private float xAngle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialRotation = Quaternion.Euler(xAngle, transform.rotation.y, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {   
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (rb)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity) * initialRotation;
        }
        
    }
}
