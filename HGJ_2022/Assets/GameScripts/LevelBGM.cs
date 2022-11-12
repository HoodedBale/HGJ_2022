using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBGM : MonoBehaviour
{
    public AudioClip startsound, loopbgm;
    AudioSource audiosource;
    bool inloop = false;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.volume = GameStats.BGM_VOLUME;
        audiosource.clip = startsound;
        audiosource.loop = false;
        audiosource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!inloop && audiosource.isPlaying == false)
        {
            inloop = true;
            audiosource.clip = loopbgm;
            audiosource.loop = true;
            audiosource.Play();
        }
    }
}
