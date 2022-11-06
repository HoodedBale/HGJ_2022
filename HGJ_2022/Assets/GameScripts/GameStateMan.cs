using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateMan : MonoBehaviour
{
    public GameObject lorry;
    public WaveManager wavemanager;
    bool triggerLose = false;
    bool triggerWin = false;

    public Text waveNumberTxt;
    public Text waveCountdownTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!triggerLose && lorry == null)
        {
            triggerLose = true;
            StartCoroutine(LoseRoutine());
        }

        if(!triggerWin && wavemanager.currentWaveMaster >= 6)
        {
            triggerWin = true;
            StartCoroutine(Winroutine());
        }
    }

    IEnumerator LoseRoutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LoseScreen");
    }

    IEnumerator Winroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("WinScreen");
    }
}
