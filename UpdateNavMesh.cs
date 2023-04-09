using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class UpdateNavMesh : MonoBehaviour {
    NavMeshSurface navMS;

    // Use this for initialization
    void Start()
    {
        navMS = gameObject.GetComponent<NavMeshSurface>();
    }
    public void UpdateNMS()
    {
        navMS.BuildNavMesh();
    }

}