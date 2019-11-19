using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player instance = null; 
    public float movementSpeed = 15f;
    private float points = 0f;
    float movement = 0f;
    private bool isMovingLeft = true;
    Rigidbody2D rb;
    public Text scoreText;
    public LevelGenerator lvlGenerator;

    public float Points { get => points; set => points = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

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
        CheckDeath();
        Flip();
        
        //erhöht die Punktzahl und zeigt sie in der Szene an
        if (rb.velocity.y > 0 && transform.position.y >points)
        {
            points = transform.position.y;
        }
        scoreText.text = "Score: " + Mathf.Round(points*1.75f).ToString();

        //Wenn Spieler den Bildschirm auf einer Seite verlässt kommt er auf der anderen Seite wieder raus
        if(transform.position.x < -EndlessGameManager.instance.FrustumWidth / 2f - 0.5f)
        {
            Vector2 temp = new Vector2();
            temp.y = transform.position.y;
            temp.x = EndlessGameManager.instance.FrustumWidth / 2f;
            transform.position = temp;
        }else if(transform.position.x > EndlessGameManager.instance.FrustumWidth / 2f + 0.5f)
        {
            Vector2 temp = new Vector2();
            temp.y = transform.position.y;
            temp.x = -EndlessGameManager.instance.FrustumWidth / 2f;
            transform.position = temp;
        }

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

        if(movement > 0 && !isMovingLeft || movement < 0 && isMovingLeft)
        {
            isMovingLeft = !isMovingLeft;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

        }
    }

    private void CheckDeath()
    {
        if (transform.position.y+10 < Camera.main.transform.position.y && EndlessGameManager.instance.GameIsRunning==true)
        {
            EndlessGameManager.instance.PlayerDeath();
        }
    }
}
