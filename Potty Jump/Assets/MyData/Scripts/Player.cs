using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float movementSpeed = 10f;
    private float points = 0f;
    float movement = 0f;
    private bool isMovingLeft = true;
    Rigidbody2D rb;
    public Text scoreText;

    public float Points { get => points; set => points = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        movement = Input.GetAxis("Horizontal") * movementSpeed;

#elif UNITY_ANDROID
        movement = Input.acceleration.x * movementSpeed;
#endif

        Flip();
        
        Debug.Log(movement);
        //erhöht die Punktzahl und zeigt sie in der Szene an
        if (rb.velocity.y > 0 && transform.position.y >points)
        {
            points = transform.position.y;
        }
        scoreText.text = "Score: " + Mathf.Round(points*1.75f).ToString();

    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = movement;
        rb.velocity = velocity;
    }

    private void Flip()
    {/*
        if (movement < 0) isMovingLeft = true;
        else isMovingLeft = false;

        if (movement <= 0 && isMovingLeft)
        {
            //transform.GetComponent<SpriteRenderer>().flipX = true;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if (movement > 0 && !isMovingLeft)
        {
            //transform.GetComponent<SpriteRenderer>().flipX = false;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }*/

        if(movement>0 && !isMovingLeft || movement < 0 && isMovingLeft)
        {
            isMovingLeft = !isMovingLeft;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

        }
    }
}
