using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    public GameObject mainCamera;
    private bool movePlattform = false;
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

        if (movePlattform)
        {
            //solange keine Zerfallanimation vorhanden ist fällt die Plattform einfach nach unten
            transform.Translate(-transform.up * Time.deltaTime * 3f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0)
        {
            transform.GetComponent<EdgeCollider2D>().enabled = false;
            movePlattform = true;
            StartCoroutine(DestroyPlattform());
        }

    }

    IEnumerator DestroyPlattform()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
