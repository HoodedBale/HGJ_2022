using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button PlayBtn, OptionsBtn, Creditsbtn;
    public AudioClip playSound;
    public GameObject MainCamera;
    bool inOptions = false;
    bool inCredits = false;
    bool pressOptions = false;
    bool pressCredits = false;
    bool backfrom = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(inOptions == true)
            {
                LeanTween.rotateX(MainCamera, -27.5f, 1).setEaseInOutCubic();
                inOptions = false;
                pressOptions = false;
                pressCredits = false;
            }

            if(inCredits == true)
            {
                LeanTween.rotateX(MainCamera, -27.5f, 1).setEaseInOutCubic();
                LeanTween.move(MainCamera, new Vector3(0,-27.5f,-35), 1).setEaseInOutCubic();
                inCredits = false;
                pressOptions = false;
                pressCredits = false;
            }
        }
    }

    public void PlayGame()
    {
        StartCoroutine(holdBeforeStart(1f));
    }

    public void OpenOptions()
    {
        if(pressOptions == false)
        {
            pressOptions = true;
            pressCredits = true;
            LeanTween.rotateX(MainCamera, -90f, 1).setEaseInOutCubic();
            StartCoroutine(OpenOptionsWait());
        }
    }

    public void OpenCredits()
    {
        if(pressCredits == false)
        {
            pressOptions = true;
            pressCredits = true;
            LeanTween.rotateX(MainCamera, 90f, 1).setEaseInOutCubic();
            LeanTween.move(MainCamera, new Vector3(0, 22, -42), 1).setEaseInOutCubic();
            StartCoroutine(OpenCreditsWait());
        }
    }

    public void back(bool backFromWhere)
    {
        if(backFromWhere == true)
        {
            if (inOptions == true)
            {
                LeanTween.rotateX(MainCamera, -27.5f, 1).setEaseInOutCubic();
                inOptions = false;
                pressOptions = false;
                pressCredits = false;
            }
        }
        else
        {
            if (inCredits == true)
            {
                LeanTween.rotateX(MainCamera, -27.5f, 1).setEaseInOutCubic();
                LeanTween.move(MainCamera, new Vector3(0, -27.5f, -35), 1).setEaseInOutCubic();
                inCredits = false;
                pressOptions = false;
                pressCredits = false;
            }
        }
    }

    IEnumerator OpenOptionsWait()
    {
        inOptions = false;
        inCredits = false;
        yield return new WaitForSeconds(1.025f);
        inOptions = true;
    }

    IEnumerator OpenCreditsWait()
    {
        inOptions = false;
        inCredits = false;
        yield return new WaitForSeconds(1.025f);
        inCredits = true;
    }

    IEnumerator holdBeforeStart(float val)
    {
        UISound.instance.bgmSource.loop = false;
        UISound.instance.ReplaceBGM(playSound);
        yield return new WaitForSeconds(val);
        SceneManager.LoadScene("Level");
    }
}
