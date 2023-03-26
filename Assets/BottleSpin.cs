using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpin : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    void FixedUpdate()
    {
        transform.Rotate(transform.rotation.x + speed, transform.rotation.y, transform.rotation.z);
    }
}
