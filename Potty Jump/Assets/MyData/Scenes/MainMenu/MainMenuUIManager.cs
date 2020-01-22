using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public GameObject[] panels;
    public PageSwiper pageSwiper;
    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.instance.availableLevel)
        {
            case 1:
                pageSwiper.totalPages = 1;
                panels[0].SetActive(true);
                break;
            case 2:
                pageSwiper.totalPages = 2;
                panels[0].SetActive(true);
                panels[1].SetActive(true);
                break;
            case 3:
                pageSwiper.totalPages = 3;
                panels[0].SetActive(true);
                panels[1].SetActive(true);
                panels[2].SetActive(true);
                break;
            case 4:
                pageSwiper.totalPages = 4;
                panels[0].SetActive(true);
                panels[1].SetActive(true);
                panels[2].SetActive(true);
                panels[3].SetActive(true);
                break;
            case 5:
                pageSwiper.totalPages = 5;
                panels[0].SetActive(true);
                panels[1].SetActive(true);
                panels[2].SetActive(true);
                panels[3].SetActive(true);
                panels[4].SetActive(true);
                break;
            default:
                pageSwiper.totalPages = 1;
                panels[0].SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
