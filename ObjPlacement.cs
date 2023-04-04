using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPlacement : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject[] bpObjects;
    [SerializeField] private Material[] mats; 
    [SerializeField] private GameObject player;
    private GameObject pendingObject;
    public bool menuOpen = false;
    public int index_ = 0; 
    public bool isSelected;
    public bool isPlaceable = true;
    Vector3 pos;
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
        if (isSelected)
        {
            ChangeSelection();
        }
        if (Input.GetKeyDown(KeyCode.F) && isSelected == false)
        {
            SelectObject(0);
            menuOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            DeselectObject();
            menuOpen = false;
        }
        if (pendingObject)
        {
            pendingObject.transform.position = pos;

            if (Input.GetMouseButtonDown(0) && isPlaceable)
            {
                PlaceObject();
            }
        }
        if (pendingObject)
        {
            UpdateMaterials();
        }
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10000, layerMask))
        {
            pos = hit.point;
        }
        if (pendingObject)
        {
            pendingObject.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
        }
    }

    public void SelectObject(int index)
    {
        isSelected = true;
        pendingObject = Instantiate(bpObjects[index], pos, transform.rotation);
        isPlaceable = true;
        index_ = index;
    }

    public void PlaceObject()
    {
            Destroy(pendingObject);
            pendingObject = Instantiate(objects[index_], pos, transform.rotation);
            pendingObject.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            SelectObject(index_);
        
    }

    public void DeselectObject()
    {
        Destroy(pendingObject);
        index_ = 0;
        pendingObject = null;
        isSelected = false;
    }

    void ChangeSelection()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (index_ < objects.Length - 1)
            {
                Destroy(pendingObject);
                SelectObject(index_ + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (index_ > 0)
            {
                Destroy(pendingObject);
                SelectObject(index_ - 1);
            }
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
