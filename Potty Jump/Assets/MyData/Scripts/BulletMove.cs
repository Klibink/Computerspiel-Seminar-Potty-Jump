using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D bulletRB;
    public GameObject cameraObject;

    private void Start()
    {
        bulletRB = transform.GetComponent<Rigidbody2D>();
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        bulletRB.velocity = transform.up * speed;
        //transform.Translate(Vector2.down * speed, Space.World);
        //Zerstört Bullet wenn 5 > x-Position < -5
        //Evtl. dynamisch an Spielfeldbreite anpassen
        if(transform.position.y > cameraObject.transform.position.y + 20 || transform.position.x < -5)
        {
            Destroy(gameObject);
        }
    }
    
}