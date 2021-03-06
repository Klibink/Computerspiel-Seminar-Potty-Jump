﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    public GameObject mainCamera;
    public float jumpForce = 20f;
    private Animator animator;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        animator = GetComponent<Animator>();
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

                if (gameObject.name.StartsWith("PlattformBouncy_1"))
                {
                    animator.SetTrigger("bouncy1");
                }

                else if (gameObject.name.StartsWith("PlattformBouncy_2"))
                {
                    animator.SetTrigger("bouncy2");
                }

                else if (gameObject.name.StartsWith("PlattformBouncy_5"))
                {
                    animator.SetTrigger("bouncy5");
                }

                else if (gameObject.name.StartsWith("PlattformBouncy_6"))
                {
                    animator.SetTrigger("bouncy6");
                }

                GetComponent<AudioSource>().Play();
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
            }
        }
    }
}

