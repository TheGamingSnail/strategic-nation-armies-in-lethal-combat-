using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private string Resource;
    private float time_;
    // Start is called before the first frame update
    void Start()
    {
        time_ = Time.time + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Resource == "Iron")
        {
            if (Time.time >= time_)
            {
                time_ = Time.time + 1f;
                ResourceHolder.i.Iron++; 
                ResourceHolder.i.UpdateHUD();
            }
        }
        else if (Resource == "Gold")
        {
            if (Time.time >= time_)
            {
                time_ = Time.time + 1f;
                ResourceHolder.i.Gold++; 
                ResourceHolder.i.UpdateHUD();
            }
        }
        else if (Resource == "Food")
        {
            if (Time.time >= time_)
            {
                time_ = Time.time + 1f;
                ResourceHolder.i.Food++; 
                ResourceHolder.i.UpdateHUD();
            }
        }
        else
        {
            Debug.LogError("Invalid resource type set for " + gameObject.transform.name + " in " + this + " .Set to " + Resource);
        }
    }
}
