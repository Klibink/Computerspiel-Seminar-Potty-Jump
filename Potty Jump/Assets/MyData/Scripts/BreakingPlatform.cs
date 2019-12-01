using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    public GameObject mainCamera;
    //public Sprite brokenSprite;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        //Löscht untere Plattformen, wenn Camera bestimmte Höhe erreicht
        if (mainCamera.transform.position.y > transform.position.y + 7)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0)
        {
            transform.GetComponent<EdgeCollider2D>().enabled = false;
            //transform.GetComponent<SpriteRenderer>().sprite = brokenSprite;
            StartCoroutine(DestroyPlattform());
        }

    }

    IEnumerator DestroyPlattform()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
