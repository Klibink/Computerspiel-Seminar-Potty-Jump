using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int leben;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        if (gameObject.name.StartsWith("Abgaswolke"))
        {
            leben = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (leben <= 0)
        {
            Destroy(gameObject);
        }

        if(gameObject.name.StartsWith("Abgaswolke"))
        {
            Vector2 tempPos = startPos;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 0.5f) * 0.5f;
            transform.position = tempPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if(collision.relativeVelocity.y > 0)
            {
                BoxCollider2D bCollider = collision.collider.GetComponent<BoxCollider2D>();
                bCollider.enabled = false;
            }
            else if (collision.relativeVelocity.y <= 0)
            {
                Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = 15;
                    rb.velocity = velocity;
                }
                Destroy(gameObject);
            }
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            leben--;
        }
    }
}
