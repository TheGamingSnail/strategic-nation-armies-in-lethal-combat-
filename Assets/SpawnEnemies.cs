using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private float nextTimeToSpawn;

    [SerializeField] private float spawnRate;
    [SerializeField] private Transform goTo;
    [SerializeField] private GameObject enemy;
    private GameObject spawnedEnemy;
    [SerializeField] private float spawnProt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTimeToSpawn && Time.time >= spawnProt) 
        {
            nextTimeToSpawn = Time.time + 1f / spawnRate;
            spawnedEnemy = Instantiate(enemy, transform);
        }
    }
}
