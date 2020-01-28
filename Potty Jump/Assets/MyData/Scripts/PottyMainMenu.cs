using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PottyMainMenu : MonoBehaviour
{
    private Transform[] allChildren;
    public List<GameObject> skins;
    public List<GameObject> flowers;
    private Rigidbody2D rb;
    public GameObject xmasSkin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        allChildren = transform.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.mute == true)
        {
            AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            for (int index = 0; index < sources.Length; ++index)
            {
                sources[index].mute = GameManager.instance.mute;
            }
        }

        //Beim Beginn der Szene wird der gewünschte Skin in der Hierarchy aktivert
        for (int i = 0; i < skins.Count; i++)
        {
            if (i == GameManager.instance.currentSkin /*&& GameManager.instance.unlockSkins[i]==true*/)
            {
                skins[i].SetActive(true);
            }
            else
            {
                skins[i].SetActive(false);
            }
        }
        //Das Gleiche für die Blume
        for (int i = 0; i < flowers.Count; i++)
        {
            if (i == GameManager.instance.currentFlower && !xmasSkin.activeSelf)
            {
                flowers[i].SetActive(true);
            }
            else
            {
                flowers[i].SetActive(false);
            }
        }

        /*if (GameObject.Find("TannenbaumSkin").activeSelf)
        {
            GameObject.Find("BlumenSpriteHolder").SetActive(false);
        }
        else
        {
            GameObject.Find("BlumenSpriteHolder").SetActive(true);
        }*/
    }
}
