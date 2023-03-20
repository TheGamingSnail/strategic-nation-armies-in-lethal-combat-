using UnityEngine;
using UnityEngine.AI;

public class ProjectileAimTest : MonoBehaviour
{
    public Transform target;
    public float angle;
    public GameObject projectilePrefab;

    public void FireLauncher()
    {
        NavMeshAgent navMA = target.GetComponent<NavMeshAgent>();
        float distance = Vector3.Distance(transform.position, target.position + target.forward * navMA.speed);

        float velocity = distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / Physics.gravity.magnitude);

        float xVelocity = Mathf.Sqrt(velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float yVelocity = Mathf.Sqrt(velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject projectile = Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.03f, 0), rotation);

        projectile.transform.Rotate(-angle, projectile.transform.rotation.y, projectile.transform.rotation.z);

        Rigidbody rb = projectile.transform.Find("complexity").Find("boom rocket").GetComponent<Rigidbody>();
        rb.velocity = new Vector3(xVelocity * direction.x, yVelocity, xVelocity * direction.z);
    }
}
