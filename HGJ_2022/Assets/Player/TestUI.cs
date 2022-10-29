using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestUI : MonoBehaviour
{
    public TextMeshProUGUI cardboardui, plasticui, metalui;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cardboardui.text = string.Format("Cardboard: {0}", GameStats.resources[(int)GameStats.RESOURCE_TYPE.CARDBOARD]);
        plasticui.text = string.Format("Plastic: {0}", GameStats.resources[(int)GameStats.RESOURCE_TYPE.PLASTIC]);
        metalui.text = string.Format("Metal: {0}", GameStats.resources[(int)GameStats.RESOURCE_TYPE.METAL]);
    }
}
