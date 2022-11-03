using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public Button PlayAgainBtn, MMBtn;
    public GameObject MainCamera;
    public Image blackout;
    Color colour;
    float alpha = 1;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateX(MainCamera, -4, 3).setEaseInOutCubic();
        LeanTween.move(MainCamera, new Vector3(12, -20, -16), 3).setEaseInOutCubic();
        colour = blackout.color;
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= 0.001f;
        colour.a = alpha;
        blackout.color = colour;
    }

    public void playAgain()
    {

    }

    public void returnToMM()
    {

    }
}
