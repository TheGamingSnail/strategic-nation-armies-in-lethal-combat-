using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelfLaunch : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    [SerializeField] private float angle;

    private void Start()
    {
        NavMeshAgent navMA = target.GetComponent<NavMeshAgent>();
        float distance = Vector3.Distance(transform.position, target.position + target.forward * navMA.speed);

        float velocity = distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / Physics.gravity.magnitude);

        float xVelocity = Mathf.Sqrt(velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float yVelocity = Mathf.Sqrt(velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject projectile = gameObject;

        projectile.transform.Rotate(-angle, projectile.transform.rotation.y, projectile.transform.rotation.z);

        Rigidbody rb = projectile.transform.Find("spear").gameObject.AddComponent<Rigidbody>();
        rb.velocity = new Vector3(xVelocity * direction.x, yVelocity, xVelocity * direction.z);
    }
}
