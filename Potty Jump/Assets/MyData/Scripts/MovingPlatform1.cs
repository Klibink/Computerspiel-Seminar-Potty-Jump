using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform1 : MonoBehaviour
{
    float speed = 0.4f;
    private Vector2 startPos;
    private Vector2 tempPos1;
    private Vector2 tempPos2;
    private Animator animator;

    bool isMovingRight = false;
    bool turnedAlready = false;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        speed = Random.Range(0.2f, 0.45f);

        if (startPos.x > 0)
        {
            tempPos1 = new Vector2((GameManager.instance.FrustumWidth / 2f) - 1f, transform.position.y);
            tempPos2 = new Vector2((-GameManager.instance.FrustumWidth / 2f) + 1f, transform.position.y);
        }
        else
        {
            tempPos1 = new Vector2((-GameManager.instance.FrustumWidth / 2f) + 1f, transform.position.y);
            tempPos2 = new Vector2((GameManager.instance.FrustumWidth / 2f) - 1f, transform.position.y);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.StartsWith("MovingPlatform 1"))
        {
            transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * speed, 1f));

            float velocity = 0f;
            if (transform.position.x != startPos.x)
            {
                velocity = transform.position.x - startPos.x;
                startPos.x = transform.position.x;
            }

            if (velocity >= 0)
            {

                //Debug.Log("rechts");

            }
            else
            {
                //Debug.Log("links");
            }

            animator.SetFloat("Velocity", velocity);
        }

        else if (gameObject.name.StartsWith("MovingPlatform 3"))
        {
            transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * speed, 1f));
            animator.SetTrigger("moving3");

            /*float velocity = 0f;
            if (transform.position.x != startPos.x)
            {
                velocity = transform.position.x - startPos.x;
                startPos.x = transform.position.x;
            }
            */
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

        else if (gameObject.name.StartsWith("MovingPlatform 4"))
        {
            transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * speed, 1f));
            animator.SetTrigger("moving4");

            /*float velocity = 0f;
            if (transform.position.x != startPos.x)
            {
                velocity = transform.position.x - startPos.x;
                startPos.x = transform.position.x;
            }
            */
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

        else if (gameObject.name.StartsWith("MovingPlatform 5"))
        {
            transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * speed, 1f));
            animator.SetTrigger("moving5");

            /*float velocity = 0f;
            if (transform.position.x != startPos.x)
            {
                velocity = transform.position.x - startPos.x;
                startPos.x = transform.position.x;
            }
            */
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

        else if (gameObject.name.StartsWith("MovingPlatform 6"))
        {
            transform.position = Vector3.Lerp(tempPos1, tempPos2, Mathf.PingPong(Time.time * speed, 1f));
            animator.SetTrigger("moving6");

            /*float velocity = 0f;
            if (transform.position.x != startPos.x)
            {
                velocity = transform.position.x - startPos.x;
                startPos.x = transform.position.x;
            }
            */
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


    }

    private bool GetVelocity()
    {
        bool direction;
        float velocity = 0f;
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
      

            
 