using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelfLaunch : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    [SerializeField] private string findName = "spear";

    [SerializeField] private float aimDis = 10;
    [SerializeField] private float angle;

    private void Start()
    {
        float velocity = 0;
        NavMeshAgent navMA = target.GetComponent<NavMeshAgent>();
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance >= aimDis)
        {
            distance = Vector3.Distance(transform.position, target.position + target.forward * navMA.speed);
        }
        else
        {
            distance =  Vector3.Distance(transform.position, target.position);
        }
                
        velocity = distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / Physics.gravity.magnitude);
        float xVelocity = Mathf.Sqrt(velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float yVelocity = Mathf.Sqrt(velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject projectile = gameObject;

        projectile.transform.Rotate(-angle, projectile.transform.rotation.y, projectile.transform.rotation.z);

        Rigidbody rb = projectile.transform.Find(findName).gameObject.AddComponent<Rigidbody>();
        BottleSpin spin = projectile.transform.Find(findName).gameObject.GetComponent<BottleSpin>();
        if (spin)
        {
            spin.enabled = true;
        }
        rb.velocity = new Vector3(xVelocity * direction.x, yVelocity, xVelocity * direction.z);
    }
}
