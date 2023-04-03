using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementChecker : MonoBehaviour
{
    private ObjPlacement objPl;
    // Start is called before the first frame update
    void Start()
    {
        objPl = GameObject.Find("BuildingManager").GetComponent<ObjPlacement>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Object"))
        {
            objPl.isPlaceable = false;
        }
    }

    void OnTriggerExit(Collider col) 
    {
        if (col.gameObject.CompareTag("Object"))
        {
            objPl.isPlaceable = true;
        }
    }
}
