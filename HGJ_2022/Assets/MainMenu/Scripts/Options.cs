using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public float bgmVol, sfxVol;

    public Slider bgmS, sfxS;
    public Toggle bgmM, sfxM;
    public Text bgmT, sfxT;

    float currentBGMVol = 0;
    float currentSFXVol = 0;

    // Start is called before the first frame update
    void Start()
    {
        bgmS.value = GameStats.BGM_VOLUME * 100.0f;
        sfxS.value = GameStats.SFX_VOLUME * 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // text
        bgmT.text = bgmS.value.ToString();
        sfxT.text = sfxS.value.ToString();

        // volumes
        bgmVol = bgmS.value;
        sfxVol = sfxS.value;

        if(bgmS.value > 0 && bgmM.isOn)
        {
            bgmM.isOn = false;
        }
        if (sfxS.value > 0 && sfxM.isOn)
        {
            sfxM.isOn = false;
        }

        GameStats.BGM_VOLUME = bgmVol / 100.0f;
        GameStats.SFX_VOLUME = sfxVol / 100.0f;
    }

    public void toggleMuteBGM()
    {
        if (bgmM.isOn)
        {
            currentBGMVol = bgmS.value;
            bgmS.value = 0;
        }
        else
        {
            bgmS.value = currentBGMVol;
        }
    }

    public void toggleMuteSFX()
    {
        if (sfxM.isOn)
        {
            currentSFXVol = sfxS.value;
            sfxS.value = 0;
        }
        else
        {
            sfxS.value = currentSFXVol;
        }
    }
}
