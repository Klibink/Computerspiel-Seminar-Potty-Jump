﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatformAlt : MonoBehaviour
{
    public GameObject mainCamera;
    public float jumpForce = 20f;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        //Löscht untere Plattformen, wenn Camera bestimmte Höhe erreicht
        if (mainCamera.transform.position.y > transform.position.y + 7 && GetComponent<AudioSource>().isPlaying == false)
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
                GetComponent<AudioSource>().Play();
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
            }
        }
    }
}

