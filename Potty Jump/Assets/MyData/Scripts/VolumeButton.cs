<<<<<<< HEAD
﻿using System.Collections;
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
=======
﻿using System.Collections;
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
>>>>>>> 0c63a426d2771fbd468e12759ef70d0d63016726
    {
        if (soundOn == true)
        {
            soundOn = false;
            image_on.SetActive(true);
            image_off.SetActive(false);
            ToggleAudioSources();
<<<<<<< HEAD
        }
=======
        }
>>>>>>> 0c63a426d2771fbd468e12759ef70d0d63016726
        else
        {
            soundOn = true;
            image_on.SetActive(false);
            image_off.SetActive(true);
            ToggleAudioSources();
<<<<<<< HEAD
        }
    }

=======
        }
    }

>>>>>>> 0c63a426d2771fbd468e12759ef70d0d63016726
    private void ToggleAudioSources()
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            sources[index].mute = !soundOn;
        }
<<<<<<< HEAD
    }
}
=======
    }
}
>>>>>>> 0c63a426d2771fbd468e12759ef70d0d63016726
