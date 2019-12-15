﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackingPlatform : MonoBehaviour
{
    public GameObject mainCamera;
    public Sprite brokenSprite;
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
            Rigidbody2D rb = collision.collider.GetComponentInParent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = -1;
                rb.velocity = velocity;
            }
            transform.GetComponent<EdgeCollider2D>().enabled = false;
            transform.GetComponent<SpriteRenderer>().sprite = brokenSprite;
            StartCoroutine(DestroyPlattform());
        }

    }

    IEnumerator DestroyPlattform()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
