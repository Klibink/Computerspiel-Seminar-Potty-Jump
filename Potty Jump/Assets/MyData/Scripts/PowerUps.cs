using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject player;
    private Vector2 startPos;
    private float jumpForce = 10f;
    private float distance = 40f;
    private bool startMoving = false;
    private bool isActivated = false;
    private GameObject[] enemys;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Potty");
        startPos = transform.position;
        anim = transform.GetComponent<Animator>();

        if (gameObject.name.StartsWith("Butterfly"))
        {
            Vector2 temp = transform.position;
            temp.x = Random.Range((GameManager.instance.FrustumWidth / 2f) - 1f, (-GameManager.instance.FrustumWidth / 2f) + 1f);
            transform.position = temp;
        }
        else if (gameObject.name.StartsWith("Bombe"))
        {
            if(enemys == null)
            {
                enemys = GameObject.FindGameObjectsWithTag("Enemy");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y > transform.position.y + 6 && !isActivated)
        {
            Destroy(gameObject);
        }

        if (startMoving)
        {
            if (transform.position.y < startPos.y + distance)
            {
                EndlessPlayer.instance.CanDie = false;
                player.GetComponent<Rigidbody2D>().gravityScale = -0.3f;
            }
            else
            {
                EndlessPlayer.instance.CanDie = true;
                player.GetComponent<Rigidbody2D>().gravityScale = 1f;
                startMoving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody" && !EndlessPlayer.instance.IsUsingPowerUp || collision.tag == "PlayerFeet" && !EndlessPlayer.instance.IsUsingPowerUp)
        {
            if (GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }

            if (gameObject.name.StartsWith("Butterfly"))
            {
                EndlessPlayer.instance.IsUsingPowerUp = true;
                isActivated = true;
                startMoving = true;
                transform.position = player.transform.position;
                transform.parent = player.transform;
                //transform.Translate(transform.up * Time.deltaTime * 6f);
                Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                }
                StartCoroutine(DestroyPlattform(3.5f));
            }
            else if (gameObject.name.StartsWith("Gießkanne"))
            {
                EndlessPlayer.instance.IsUsingPowerUp = true;
                isActivated = true;
                EndlessPlayer.instance.IsInvincible = true;
                StartCoroutine(DestroyPlattform(0.5f));
            }
            else if (gameObject.name.StartsWith("Tropfen"))
            {
                EndlessPlayer.instance.IsUsingPowerUp = true;
                isActivated = true;
                EndlessPlayer.instance.IsInvincible = true;
                StartCoroutine(DestroyPlattform(0.5f));
            }
            else if (gameObject.name.StartsWith("Fertilizer"))
            {
                Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = 25f;
                    rb.velocity = velocity;
                }
                StartCoroutine(DestroyPlattform(0.5f));
            }
            else if (gameObject.name.StartsWith("Muschel"))
            {
                EndlessPlayer.instance.IsUsingPowerUp = true;
                isActivated = true;
                EndlessPlayer.instance.IsInvincible = true;
                StartCoroutine(DestroyPlattform(0.5f));
            }
            else if (gameObject.name.StartsWith("Regenbogenfisch"))
            {
                if (GetComponent<AudioSource>() != null)
                {
                    GetComponent<AudioSource>().Play();
                }
                EndlessPlayer.instance.IsUsingPowerUp = true;
                isActivated = true;
                startMoving = true;
                transform.position = player.transform.position;
                transform.parent = player.transform;
                //transform.Translate(transform.up * Time.deltaTime * 6f);
                Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                }
                StartCoroutine(DestroyPlattform(3.5f));
            }
            else if (gameObject.name.StartsWith("Rettungsring"))
            {
                Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = 25f;
                    rb.velocity = velocity;
                }
                StartCoroutine(DestroyPlattform(0.5f));
            }
            else if (gameObject.name.StartsWith("Seestern"))
            {
                EndlessPlayer.instance.IsUsingPowerUp = true;
                isActivated = true;
                EndlessPlayer.instance.IsInvincible = true;
                StartCoroutine(DestroyPlattform(0.5f));
            }
            else if (gameObject.name.StartsWith("Bombe"))
            {
                foreach(GameObject enemy in enemys)
                {
                    Destroy(enemy);
                }

                StartCoroutine(DestroyPlattform(0.5f));
            }
            else if (gameObject.name.StartsWith("Propellerhelm"))
            {
                EndlessPlayer.instance.ShowFlowers = false;
                Vector3 scale = new Vector3(0.07f, 0.07f, 0.07f);
                transform.localScale = scale;
                anim.SetBool("startAnim", true);
                if (GetComponent<AudioSource>() != null)
                {
                    GetComponent<AudioSource>().Play();
                }
                EndlessPlayer.instance.IsUsingPowerUp = true;
                isActivated = true;
                startMoving = true;
                Vector3 itemPos = new Vector3(player.transform.position.x, player.transform.position.y+0.77f, player.transform.position.z);
                transform.position = itemPos;
                transform.parent = player.transform;
                //transform.Translate(transform.up * Time.deltaTime * 6f);
                Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                }
                StartCoroutine(DestroyPlattform(3.5f));
            }
            else if (gameObject.name.StartsWith("Stoppuhr"))
            {
                EndlessGameManager.instance.EnemysFrozen = true;
                StartCoroutine(DestroyPlattform(0.5f));
            }
        }
        
    }

    IEnumerator DestroyPlattform(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!EndlessPlayer.instance.ShowFlowers)
        {
            EndlessPlayer.instance.ShowFlowers = true;
        }
        Destroy(gameObject);
    }

    IEnumerator DeOrActivateGameObject()
    {
        yield return new WaitForSeconds(2f);
        EndlessPlayer.instance.IsInvincible = false;
        GameObject.Find("BubbleSprite").GetComponent<SpriteRenderer>().enabled = false;
        //Debug.Log(EndlessPlayer.instance.IsInvincible);
        yield return StartCoroutine(DestroyPlattform(1f));
    }

    
}
