using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class UpdateNM : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface = GameObject.Find("NavMesh Surface").GetComponent<NavMeshSurface>();
        navMeshSurface.UpdateNavMesh( navMeshSurface.navMeshData );
    }
}
