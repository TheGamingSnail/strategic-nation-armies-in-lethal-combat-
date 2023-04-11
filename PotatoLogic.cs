using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PotatoLogic : MonoBehaviour
{
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    private float startSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startSpeed = rb.velocity.magnitude;
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        Target target = col.gameObject.GetComponent<Target>();
        if (target)
        {
            float damage = Mathf.Lerp(minDamage, maxDamage, rb.velocity.magnitude / startSpeed);
            target.TakeDamage(damage);
            Debug.Log(damage);
        }
    }
}
