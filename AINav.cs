using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AINav : MonoBehaviour
{
    [SerializeField] private Transform player_;
    private bool foundEnemy;
    private bool attacking;

    [SerializeField] private bool isCannon = false;
    [SerializeField] private bool isMelee = true;
    [SerializeField] private bool attacksOwnTeam = false;
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
    [SerializeField] private GameObject deathZEffect;

    [SerializeField] private bool fireWhenFound = false;

    [SerializeField] private bool victimIsRand = false;
    public bool canTeleport = false;
    private float time = 1f;
    private float time2 = 1f;
    private GameObject attBuilding;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Fight()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fighting = !fighting;
        
    }

    // Update is called once per frame
    void navUpdate()
    {
        if (attBuilding && !attBuilding.GetComponent<Target>())
        {
            navMeshAgent.isStopped = false;
            attacking = false;
            if (FindClosestEnemy())
            {
                navMeshAgent.destination = FindClosestEnemy().transform.position;
            }
            if (victimIsRand)
            {
                if (FindClosestEnemy())
                {
                    attBuilding = FindClosestEnemy();
                }
            }
        }
        Debug.DrawLine(transform.position, navMeshAgent.destination, Color.red);
        if (!attBuilding || !attBuilding.GetComponent<UnityEngine.AI.NavMeshAgent>())
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
                            if (transform.tag != hit.transform.tag || attacksOwnTeam)
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
                            else if(transform.tag == "TestUnit")
                            {
                                navMeshAgent.destination = transform.position;
                                attacking = true;
                                navMeshAgent.isStopped = true;
                                attBuilding = FindClosestEnemy();
                                transform.LookAt(new Vector3(attBuilding.transform.position.x, 0, attBuilding.transform.position.z));
                            }
                            else
                            {
                                navMeshAgent.isStopped = false;
                                attacking = false;
                                navMeshAgent.destination = FindClosestEnemy().transform.position;
                            }
                            
                        }
                        else if (isCannon)
                        {
                            navMeshAgent.destination = transform.position;
                            attacking = true;
                            navMeshAgent.isStopped = true;
                            attBuilding = FindClosestEnemy();
                            transform.LookAt(new Vector3(attBuilding.transform.position.x, 0, attBuilding.transform.position.z));
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

        if (attacking && !fireWhenFound)
        {
            
            if (Time.time >= time && attBuilding && isMelee && !fireWhenFound)
            {
                if (!attBuilding.GetComponent<Target>() || !attBuilding.GetComponent<UnityEngine.AI.NavMeshAgent>())
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
            else if (Time.time >= time && !rpgFirePos && !heldThrowable && attBuilding && !isMelee && !fireWhenFound)
            {
                time = Time.time + attackTime;
                gunScript.Shoot(attDamage);
                if (!attBuilding.GetComponent<Target>())
                {
                    attacking = false;
                    attBuilding = null;
                }
            }

            else if (Time.time >= time && attBuilding && !heldThrowable && !isMelee && rpgFirePos && !fireWhenFound)
            {
                time = Time.time + attackTime;
                ProjectileAimTest rpg = rpgFirePos.GetComponent<ProjectileAimTest>();
                rpg.target = attBuilding.transform;
                rpg.FireLauncher();
                if (victimIsRand)
                {
                    if (FindClosestEnemy())
                    {
                        attBuilding = FindClosestEnemy();
                    }
                }
            }

            else if (Time.time >= time && attBuilding && !isMelee && !rpgFirePos && heldThrowable && !fireWhenFound)
            {
                if (attBuilding.GetComponent<UnityEngine.AI.NavMeshAgent>())
                {
                    time = Time.time + attackTime;
                    SelfLaunch throwable = heldThrowable.GetComponent<SelfLaunch>();
                    throwable.target = attBuilding.transform;
                    if (heldThrowable.transform.Find("spear"))
                    {
                        heldThrowable.transform.Find("spear").gameObject.GetComponent<Stick>().thrower = transform;
                    }
                    GameObject newThrowable = Instantiate(throwablePrefab, heldThrowable.transform.position, heldThrowable.transform.rotation);
                    newThrowable.transform.parent = heldThrowable.transform.parent;
                    heldThrowable.transform.parent = null;
                    throwable.enabled = true;
                    heldThrowable = newThrowable;
                    if (victimIsRand)
                    {
                        if (FindClosestEnemy())
                        {
                            if (FindClosestEnemy() != attBuilding)
                            {
                                attBuilding = FindClosestEnemy();
                            }
                        }
                    }
                }
            }
            
            if (!attBuilding && FindClosestEnemy() && !isMelee)
            {
                navMeshAgent.destination = FindClosestEnemy().transform.position;
            }
        }
        else if (foundEnemy && !isMelee && fireWhenFound)
        {
            if (Time.time >= time && !rpgFirePos && !heldThrowable && navMeshAgent.destination != null && !isMelee)
            {
                time = Time.time + attackTime;
                gunScript.Shoot(attDamage);
                if (victimIsRand)
                {
                    if (FindClosestEnemy())
                    {
                        navMeshAgent.destination = FindClosestEnemy().transform.position;
                        attBuilding = FindClosestEnemy();
                    }
                }
                navMeshAgent.destination = transform.position;
                attacking = true;
                navMeshAgent.isStopped = true;
                attBuilding = FindClosestEnemy();
                transform.LookAt(new Vector3(attBuilding.transform.position.x, 0, attBuilding.transform.position.z));
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
        else
        {
            if (navMeshAgent)
            {
                navMeshAgent.isStopped = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            gameObject.GetComponent<Target>().health = 0;
            Instantiate(deathZEffect, transform.position, Quaternion.identity);
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
        if (gameObject.tag == "Red" && !attacksOwnTeam)
        {
            GameObject[] array1 = GameObject.FindGameObjectsWithTag("Blue");
            GameObject[] array2 = GameObject.FindGameObjectsWithTag("TestUnit");
            gos = array1.Concat(array2).ToArray();
        }
        else if (gameObject.tag == "Blue" && !attacksOwnTeam)
        {
            GameObject[] array1 = GameObject.FindGameObjectsWithTag("Red");
            GameObject[] array2 = GameObject.FindGameObjectsWithTag("TestUnit");
            gos = array1.Concat(array2).ToArray();
        }
        else if (gameObject.tag == "TestUnit")
        {
            GameObject[] array1 = GameObject.FindGameObjectsWithTag("Red");
            GameObject[] array2 = GameObject.FindGameObjectsWithTag("Blue");
            // GameObject[] array3 = GameObject.FindGameObjectsWithTag("TestUnit");
            // gos = array1.Concat(array2).Concat(array3).ToArray();
            gos = array1.Concat(array2).ToArray();
        }
        else
        {
            gos = GameObject.FindGameObjectsWithTag(gameObject.tag);
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        if (!victimIsRand)
        {
            foreach (GameObject go in gos)
            {
                if (go != gameObject)
                {
                    if (attacksOwnTeam  && !go.GetComponent<AINav>().attacksOwnTeam)
                    {
                        Vector3 diff = go.transform.position - position;
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance && go.GetComponent<Target>())
                        {
                            closest = go;
                            distance = curDistance;
                        }
                    }
                    else if (!attacksOwnTeam)
                    {
                        Vector3 diff = go.transform.position - position;
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance && go.GetComponent<Target>())
                        {
                            closest = go;
                            distance = curDistance;
                        }
                    }
                
                }
            }
        }
        else
        {
            int checkedAmount = 0;
            while (!closest)
            {
                GameObject gobj = gos[Random.Range(0, gos.Length - 1)];
                if (gobj != gameObject)
                {
                    if (gobj.GetComponent<Target>() && gobj.GetComponent<UnityEngine.AI.NavMeshAgent>())
                    {
                        closest = gobj;
                    }
                }
                checkedAmount++;
                if (gos.Length == 1)
                {
                    if (gobj.GetComponent<Target>() && gobj.GetComponent<UnityEngine.AI.NavMeshAgent>())
                    {
                        closest = gobj;
                    }
                }
                if(checkedAmount > gos.Length)
                {
                    return null;
                }
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
