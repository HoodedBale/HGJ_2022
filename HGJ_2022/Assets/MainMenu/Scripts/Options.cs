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
