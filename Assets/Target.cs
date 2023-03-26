using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update

    public float health = 50f;
    [SerializeField] private float tpCooldown;
    private float nextTpTime;
    private AINav navCode;
    private float maxHealth;

    private void Start()
    {
        navCode = gameObject.GetComponent<AINav>();
        maxHealth = health;
    }
    public void TakeDamage (float amount)
    {
        if (navCode)
        {
            if (!navCode.canTeleport || nextTpTime > Time.time)
            {
                health -= amount;
                if (health <= 0f)
                {
                    Die();
                }
            }
            else
            {
                navCode.Teleport();
                nextTpTime = Time.time + tpCooldown;
            }
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    void Die()
    {
        if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
        {
            Destroy(gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>());
            Destroy(gameObject.GetComponent<AINav>());
            Destroy(gameObject.GetComponent<Target>());
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false; 
            Destroy(gameObject, 2f);
        }
        else 
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false; 
            rb.useGravity = true;
            Destroy(gameObject, 2f);
        }
    }

}