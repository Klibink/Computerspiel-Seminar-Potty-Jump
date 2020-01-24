using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicManager : MonoBehaviour
{
    public GameObject[] images;
    public int counter;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.firstTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (counter)
        {
            case 0:
                images[0].SetActive(true);
                images[1].SetActive(false);
                images[2].SetActive(false);
                images[3].SetActive(false);
                images[4].SetActive(false);
                images[5].SetActive(false);
                break;
            case 1:
                images[0].SetActive(false);
                images[1].SetActive(true);
                images[2].SetActive(false);
                images[3].SetActive(false);
                images[4].SetActive(false);
                images[5].SetActive(false);
                break;
            case 2:
                images[0].SetActive(false);
                images[1].SetActive(false);
                images[2].SetActive(true);
                images[3].SetActive(false);
                images[4].SetActive(false);
                images[5].SetActive(false);
                break;
            case 3:
                images[0].SetActive(false);
                images[1].SetActive(false);
                images[2].SetActive(false);
                images[3].SetActive(true);
                images[4].SetActive(false);
                images[5].SetActive(false);
                break;
            case 4:
                images[0].SetActive(false);
                images[1].SetActive(false);
                images[2].SetActive(false);
                images[3].SetActive(false);
                images[4].SetActive(true);
                images[5].SetActive(false);
                break;
            case 5:
                images[0].SetActive(false);
                images[1].SetActive(false);
                images[2].SetActive(false);
                images[3].SetActive(false);
                images[4].SetActive(false);
                images[5].SetActive(true);
                
                break;
        }
    }

    public void PushButton()
    {
        counter++;
    }
}
