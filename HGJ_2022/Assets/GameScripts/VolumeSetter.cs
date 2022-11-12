using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetter : MonoBehaviour
{
    public AudioSource[] bgmsources;
    public AudioSource[] sfxsources;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < bgmsources.Length; ++i)
        {
            bgmsources[i].volume = GameStats.BGM_VOLUME;
        }

        for(int i = 0; i < sfxsources.Length; ++i)
        {
            sfxsources[i].volume = GameStats.SFX_VOLUME;
        }
    }
}
