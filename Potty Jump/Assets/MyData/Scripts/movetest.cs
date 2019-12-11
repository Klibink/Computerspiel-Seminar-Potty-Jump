using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetest : MonoBehaviour
{
    float speed = 0.4f;
    private Vector2 startPos;
    private Vector2 tempPos1;
    private Vector2 tempPos2;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
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
}
      

            
 