using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update

    public float health = 50f;
    [SerializeField] private float tpCooldown;

    private bool isDead = false;
    private float nextTpTime;
    private AINav navCode;
    private float maxHealth;

    private void Start()
    {
        if (transform.CompareTag("Blue"))
        {
            UnitCounter.i.AddBlue(1);
        }
        else if (transform.CompareTag("Red"))
        {
            UnitCounter.i.AddRed(1);
        }
        else if (transform.CompareTag("TestUnit"))
        {
            UnitCounter.i.AddTest(1);
        }
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
            else if(amount > 0)
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
        if (transform.CompareTag("Blue") && !isDead)
        {
            UnitCounter.i.AddBlue(-1);
        }
        else if (transform.CompareTag("Red") && !isDead)
        {
            UnitCounter.i.AddRed(-1);
        }
        else if (transform.CompareTag("TestUnit") && !isDead)
        {
            UnitCounter.i.AddTest(-1);
        }
        isDead = true;
    }

}