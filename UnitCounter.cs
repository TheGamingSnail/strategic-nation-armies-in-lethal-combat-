using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UnitCounter : MonoBehaviour
{
    public static UnitCounter i {get; set;}
    private int blueCount = 0;
    private int redCount = 0;
    private int testCount = 0;

    [SerializeField] private TextMeshProUGUI blueText;
    [SerializeField] private TextMeshProUGUI redText;
    [SerializeField] private TextMeshProUGUI testText;
    // Start is called before the first frame update
    void Start()
    {
        i = this;
    }

    // Update is called once per frame
    public void AddBlue(int amount)
    {
        blueCount += amount;
        blueText.SetText("Blue Units: {0}", blueCount);
    }
    public void AddRed(int amount)
    {
        redCount += amount;
        redText.SetText("Red Units: {0}", redCount);
    }
    public void AddTest(int amount)
    {
        testCount += amount;
        testText.SetText("Test Units: {0}", testCount);
    }
}
