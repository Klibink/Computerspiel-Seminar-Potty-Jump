using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUIManager : MonoBehaviour
{
    public GameObject collectItemsImage;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.currentLevel + 1 == GameManager.instance.maxLevel)
        {
            collectItemsImage.SetActive(false);
        }
        else
        {
            collectItemsImage.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
