using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioSource EventSFXSource;
    public AudioSource MoveSFXSource;
    public AudioSource bgmSource;

    public static UISound instance = null;

    public float PitchMod_Low = .95f;
    public float PitchMod_High = 1.05f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip sfx)
    {
        EventSFXSource.clip = sfx;
        EventSFXSource.Play();
    }

    public void ReplaceBGM(AudioClip BGM)
    {
        bgmSource.clip = BGM;
        bgmSource.Play();
    }


    public void RandomizeSfx(params AudioClip[] sfx)
    {
        int randomIndex = Random.Range(0, sfx.Length);

        float randomPitch = Random.Range(PitchMod_Low, PitchMod_High);

        MoveSFXSource.pitch = randomPitch;

        MoveSFXSource.clip = sfx[randomIndex];

        MoveSFXSource.Play();
    }
}
