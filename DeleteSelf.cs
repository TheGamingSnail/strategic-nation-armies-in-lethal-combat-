using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour
{

    [SerializeField] private float time;
    // Start is called before the first frame update
    void Awake()
    {
        time += Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= time)
        {
            Destroy(gameObject);
        }
    }
}
