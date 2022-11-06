using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToTemp : MonoBehaviour
{
    public Image currentScreenshot;
    public Text currentText;
    public GameObject PrevBtn, NextBtn;

    public Sprite[] screenshots;
    [TextArea(15,10)]
    public string[] texts;

    int counter = 0;
    int maxCounter;


    // Start is called before the first frame update
    void Start()
    {
        maxCounter = texts.Length;
        maxCounter -= 1;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter <= 0)
        {
            counter = 0;
            PrevBtn.SetActive(false);
        }

        if(counter >= maxCounter)
        {
            counter = maxCounter;
            NextBtn.SetActive(false);
        }

        if(counter != 0 && counter != maxCounter)
        {
            PrevBtn.SetActive(true);
            NextBtn.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }

        currentScreenshot.sprite = screenshots[counter];
        currentText.text = texts[counter];
    }

    public void ChangeTip(int countPlus)
    {
        counter += countPlus;
    }
}
