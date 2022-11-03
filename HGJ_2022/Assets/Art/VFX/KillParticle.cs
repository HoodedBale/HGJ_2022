using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParticle : MonoBehaviour
{
    public float killTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeOut(gameObject, killTimer));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TimeOut(GameObject obj, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }
}
