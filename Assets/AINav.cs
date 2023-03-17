using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINav : MonoBehaviour
{
    [SerializeField] private Transform player_;
    private bool foundEnemy;
    private bool attacking;
    [SerializeField] private bool isMelee = true;
    [SerializeField] private float range = 50f;
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float checkTime = 1f;
    [SerializeField] private float attDamage = 5f;
    [SerializeField] private float m_Speed;
    [SerializeField] private GameObject rpgFirePos;
    [SerializeField] private GameObject heldThrowable;
    [SerializeField] private GameObject throwablePrefab;
    [SerializeField] private Gun gunScript;
    [SerializeField] private bool fighting = false;
    [SerializeField] private GameObject tpPart;
    public bool canTeleport = false;
    private float time = 1f;
    private float time2 = 1f;
    private GameObject attBuilding;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Fight()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fighting = true;
        
    }

    // Update is called once per frame
    void navUpdate()
    {
        if (!attBuilding)
        {
            navMeshAgent.isStopped = false;
            attacking = false;
            if (FindClosestEnemy())
            {
                navMeshAgent.destination = FindClosestEnemy().transform.position;
            }
            
        }
        if (!foundEnemy && FindClosestEnemy())
        {
            if (isMelee || Vector3.Distance(transform.position, FindClosestEnemy().transform.position) > range)
            {
                navMeshAgent.isStopped = false;
                attacking = false;
                navMeshAgent.destination = FindClosestEnemy().transform.position;
            }
            
        }
        if (FindClosestEnemy())
        {
            if (!isMelee && Vector3.Distance(transform.position, FindClosestEnemy().transform.position) <= range)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.forward, out hit, range))
                    {
                        if (hit.transform.GetComponent<Target>())
                        {
                            if (transform.tag != hit.transform.tag)
                            {
                                navMeshAgent.destination = transform.position;
                                attacking = true;
                                navMeshAgent.isStopped = true;
                                attBuilding = FindClosestEnemy();
                                transform.LookAt(new Vector3(attBuilding.transform.position.x, 0, attBuilding.transform.position.z));
                                // Quaternion OriginalRot = transform.rotation;
                                // transform.LookAt(attBuilding.transform);
                                // Quaternion NewRot = transform.rotation;
                                // transform.rotation = OriginalRot;
                                // transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, m_Speed * Time.deltaTime);
                            }
                            else
                            {
                                navMeshAgent.isStopped = false;
                                attacking = false;
                                navMeshAgent.destination = FindClosestEnemy().transform.position;
                            }
                            
                        }
                    }
                    
                }
                // else
                // {
                //     navMeshAgent.isStopped = true;
                // }
            if (!isMelee && Vector3.Distance(transform.position, FindClosestEnemy().transform.position) > range)
            {
                navMeshAgent.isStopped = false;
                attacking = false;
                navMeshAgent.destination = FindClosestEnemy().transform.position;
            }
        }

        if (attacking)
        {
            if (Time.time >= time && attBuilding && isMelee)
            {
                if (!attBuilding.GetComponent<Target>())
                {
                    attacking = false;
                }
                else
                {
                    attBuilding.GetComponent<Target>().TakeDamage(attDamage);
                    Debug.Log("Attacked " + attBuilding);
                    time = Time.time + attackTime;
                }
                
            }
            else if (Time.time >= time && !rpgFirePos && !heldThrowable && attBuilding && !isMelee)
            {
                time = Time.time + attackTime;
                gunScript.Shoot(attDamage);
                if (!attBuilding.GetComponent<Target>())
                {
                    attacking = false;
                    attBuilding = null;
                }
            }

            else if (Time.time >= time && attBuilding && !heldThrowable && !isMelee && rpgFirePos)
            {
                time = Time.time + attackTime;
                ProjectileAimTest rpg = rpgFirePos.GetComponent<ProjectileAimTest>();
                rpg.target = attBuilding.transform;
                rpg.FireLauncher();
            }

            else if (Time.time >= time && attBuilding && !isMelee && !rpgFirePos && heldThrowable)
            {
                time = Time.time + attackTime;
                SelfLaunch throwable = heldThrowable.GetComponent<SelfLaunch>();
                throwable.target = attBuilding.transform;
                GameObject newThrowable = Instantiate(throwablePrefab, heldThrowable.transform.position, heldThrowable.transform.rotation);
                newThrowable.transform.parent = heldThrowable.transform.parent;
                heldThrowable.transform.parent = null;
                throwable.enabled = true;
                heldThrowable = newThrowable;
            }
            
            if (!attBuilding && FindClosestEnemy() && !isMelee)
            {
                navMeshAgent.destination = FindClosestEnemy().transform.position;
            }
        }

        if (Time.time >= time2 && FindClosestEnemy())
        {
            time2 = Time.time + checkTime;
            navMeshAgent.destination = FindClosestEnemy().transform.position;
        }
        
    }

    public void Teleport()
    {
        if (FindClosestEnemy())
        {
            Instantiate(tpPart, transform.position, Quaternion.identity);
            Vector3 behindPosition = FindClosestEnemy().transform.position;
            Vector3 teleportPosition = behindPosition + new Vector3(0, 0, 1);
            transform.position = teleportPosition;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Fight();
        }
        if (fighting)
        {
            navUpdate();
        }
    }

    private void OnTriggerEnter(Collider obj) 
    {
        if (obj.transform.gameObject.GetComponent<Target>() && obj.transform.tag != transform.tag && isMelee)
        {
            attacking = true;
            attBuilding = obj.transform.gameObject;
        }

    }

    // private void OnTriggerExit(Collider obj) 
    // {
    //     if (obj.transform.gameObject.GetComponent<Target>())
    //     {
    //         attacking = false;
    //     }
    // }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        if (gameObject.tag == "Red")
        {
            gos = GameObject.FindGameObjectsWithTag("Blue");
        }
        else 
        {
            gos = GameObject.FindGameObjectsWithTag("Red");
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && go.GetComponent<Target>())
            {
                closest = go;
                distance = curDistance;
            }
        }
        if (!closest)
        {
            foundEnemy = false;
        }
        else
        {
            foundEnemy = true;
        }
        return closest;
    }
    
}
