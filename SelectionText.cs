using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private ObjPlacement objPl;

    private int selArray;
    [SerializeField] private string[] buildingObjText;

    [SerializeField] private string[] blueObjText;

    [SerializeField] private string[] redObjText;

    [SerializeField] private string[] testObjText;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        objPl = GameObject.Find("BuildingManager").GetComponent<ObjPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        selArray = objPl.selArray;
        UpdateText(objPl.index_, selArray);
    }

    void UpdateText(int index, int selArray)
    {
        if (objPl.isSelected)
        {
            if (selArray == 1)
            {
                text.text = buildingObjText[index];
            }
            if (selArray == 2)
            {
                text.text = blueObjText[index];
            }
            if (selArray == 3)
            {
                text.text = redObjText[index];
            }
            if (selArray == 4)
            {
                text.text = testObjText[index];
            }
        }
        else
        {
            text.text = " ";
        }
    }
}
