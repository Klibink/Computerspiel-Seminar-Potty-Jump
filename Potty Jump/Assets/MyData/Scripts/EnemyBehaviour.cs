using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int leben;
    private Vector2 startPos;
    private Vector2 tempPos1;
    private Vector2 tempPos2;
    bool isMovingRight = false;
    bool turnedAlready = false;
    public GameObject bullet;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        if (gameObject.name.StartsWith("Abgaswolke"))
        {
            leben = 3;
        }
        else if (gameObject.name.StartsWith("Flugzeug01"))
        {
            leben = 2;
        }
        else if (gameObject.name.StartsWith("Laus"))
        {
            leben = 1;
            Vector2 temp = transform.position;
            temp.x = Random.Range((GameManager.instance.FrustumWidth / 2f) - 1f, (-GameManager.instance.FrustumWidth / 2f) + 1f);
            transform.position = temp;
        }
        else if (gameObject.name.StartsWith("Mistkäfer"))
        {
            leben = 2;
            tempPos1 = new Vector2(startPos.x - 0.2f, startPos.y + 0.1f);
            tempPos2 = new Vector2(startPos.x + 0.2f, startPos.y + 0.1f);
        }
        else if (gameObject.name.StartsWith("Feuerball"))
        {
            leben = 1;
            if(startPos.x > 0)
            {
                tempPos1 = new Vector2(startPos.x + 10, startPos.y + 5);
                tempPos2 = new Vector2(startPos.x - 10, startPos.y - 5);
            }
            else
            {
                tempPos1 = new Vector2(startPos.x - 10, startPos.y + 5);
                tempPos2 = new Vector2(startPos.x + 10, startPos.y - 5);
            }
        }
        else if (gameObject.name.StartsWith("KäferBrennend"))
        {
            leben = 2;
            tempPos1 = new Vector2(startPos.x - 0.2f, startPos.y);
            tempPos2 = new Vector2(startPos.x + 0.2f, startPos.y);
        }
        else if (gameObject.name.StartsWith("Ölmonster"))
        {
            leben = 2;
            tempPos1 = new Vector2(startPos.x - 0.2f, startPos.y);
            tempPos2 = new Vector2(startPos.x + 0.2f, startPos.y);
        }
        else if (gameObject.name.StartsWith("Kröte"))
        {
            leben = 1;
        }
        else if (gameObject.name.StartsWith("BrennendesBlatt"))
        {
            leben = 1;
        }
        else if (gameObject.name.StartsWith("Killerpflanze"))
        {
            leben = 1;
        }
        else if (gameObject.name.StartsWith("PlastiktüteBlau"))
        {
            leben = 1;
        }
        else if (gameObject.name.StartsWith("PlastiktüteRot"))
        {
            leben = 1;
        }
        else if (gameObject.name.StartsWith("Müllmonster"))
        {
            leben = 1;
            if(transform.position.x > 0)
            {
                Vector3 scale = new Vector3(-0.3f, 0.3f, 0.3f);
                transform.localScale = scale;
            }
            else
            {
                Vector3 scale = new Vector3(0.3f, 0.3f, 0.3f);
                transform.localScale = scale;
            }
        }
        else if (gameObject.name.StartsWith("Pufferfisch"))
        {
            leben = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!EndlessGameManager.instance.GamePaused)
        {
            if (leben <= 0 || Camera.main.transform.position.y > transform.position.y + 6)
            {
                Destroy(gameObject);
            }
            if (EndlessPlayer.instance != null)
            {
                if (EndlessPlayer.instance.CanDie || EndlessPlayer.instance.IsUsingPowerUp)
                {
                    transform.GetComponent<BoxCollider2D>().enabled = true;
                }
                else
                {
                    transform.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else if (StoryPlayer.instance != null)
            {
                if (StoryPlayer.instance.CanDie)
                {
                    transform.GetComponent<BoxCollider2D>().enabled = true;
                }
                else
                {
                    transform.GetComponent<BoxCollider2D>().enabled = false;
                }
            }

            if (gameObject.name.StartsWith("Abgaswolke"))
            {
                Vector2 tempPos = startPos;
                tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 0.5f) * 0.5f;
                transform.position = tempPos;
            }
            else if (gameObject.name.StartsWith("Flugzeug01"))
            {
                float speed = 1.0f;
                Vector2 tempPos1 = new Vector2((GameManager.instance.FrustumWidth / 2f) - 1f, transform.position.y);
                Vector2 tempPos2 = new Vector2((-GameManager.instance.FrustumWidth / 2f) + 1f, transform.position.y);
                transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * speed, 1.0f));

                isMovingRight = GetVelocity();

                if (isMovingRight && !turnedAlready)
                {
                    Scale();
                }
                else if (!isMovingRight && turnedAlready)
                {
                    Scale();
                }

            }
            else if (gameObject.name.StartsWith("Laus"))
            {

            }
            else if (gameObject.name.StartsWith("Mistkäfer"))
            {
                transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * 0.5f, 1.0f));
                isMovingRight = GetVelocity();

                if (isMovingRight && !turnedAlready)
                {
                    Scale();
                }
                else if (!isMovingRight && turnedAlready)
                {
                    Scale();
                }
            }
            else if (gameObject.name.StartsWith("Feuerball"))
            {
                transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * 0.1f, 1.0f));
            }
            else if (gameObject.name.StartsWith("KäferBrennend"))
            {
                transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * 0.5f, 1.0f));

                isMovingRight = GetVelocity();

                if (isMovingRight && !turnedAlready)
                {
                    Scale();
                }
                else if (!isMovingRight && turnedAlready)
                {
                    Scale();
                }
            }
            else if (gameObject.name.StartsWith("Ölmonster"))
            {
                transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * 0.5f, 1.0f));

                isMovingRight = GetVelocity();

                if (isMovingRight && turnedAlready)
                {
                    Scale();
                }
                else if (!isMovingRight && !turnedAlready)
                {
                    Scale();
                }
            }
            else if (gameObject.name.StartsWith("Kröte"))
            {

            }
            else if (gameObject.name.StartsWith("BrennendesBlatt"))
            {
                transform.Translate(-transform.up * Time.deltaTime * 3f);
            }
            else if (gameObject.name.StartsWith("Killerpflanze"))
            {

            }
            else if (gameObject.name.StartsWith("PlastiktüteBlau"))
            {
                Vector2 tempPos = startPos;
                tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 0.5f) * 0.15f;
                transform.position = tempPos;
            }
            else if (gameObject.name.StartsWith("PlastiktüteRot"))
            {
                Vector2 tempPos = startPos;
                tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 0.5f) * 0.15f;
                transform.position = tempPos;
            }
            else if (gameObject.name.StartsWith("Müllmonster"))
            {
                if (canShoot)
                {
                    canShoot = false;
                    Instantiate(bullet,transform.position,transform.rotation);
                    StartCoroutine(MonsterShoot());
                }
            }
            else if (gameObject.name.StartsWith("Pufferfisch"))
            {

            }
        }
        else
        {
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "PlayerFeet")
        {
            if (!EndlessPlayer.instance.IsUsingPowerUp)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    BoxCollider2D bCollider = collision.collider.GetComponent<BoxCollider2D>();
                    bCollider.enabled = false;
                    EndlessPlayer.instance.ChangeToDeathSprite();
                }
                else if (collision.relativeVelocity.y <= 0)
                {
                    Rigidbody2D rb = collision.collider.GetComponentInParent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 velocity = rb.velocity;
                        velocity.y = 15;
                        rb.velocity = velocity;
                    }
                    Destroy(gameObject);
                }
            }
            else
            {
                if(collision.relativeVelocity.y <= 0)
                {
                    Rigidbody2D rb = collision.collider.GetComponentInParent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 velocity = rb.velocity;
                        velocity.y = 15;
                        rb.velocity = velocity;
                    }
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
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
        else if (collision.tag == "PlayerBody")
        {
            if (!EndlessPlayer.instance.IsUsingPowerUp)
            {
                BoxCollider2D bCollider = GameObject.FindGameObjectWithTag("PlayerFeet").transform.GetComponent<BoxCollider2D>();
                bCollider.enabled = false;
                EndlessPlayer.instance.ChangeToDeathSprite();
                Debug.Log("DIE!!!!");
            }
            else
            {
                Destroy(gameObject);
            }
             
            
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

    IEnumerator MonsterShoot()
    {
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }
}
