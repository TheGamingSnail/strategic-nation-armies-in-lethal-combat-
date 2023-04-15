using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPlacement : MonoBehaviour
{
    public GameObject[] buildingObjects;

    public GameObject[] blueObjects;

    public GameObject[] redObjects;

    public GameObject[] testObjects;

    public GameObject[] bpBuildingObjects;

    public GameObject[] bpBlueObjects;

    public GameObject[] bpRedObjects;

    public GameObject[] bpTestObjects;
    [SerializeField] private Material[] mats; 
    [SerializeField] private GameObject player;
    private GameObject pendingObject;
    public bool menuOpen = false;
    public int index_ = 0; 
    public bool isSelected;
    public bool isPlaceable = true;

    [SerializeField] private GameObject mCamera;

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
            selArray = 1;
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
        if (menuOpen && Input.GetMouseButtonDown(1))
        {
            selArray++;
            if (selArray > 4)
            {
                selArray = 1;
            }
            Destroy(pendingObject);
            index_ = 0;
            SelectObject(index_);
            
        }
        if (selArray == 1 && menuOpen == true && pendingObject == true)
        {
            Transform child = pendingObject.transform.GetChild(0);
            child.localPosition = new Vector3(0, child.localPosition.y, 0);
        }
    }

    void FixedUpdate()
    {
        if (Physics.Linecast(mCamera.transform.position, mCamera.transform.forward * 100, out hit, layerMask))
        {
            pos = hit.point;
            Debug.DrawRay(mCamera.transform.position, mCamera.transform.forward * 100, Color.red);
            Debug.Log(pos);
        }
        if (pendingObject)
        {
            pendingObject.transform.LookAt(new Vector3(player.transform.position.x, pendingObject.transform.position.y, player.transform.position.z));
        }
    }

    public void SelectObject(int index)
    {
        isSelected = true;
        if (selArray == 1)
        {
            pendingObject = Instantiate(bpBuildingObjects[index], pos, transform.rotation);
        }
        if (selArray == 2)
        {
            pendingObject = Instantiate(bpBlueObjects[index], pos, transform.rotation);
        }
        if (selArray == 3)
        {
            pendingObject = Instantiate(bpRedObjects[index], pos, transform.rotation);
        }
        if (selArray == 4)
        {
            pendingObject = Instantiate(bpTestObjects[index], pos, transform.rotation);
        }
        isPlaceable = true;
        index_ = index;
    }

    public void PlaceObject()
    {
            if (selArray == 1)
            {
                Transform pTrans = pendingObject.transform;
                Destroy(pendingObject);
                pendingObject = Instantiate(buildingObjects[index_], pendingObject.transform.position, pendingObject.transform.rotation);
                pendingObject = null;
            }
            else
            {
                Destroy(pendingObject);
            }
            if (selArray == 2)
            {
                pendingObject = Instantiate(blueObjects[index_], pos, transform.rotation);
            }
            if (selArray == 3)
            {
                pendingObject = Instantiate(redObjects[index_], pos, transform.rotation);
            }
            if (selArray == 4)
            {
                pendingObject = Instantiate(testObjects[index_], pos, transform.rotation);
            }
            if (pendingObject)
            {
                pendingObject.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            }
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
            if (selArray == 1)
            {
                if (index_ < buildingObjects.Length - 1)      
                {
                    Destroy(pendingObject);
                    SelectObject(index_ + 1);
                }
                else
                {
                    Destroy(pendingObject);
                    index_ = 0;
                    SelectObject(index_);
                }
            }
            if (selArray == 2)
            {
                if (index_ < blueObjects.Length - 1)      
                {
                    Destroy(pendingObject);
                    SelectObject(index_ + 1);
                }
                else
                {
                    Destroy(pendingObject);
                    index_ = 0;
                    SelectObject(index_);
                } 
            }
            if (selArray == 3)
            {
                if (index_ < redObjects.Length - 1)      
                {
                    Destroy(pendingObject);
                    SelectObject(index_ + 1);
                }
                else
                {
                    Destroy(pendingObject);
                    index_ = 0;
                    SelectObject(index_);
                }
            }
            if (selArray == 4)
            {
                if (index_ < testObjects.Length - 1)      
                {
                    Destroy(pendingObject);
                    SelectObject(index_ + 1);
                }
                else
                {
                    Destroy(pendingObject);
                    index_ = 0;
                    SelectObject(index_);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selArray == 1)
            {
                if (index_ > 0)      
                {
                    Destroy(pendingObject);
                    SelectObject(index_ - 1);
                }
                else
                {
                    Destroy(pendingObject);
                    index_ = buildingObjects.Length - 1;
                    SelectObject(index_);
                }
            }
            if (selArray == 2)
            {
                if (index_ > 0)      
                {
                    Destroy(pendingObject);
                    SelectObject(index_ - 1);
                }
                else
                {
                    Destroy(pendingObject);
                    index_ = blueObjects.Length - 1;
                    SelectObject(index_);
                }
            }
            if (selArray == 3)
            {
                if (index_ > 0)      
                {
                    Destroy(pendingObject);
                    SelectObject(index_ - 1);
                }
                else
                {
                    Destroy(pendingObject);
                    index_ = redObjects.Length - 1;
                    SelectObject(index_);
                }
            }
            if (selArray == 4)
            {
                if (index_ > 0)      
                {
                    Destroy(pendingObject);
                    SelectObject(index_ - 1);
                }
                else
                {
                    Destroy(pendingObject);
                    index_ = testObjects.Length - 1;
                    SelectObject(index_);
                }
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
