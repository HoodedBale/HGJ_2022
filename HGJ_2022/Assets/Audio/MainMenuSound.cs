using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    public AudioClip highlight_sound, click_sound, clickPlay_sound;

    public void highlight()
    {
        UISound.instance.PlaySingle(highlight_sound);
    }

    public void click()
    {
        UISound.instance.PlaySingle(click_sound);
    }

    public void click_play()
    {
        UISound.instance.PlaySingle(clickPlay_sound);
    }
}
