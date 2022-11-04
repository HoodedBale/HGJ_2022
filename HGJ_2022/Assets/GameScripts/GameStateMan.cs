using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMan : MonoBehaviour
{
    public GameObject lorry;
    bool triggerLose = false;

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
    }

    IEnumerator LoseRoutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LoseScreen");
    }
}
