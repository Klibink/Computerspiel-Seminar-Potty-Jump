using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private int startLevel;
    // Start is called before the first frame update
    void Start()
    {
        startLevel = GameManager.instance.availableLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.availableLevel != startLevel)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerFeet" || collision.tag == "PlayerBody")
        {
            EndlessGameManager.instance.currentItems++;
            Destroy(gameObject);
            //StartCoroutine(DestroyItem());
        }
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
