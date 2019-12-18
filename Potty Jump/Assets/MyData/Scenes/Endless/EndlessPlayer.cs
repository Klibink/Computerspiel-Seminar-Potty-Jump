using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class EndlessPlayer : MonoBehaviour
{
    public static EndlessPlayer instance = null;
    private Transform[] allChildren;
    public List< GameObject> skins;
    public List<GameObject> flowers;
    public float movementSpeed = 15f;
    private float currentHeight = 0f;
    private float points = 1f;
    float movement = 0f;
    private bool isMovingLeft = true;
    private bool canDie = true;
    private bool isInvincible = false;
    private bool isUsingPowerUp = false;
    Rigidbody2D rb;
    public Text scoreText;
    public Sprite deathSprite;

    public float Points { get => points; set => points = value; }
    public bool CanDie { get => canDie; set => canDie = value; }
    public bool IsInvincible { get => isInvincible; set => isInvincible = value; }
    public bool IsUsingPowerUp { get => isUsingPowerUp; set => isUsingPowerUp = value; }

    RigidbodyConstraints2D originalConstraints;

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
        originalConstraints = rb.constraints;
        allChildren = transform.GetComponentsInChildren<Transform>();

        //Beim Beginn der Szene wird der gewünschte Skin in der Hierarchy aktivert
        for(int i = 0; i < skins.Count; i++)
        {
            if (i == GameManager.instance.currentSkin /*&& GameManager.instance.unlockSkins[i] == true*/)
            {
                skins[i].SetActive(true);
            }
            else
            {
                skins[i].SetActive(false);
            }
        }
        //Das Gleiche für die Blume
        for (int i = 0; i < flowers.Count; i++)
        {
            if (i == GameManager.instance.currentFlower)
            {
                flowers[i].SetActive(true);
            }
            else
            {
                flowers[i].SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!EndlessGameManager.instance.GamePaused)
        {
            rb.constraints = originalConstraints;
#if UNITY_EDITOR
            movement = Input.GetAxis("Horizontal") * movementSpeed;

#elif UNITY_ANDROID
        movement = Input.acceleration.x * movementSpeed;
#endif
            CheckIfMoving();
            CheckDeath();
            Flip();

            if (isInvincible)
            {
                ActivateInvincibility();
            }

            if (isUsingPowerUp)
            {
                StartCoroutine(DeactivatePowerUp());
            }

            if (transform.GetComponent<Rigidbody2D>().velocity.y > 10 || isInvincible)
            {
                CanDie = false;
                Debug.Log("Ich bin unsterblich");
            }
            else
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
            if (transform.position.x < -GameManager.instance.FrustumWidth / 2f - 0.5f)
            {
                Vector2 temp = new Vector2();
                temp.y = transform.position.y;
                temp.x = GameManager.instance.FrustumWidth / 2f;
                transform.position = temp;
            }
            else if (transform.position.x > GameManager.instance.FrustumWidth / 2f + 0.5f)
            {
                Vector2 temp = new Vector2();
                temp.y = transform.position.y;
                temp.x = -GameManager.instance.FrustumWidth / 2f;
                transform.position = temp;
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }



    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = movement;
        rb.velocity = velocity;
    }

    public void ActivateInvincibility()
    {
        foreach (Transform child in allChildren)
        {
            if (child.name == "SpriteHolder")
            {
                //child.GetComponentInChildren<SpriteRenderer>().enabled = true;
                GameObject.Find("BubbleSprite").GetComponent<SpriteRenderer>().enabled = true;
                isInvincible = true;
                StartCoroutine(InvincibleTime());
            }
        }
    }

    IEnumerator InvincibleTime()
    {
        yield return new WaitForSeconds(4f);
        isInvincible = false;
        if (GameObject.Find("BubbleSprite") != null)
        {
            GameObject.Find("BubbleSprite").GetComponent<SpriteRenderer>().enabled = false;
        }
        Debug.Log(EndlessPlayer.instance.IsInvincible);

    }

    public IEnumerator DeactivatePowerUp()
    {
        yield return new WaitForSeconds(4f);
        isUsingPowerUp = false;
    }

    public void SetDeathStatus()
    {
        canDie = !canDie;
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

    private void CheckIfMoving()
    {
        if (rb.velocity.y == 0 && EndlessGameManager.instance.GamePaused)
        {
            Vector2 velocity = rb.velocity;
            velocity.y = 10f;
            rb.velocity = velocity;
            Debug.Log("Notjump aktiviert");
        }
        else return;
            
    }

    private void CheckDeath()
    {
        if (transform.position.y+10 < Camera.main.transform.position.y && EndlessGameManager.instance.GameIsRunning==true)
        {
            if (this.transform.Find("DeathSprite").GetComponent<AudioSource>().isPlaying == false)
            {
                this.transform.Find("DeathSprite").GetComponent<AudioSource>().Play();
            }
            EndlessGameManager.instance.PlayerDeath();
        }
    }

    public void ChangeToDeathSprite()
    {
        foreach(Transform child in allChildren)
        {
            if(child.name == "DeathSprite")
            {
                child.GetComponent<AudioSource>().Play();
                child.GetComponent<SpriteRenderer>().enabled = true;
                //child.GetComponent<SpriteRenderer>().sprite = deathSprite;
                //Vector3 newScale = new Vector3(1.5f, 1.5f, 0);
                //child.localScale = newScale;
            }
            else if(child.name!="Potty" && child.name!="DeathSprite")
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
