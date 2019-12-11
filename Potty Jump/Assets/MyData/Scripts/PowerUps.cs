using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject player;
    private Vector2 startPos;
    private float jumpForce = 10f;
    private float distance = 20f;
    private bool startMoving = false;
    private bool isActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Potty");
        startPos = transform.position;

        if (gameObject.name.StartsWith("Butterfly"))
        {
            Vector2 temp = transform.position;
            temp.x = Random.Range((GameManager.instance.FrustumWidth / 2f) - 1f, (-GameManager.instance.FrustumWidth / 2f) + 1f);
            transform.position = temp;
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
        if (collision.tag == "PlayerBody" && EndlessPlayer.instance.CanDie || collision.tag == "PlayerFeet" && EndlessPlayer.instance.CanDie)
        {
            if (gameObject.name.StartsWith("Butterfly"))
            {
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
                StartCoroutine(DestroyPlattform(2.5f));
            }
            else if (gameObject.name.StartsWith("Gießkanne"))
            {
                isActivated = true;
                EndlessPlayer.instance.IsInvincible = true;
                StartCoroutine(DestroyPlattform(0.5f));
            }
            
        }
        
    }

    IEnumerator DestroyPlattform(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    IEnumerator DeOrActivateGameObject()
    {
        yield return new WaitForSeconds(2f);
        EndlessPlayer.instance.IsInvincible = false;
        GameObject.Find("BubbleSprite").GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log(EndlessPlayer.instance.IsInvincible);
        yield return StartCoroutine(DestroyPlattform(1f));
    }
}
