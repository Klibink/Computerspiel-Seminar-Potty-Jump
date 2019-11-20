using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int leben;
    private Vector2 startPos;
    bool isMovingRight = false;
    bool turnedAlready = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        if (gameObject.name.StartsWith("Abgaswolke"))
        {
            leben = 5;
        }
        else if (gameObject.name.StartsWith("Flugzeug01"))
        {
            leben = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (leben <= 0 || Camera.main.transform.position.y > transform.position.y + 6)
        {
            Destroy(gameObject);
        }
        if(EndlessPlayer.instance != null)
        {
            if (EndlessPlayer.instance.CanDie)
            {
                transform.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                transform.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        

        if(gameObject.name.StartsWith("Abgaswolke"))
        {
            Vector2 tempPos = startPos;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 0.5f) * 0.5f;
            transform.position = tempPos;
        }
        else if (gameObject.name.StartsWith("Flugzeug01"))
        {
            float speed = 1.0f;
            Vector2 tempPos1 = new Vector2((GameManager.instance.FrustumWidth / 2f) -1f, transform.position.y);
            Vector2 tempPos2 = new Vector2((-GameManager.instance.FrustumWidth / 2f) +1f, transform.position.y);
            transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * speed, 1.0f));

            isMovingRight = GetVelocity();
            
            if (isMovingRight && !turnedAlready )
            {
                Scale();
            }
            else if (!isMovingRight && turnedAlready)
            {
                Scale();
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
              if (collision.relativeVelocity.y > 0)
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

    private bool GetVelocity()
    {
        bool direction;
        float velocity=0f;
        if (transform.position.x != startPos.x)
        {
            velocity = startPos.x - transform.position.x;
            startPos.x = transform.position.x;
        }

        if (velocity >= 0)
        {
            direction = false;

        }
        else
        {
            direction = true;
        }
        
        return direction;
    }

    private void Scale()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        turnedAlready = !turnedAlready;
    }
}
