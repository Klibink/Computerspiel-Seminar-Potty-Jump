using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    public GameObject mainCamera;
    public float jumpForce = 10f;
    private Animator animator;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Löscht untere Plattformen, wenn Camera bestimmte Höhe erreicht
        if (mainCamera.transform.position.y > transform.position.y + 6 /*&& GetComponent<AudioSource>().isPlaying == false*/)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "PlayerFeet")
        {
            if (collision.relativeVelocity.y <= 0)
            {
                Rigidbody2D rb = collision.collider.GetComponentInParent<Rigidbody2D>();
                if (rb != null)
                {
                    if (GetComponent<AudioSource>() != null)
                    {
                        GetComponent<AudioSource>().Play();
                    }


                    if (gameObject.name.StartsWith("BreakingPlatform 2"))
                    {
                        animator.SetTrigger("breaking2");
                    }

                    else if (gameObject.name.StartsWith("BreakingPlatform 3"))
                    {
                        animator.SetTrigger("breaking3");
                    }

                    else if (gameObject.name.StartsWith("BreakingPlatform 4"))
                    {
                        animator.SetTrigger("breaking4");
                    }

                    else if (gameObject.name.StartsWith("BreakingPlatform 5"))
                    {
                        animator.SetTrigger("breaking5");
                    }

                    else if (gameObject.name.StartsWith("BreakingPlatform 6"))
                    {
                        animator.SetTrigger("breaking6");
                    }


                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                    transform.GetComponent<EdgeCollider2D>().enabled = false;
                    StartCoroutine(DestroyPlattform());
                }
            }
        }

    }
    IEnumerator DestroyPlattform()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
