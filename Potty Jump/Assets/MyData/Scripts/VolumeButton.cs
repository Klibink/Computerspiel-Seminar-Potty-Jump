using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeButton : MonoBehaviour
{
    public bool soundOn;
    public GameObject image_on;
    public GameObject image_off;

    // Start is called before the first frame update
    void Start()
    {
        soundOn = true;
    }

    public void ToggleAudio()
    {
        if (soundOn == true)
        {
            soundOn = false;
            image_on.SetActive(true);
            image_off.SetActive(false);
            ToggleAudioSources();
        }
        else
        {
            soundOn = true;
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
            sources[index].mute = !soundOn;
        }
    }
}
