using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprites[GameManager.instance.currentLevel];
        switch (GameManager.instance.currentLevel)
        {
            case 0:
                transform.localScale = new Vector3(1.2f, 1, 1);

                break;
            case 1:
                transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
