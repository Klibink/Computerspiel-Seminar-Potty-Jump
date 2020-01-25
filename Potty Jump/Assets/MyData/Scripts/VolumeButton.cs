using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeButton : MonoBehaviour
{
    public bool soundOn = true;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ToggleAudio()
    {
        if (soundOn)
        {
            camera.GetComponent<AudioListener>().enabled = false;
            soundOn = false;
        }
        else
        {
            camera.GetComponent<AudioListener>().enabled = true;
            soundOn = true;
        }
    }
}
