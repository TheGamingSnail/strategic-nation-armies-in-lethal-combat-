using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private ObjPlacement objPl;
    [SerializeField] private string[] objText;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        objPl = GameObject.Find("BuildingManager").GetComponent<ObjPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText(objPl.index_);
    }

    void UpdateText(int index)
    {
        if (objPl.isSelected)
        {
            text.text = objText[index];
        }
        else
        {
            text.text = " ";
        }
    }
}
