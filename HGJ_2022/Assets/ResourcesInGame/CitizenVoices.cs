using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenVoices : MonoBehaviour
{
    public AudioClip[] voices;
    AudioSource audiosource;
    public static CitizenVoices current;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCitizenVoice(Vector3 position)
    {
        transform.position = position;

        bool play = Random.Range(0, 100) < 50;
        if(play && !audiosource.isPlaying)
        {
            if(!audiosource.isPlaying)
            {
                int id = Random.Range(0, voices.Length);
                audiosource.clip = voices[id];
                audiosource.Play();
            }
        }
    }
}
