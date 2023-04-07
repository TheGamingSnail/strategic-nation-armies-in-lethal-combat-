using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private float damage;
    public Transform thrower;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {

            if (col.transform != thrower)
            {
                if (col.gameObject.layer != LayerMask.NameToLayer("Blueprints"))
                {
                    Rigidbody rb = GetComponent<Rigidbody>();
                    rb.isKinematic = true;
                    gameObject.GetComponent<BoxCollider>().isTrigger = false;
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

                    Destroy(this);
            
                }
            }
    }
}
