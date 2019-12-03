using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class EndlessPlayer : MonoBehaviour
{
    public static EndlessPlayer instance = null;
    private Transform[] allChildren;
    public float movementSpeed = 15f;
    private float currentHeight = 0f;
    private float points = 1f;
    float movement = 0f;
    private bool isMovingLeft = true;
    private bool canDie = true;
    Rigidbody2D rb;
    public Text scoreText;
    public Sprite deathSprite;

    public float Points { get => points; set => points = value; }
    public bool CanDie { get => canDie; set => canDie = value; }

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
        allChildren = GetComponentsInChildren<Transform>();
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
        
        if(transform.GetComponent<Rigidbody2D>().velocity.y > 10)
        {
            CanDie = false;
            Debug.Log("Ich bin unsterblich");
        }
        else if(transform.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            CanDie = true;
            Debug.Log("Ich kann sterben");
        }

        //erhöht die Punktzahl und zeigt sie in der Szene an
        if (rb.velocity.y > 0 && transform.position.y > currentHeight)
        {
            currentHeight = transform.position.y;
            points = currentHeight * 2.5f;
        }
        scoreText.text = "Score: " + Mathf.Round(points).ToString();

        //Wenn Spieler den Bildschirm auf einer Seite verlässt kommt er auf der anderen Seite wieder raus
        if(transform.position.x < -GameManager.instance.FrustumWidth / 2f - 0.5f)
        {
            Vector2 temp = new Vector2();
            temp.y = transform.position.y;
            temp.x = GameManager.instance.FrustumWidth / 2f;
            transform.position = temp;
        }
        else if(transform.position.x > GameManager.instance.FrustumWidth / 2f + 0.5f)
        {
            Vector2 temp = new Vector2();
            temp.y = transform.position.y;
            temp.x = -GameManager.instance.FrustumWidth / 2f;
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
    {
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

    public void ChangeToDeathSprite()
    {
        foreach(Transform child in allChildren)
        {
            if(child.name == "SpriteHolder")
            {
                child.GetComponent<SpriteRenderer>().sprite = deathSprite;
                Vector3 newScale = new Vector3(1.5f, 1.5f, 0);
                child.localScale = newScale;
            }
        }
    }
}
