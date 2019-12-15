using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PottyMainMenu : MonoBehaviour
{
    private Transform[] allChildren;
    public GameObject[] skins;
    public GameObject[] flowers;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        allChildren = transform.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Beim Beginn der Szene wird der gewünschte Skin in der Hierarchy aktivert
        for (int i = 0; i < skins.Length; i++)
        {
            if (i == GameManager.instance.currentSkin)
            {
                skins[i].SetActive(true);
            }
            else
            {
                skins[i].SetActive(false);
            }
        }
        //Das Gleiche für die Blume
        for (int i = 0; i < flowers.Length; i++)
        {
            if (i == GameManager.instance.currentFlower)
            {
                flowers[i].SetActive(true);
            }
            else
            {
                flowers[i].SetActive(false);
            }
        }
    }
}
