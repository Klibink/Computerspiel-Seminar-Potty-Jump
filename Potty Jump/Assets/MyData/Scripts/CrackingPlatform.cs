using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackingPlatform : MonoBehaviour
{
    public GameObject mainCamera;
    public float jumpForce = 10f;

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
            Destroy(gameObject);
        }

    }
}
