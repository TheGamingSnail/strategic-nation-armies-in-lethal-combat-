using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceHolder : MonoBehaviour
{
    public static ResourceHolder i {get; set; }
    public int Iron = 0;
    public int Gold = 0;
    public int Food = 0;
    [SerializeField] private TextMeshProUGUI rText;
    // Start is called before the first frame update
    void Awake()
    {
        i = this;
    }
    void Start()
    {
        UpdateHUD();
    }

    void Update()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        rText.text = " Iron: " + Iron + "\n Gold: " + Gold + "\n Food: " + Food;
    }
}
