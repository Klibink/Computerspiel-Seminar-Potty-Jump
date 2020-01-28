using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeButton : MonoBehaviour
{
    public GameObject image_on;
    public GameObject image_off;

    public void ToggleAudio()
    {
        if (GameManager.instance.mute == false)
        {
            GameManager.instance.mute = true;
            image_on.SetActive(true);
            image_off.SetActive(false);
            ToggleAudioSources();
        }
        else
        {
            GameManager.instance.mute = false;
            image_on.SetActive(false);
            image_off.SetActive(true);
            ToggleAudioSources();
        }
    }

    private void ToggleAudioSources()
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            sources[index].mute = GameManager.instance.mute;
            Debug.Log(!GameManager.instance.mute);
        }
    }
}

