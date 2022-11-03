using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject soundprefab;
    public static SoundManager current;

    Queue<GameObject> instanceQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        current = this;

        for(int i = 0; i < 100; ++i)
        {
            instanceQueue.Enqueue(Instantiate(soundprefab));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip, Transform position = null)
    {
        if(instanceQueue.Count > 0)
        {
            GameObject instance = instanceQueue.Dequeue();
            instance.GetComponent<AudioSource>().clip = clip;
            if(position == null)
            {
                instance.transform.SetParent(Camera.main.transform);
                instance.transform.localPosition = Vector3.zero;
            }
            else
            {
                instance.transform.SetParent(position);
                instance.transform.localPosition = Vector3.zero;
            }
            instance.GetComponent<AudioSource>().Play();
            instance.GetComponent<AudioSource>().loop = false;
            StartCoroutine(KillSound(instance));
        }
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        if (instanceQueue.Count > 0)
        {
            GameObject instance = instanceQueue.Dequeue();
            instance.GetComponent<AudioSource>().clip = clip;
            instance.transform.localPosition = position;
            instance.GetComponent<AudioSource>().Play();
            instance.GetComponent<AudioSource>().loop = false;
            StartCoroutine(KillSound(instance));
        }
    }

    IEnumerator KillSound(GameObject audioObj)
    {
        AudioSource audio = audioObj.GetComponent<AudioSource>();
        while(audio.isPlaying)
        {
            yield return null;
        }
        audio.clip = null;
        instanceQueue.Enqueue(audioObj);
    }
}
