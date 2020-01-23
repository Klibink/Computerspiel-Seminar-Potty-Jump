using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    private float speed = 5f;
    private Rigidbody2D rb;
    private bool goRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        if (transform.position.x < 0)
        {
            goRight = true;
        }
        else
        {
            goRight = false;
        }
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        if (goRight)
        {
            transform.Translate(transform.right * Time.deltaTime * 3f);
        }
        else
        {
            transform.Translate(-transform.right * Time.deltaTime * 3f);
        }

        /*if(transform.position.x > GameManager.instance.FrustumWidth / 2f || transform.position.x > -GameManager.instance.FrustumWidth / 2f)
        {
            Destroy(gameObject);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "PlayerFeet" || collision.collider.tag == "PlayerBody")
        {

        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
