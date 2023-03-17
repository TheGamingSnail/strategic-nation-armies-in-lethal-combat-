using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private float damage;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision col)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        gameObject.GetComponent<RotateProjectile>().enabled = false;
        if (col.gameObject.GetComponent<Target>())
        {
            col.gameObject.GetComponent<Target>().TakeDamage(damage);
        }
        if (col.transform.name != "Player")
        {
            transform.parent = col.transform;
            if (col.transform.name == "shield")
            {
                transform.parent = col.transform.parent;
            }
        }

        this.enabled = false;
        
    }
}
