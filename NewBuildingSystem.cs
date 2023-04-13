using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBuildingSystem : MonoBehaviour
{
    public GameObject[] buildingObjects;

    public GameObject[] blueObjects;

    public GameObject[] redObjects;

    public GameObject[] testObjects;

    public GameObject[] bpBuildingObjects;

    public GameObject[] bpBlueObjects;

    public GameObject[] bpRedObjects;

    public GameObject[] bpTestObjects;
    
    private GameObject[] currentList;
    [SerializeField] private Material[] mats; 
    [SerializeField] private GameObject player;
    private GameObject pendingObject;
    public bool menuOpen = false;
    public int index_ = 0; 
    public bool isSelected;
    public bool isPlaceable = true;

    public int selArray = 1;
    [SerializeField] Vector3 pos;
    private RaycastHit hit;

    private MeshRenderer[] child_mesh;
    [SerializeField] private LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        isPlaceable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            menuOpen = !menuOpen;
        }
        if (pendingObject)
        {
            pendingObject.transform.position = pos;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ChangeIndex(-1);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            ChangeIndex(1);
        }

        SelectArray();
    }

    void FixedUpdate()
    {
        if (menuOpen)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10000, layerMask))
            {
                pos = hit.point;
                SelectObject();
            }
        }
    }
    
    void SelectObject()
    {

    }

    void ChangeIndex(int i)
    {
        if (i == -1)
        {
            if (index_ > 0)
            {
                index_ += i;
            }
            else if (index_ == 0)
            {
                index_ = currentList.Length;
            }
        }
        if (i == 1)
        {
            if (index_ < currentList.Length)
            {
                index_ += i;
            }
            else if (index_ == 0)
            {
                index_ = 0;
            }
        }
    }

    void SelectArray()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (selArray < 4)
            {
                selArray++;
            }
            else
            {
                selArray = 1;
            }
        }
        if (selArray == 1)
        {
            currentList = buildingObjects;
        }
        else if (selArray == 2)
        {
            currentList = blueObjects;
        }
        else if (selArray == 3)
        {
            currentList = redObjects;
        }
        else if (selArray == 4)
        {
            currentList = testObjects;
        }
    }

    void UpdateMaterials()
    {
        if (isPlaceable)
        {
            child_mesh = pendingObject.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mesh in child_mesh)
            {
                mesh.material = mats[0];
            }
        }
        else
        {
            child_mesh = pendingObject.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mesh in child_mesh)
            {
                mesh.material = mats[1];
            }
        }
    }

}
