using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionEff;
    [SerializeField] private float force;
    [SerializeField] private float radius;
    [SerializeField] private float maxDamage;
    [SerializeField] private float minDamage;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource source;
    private bool canExplode = true;

    // Start is called before the first frame update
    
    private void OnCollisionEnter(Collision collision)
    {
        if (canExplode)
        {
            GameObject obj = Instantiate(ExplosionEff);
            obj.transform.position = transform.position;
            Explode();
            gameObject.GetComponent<Renderer>().enabled = false; 
            canExplode = false;
            Destroy(obj, 1f);
            Destroy(gameObject, 1f);
           
        }
        
    }

    private void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, explosionPos, radius, 3.0F);
            }

            Target target = hit.transform.gameObject.GetComponent<Target>();
            if (target != null)
            {
                float distance = Vector3.Distance(explosionPos, target.transform.position);
                float damage = Mathf.Lerp(maxDamage, minDamage, distance / radius);
                Debug.Log(damage);
                target.TakeDamage(damage);
            }
        }
        source.PlayOneShot(clip);
    }
}
